using System.Globalization;

using aDVanceERP.Core.Modelos.Modulos.Compraventa;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Core.Repositorios.Modulos.Compraventa;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra.Plantillas;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra;

public partial class VistaGestionCompras : Form, IVistaGestionCompras {
    private int _paginaActual = 1;
    private int _paginasTotales = 1;

    public VistaGestionCompras() {
        InitializeComponent();

        NombreVista = nameof(VistaGestionCompras);

        Inicializar();
    }

    public string NombreVista {
        get => Name;
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

    public string FormatoReporte {
        get => fieldFormatoReporte.Text;
        set => fieldFormatoReporte.Text = value;
    }

    public FiltroBusquedaCompra FiltroBusqueda {
        get => fieldFiltroBusqueda.SelectedIndex >= 0
            ? (FiltroBusquedaCompra) fieldFiltroBusqueda.SelectedIndex
            : default;
        set => fieldFiltroBusqueda.SelectedIndex = (int) value;
    }

    public string? CriterioBusqueda {
        get => fieldDatoBusqueda.Text;
        set => fieldDatoBusqueda.Text = value;
    }

    public string ValorBrutoCompra {
        get => fieldValorBrutoCompra.Text;
        set {
            layoutValorBrutoCompra.Visible = !value.Equals("0.00");
            fieldValorBrutoCompra.Text = value;
        }
    }

    public int AlturaContenedorVistas {
        get => contenedorVistas.Height;
    }

    public int TuplasMaximasContenedor {
        get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
    }

    public int PaginaActual {
        get => _paginaActual;
        set {
            _paginaActual = value;
            fieldPaginaActual.Text = $@"Página {value}";
        }
    }

    public int PaginasTotales {
        get => _paginasTotales;
        set {
            _paginasTotales = value;
            fieldPaginasTotales.Text = $@"de {value}";
            HabilitarBotonesPaginacion();
        }
    }

    public RepoVistaBase? PanelCentral { get; private set; }

    public event EventHandler? AlturaContenedorTuplasModificada;
    public event EventHandler? MostrarPrimeraPagina;
    public event EventHandler? MostrarPaginaAnterior;
    public event EventHandler? MostrarPaginaSiguiente;
    public event EventHandler? MostrarUltimaPagina;
    public event EventHandler? SincronizarDatos;

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    public event EventHandler? DescargarReporte;
    public event EventHandler? ImprimirReporte;
    public event EventHandler<(FiltroBusquedaCompra, string?)>? BuscarEntidades;

    public void Inicializar() {
        // Vistas
        PanelCentral = new RepoVistaBase(contenedorVistas);

        // Eventos
        btnDescargar.Click += delegate {
            var filas = new List<string[]>();

            using (var datosCompras = new RepoCompra()) {
                var comprasFecha = datosCompras.Buscar(FiltroBusquedaCompra.Fecha, fieldDatoBusquedaFecha.Value.ToString("yyyy-MM-dd")).entidades;

                foreach (var venta in comprasFecha) {
                    using (var datosCompraProducto = new RepoDetalleCompraProducto()) {
                        var detalleCompraProducto = datosCompraProducto.Buscar(CriterioDetalleCompraProducto.IdCompra, venta.Id.ToString()).entidades;

                        foreach (var ventaProducto in detalleCompraProducto) {
                            var fila = new string[7];

                            fila[0] = ventaProducto.Id.ToString();
                            fila[1] = UtilesProducto.ObtenerNombreProducto(ventaProducto.IdProducto).Result ?? string.Empty;
                            fila[2] = ventaProducto.Cantidad.ToString("N2", CultureInfo.InvariantCulture);
                            fila[3] = ventaProducto.PrecioCompra.ToString("N2", CultureInfo.InvariantCulture);
                            fila[4] = "0";
                            fila[5] = "0.00%";
                            fila[6] = (ventaProducto.PrecioCompra * (decimal) ventaProducto.Cantidad).ToString("N2", CultureInfo.InvariantCulture);

                            filas.Add(fila);
                        }
                    }
                }
            }

            UtilesReportes.GenerarEntradaMercancias(fieldDatoBusquedaFecha.Value, filas);
        };
        fieldFiltroBusqueda.SelectedIndexChanged += delegate {
            if (FiltroBusqueda == FiltroBusquedaCompra.Fecha) {
                fieldDatoBusquedaFecha.Value = DateTime.Now;
                fieldDatoBusquedaFecha.Focus();

                ActualizarValorBrutoCompras();
            } else {
                layoutValorBrutoCompra.Visible = false;

                fieldDatoBusqueda.Text = string.Empty;
                fieldDatoBusqueda.Focus();
            }

            fieldDatoBusqueda.Visible = FiltroBusqueda != FiltroBusquedaCompra.Fecha &&
                                        fieldFiltroBusqueda.SelectedIndex != 0;
            fieldDatoBusquedaFecha.Visible = FiltroBusqueda == FiltroBusquedaCompra.Fecha &&
                                             fieldFiltroBusqueda.SelectedIndex != 0;

            if (FiltroBusqueda != FiltroBusquedaCompra.Fecha)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, string.Empty));

            // Ir a la primera página al cambiar el criterio de búsqueda
            PaginaActual = 1;
            HabilitarBotonesPaginacion();
        };
        fieldDatoBusqueda.KeyDown += delegate(object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            if (!string.IsNullOrEmpty(CriterioBusqueda))
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, CriterioBusqueda));
            else SincronizarDatos?.Invoke(sender, args);

            args.SuppressKeyPress = true;
        };
        fieldDatoBusquedaFecha.ValueChanged += delegate (object? sender, EventArgs e) {
            BuscarEntidades?.Invoke(this, (FiltroBusqueda, fieldDatoBusquedaFecha.Value.ToString("yyyy-MM-dd")));
        };
        btnCerrar.Click += delegate (object? sender, EventArgs e) {
            Ocultar();
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs e) { RegistrarEntidad?.Invoke(sender, e); };
        btnPrimeraPagina.Click += delegate (object? sender, EventArgs e) {
            PaginaActual = 1;
            MostrarPrimeraPagina?.Invoke(sender, e);
            SincronizarDatos?.Invoke(sender, e);
            HabilitarBotonesPaginacion();
        };
        btnPaginaAnterior.Click += delegate (object? sender, EventArgs e) {
            PaginaActual--;
            MostrarPaginaAnterior?.Invoke(sender, e);
            SincronizarDatos?.Invoke(sender, e);
            HabilitarBotonesPaginacion();
        };
        btnPaginaSiguiente.Click += delegate (object? sender, EventArgs e) {
            PaginaActual++;
            MostrarPaginaSiguiente?.Invoke(sender, e);
            SincronizarDatos?.Invoke(sender, e);
            HabilitarBotonesPaginacion();
        };
        btnUltimaPagina.Click += delegate (object? sender, EventArgs e) {
            PaginaActual = PaginasTotales;
            MostrarUltimaPagina?.Invoke(sender, e);
            SincronizarDatos?.Invoke(sender, e);
            HabilitarBotonesPaginacion();
        };
        btnSincronizarDatos.Click += delegate (object? sender, EventArgs e) { SincronizarDatos?.Invoke(sender, e); };
        contenedorVistas.Resize += delegate { AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty); };
    }

    public void ActualizarValorBrutoCompras() {
        ValorBrutoCompra = UtilesCompra.ObtenerValorBrutoCompraDia(fieldDatoBusquedaFecha.Value)
            .ToString("N2", CultureInfo.InvariantCulture);
        ;
    }

    public void CargarFiltrosBusqueda(object[] criteriosBusqueda) {
        fieldFiltroBusqueda.Items.Clear();
        fieldFiltroBusqueda.Items.AddRange(criteriosBusqueda);

        fieldFiltroBusqueda.SelectedIndex = 4;
    }

    public void Mostrar() {
        Habilitada = true;
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        Habilitada = true;
        PaginaActual = 1;
        PaginasTotales = 1;

        fieldFiltroBusqueda.SelectedIndex = 4;
    }

    public void Ocultar() {
        Habilitada = false;
        Hide();
    }

    public void Cerrar() {
        // ...
    }

    private void VerificarPermisos() {
        btnRegistrar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_COMPRAVENTA_COMPRA_ADICIONAR")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_COMPRAVENTA_COMPRA_TODOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
    }

    private void HabilitarBotonesPaginacion() {
        btnPrimeraPagina.Enabled = PaginaActual > 1;
        btnPaginaAnterior.Enabled = PaginaActual > 1;
        btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
        btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
    }
}