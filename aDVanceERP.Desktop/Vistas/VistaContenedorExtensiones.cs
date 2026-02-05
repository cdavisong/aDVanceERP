using aDVanceERP.Desktop.Interfaces;

namespace aDVanceERP.Desktop.Vistas {
    public partial class VistaContenedorExtensiones : Form, IVistaContenedorExtensiones {
        public VistaContenedorExtensiones() {
            InitializeComponent();

            NombreVista = nameof(VistaContenedorExtensiones);

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

        public void Inicializar() {
            
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            
        }
    }
}