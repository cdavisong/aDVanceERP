using System.Globalization;

using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto;

public partial class VistaGestionProductos : Form, IVistaGestionProductos {
    private int _paginaActual = 1;
    private int _paginasTotales = 1;

    public VistaGestionProductos() {
        InitializeComponent();

        NombreVista = nameof(VistaGestionProductos);

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
        get => fieldNombreAlmacen.Text;
        set => fieldNombreAlmacen.Text = value;
    }

    public int Categoria {
        get => fieldCriterioCategoriaProducto.SelectedIndex - 1;
        set => fieldCriterioCategoriaProducto.SelectedIndex = value + 1;
    }

    public FiltroBusquedaProducto FiltroBusqueda {
        get => fieldFiltroBusqueda.SelectedIndex > 0
            ? (FiltroBusquedaProducto) fieldFiltroBusqueda.SelectedIndex
            : default;
        set => fieldFiltroBusqueda.SelectedIndex = (int) value;
    }

    public string? CriterioBusqueda {
        get => fieldDatoBusqueda.Text;
        set => fieldDatoBusqueda.Text = value;
    }

    public decimal ValorTotalInventario {
        get => decimal.TryParse(fieldValorTotalInventario.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorTotal) ? valorTotal : 0m;
        set {
            layoutValorBrutoInversion.Visible = value > 0;
            fieldValorTotalInventario.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }
    }

    public bool MostrarBtnHabilitarDeshabilitarProducto {
        get => btnHabilitarDeshabilitarProducto.Visible;
        set => btnHabilitarDeshabilitarProducto.Visible = value;
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
    public event EventHandler<(FiltroBusquedaProducto, string?)>? BuscarEntidades;
    public event EventHandler? HabilitarDeshabilitarProducto;

    public void Inicializar() {
        // Variables locales
        PanelCentral = new RepoVistaBase(contenedorVistas);

        // Eventos
        fieldNombreAlmacen.SelectedIndexChanged += delegate (object? sender, EventArgs e) {
            if (!string.IsNullOrEmpty(NombreAlmacen))
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, string.Join(';', NombreAlmacen, Categoria.ToString(), CriterioBusqueda)));
            else SincronizarDatos?.Invoke(sender, e);

            ActualizarValorTotalInventario();
        };
        fieldCriterioCategoriaProducto.SelectedIndexChanged += delegate (object? sender, EventArgs e) {
            if (Categoria > -1)
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, string.Join(';', NombreAlmacen, Categoria.ToString(), CriterioBusqueda)));
            else SincronizarDatos?.Invoke(sender, e);

            ActualizarValorTotalInventario();
        };
        fieldFiltroBusqueda.SelectedIndexChanged += delegate {
            fieldDatoBusqueda.Text = string.Empty;
            fieldDatoBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0 && fieldFiltroBusqueda.SelectedIndex != 5;
            fieldDatoBusqueda.Focus();

            BuscarEntidades?.Invoke(this, (FiltroBusqueda, string.Join(';', NombreAlmacen, Categoria.ToString(), CriterioBusqueda)));

            // Ir a la primera página al cambiar el criterio de búsqueda
            PaginaActual = 1;
            HabilitarBotonesPaginacion();
        };
        fieldDatoBusqueda.KeyDown += delegate(object? sender, KeyEventArgs args) {
            if (args.KeyCode != Keys.Enter)
                return;

            if (!string.IsNullOrEmpty(CriterioBusqueda))
                BuscarEntidades?.Invoke(this, (FiltroBusqueda, string.Join(';', NombreAlmacen, Categoria.ToString(), CriterioBusqueda)));
            else SincronizarDatos?.Invoke(sender, args);

            args.SuppressKeyPress = true;
        };
        btnCerrar.Click += delegate (object? sender, EventArgs e) {
            Ocultar();
        };
        btnRegistrar.Click += delegate (object? sender, EventArgs e) {
            RegistrarEntidad?.Invoke(sender, e);

            ActualizarValorTotalInventario();
        };
        btnHabilitarDeshabilitarProducto.Click += delegate (object? sender, EventArgs e) {
            HabilitarDeshabilitarProducto?.Invoke(sender, e);
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
        contenedorVistas.Resize += delegate {
            AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty);
        };
    }

    public void CargarNombresAlmacenes(object[] nombresAlmacenes) {
        fieldNombreAlmacen.Items.Clear();
        fieldNombreAlmacen.Items.Add("Todos los almacenes");
        fieldNombreAlmacen.Items.AddRange(nombresAlmacenes);
        fieldNombreAlmacen.SelectedIndex = fieldNombreAlmacen.Items.Count > 0 ? 0 : -1;
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
        MostrarBtnHabilitarDeshabilitarProducto = false;

        fieldNombreAlmacen.SelectedIndex = 0;
        fieldFiltroBusqueda.SelectedIndex = 0;
    }

    public void Ocultar() {
        Habilitada = false;
        Hide();
    }

    public void Cerrar() {
        //...
    }

    private void ActualizarValorTotalInventario() {
        ValorTotalInventario = RepoProducto.Instancia.ObtenerValorTotalBruto(
            RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, NombreAlmacen).resultados.FirstOrDefault()?.Id ?? 0);
    }

    private void VerificarPermisos() {
        if (UtilesCuentaUsuario.UsuarioAutenticado == null || UtilesCuentaUsuario.PermisosUsuario == null) {
            btnRegistrar.Enabled = false;
            return;
        }

        btnRegistrar.Enabled = (UtilesCuentaUsuario.UsuarioAutenticado?.Administrador ?? false)
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_INVENTARIO_PRODUCTOS_ADICIONAR")
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto(
                                   "MOD_INVENTARIO_PRODUCTOS_TODOS")
                               || UtilesCuentaUsuario.PermisosUsuario.ContienePermisoExacto("MOD_INVENTARIO_TODOS");
    }

    private void HabilitarBotonesPaginacion() {
        btnPrimeraPagina.Enabled = PaginaActual > 1;
        btnPaginaAnterior.Enabled = PaginaActual > 1;
        btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
        btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
    }
}