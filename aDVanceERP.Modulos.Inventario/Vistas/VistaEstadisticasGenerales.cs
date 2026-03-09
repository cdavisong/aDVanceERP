using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaEstadisticasGenerales : Form, IVistaEstadisticasGenerales {
        public VistaEstadisticasGenerales() {
            InitializeComponent();

            NombreVista = nameof(VistaEstadisticasGenerales);

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
            // Eventos
            
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
            // ...
        }
    }
}