using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Repositorios.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Vistas {
    public partial class VistaContenedorModulos : Form, IVistaContenedorModulos {
        public VistaContenedorModulos() {
            InitializeComponent();

            NombreVista = nameof(VistaContenedorModulos);
            PanelCentral = new RepoVistaBase(panelCentral);

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

        public FlowLayoutPanel PanelMenuLateral => layoutModulos;

        public RepoVistaBase PanelCentral { get; private set; }

        public Label NombreModulo => fieldNombreModulo;

        public string MensajePortada {
            get => fieldTextoBienvenida.Text;
            set {
                fieldTextoBienvenida.Text = value;
                fieldTextoBienvenida.Visible = true;
            }
        }

        public void Inicializar() {
            // Estado inicial
            btnInicio.PerformClick();

            // Eventos
            btnInicio.Click += delegate { PanelCentral.OcultarTodos(); };
            btnInicio.Click += (s, e) => {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);                
            };
            btnInicio.MouseEnter += delegate {
                NombreModulo.Text = "Inicio";
                NombreModulo.Location = new Point(5, btnInicio.Top + 12);
                NombreModulo.BringToFront();
                NombreModulo.Show();
            };
            btnInicio.MouseLeave += delegate {
                NombreModulo.Hide();
            };
            btnGestorModulos.Click += (s, e) => {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaContenedorExtensiones", string.Empty);
            };

            AgregadorEventos.Suscribir("EventoCambioModulo", OnEventoCambioModulo);
        }

        private void OnEventoCambioModulo(string obj) {
            Restaurar();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            PanelCentral.OcultarTodos();

            btnGestorModulos.Checked = false;
            btnConfiguracionGeneral.Checked = false;
        }

        public void Ocultar() {
            btnInicio.Checked = true;
            btnGestorModulos.Checked = false;
            btnConfiguracionGeneral.Checked = false;

            Hide();
        }

        public void Cerrar() {
            AgregadorEventos.Desuscribir("EventoCambioModulo", OnEventoCambioModulo);

            PanelCentral?.CerrarTodos();
        }
    }
}