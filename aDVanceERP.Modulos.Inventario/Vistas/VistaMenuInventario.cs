using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaMenuInventario : Form, IVistaMenuInventario {
        public VistaMenuInventario() {
            InitializeComponent();

            NombreVista = nameof(VistaMenuInventario);

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
            btnEstadisticas.Click += delegate { 
                AgregadorEventos.Publicar(new EventoMostrarVistaEstadisticasInventario()); 
            };
            btnMovimientos.Click += delegate { 
                AgregadorEventos.Publicar(new EventoMostrarVistaGestionMovimientos()); 
            };
            btnMaestros.Click += delegate { 
                AgregadorEventos.Publicar(new EventoMostrarVistaMenuMaestrosInventario()); 
            };
        }

        public void SeleccionarVistaInicial() {
            if (btnEstadisticas.Visible)
                btnEstadisticas.PerformClick();
            else if (btnMovimientos.Visible)
                btnMovimientos.PerformClick();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnMovimientos.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}