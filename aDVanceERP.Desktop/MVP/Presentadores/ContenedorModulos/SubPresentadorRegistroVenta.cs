using System.Globalization;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorRegistroVenta? _registroVentaProducto;
    private long _proximoIdVenta = 0;

    private List<string[]>? ProductosVenta { get; set; } = new();

    private async Task InicializarVistaRegistroVentaProducto() {
        try {
            _registroVentaProducto = new PresentadorRegistroVenta(new VistaRegistroVenta());
            _registroVentaProducto.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
            _registroVentaProducto.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
            _registroVentaProducto.Vista.CargarNombresAlmacenes(UtilesAlmacen.ObtenerNombresAlmacenes(true));
            _registroVentaProducto.Vista.IdTipoEntrega = await UtilesEntrega.ObtenerIdTipoEntrega("Presencial");
            _registroVentaProducto.EntidadRegistradaActualizada += delegate {
                ProductosVenta = _registroVentaProducto.Vista.Productos;

                RegistrarDetallesVentaProducto();
                RegistrarTransferenciaVenta();

                if (_gestionVentas == null)
                    return;

                _gestionVentas.Vista.HabilitarBtnConfirmarEntrega = false;
                _gestionVentas.Vista.HabilitarBtnConfirmarPagos = false;
                _gestionVentas.ActualizarResultadosBusqueda();
            };
            //TODO: Verificar cancelación de la venta
            /*_registroVentaProducto.Vista. += delegate {
                if (!_registroVentaProducto.Vista.ModoEdicionDatos && !UtilesVenta.ExisteVenta(_proximoIdVenta))
                    CancelarVenta();
            };*/

            ProductosVenta?.Clear();
        }
        catch (ExcepcionConexionServidorMySQL e) {
            CentroNotificaciones.Mostrar(e.Message, TipoNotificacion.Error);
        }
    }

    private void CancelarVenta() {
        // Eliminar pagos registrados si se cancela la venta
        var pagosVenta = UtilesVenta.ObtenerPagosPorVenta(_proximoIdVenta);

        if (pagosVenta.Count > 0) {
            using (var datosPago = new RepoPago())
                foreach (var pago in pagosVenta) {
                    var pagoSplit = pago.Split("|");

                    datosPago.Eliminar(long.Parse(pagoSplit[0]));
                }
        }

        // Eliminar seguimientos de entrega registrados si se cancela la venta
        var idSeguimientoEntrega = UtilesEntrega.ObtenerIdSeguimientoEntrega(_proximoIdVenta).Result;

        if (idSeguimientoEntrega != 0)
            using (var datosSeguimientoEntrega = new RepoSeguimientoEntrega())
                datosSeguimientoEntrega.Eliminar(idSeguimientoEntrega);
    }

    private async void OnMostrarVistaRegistroVentaProducto(object? sender, EventArgs e) {
        // Comprobar la existencia de al menos un almacén registrado.
        var existenAlmacenes = false;

        using (var datos = new RepoAlmacen())
            existenAlmacenes = datos.Cantidad() > 0;

        if (!existenAlmacenes) {
            CentroNotificaciones.Mostrar("No es posible registrar nuevas ventas. Debe existir al menos un almacén registrado.", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
            return;
        }

        await InicializarVistaRegistroVentaProducto();

        if (_registroVentaProducto == null)
            return;

        _proximoIdVenta = UtilesBD.ObtenerUltimoIdTabla("venta") + 1;
        _registroVentaProducto.Vista.EfectuarPago += delegate {
            MostrarVistaRegistroPago(sender, e);
        };
        _registroVentaProducto.Vista.AsignarMensajeria += delegate {
            MostrarVistaRegistroMensajeria(sender, e);
        };
        _registroVentaProducto.Vista.Mostrar();
        _registroVentaProducto.Dispose();
    }

    private async void OnMostrarVistaEdicionVentaProducto(object? sender, EventArgs e) {
        await InicializarVistaRegistroVentaProducto();

        if (_registroVentaProducto != null && sender is Venta venta) {            
            _registroVentaProducto.Vista.EfectuarPago += delegate {
                MostrarVistaEdicionPago(sender, e);
            };
            _registroVentaProducto.Vista.AsignarMensajeria += delegate {
                MostrarVistaEdicionMensajeria(sender, e);
            };

            _registroVentaProducto.PopularVistaDesdeEntidad(venta);
            _registroVentaProducto.Vista.Mostrar();
        }

        _registroVentaProducto?.Dispose();
    }

    private void RegistrarDetallesVentaProducto() {
        if (ProductosVenta == null || ProductosVenta.Count == 0)
            return;

        var ultimoIdVenta = UtilesBD.ObtenerUltimoIdTabla("venta");

        foreach (var producto in ProductosVenta) {
            var detalleVentaProducto = new DetalleVentaProducto(
                0,
                ultimoIdVenta,
                long.Parse(producto[0]),
                decimal.TryParse(producto[2], NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var precioCompraVigente)
                    ? precioCompraVigente
                    : 0.00m,
                decimal.TryParse(producto[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var precioVentaFinal)
                    ? precioVentaFinal
                    : 0.00m,
                decimal.TryParse(producto[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var cantidad)
                    ? cantidad
                    : 0.00m
            );

            using (var datosProducto = new RepoDetalleVentaProducto()) {
                datosProducto.Adicionar(detalleVentaProducto);
            }

            RegistrarMovimientoVentaProducto(detalleVentaProducto, producto);
            ModificarStockVentaProducto(detalleVentaProducto, producto);

            // Actualizar precio de venta en tabla producto
            UtilesProducto.ActualizarPrecioVentaBase(
                detalleVentaProducto.IdProducto,
                detalleVentaProducto.PrecioVentaFinal
            );
        }
    }

    private static void RegistrarMovimientoVentaProducto(DetalleVentaProducto detalleVentaProducto, IReadOnlyList<string> datosProducto) {
        var producto = RepoProducto.Instancia.ObtenerPorId(detalleVentaProducto.IdProducto);
        var almacenOrigen = RepoAlmacen.Instancia.ObtenerPorId(long.Parse(datosProducto[5]));
        var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString()).resultados.FirstOrDefault(i => i.IdAlmacen.Equals(almacenOrigen.Id));
        var tipoMovimientoProducto = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Venta").resultados.FirstOrDefault();
        var saldoFinalProducto = inventarioProducto.Cantidad + (detalleVentaProducto.Cantidad * (tipoMovimientoProducto?.Efecto == EfectoMovimiento.Carga ? 1 : -1));

        using (var datosMovimiento = new RepoMovimiento()) {
            datosMovimiento.Adicionar(new Movimiento(
                0,
                detalleVentaProducto.IdProducto,
                detalleVentaProducto.PrecioCompraVigente,
                detalleVentaProducto.PrecioCompraVigente * detalleVentaProducto.Cantidad,
                almacenOrigen.Id,
                0,
                DateTime.Now,
                EstadoMovimiento.Completado,
                DateTime.MinValue,
                inventarioProducto?.Cantidad ?? 0,
                detalleVentaProducto.Cantidad,
                saldoFinalProducto,
                tipoMovimientoProducto?.Id ?? 0,
                UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0
            ));
        }
    }

    private static void ModificarStockVentaProducto(DetalleVentaProducto detalleVentaProducto,
        IReadOnlyList<string> producto) {
        RepoInventario.Instancia.ModificarInventario(
            detalleVentaProducto.IdProducto,
            long.Parse(producto[5]),
            0,
            detalleVentaProducto.Cantidad
        );
    }
}