using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaMenuVenta : Form, IVistaMenuVenta {
        public VistaMenuVenta() {
            InitializeComponent();

            NombreVista = nameof(VistaMenuVenta);

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
            btnPedidos.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionPedidos", string.Empty); };
            btnVentas.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionVentas", string.Empty); };
            btnEnvios.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionEnvios", string.Empty); };
            btnMaestros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuMaestrosVenta", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnPedidos.Visible)
                btnPedidos.PerformClick();
            else if (btnVentas.Visible)
                btnVentas.PerformClick();
            else if (btnEnvios.Visible)
                btnEnvios.PerformClick();
        }

        public void Mostrar() {
            
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnPedidos.Checked = false;
            btnVentas.Checked = false;
            btnEnvios.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        
    }
}