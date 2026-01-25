using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas {
    public partial class VistaGestionProveedores : Form, IVistaGestionProveedores {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;

        public VistaGestionProveedores() {
            InitializeComponent();

            NombreVista = nameof(VistaGestionProveedores);
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

        public FiltroBusquedaProveedor FiltroBusqueda {
            get => fieldFiltroBusqueda.SelectedIndex >= 0
                ? (FiltroBusquedaProveedor)fieldFiltroBusqueda.SelectedIndex
                : default;
            set => fieldFiltroBusqueda.SelectedIndex = (int)value;
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

        public event EventHandler? AlturaContenedorTuplasModificada;
        public event EventHandler? MostrarPrimeraPagina;
        public event EventHandler? MostrarPaginaAnterior;
        public event EventHandler? MostrarPaginaSiguiente;
        public event EventHandler? MostrarUltimaPagina;
        public event EventHandler? SincronizarDatos;
    
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;
        public event EventHandler<(FiltroBusquedaProveedor, string[])>? BuscarEntidades;

        public void Inicializar() {
            // Variables locales
            PanelCentral = new RepoVistaBase(contenedorVistas);

            // Eventos
            AgregadorEventos.Suscribir("ResultadosBusquedaActualizados", OcultarMostrarBotonActivarDesactivarProveedor);
            AgregadorEventos.Suscribir("CambioSeleccionTuplaEntidad", OcultarMostrarBotonActivarDesactivarProveedor);

            fieldFiltroBusqueda.SelectedIndexChanged += OnCambioIndiceFiltroBusqueda;
            fieldDatoBusqueda.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                if (!string.IsNullOrEmpty(CriteriosBusqueda[0]))
                    BuscarEntidades?.Invoke(this, (FiltroBusqueda, CriteriosBusqueda));
                else SincronizarDatos?.Invoke(sender, args);

                args.SuppressKeyPress = true;
            };
            btnActivarDesactivarProveedor.Click += delegate (object? sender, EventArgs e) {
                AgregadorEventos.Publicar("ActivarDesactivarProveedor", string.Empty);
            };
            btnRegistrar.Click += delegate(object? sender, EventArgs e) { RegistrarEntidad?.Invoke(sender, e); };
            btnPrimeraPagina.Click += delegate(object? sender, EventArgs e) {
                PaginaActual = 1;
                MostrarPrimeraPagina?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnPaginaAnterior.Click += delegate(object? sender, EventArgs e) {
                PaginaActual--;
                MostrarPaginaAnterior?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnPaginaSiguiente.Click += delegate(object? sender, EventArgs e) {
                PaginaActual++;
                MostrarPaginaSiguiente?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnUltimaPagina.Click += delegate(object? sender, EventArgs e) {
                PaginaActual = PaginasTotales;
                MostrarUltimaPagina?.Invoke(sender, e);
                SincronizarDatos?.Invoke(sender, e);
                HabilitarBotonesPaginacion();
            };
            btnSincronizarDatos.Click += delegate(object? sender, EventArgs e) { SincronizarDatos?.Invoke(sender, e); };
            contenedorVistas.Resize += delegate { AlturaContenedorTuplasModificada?.Invoke(this, EventArgs.Empty); };
        }

        private void OcultarMostrarBotonActivarDesactivarProveedor(string obj) {
            if (string.IsNullOrEmpty(obj))
                return;

            try {
                var visibilidadBoton = Convert.ToBoolean(obj.ToString());

                btnActivarDesactivarProveedor.Visible = visibilidadBoton;
            } catch (FormatException) {
                btnActivarDesactivarProveedor.Visible = false;
            }
        }

        private void OnCambioIndiceFiltroBusqueda(object? sender, EventArgs e) {
            fieldDatoBusqueda.Text = string.Empty;
            fieldDatoBusqueda.Visible = fieldFiltroBusqueda.SelectedIndex != 0;

            if (fieldDatoBusqueda.Visible)
                fieldDatoBusqueda.Focus();

            BuscarEntidades?.Invoke(this, (FiltroBusqueda, new[] { string.Empty }));

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

            btnActivarDesactivarProveedor.Hide();
            fieldFiltroBusqueda.SelectedIndex = 0;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            // ...
        }

        private void VerificarPermisos() {
            btnRegistrar.Enabled = (ContextoSeguridad.UsuarioAutenticado?.Administrador ?? false)
                                   || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                       "MOD_RRHH_PROVEEDORES_ADICIONAR")
                                   || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto(
                                       "MOD_RRHH_PROVEEDORES_TODOS")
                                   || ContextoSeguridad.PermisosUsuario.ContienePermisoExacto("MOD_RRHH_TODOS");
        }

        private void HabilitarBotonesPaginacion() {
            btnPrimeraPagina.Enabled = PaginaActual > 1;
            btnPaginaAnterior.Enabled = PaginaActual > 1;
            btnUltimaPagina.Enabled = PaginaActual < PaginasTotales;
            btnPaginaSiguiente.Enabled = PaginaActual < PaginasTotales;
        }
    }
}