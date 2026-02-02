using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Properties;

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
            btnMensajeros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionMensajeros", string.Empty); };
            btnUbicaciones.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionUbicacionesVenta", string.Empty); };
            btnAtras.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuVenta", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnClientes.Visible)
                btnClientes.PerformClick();
            else if (btnMensajeros.Visible)
                btnMensajeros.PerformClick();
            else if (btnUbicaciones.Visible)
                btnUbicaciones.PerformClick();
        }

        public void Mostrar() {
            
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnClientes.Checked = false;
            btnMensajeros.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        
    }
}