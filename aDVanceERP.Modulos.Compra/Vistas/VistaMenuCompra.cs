using aDVanceERP.Core.Eventos;
using aDVanceERP.Modulos.Compra.Interfaces;

namespace aDVanceERP.Modulos.Compra.Vistas {
    public partial class VistaMenuCompra : Form, IVistaMenuCompra {
        public VistaMenuCompra() {
            InitializeComponent();

            NombreVista = nameof(VistaMenuCompra);

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
            btnSolicitudes.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionSolicitudes", string.Empty); };
            btnCompras.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionCompras", string.Empty); };
            btnPagos.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionPagosCompra", string.Empty); };
            btnMaestros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuMaestrosCompra", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnSolicitudes.Visible)
                btnSolicitudes.PerformClick();
            else if (btnCompras.Visible)
                btnCompras.PerformClick();
            else if (btnPagos.Visible)
                btnPagos.PerformClick();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnSolicitudes.Checked = false;
            btnCompras.Checked = false;
            btnPagos.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}