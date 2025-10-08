using aDVanceERP.Core.Documentos.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Utiles;

using aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen.Plantillas;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen;

public partial class VistaGestionAlmacenes : Form, IVistaGestionAlmacenes {
    private int _paginaActual = 1;
    private int _paginasTotales = 1;

    public VistaGestionAlmacenes() {
        InitializeComponent();

        NombreVista = nameof(VistaGestionAlmacenes);
        PanelCentral = new RepoVistaBase(contenedorVistas);

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

    public FiltroBusquedaAlmacen FiltroBusqueda {
        get => fieldFiltroBusqueda.SelectedIndex >= 0
            ? (FiltroBusquedaAlmacen)fieldFiltroBusqueda.SelectedIndex
            : default;
        set => fieldFiltroBusqueda.SelectedIndex = (int)value;
    }

    public string? CriterioBusqueda {
        get => fieldDatoBusqueda.Text;
        set => fieldDatoBusqueda.Text = value;
    }

    public bool MostrarBtnImportarInventarioVersat {
        get => btnImportarInventarioVersat.Visible;
        set => btnImportarInventarioVersat.Visible = value;
    }

    public int TuplasMaximasContenedor {
        get => contenedorVistas.Height / VariablesGlobales.AlturaTuplaPredeterminada;
    }

    public int PaginaActual {
        get => _paginaActual;
        set {
            _paginaActual = value;
            fieldPaginaActual.Text = $"Página {value}";
        }
    }

    public int PaginasTotales {
        get => _paginasTotales;
        set {
            _paginasTotales = value;
            fieldPaginasTotales.Text = $"de {value}";
            HabilitarBotonesPaginacion();
        }
    }

    public RepoVistaBase PanelCentral { get; private set; }

    public bool DispositivoConectado { get; private set; }

    public event EventHandler<FormatoDocumento>? ExportarDocumentoInventario;
    public event EventHandler<string>? ImportarInventarioVersat;

    public event EventHandler? AlturaContenedorTuplasModificada;
    public event EventHandler? MostrarPrimeraPagina;
    public event EventHandler? MostrarPaginaAnterior;
    public event EventHandler? MostrarPaginaSiguiente;
    public event EventHandler? MostrarUltimaPagina;
    public event EventHandler? SincronizarDatos;
    
    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    public event EventHandler<(FiltroBusquedaAlmacen, string?)>? BuscarEntidades;

    public void Inicializar() {
        // Eventos
        fieldFiltroBusqueda.SelectedIndexChanged += delegate {
            fieldDatoBusqueda.Text = string.Empty;
            fieldDatoBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0;
            fieldDatoBusqueda.Focus();

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
        btnCerrar.Click += delegate (object? sender, EventArgs e) {
            Ocultar();
        };
        btnExportarInventarioAlmacenes.Click += delegate {
            btnExportarInventarioAlmacenes.ContextMenuStrip?.Show(btnExportarInventarioAlmacenes, new Point(0, 40));
        };
        btnExportarPdf.Click += delegate { ExportarDocumentoInventario?.Invoke(this, FormatoDocumento.PDF); };
        btnExportarXlsx.Click += delegate { ExportarDocumentoInventario?.Invoke(this,FormatoDocumento.Excel); };
        btnImportarInventarioVersat.Click += delegate {
            var resultado = fieldImportarArchivo.ShowDialog(this);

            if (resultado == DialogResult.OK) {
                var rutaArchivo = fieldImportarArchivo.FileName;

                ImportarInventarioVersat?.Invoke(this, rutaArchivo);
            }
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
        btnSincronizarDatos.Click += delegate (object? sender, EventArgs e) {
            SincronizarDatos?.Invoke(sender, e);            
        };
        contenedorVistas.Resize += delegate { 
            AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty); 
        };
    }

    public void CargarFiltrosBusqueda(object[] criteriosBusqueda) {
        fieldFiltroBusqueda.Items.Clear();
        fieldFiltroBusqueda.Items.AddRange(criteriosBusqueda);
        fieldFiltroBusqueda.SelectedIndex = 0;
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
        MostrarBtnImportarInventarioVersat = false;

        if (fieldFiltroBusqueda.Items.Count > 0)
            fieldFiltroBusqueda.SelectedIndex = 0;
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
                                   "MOD_INVENTARIO_ALMACENES_ADICIONAR")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_INVENTARIO_ALMACENES_TODOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }

    private void HabilitarBotonesPaginacion() {
        btnPrimeraPagina.Enabled = PaginaActual > 1;
        btnPaginaAnterior.Enabled = PaginaActual > 1;
        btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
        btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
    }
}