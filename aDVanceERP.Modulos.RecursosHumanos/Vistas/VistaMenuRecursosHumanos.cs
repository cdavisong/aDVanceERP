using aDVanceERP.Core.Eventos;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Vistas {
    public partial class VistaMenuRecursosHumanos : Form, IVistaMenuRecursosHumanos {
        public VistaMenuRecursosHumanos() {
            InitializeComponent();

            NombreVista = nameof(VistaMenuRecursosHumanos);

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
            btnMaestros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuMaestrosRecursosHumanos", string.Empty); };
        }

        public void SeleccionarVistaInicial() {

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
            Dispose();
        }

        
    }
}