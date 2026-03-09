using aDVanceERP.Core.Eventos;
using aDVanceERP.Modulos.Movil.Interfaces;

namespace aDVanceERP.Modulos.Movil.Vistas {
    public partial class VistaMenuMovil : Form, IVistaMenuMovil {
        public VistaMenuMovil() {
            InitializeComponent();

            NombreVista = nameof(VistaMenuMovil);

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

        public bool BtnAdvancePosVisible {
            get => btnAdvancePos.Visible;
            set => btnAdvancePos.Visible = value;
        }

        public bool BtnAdvanceStockVisible {
            get => btnAdvanceStock.Visible;
            set => btnAdvanceStock.Visible = value;
        }
    
        public void Inicializar() {
            // Eventos
            btnAdvancePos.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionPos", string.Empty); };
            btnAdvanceStock.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionStock", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnAdvancePos.Visible)
                btnAdvancePos.PerformClick();
            else if (btnAdvanceStock.Visible)
                btnAdvanceStock.PerformClick();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnAdvancePos.Checked = false;
            btnAdvanceStock.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}