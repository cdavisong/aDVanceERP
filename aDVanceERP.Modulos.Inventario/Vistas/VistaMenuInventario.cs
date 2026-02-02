using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;

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
            btnMovimientos.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionMovimientos", string.Empty); };
            btnMaestros.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuMaestrosInventario", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnMovimientos.Visible)
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