using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Utiles;

using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas.CuentaUsuario;

public partial class VistaGestionCuentasUsuarios : Form, IVistaGestionCuentasUsuarios {
    private int _paginaActual = 1;
    private int _paginasTotales = 1;

    public VistaGestionCuentasUsuarios() {
        InitializeComponent();

        NombreVista = nameof(VistaGestionCuentasUsuarios);
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

    public FiltroBusquedaCuentaUsuario FiltroBusqueda {
        get => fieldFiltroBusqueda.SelectedIndex >= 0
            ? (FiltroBusquedaCuentaUsuario) fieldFiltroBusqueda.SelectedIndex
            : default;
        set => fieldFiltroBusqueda.SelectedIndex = (int) value;
    }

    public string? CriterioBusqueda {
        get => fieldDatoBusqueda.Text;
        set => fieldDatoBusqueda.Text = value;
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

    public bool MostrarBtnAprobacionSolicitudCuenta {
        get => btnAprobarSolicitudCuenta.Visible;
        set => btnAprobarSolicitudCuenta.Visible = value;
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
    public event EventHandler<(FiltroBusquedaCuentaUsuario, string?)>? BuscarEntidades;

    public event EventHandler? AprobarSolicitudCuenta;

    public void Inicializar() {
        // Eventos
        fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
        fieldDatoBusqueda.KeyDown += delegate(object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            if (!string.IsNullOrEmpty(CriterioBusqueda))
                BuscarEntidades?.Invoke(sender, (FiltroBusqueda, CriterioBusqueda));
            else SincronizarDatos?.Invoke(sender, args);

            args.SuppressKeyPress = true;
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs e) { RegistrarEntidad?.Invoke(sender, e); };
        btnAprobarSolicitudCuenta.Click += delegate (object? sender, EventArgs e) {
            btnAprobarSolicitudCuenta.Hide();
            AprobarSolicitudCuenta?.Invoke(sender, e);
        };
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

    private void OnCambioIndiceFiltroBusqueda(object? sender, EventArgs e) {
        fieldDatoBusqueda.Text = string.Empty;
        fieldDatoBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0;

        if (fieldDatoBusqueda.Visible)
            fieldDatoBusqueda.Focus();

        BuscarEntidades?.Invoke(this, (FiltroBusqueda, string.Empty));

        // Ir a la primera página al cambiar el criterio de búsqueda
        PaginaActual = 1;
        HabilitarBotonesPaginacion();
    }

    public void CargarFiltrosBusqueda(object[] criteriosBusqueda) {
        // Evitar que se dispare el evento SelectedIndexChanged al modificar los ítems
        fieldFiltroBusqueda.SelectedIndexChanged -= OnCambioIndiceFiltroBusqueda;

        fieldFiltroBusqueda.Items.Clear();
        fieldFiltroBusqueda.Items.AddRange(criteriosBusqueda);

        if (fieldFiltroBusqueda.Items.Count > 0)
            fieldFiltroBusqueda.SelectedIndex = 0;

        // Reasignar el evento SelectedIndexChanged
        fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;

        btnAprobarSolicitudCuenta.Hide();
    }

    public void Mostrar() {
        Habilitada = true;
        BringToFront();
        Show();
    }

    public void Restaurar() {
        Habilitada = true;
        PaginaActual = 1;
        PaginasTotales = 1;
        MostrarBtnAprobacionSolicitudCuenta = false;

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

    private void HabilitarBotonesPaginacion() {
        btnPrimeraPagina.Enabled = PaginaActual > 1;
        btnPaginaAnterior.Enabled = PaginaActual > 1;
        btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
        btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
    }
}