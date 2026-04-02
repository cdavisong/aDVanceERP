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
            btnUnidadesMedida.Click += delegate { AgregadorEventos.Publicar("MostrarVistaGestionUnidadesMedida", string.Empty); };
            btnAtras.Click += delegate { AgregadorEventos.Publicar("MostrarVistaMenuInventario", string.Empty); };
        }

        public void SeleccionarVistaInicial() {
            if (btnAlmacenes.Visible)
                btnAlmacenes.PerformClick();
            else if (btnProductos.Visible)
                btnProductos.PerformClick();
            else if (btnTiposClasificacionesProducto.Visible)
                btnTiposClasificacionesProducto.PerformClick();
            else if (btnUnidadesMedida.Visible)
                btnUnidadesMedida.PerformClick();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnAlmacenes.Checked = false;
            btnProductos.Checked = false;
            btnTiposClasificacionesProducto.Checked = false;
            btnUnidadesMedida.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}