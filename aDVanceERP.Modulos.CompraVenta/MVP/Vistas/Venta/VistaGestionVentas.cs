using System.Globalization;

using aDVanceERP.Core.Controladores;
using aDVanceERP.Core.Modelos.Modulos.Compraventa;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Core.Repositorios.Modulos.Compraventa;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta;

public partial class VistaGestionVentas : Form, IVistaGestionVentas {
    private ControladorArchivosAndroid _androidFileManager;

    private int _paginaActual = 1;
    private int _paginasTotales = 1;

    public VistaGestionVentas() {
        InitializeComponent();

        NombreVista = nameof(VistaGestionVentas);

        Inicializar();

        _androidFileManager = new ControladorArchivosAndroid(Application.StartupPath);
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

    public bool HabilitarBtnConfirmarEntrega {
        get => btnConfirmarEntrega.Visible;
        set => btnConfirmarEntrega.Visible = value;
    }

    public bool HabilitarBtnConfirmarPagos {
        get => btnConfirmarPagos.Visible;
        set => btnConfirmarPagos.Visible = value;
    }

    public FiltroBusquedaVenta FiltroBusqueda {
        get => fieldFiltroBusqueda.SelectedIndex >= 0
            ? (FiltroBusquedaVenta) fieldFiltroBusqueda.SelectedIndex
            : default;
        set => fieldFiltroBusqueda.SelectedIndex = (int) value;
    }

    public string? CriterioBusqueda {
        get => fieldDatoBusqueda.Text;
        set => fieldDatoBusqueda.Text = value;
    }

    public string ValorBrutoVenta {
        get => fieldValorBrutoVenta.Text;
        set {
            layoutValorBrutoVenta.Visible = !value.Equals("0.00");
            fieldValorBrutoVenta.Text = value;
        }
    }

    public int TuplasMaximasContenedor {
        get => contenedorVistas.Height / VariablesGlobales.AlturaTuplaPredeterminada;
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
    public event EventHandler<string>? ImportarVentasArchivo;
    public event EventHandler? ConfirmarEntrega;
    public event EventHandler? ConfirmarPagos;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    public event EventHandler? DescargarReporte;
    public event EventHandler? ImprimirReporte;
    public event EventHandler<(FiltroBusquedaVenta, string?)>? BuscarEntidades;

    public void Inicializar() {
        // Vistas
        PanelCentral = new RepoVistaBase(contenedorVistas);

        // Eventos
        btnDescargar.Click += delegate {
            var filas = new List<string[]>();

            using (var datosVentas = new RepoVenta()) {
                var ventasFecha = datosVentas.Buscar(FiltroBusquedaVenta.Fecha, fieldDatoBusquedaFecha.Value.ToString("yyyy-MM-dd")).entidades;

                foreach (var venta in ventasFecha) {
                    using (var datosVentaProducto = new RepoDetalleVentaProducto()) {
                        var detalleVentaProducto = datosVentaProducto.Buscar(CriterioDetalleVentaProducto.IdVenta, venta.Id.ToString()).entidades;

                        foreach (var ventaProducto in detalleVentaProducto) {
                            var producto = RepoProducto.Instancia.ObtenerPorId(ventaProducto.IdProducto);
                            var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(producto?.IdUnidadMedida ?? 0);
                            var fila = new string[6];


                            fila[0] = ventaProducto.Id.ToString();
                            fila[1] = producto?.Nombre ?? string.Empty;
                            fila[2] = unidadMedidaProducto?.Abreviatura ?? "u";
                            fila[3] = ventaProducto.PrecioVentaFinal.ToString("N2", CultureInfo.InvariantCulture);
                            fila[4] = ventaProducto.Cantidad.ToString("N2", CultureInfo.InvariantCulture);
                            fila[5] = (ventaProducto.PrecioVentaFinal * ventaProducto.Cantidad).ToString("N2", CultureInfo.InvariantCulture);

                            filas.Add(fila);
                        }
                    }
                }
            }

            UtilesReportes.GenerarReporteVentas(fieldDatoBusquedaFecha.Value, filas);
        };
        fieldFiltroBusqueda.SelectedIndexChanged += delegate {
            if (FiltroBusqueda == FiltroBusquedaVenta.Fecha) {
                fieldDatoBusquedaFecha.Value = DateTime.Now;
                fieldDatoBusquedaFecha.Focus();

                ActualizarValorBrutoVentas();
            } else {
                layoutValorBrutoVenta.Visible = false;

                fieldDatoBusqueda.Text = string.Empty;
                fieldDatoBusqueda.Focus();
            }

            btnDescargar.Enabled = FiltroBusqueda == FiltroBusquedaVenta.Fecha;
            fieldDatoBusqueda.Visible = FiltroBusqueda != FiltroBusquedaVenta.Fecha &&
                                        fieldFiltroBusqueda.SelectedIndex != 0;
            fieldDatoBusquedaFecha.Visible = FiltroBusqueda == FiltroBusquedaVenta.Fecha &&
                                             fieldFiltroBusqueda.SelectedIndex != 0;

            if (FiltroBusqueda != FiltroBusquedaVenta.Fecha)
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
        btnRegistrar.Click += delegate (object? sender, EventArgs e) {
            // Comprobar la existencia de una caja abierta  antes de comenzar las ventas del día
            if (!UtilesCaja.ExisteCajaActiva()) {
                CentroNotificaciones.Mostrar("No existen cajas activas en la sección de finanzas, debe registrar una apertura de caja antes de proceder con nuevas ventas", TipoNotificacion.Advertencia);

                return;
            }

            RegistrarEntidad?.Invoke(sender, e);
        };
        btnImportarArchivoVentas.Click += delegate (object? sender, EventArgs e) {
            // Comprobar la existencia de una caja abierta  antes de importar ventas
            if (!UtilesCaja.ExisteCajaActiva()) {
                CentroNotificaciones.Mostrar("No existen cajas activas en la sección de finanzas, debe registrar una apertura de caja antes de proceder con nuevas ventas", TipoNotificacion.Advertencia);

                return;
            }

            if (!VerificarConexionDispositivo()) {
                CentroNotificaciones.Mostrar("Conecte un dispositivo Android con depuración USB activada", TipoNotificacion.Advertencia);
                return;
            } else {
                if (!_androidFileManager.EnsureDirectoryExists()) {
                    CentroNotificaciones.Mostrar("No se pudo crear el directorio en el dispositivo Android o la aplicación no está instalada", TipoNotificacion.Error);

                    return;
                }
            }

            var ventasFiles = _androidFileManager.ListVentasFilesOnDevice();

            if (!ventasFiles.Any()) {
                CentroNotificaciones.Mostrar("No se encontraron archivos de ventas en el dispositivo", TipoNotificacion.Advertencia);

                return;
            }

            var latestFile = ventasFiles
                .OrderByDescending(f => f.exportTime)
                .First();

            var rutaArchivoVentas = Path.Combine(Application.StartupPath, "ventas.json");

            if (_androidFileManager.PullFileFromDevice(latestFile.fileName, rutaArchivoVentas)) {
                _androidFileManager.DeleteFileFromDevice(latestFile.fileName);

                var ventasJson = File.ReadAllText(rutaArchivoVentas);
                if (string.IsNullOrEmpty(ventasJson)) {
                    CentroNotificaciones.Mostrar("No se encontraron datos de ventas en el archivo importado", TipoNotificacion.Advertencia);
                    return;
                }

                ImportarVentasArchivo?.Invoke(sender, ventasJson);

                // Limpiar archivo temporal
                try { File.Delete(rutaArchivoVentas); } catch { }
            } else {
                CentroNotificaciones.Mostrar("Error al importar el archivo de ventas desde el dispositivo Android", TipoNotificacion.Error);
            }
        };
        btnConfirmarEntrega.Click += delegate (object? sender, EventArgs e) { ConfirmarEntrega?.Invoke(sender, e); };
        btnConfirmarPagos.Click += delegate (object? sender, EventArgs e) { ConfirmarPagos?.Invoke(sender, e); };
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

    private bool VerificarConexionDispositivo() {
        var conexionOk = true;

        try {
            // Verificar conexión del dispositivo
            if (!_androidFileManager.CheckDeviceConnection())
                conexionOk = false;
        } catch (Exception ex) {
            CentroNotificaciones.Mostrar($"Error al verificar conexión del dispositivo: {ex.Message}", TipoNotificacion.Error);
        }

        btnImportarArchivoVentas.Visible = conexionOk;

        return conexionOk;
    }

    public void ActualizarValorBrutoVentas() {
        ValorBrutoVenta = UtilesVenta.ObtenerValorBrutoVentaDia(fieldDatoBusquedaFecha.Value)
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
        VerificarConexionDispositivo();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        Habilitada = true;
        PaginaActual = 1;
        PaginasTotales = 1;
        HabilitarBtnConfirmarEntrega = false;
        HabilitarBtnConfirmarPagos = false;

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
                                   "MOD_COMPRAVENTA_VENTA_ADICIONAR")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_COMPRAVENTA_VENTA_TODOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
    }

    private void HabilitarBotonesPaginacion() {
        btnPrimeraPagina.Enabled = PaginaActual > 1;
        btnPaginaAnterior.Enabled = PaginaActual > 1;
        btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
        btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
    }
}