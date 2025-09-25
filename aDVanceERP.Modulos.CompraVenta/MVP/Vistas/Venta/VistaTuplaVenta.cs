using System.Globalization;

using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;
using DocumentFormat.OpenXml.Packaging;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta;

public partial class VistaTuplaVenta : Form, IVistaTuplaVenta {
    public VistaTuplaVenta() {
        InitializeComponent();

        NombreVista = nameof(VistaTuplaVenta);

        Inicializar();
    }

    public string NombreVista {
        get => $"{Name}{Id}";
        private set => Name = value;
    }

    public bool Habilitada {
        get => Enabled;
        set => Enabled = value;
    }

    public Point Coordenadas {
        get => Location;
        set => Location = value;
    }

    public Size Dimensiones {
        get => Size;
        set => Size = value;
    }

    public string Id {
        get => fieldId.Text;
        set => fieldId.Text = value;
    }

    public string Fecha {
        get => fieldFecha.Text;
        set => fieldFecha.Text = value;
    }

    public string NombreAlmacen {
        get => fieldNombreAlmacen.Text;
        set {
            fieldNombreAlmacen.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
            fieldNombreAlmacen.Margin = new Padding(1, value.Length > 16 ? 10 : 1, 1, 1);
        }
    }

    public string NombreCliente {
        get => fieldNombreCliente.Text;
        set => fieldNombreCliente.Text = value;
    }

    public string CantidadProductos {
        get => fieldCantidadProductos.Text;
        set => fieldCantidadProductos.Text = value;
    }

    public string MontoTotal {
        get => fieldMontoTotal.Text;
        set => fieldMontoTotal.Text = value;
    }

    public string? EstadoEntrega {
        get => fieldEstadoEntrega.Text;
        set {
            fieldEstadoEntrega.Text = value;

            ColorFondoTupla = ObtenerColorTupla();
        }
    }

    public string? EstadoPago {
        get => fieldEstadoPago.Text;
        set {
            fieldEstadoPago.Text = value;

            ColorFondoTupla = ObtenerColorTupla();
        }
    }

    public Color ColorFondoTupla {
        get => layoutVista.BackColor;
        set => layoutVista.BackColor = value;
    }

    public event EventHandler? TuplaSeleccionada;
    public event EventHandler? DescargarFactura;
    public event EventHandler? EditarDatosTupla;
    public event EventHandler? EliminarDatosTupla;
    

    public void Inicializar() {
        // VAriables
        ColorFondoTupla = ObtenerColorTupla();

        // Eventos
        fieldId.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldFecha.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombreAlmacen.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldNombreCliente.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldCantidadProductos.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldMontoTotal.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldEstadoEntrega.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        fieldEstadoPago.Click += delegate (object? sender, EventArgs e) { TuplaSeleccionada?.Invoke(this, e); };
        btnDescargarFactura.Click += delegate (object? sender, EventArgs e) {
            var datosCliente = new string[3];
            var datosVentaProductos = new List<string[]>();
            var fechaFactura = DateTime.ParseExact(Fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var numeroFactura = $"{fechaFactura.ToString("yyyyMMdd")}-{decimal.Parse(CantidadProductos, NumberStyles.Any, CultureInfo.InvariantCulture):000.00}-{long.Parse(Id):000000}";
            var pagos = UtilesVenta.ObtenerPagosPorVenta(long.Parse(Id));
            var metodoPago = string.Empty;
            var cantidadPagada = 0m;

            using (var datosVentas = new RepoVenta()) {
                var venta = datosVentas.Buscar(FiltroBusquedaVenta.Id, Id).resultados.FirstOrDefault();

                if (venta == null)
                    return;

                datosCliente[0] = UtilesCliente.ObtenerRazonSocialCliente(venta.IdCliente) ?? "Anónimo";
                datosCliente[1] = UtilesCliente.ObtenerDireccionCliente(venta.IdCliente) ?? string.Empty;
                datosCliente[2] = UtilesCliente.ObtenerNumeroCliente(venta.IdCliente) ?? string.Empty;

                using (var datosVentaProducto = new RepoDetalleVentaProducto()) {
                    var detalleVentaProducto = datosVentaProducto.Buscar(CriterioDetalleVentaProducto.IdVenta, venta.Id.ToString()).resultados;

                    foreach (var ventaProducto in detalleVentaProducto) {
                        var fila = new string[7];

                        fila[0] = ventaProducto.Id.ToString();
                        fila[1] = UtilesProducto.ObtenerNombreProducto(ventaProducto.IdProducto).Result ?? string.Empty;
                        fila[2] = ventaProducto.Cantidad.ToString("N2", CultureInfo.InvariantCulture);
                        fila[3] = ventaProducto.PrecioVentaFinal.ToString("N2", CultureInfo.InvariantCulture);
                        fila[4] = "-";
                        fila[5] = "0.00%";
                        fila[6] = (ventaProducto.PrecioVentaFinal * (decimal)ventaProducto.Cantidad).ToString("N2", CultureInfo.InvariantCulture);

                        datosVentaProductos.Add(fila);
                    }
                }

            }

            if (pagos.Count > 0) {
                foreach (var pago in pagos) {
                    var pagoSplit = pago.Split("|");

                    metodoPago = pagoSplit[2];
                    cantidadPagada += decimal.Parse(pagoSplit[3], CultureInfo.InvariantCulture);
                }
            }

            UtilesReportes.GenerarFacturaVenta(
                fechaFactura, 
                datosVentaProductos, 
                datosCliente,
                numeroFactura,
                EstadoPago,
                metodoPago,
                cantidadPagada);
            DescargarFactura?.Invoke(this, e);
        };
        btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
        btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        ColorFondoTupla = ObtenerColorTupla();
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        Dispose();
    }

    private void VerificarPermisos() {
        btnEditar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_EDITAR")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_TODOS")
                            || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
        btnEliminar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_COMPRAVENTA_VENTA_ELIMINAR")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                  "MOD_COMPRAVENTA_VENTA_TODOS")
                              || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
    }

    private Color ObtenerColorTupla() {
        if (EstadoPago?.Equals("Pendiente") ?? false)
            return VariablesGlobales.ColorErrorTupla;
        else if (EstadoEntrega?.Equals("Pendiente") ?? false)
            return VariablesGlobales.ColorAdvertenciaTupla;

        return BackColor;
    }
}