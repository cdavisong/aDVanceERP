using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

using System.Globalization;

using Size = System.Drawing.Size;

namespace aDVanceERP.Modulos.Inventario.Vistas;

public partial class VistaGestionProductos : Form, IVistaGestionProductos {
    private int _paginaActual = 1;
    private int _paginasTotales = 1;

    public VistaGestionProductos() {
        InitializeComponent();

        NombreVista = nameof(VistaGestionProductos);
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

    public string? NombreAlmacen {
        get => fieldFiltroAlmacen.Text;
        set => fieldFiltroAlmacen.Text = value;
    }

    public int Categoria {
        get => fieldFiltroCategoriaProducto.SelectedIndex;
        set => fieldFiltroCategoriaProducto.SelectedIndex = value + 1;
    }

    public FiltroBusquedaProducto FiltroBusqueda {
        get => fieldFiltroBusqueda.SelectedIndex > 0
            ? (FiltroBusquedaProducto) fieldFiltroBusqueda.SelectedIndex
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

    public decimal ValorTotalInventario {
        get => decimal.TryParse(fieldValorTotalInventario.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorTotal) ? valorTotal : 0m;
        set {
            layoutValorBrutoInversion.Visible = value > 0;
            fieldValorTotalInventario.Text = value.ToString("N2", CultureInfo.InvariantCulture);
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
    public event EventHandler<(FiltroBusquedaProducto, string[])>? BuscarEntidades;

    public async void Inicializar() {
        // Eventos
        AgregadorEventos.Suscribir("ResultadosBusquedaActualizados", OcultarMostrarBotonActivarDesactivarProducto);
        AgregadorEventos.Suscribir("CambioSeleccionTuplaEntidad", OcultarMostrarBotonActivarDesactivarProducto);

        fieldFiltroAlmacen.SelectedIndexChanged += OnCambioIndiceFiltroAlmacen;
        fieldFiltroCategoriaProducto.SelectedIndexChanged += OnCambioIndiceCategoriaProducto;
        fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
        fieldDatoBusqueda.KeyDown += delegate (object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            if (CriteriosBusqueda.Length > 0 && !string.IsNullOrEmpty(CriteriosBusqueda[0]))
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { string.IsNullOrEmpty(NombreAlmacen) ? "Todos" : NombreAlmacen, Categoria.ToString(), CriteriosBusqueda[0] }));
            else SincronizarDatos?.Invoke(sender, args);

            args.SuppressKeyPress = true;
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs e) {
            RegistrarEntidad?.Invoke(sender, e);

            ActualizarValorTotalInventario();
        };
        btnHabilitarDeshabilitarProducto.Click += delegate (object? sender, EventArgs e) {
            AgregadorEventos.Publicar("HabilitarDeshabilitarProducto", string.Empty);
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

            ActualizarValorTotalInventario();
        };
        contenedorVistas.Resize += delegate { AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty); };
    }

    private void OcultarMostrarBotonActivarDesactivarProducto(string obj) {
        if (string.IsNullOrEmpty(obj))
            return;

        try {
            var visibilidadBoton = Convert.ToBoolean(obj.ToString());

            btnHabilitarDeshabilitarProducto.Visible = visibilidadBoton;
        } catch (FormatException) {
            btnHabilitarDeshabilitarProducto.Visible = false;
        }
    }

    private void OnCambioIndiceFiltroAlmacen(object? sender, EventArgs e) {
        if (!string.IsNullOrEmpty(NombreAlmacen))
            BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { string.IsNullOrEmpty(NombreAlmacen) ? "Todos" : NombreAlmacen, Categoria.ToString(), CriteriosBusqueda[0] }));
        else SincronizarDatos?.Invoke(sender, e);

        ActualizarValorTotalInventario();
    }

    private void OnCambioIndiceCategoriaProducto(object? sender, EventArgs e) {
        if (Categoria > -1)
            BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { string.IsNullOrEmpty(NombreAlmacen) ? "Todos" : NombreAlmacen, Categoria.ToString(), CriteriosBusqueda[0] }));
        else SincronizarDatos?.Invoke(sender, e);

        ActualizarValorTotalInventario();
    }

    private void OnCambioIndiceFiltroBusqueda(object? sender, EventArgs e) {
        fieldDatoBusqueda.Text = string.Empty;
        fieldDatoBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0 && fieldFiltroBusqueda.SelectedIndex != 5;

        if (fieldDatoBusqueda.Visible)
            fieldDatoBusqueda.Focus();

        BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { string.IsNullOrEmpty(NombreAlmacen) ? "Todos" : NombreAlmacen, Categoria.ToString(), CriteriosBusqueda[0] }));

        // Ir a la primera página al cambiar el criterio de búsqueda
        PaginaActual = 1;
        HabilitarBotonesPaginacion();
    }

    public void CargarFiltroAlmacenes(object[] nombresAlmacenes) {
        // Evitar que se dispare el evento SelectedIndexChanged al modificar los ítems
        fieldFiltroAlmacen.SelectedIndexChanged -= OnCambioIndiceFiltroAlmacen;

        fieldFiltroAlmacen.Items.Clear();
        fieldFiltroAlmacen.Items.AddRange(nombresAlmacenes);

        if (fieldFiltroAlmacen.Items.Count > 0)
            fieldFiltroAlmacen.SelectedIndex = 0;

        // Reasignar el evento SelectedIndexChanged
        fieldFiltroAlmacen.SelectedIndexChanged += OnCambioIndiceFiltroAlmacen;

        ActualizarValorTotalInventario();
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

        btnHabilitarDeshabilitarProducto.Hide();
    }

    public void Mostrar() {
        VerificarPermisos();
        BringToFront();
        Show();
    }

    public void Restaurar() {
        PaginaActual = 1;
        PaginasTotales = 1;

        btnHabilitarDeshabilitarProducto.Hide();
        if (fieldFiltroAlmacen.Items.Count > 0)
            fieldFiltroAlmacen.SelectedIndex = 0;
        if (fieldFiltroBusqueda.Items.Count > 0)
            fieldFiltroBusqueda.SelectedIndex = 0;
    }

    public void Ocultar() {
        Hide();
    }

    public void Cerrar() {
        //...
    }

    private void ActualizarValorTotalInventario() {
        ValorTotalInventario = RepoProducto.Instancia.ObtenerValorTotalBruto(
            RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, NombreAlmacen).resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0);
    }

    private void VerificarPermisos() {
        if (ContextoSeguridad.UsuarioAutenticado == null || ContextoSeguridad.PermisosUsuario == null) {
            btnRegistrar.Enabled = false;
            return;
        }

        btnRegistrar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_INVENTARIO_PRODUCTOS_ADICIONAR")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_INVENTARIO_PRODUCTOS_TODOS")
                               || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }

    private void HabilitarBotonesPaginacion() {
        btnPrimeraPagina.Enabled = PaginaActual > 1;
        btnPaginaAnterior.Enabled = PaginaActual > 1;
        btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
        btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
    }
}