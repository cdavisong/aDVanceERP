using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;
using aDVanceERP.Modulos.CajaRegistradora.Properties;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaDetalleTurno : Form, IVistaDetalleTurno {
        private int _paginaActual = 1;
        private int _paginasTotales = 1;

        public VistaDetalleTurno() {
            InitializeComponent();

            NombreVista = nameof(VistaDetalleTurno);
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

        public FiltroBusquedaCajaMovimiento FiltroBusqueda {
            get => FiltroBusquedaCajaMovimiento.Todos;
        }

        public string[] CriteriosBusqueda {
            get => [fieldDatoBusqueda.Text];
            set => fieldDatoBusqueda.Text = value.Length > 0 ? value[0] : string.Empty;
        }

        public int TuplasMaximasContenedor {
            get => contenedorVistas.Height / ContextoAplicacion.AlturaTuplaPredeterminada;
        }

        public int PaginaActual {
            get => _paginaActual;
            set {
                _paginaActual = value;
                fieldPaginaActual.Text = $"PÁGINA {value}";
            }
        }

        public int PaginasTotales {
            get => _paginasTotales;
            set {
                _paginasTotales = value;
                fieldPaginasTotales.Text = $"DE {value}";
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

        public event EventHandler<(FiltroBusquedaCajaMovimiento, string[])>? BuscarEntidades;
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            // Eventos
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
            //...
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            PaginaActual = 1;
            PaginasTotales = 1;
        }

        public void Ocultar() {
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

            // Iconos
            btnPrimeraPagina.CustomImages.Image = PaginaActual > 1
                ? Resources.page_first_24px
                : Resources.page_first_disabled_24px;
            btnPaginaAnterior.CustomImages.Image = PaginaActual > 1
                ? Resources.page_previous_24px
                : Resources.page_previous_disabled_24px;
            btnUltimaPagina.CustomImages.Image = PaginaActual < PaginasTotales
                ? Resources.page_last_24px
                : Resources.page_last_disabled_24px;
            btnPaginaSiguiente.CustomImages.Image = PaginaActual < PaginasTotales
                ? Resources.page_next_24px
                : Resources.page_next_disabled_24px;
        }
    }
}