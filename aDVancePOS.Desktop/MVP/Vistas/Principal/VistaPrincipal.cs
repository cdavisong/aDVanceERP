using aDVanceERP.Core.MVP.Modelos.Repositorios.Plantillas;
using aDVanceERP.Core.MVP.Modelos.Repositorios;
using aDVanceERP.Core.Utiles;
using aDVancePOS.Desktop.MVP.Vistas.Principal.Plantillas;

namespace aDVancePOS.Desktop.MVP.Vistas.Principal {
    public partial class VistaPrincipal : Form, IVistaPrincipal {
        public VistaPrincipal() {
            InitializeComponent();
            Inicializar();
        }

        public IRepositorioVista? Vistas { get; private set; }

        public IRepositorioVista Menus { get; private set; }

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

        public int AlturaContenedorVistas {
            get => contenedorVistas.Height;
        }

        public int TuplasMaximasContenedor {
            get => AlturaContenedorVistas / VariablesGlobales.AlturaTuplaPredeterminada;
        }

        public bool BtnSubmenuUsuarioDisponible {
            get => btnSubMenuUsuario.Visible;
            set {
                btnSubMenuUsuario.Visible = value;
                btnMensajes.Visible = value;
                btnNotificaciones.Visible = value;
            }
        }

        public event EventHandler? VerNotificaciones;
        public event EventHandler? VerMensajes;
        public event EventHandler? SubMenuUsuario;
        public event EventHandler? Salir;

        public void Inicializar() {
            FormClosing += delegate {
                Salir?.Invoke(this, EventArgs.Empty);
                Cerrar();
            };

            // Repositorios
            Vistas = new RepositorioVistaBase(contenedorVistas);
            Menus = new RepositorioVistaBase(contenedorMenus);

            // Eventos
            btnNotificaciones.Click += delegate (object? sender, EventArgs args) {
                VerNotificaciones?.Invoke(sender, args);
            };
            btnMensajes.Click += delegate(object? sender, EventArgs args) {
                VerMensajes?.Invoke(sender, args);
            };
            btnSubMenuUsuario.Click += delegate(object? sender, EventArgs args) {
                SubMenuUsuario?.Invoke(sender, args);
            };
            Resize += delegate { };
            FormClosing += delegate(object? sender, FormClosingEventArgs args) {
                Salir?.Invoke(sender, args);
            };
        }

        public void Mostrar() {
            WindowState = FormWindowState.Maximized;
        }

        public void Ocultar() {
            WindowState = FormWindowState.Minimized;
        }

        public void Restaurar() {
            throw new NotImplementedException();
        }

        public void Cerrar() {
            Vistas.Cerrar(true);
        }
    }
}
