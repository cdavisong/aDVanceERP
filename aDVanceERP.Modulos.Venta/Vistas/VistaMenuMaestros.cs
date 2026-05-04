using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaMenuMaestros : Form, IVistaMenuMaestros {
        public VistaMenuMaestros() {
            InitializeComponent();

            NombreVista = $"{nameof(VistaMenuMaestros)}Venta";

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
            btnClientes.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionClientes", string.Empty); };
            btnAtras.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuVenta", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnClientes.Visible)
                btnClientes.PerformClick();
        }

        public void Mostrar() {
            
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnClientes.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        
    }
}