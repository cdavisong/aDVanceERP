using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Temas;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using System.Drawing.Drawing2D;

namespace aDVanceERP.Desktop.Vistas {
    public partial class VistaContenedorSeguridad : Form, IVistaContenedorSeguridad {
        public VistaContenedorSeguridad() {
            InitializeComponent();

            PanelCentral = new RepoVistaBase(contenedorVistas);
            NombreVista = nameof(VistaContenedorSeguridad);

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

        public int TuplasMaximasContenedor {
            get => contenedorVistas.Height / ContextoAplicacion.AlturaTuplaPredeterminada;
        }

        public RepoVistaBase PanelCentral { get; private set; }

        public void Inicializar() {
            // Eventos 
            layoutDistribucion.Paint += (s, e) => FondosAplicacion.DibujarOndasSuaves(e.Graphics, layoutDistribucion.ClientRectangle);
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
            PanelCentral?.CerrarTodos();
        }
    }
}