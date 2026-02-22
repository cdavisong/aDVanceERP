using aDVanceERP.Core.Eventos;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaMenuMaestros : Form, IVistaMenuMaestros {
        public VistaMenuMaestros() {
            InitializeComponent();

            NombreVista = $"{nameof(VistaMenuMaestros)}Inventario";

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
            btnProductos.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionProductos", string.Empty); };
            btnAlmacenes.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionAlmacenes", string.Empty); };
            btnTiposClasificacionesProducto.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionClasificaciones", string.Empty); };
            btnAtras.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuInventario", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnProductos.Visible)
                btnProductos.PerformClick();
            else if (btnAlmacenes.Visible)
                btnAlmacenes.PerformClick();
            else if (btnTiposClasificacionesProducto.Visible)
                btnTiposClasificacionesProducto.PerformClick();
        }

        public void Mostrar() {
            
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnProductos.Checked = false;
            btnAlmacenes.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        
    }
}