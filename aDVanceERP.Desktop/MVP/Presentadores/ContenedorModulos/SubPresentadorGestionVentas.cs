using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Utiles.Datos;

using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta;

using Newtonsoft.Json;

using System.Globalization;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorGestionVentas? _gestionVentas;
    private ControladorArchivosAndroid _androidFileManager;

    private async void InicializarVistaGestionVentas() {
        _gestionVentas = new PresentadorGestionVentas(new VistaGestionVentas());
        _gestionVentas.EditarEntidad += OnMostrarVistaEdicionVentaProducto;
        _gestionVentas.Vista.RegistrarEntidad += OnMostrarVistaRegistroVentaProducto;
        _gestionVentas.Vista.ImportarVentasArchivo += OnImportarVentasArchivo;
        _gestionVentas.Vista.ConfirmarPagos += OnConfirmarPagosVenta;
        _androidFileManager = new ControladorArchivosAndroid(Application.StartupPath);

        Vista.PanelCentral.Registrar(_gestionVentas.Vista);
    }

    private void MostrarVistaGestionVentas(object? sender, EventArgs e) {
        if (_gestionVentas?.Vista == null)
            return;

        _gestionVentas.Vista.CargarFiltrosBusqueda(UtilesBusquedaVenta.FiltroBusquedaVenta);
        _gestionVentas.Vista.Restaurar();
        _gestionVentas.Vista.Mostrar();

        _gestionVentas.ActualizarResultadosBusqueda();
    }

    private void OnImportarVentasArchivo(object? sender, string ventas) {
        var ventasJson = JsonConvert.DeserializeObject<List<VentaJson>>(ventas);

        if (ventasJson == null)
            return;

        //TODO: Mejorar la velocidad de la operación con una conexion unica 
        //var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());

        foreach (var ventaJson in ventasJson) {
            var idAlmacen = 0L;

            foreach (var productoVendidoJson in ventaJson.Productos) {
                if (idAlmacen == 0)
                    idAlmacen = UtilesAlmacen.ObtenerIdAlmacen(productoVendidoJson.Producto.nombre_almacen).Result;

                var tuplaProducto = new string[] {
                    productoVendidoJson.Producto.id_producto.ToString(),
                    productoVendidoJson.Producto.nombre,
                    productoVendidoJson.Producto.precio_compra.ToString("N2", CultureInfo.InvariantCulture),
                    productoVendidoJson.Producto.costo_produccion_unitario.ToString("N2", CultureInfo.InvariantCulture),
                    productoVendidoJson.Producto.precio_venta_base.ToString("N2", CultureInfo.InvariantCulture),
                    productoVendidoJson.Cantidad.ToString(),
                    idAlmacen.ToString()
                };

                if (tuplaProducto ==  null) 
                    continue;

                ProductosVenta?.Add(tuplaProducto);
            }

            using (var repoVentas = new RepoVenta()) {
                ventaJson.IdVenta = repoVentas.Adicionar(new Venta(0,
                    ventaJson.Fecha,
                    idAlmacen,
                    0,
                    UtilesEntrega.ObtenerIdTipoEntrega("Presencial").Result,
                    "",
                    "Completada",
                    ventaJson.Total
                    ));
            }

            var pago = new Pago(0,
               ventaJson.IdVenta,
               ventaJson.MetodoPago,
               ventaJson.Total);


            using (var repoPagos = new RepoPago()) {
                pago.Id = repoPagos.Adicionar(pago);
            }

            RegistrarDetallesVentaProducto();
            ActualizarSeguimientoEntrega(ventaJson.IdVenta);
            ActualizarMovimientoCaja([pago]);

            ProductosVenta?.Clear();
        }

        _gestionVentas?.ActualizarResultadosBusqueda();

        CentroNotificaciones.Mostrar("Importación de ventas finalizada, las ventas han sido importadas correctamente desde el dispositivo.");
    }

    private void OnConfirmarPagosVenta(object? sender, EventArgs e) {
        if (_gestionVentas?.Vista == null)
            return;

        if (!_gestionVentas.TuplasSeleccionadas.Any()) {
            _gestionVentas.Vista.HabilitarBtnConfirmarPagos = false;
            return;
        }

        // 2. Mover las instancias de RepoPago y RepoSeguimientoEntrega fuera del bucle
        using (var datosPago = new RepoPago())
        using (var datosSeguimiento = new RepoSeguimientoEntrega()) {
            foreach (var tupla in _gestionVentas.TuplasSeleccionadas) {
                var ventaId = long.Parse(tupla.Vista.Id);
                var montoTotal = decimal.Parse(tupla.Vista.MontoTotal, CultureInfo.InvariantCulture);
                var pagos = UtilesVenta.ObtenerPagosPorVenta(ventaId);
                var ahora = DateTime.Now;

                // 3. Procesar pagos
                if (pagos.Count == 0) {
                    // Crear nuevo pago
                    var nuevoPago = new Pago(
                        0,
                        ventaId,
                        "Efectivo",
                        montoTotal) {
                        Estado = "Confirmado",
                        FechaConfirmacion = ahora
                    };

                    datosPago.Adicionar(nuevoPago);

                    ActualizarMovimientoCaja([nuevoPago]);
                } else {
                    // Actualizar pagos existentes
                    foreach (var pago in pagos) {
                        var pagoSplit = pago.Split('|');
                        var pagoActualizado = new Pago(
                            long.Parse(pagoSplit[0]),
                            ventaId,
                            pagoSplit[2],
                            decimal.Parse(pagoSplit[3], CultureInfo.InvariantCulture)) {
                            Estado = "Confirmado",
                            FechaConfirmacion = ahora
                        };

                        datosPago.Editar(pagoActualizado);

                        ActualizarMovimientoCaja([pagoActualizado]);
                    }
                }

                // 4. Actualizar seguimiento de entrega (una sola vez por tupla)
                var objetoSeguimiento = datosSeguimiento.Buscar(
                    FiltroBusquedaSeguimientoEntrega.IdVenta,
                    tupla.Vista.Id).resultados.FirstOrDefault();

                if (objetoSeguimiento != null) {
                    objetoSeguimiento.FechaPago = ahora;
                    // Nota: Corregí FechaEntrega a FechaPago para consistencia con el caso de pagos.Count == 0
                    datosSeguimiento.Editar(objetoSeguimiento);
                }
            }
        }

        _gestionVentas.Vista.HabilitarBtnConfirmarPagos = false;
        _gestionVentas.ActualizarResultadosBusqueda();
    }
}

internal class ProductoJson {
    public int id_producto { get; set; }
    public string codigo { get; set; }
    public string nombre { get; set; }
    public string categoria { get; set; }
    public decimal precio_compra { get; set; }
    public decimal costo_produccion_unitario { get; set; }
    public decimal precio_venta_base { get; set; }
    public decimal cantidad { get; set; }
    public string nombre_almacen { get; set; }
    public string unidad_medida { get; set; }
    public string abreviatura_medida { get; set; }
}

internal class ProductoVendidoJson {
    public ProductoJson Producto { get; set; }
    public decimal Cantidad { get; set; }
}

internal class VentaJson {
    public long IdVenta { get; set; }
    public DateTime Fecha { get; set; }
    public List<ProductoVendidoJson> Productos { get; set; }
    public decimal Total { get; set; }
    public string MetodoPago { get; set; } // "Efectivo" o "Transferencia"
}