using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Ventas;
using aDVanceERP.Core.Eventos;

namespace DVanceERP.Modulos.Venta.Vistas;

public partial class VistaGestionVentas : Form, IVistaGestionVentas {
    private int _paginaActual = 1;
    private int _paginasTotales = 1;

    public VistaGestionVentas() {
        InitializeComponent();

        NombreVista = nameof(VistaGestionVentas);
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
    public FiltroBusquedaVenta FiltroBusqueda {
        get => fieldFiltroBusqueda.SelectedIndex >= 0
            ? (FiltroBusquedaVenta) fieldFiltroBusqueda.SelectedIndex
            : default;
        set => fieldFiltroBusqueda.SelectedIndex = (int) value;
    }

    public string[] CriteriosBusqueda {
        get => new[] { fieldDatoBusqueda.Text };
        set => fieldDatoBusqueda.Text = value.Length > 0 ? value[0] : string.Empty;
    }

    public int TuplasMaximasContenedor {
        get => contenedorVistas.Height / ContextoAplicacion.AlturaTuplaPredeterminada;
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

    public RepoVistaBase PanelCentral { get; private set; }


    public event EventHandler? AlturaContenedorTuplasModificada;
    public event EventHandler? MostrarPrimeraPagina;
    public event EventHandler? MostrarPaginaAnterior;
    public event EventHandler? MostrarPaginaSiguiente;
    public event EventHandler? MostrarUltimaPagina;
    public event EventHandler? SincronizarDatos;

    public event EventHandler? RegistrarEntidad;
    public event EventHandler? EditarEntidad;
    public event EventHandler? EliminarEntidad;
    public event EventHandler<(FiltroBusquedaVenta, string[])>? BuscarEntidades;

    public void Inicializar() {
        // Eventos
        AgregadorEventos.Suscribir("ResultadosBusquedaActualizados", OcultarMostrarBotonActivarDesactivarVenta);
        AgregadorEventos.Suscribir("CambioSeleccionTuplaEntidad", OcultarMostrarBotonActivarDesactivarVenta);

        fieldFiltroBusquedaFechaDesde.ValueChanged += OnCambioValorFechaDesde;
        fieldFiltroBusquedaFechaHasta.ValueChanged += OnCambioValorFechaHasta;
        fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
        fieldDatoBusqueda.KeyDown += delegate (object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            if (CriteriosBusqueda.Length > 0 && !string.IsNullOrEmpty(CriteriosBusqueda[0]))
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), CriteriosBusqueda[0] }));
            else SincronizarDatos?.Invoke(sender, args);

            args.SuppressKeyPress = true;
        };
        btnRegistrarVentaManual.Click += delegate (object? sender, EventArgs e) {
            RegistrarEntidad?.Invoke(sender, e);
        };
        btnHabilitarDeshabilitarVenta.Click += delegate (object? sender, EventArgs e) {
            AgregadorEventos.Publicar("HabilitarDeshabilitarVenta", string.Empty);
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
        btnSincronizarDatos.Click += delegate (object? sender, EventArgs e) {
            SincronizarDatos?.Invoke(sender, e);
        };
        contenedorVistas.Resize += delegate { AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty); };
    }

    private void OcultarMostrarBotonActivarDesactivarVenta(string obj) {
        if (string.IsNullOrEmpty(obj))
            return;

        try {
            var visibilidadBoton = Convert.ToBoolean(obj.ToString());

            btnHabilitarDeshabilitarVenta.Visible = visibilidadBoton;
        } catch (FormatException) {
            btnHabilitarDeshabilitarVenta.Visible = false;
        }
    }

    private void OnCambioValorFechaDesde(object? sender, EventArgs e) {
        var valorFechaDesde = fieldFiltroBusquedaFechaDesde.Value;
        var valorFechaHasta = fieldFiltroBusquedaFechaHasta.Value;

        if (valorFechaDesde <= valorFechaHasta)
            BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), CriteriosBusqueda[0] }));
        else {
            fieldFiltroBusquedaFechaDesde.Value = valorFechaHasta;

            CentroNotificaciones.Mostrar("La fecha de inicio no puede ser mayor que la fecha final o fecha del día de hoy, por favor, corrija los datos de entrada", aDVanceERP.Core.Modelos.Comun.TipoNotificacion.Advertencia);
        }
    }

    private void OnCambioValorFechaHasta(object? sender, EventArgs e) {
        var valorFechaDesde = fieldFiltroBusquedaFechaDesde.Value;
        var valorFechaHasta = fieldFiltroBusquedaFechaHasta.Value;

        if (valorFechaHasta >= valorFechaDesde && valorFechaHasta <= DateTime.Now)
            BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), CriteriosBusqueda[0] }));
        else {
            fieldFiltroBusquedaFechaHasta.Value = DateTime.Now;

            CentroNotificaciones.Mostrar("La fecha final no puede ser menor que la fecha inicial o mayor que la fecha del día de hoy, por favor, corrija los datos de entrada", aDVanceERP.Core.Modelos.Comun.TipoNotificacion.Advertencia);
        }
    }

    private void OnCambioIndiceFiltroBusqueda(object? sender, EventArgs e) {
        fieldDatoBusqueda.Text = string.Empty;
        fieldDatoBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0;

        if (fieldDatoBusqueda.Visible)
            fieldDatoBusqueda.Focus();

        BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { fieldFiltroBusquedaFechaDesde.Value.ToString("yyyy-MM-dd"), fieldFiltroBusquedaFechaHasta.Value.ToString("yyyy-MM-dd"), string.Empty }));

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
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        PaginaActual = 1;
        PaginasTotales = 1;

        btnHabilitarDeshabilitarVenta.Hide();

        // Evitar búsquedas innecesarias al restablecer los campos de fecha
        fieldFiltroBusquedaFechaDesde.ValueChanged -= OnCambioValorFechaDesde;
        fieldFiltroBusquedaFechaHasta.ValueChanged -= OnCambioValorFechaHasta;

        fieldFiltroBusquedaFechaDesde.Value = DateTime.Now;
        fieldFiltroBusquedaFechaHasta.Value = DateTime.Now;

        fieldFiltroBusquedaFechaDesde.ValueChanged += OnCambioValorFechaDesde;
        fieldFiltroBusquedaFechaHasta.ValueChanged += OnCambioValorFechaHasta;

        fieldFiltroBusqueda.SelectedIndex = 0;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        // ...
    }

    private void VerificarPermisos() {
        btnRegistrarVentaManual.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_ADICIONAR")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_VENTA_TODOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_COMPRAVENTA_TODOS");
    }

    private void HabilitarBotonesPaginacion() {
        btnPrimeraPagina.Enabled = PaginaActual > 1;
        btnPaginaAnterior.Enabled = PaginaActual > 1;
        btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
        btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
    }
}