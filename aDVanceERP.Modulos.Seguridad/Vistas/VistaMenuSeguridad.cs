using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas {
    public partial class VistaMenuSeguridad : Form, IVistaMenuSeguridad {
        public VistaMenuSeguridad() {
            InitializeComponent();

            NombreVista = nameof(VistaMenuSeguridad);

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
            btnUsuarios.Click += delegate { 
                AgregadorEventos.Publicar(new EventoMostrarVistaGestionCuentasUsuarios()); 
            };
        }

        public void SeleccionarVistaInicial() {
            if (btnUsuarios.Visible)
                btnUsuarios.PerformClick();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            btnUsuarios.Checked = false;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }
    }
}