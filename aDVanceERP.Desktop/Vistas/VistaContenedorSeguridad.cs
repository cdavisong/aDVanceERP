using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Vistas {
    public partial class VistaContenedorSeguridad : Form, IVistaContenedorSeguridad {
        public VistaContenedorSeguridad() {
            InitializeComponent();

            PanelCentral = new RepoVistaBase(contenedorVistas);
            NombreVista = nameof(VistaContenedorSeguridad);

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

        public int TuplasMaximasContenedor {
            get => contenedorVistas.Height / ContextoAplicacion.AlturaTuplaPredeterminada;
        }

        public RepoVistaBase PanelCentral { get; private set; }

        public void Inicializar() {
                
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            PanelCentral?.Restaurar("vistaAutenticacionUsuario");

            // Restablecer el usuario autenticado
            ContextoSeguridad.UsuarioAutenticado = null;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            PanelCentral?.CerrarTodos();
        }
    }
}