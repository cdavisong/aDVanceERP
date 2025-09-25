
using aDVanceERP.Core.Properties;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Vistas.Comun {
    public partial class VistaCargaDatos : Form, IVistaBase {
        private string _textoProgreso = "Filtrando resultados de búsqueda...";
        private const string _icono = "pX_48px";
        private int _iconoActual = 1;
        private System.Windows.Forms.Timer _timerIconoCarga = new System.Windows.Forms.Timer();

        public VistaCargaDatos() {
            InitializeComponent();

            NombreVista = nameof(VistaCargaDatos);
            DoubleBuffered = true;

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

        public string TextoProgreso {
            get => _textoProgreso;
            set {
                _textoProgreso = value;

                ActualizarTextoProgreso();
            }
        }

        public void Inicializar() {
            _timerIconoCarga.Interval = 42;
            _timerIconoCarga.Tick += Actualizar;
        }

        public void Mostrar() {
            if (InvokeRequired) {
                Invoke(new Action(Mostrar));
                return;
            }

            Show();
            BringToFront();

            TopMost = true; // Asegurar que esté siempre visible

            _timerIconoCarga.Start();

            // Ejecutar eventos pendientes
            Application.DoEvents();
        }

        public void Ocultar() {
            if (InvokeRequired) {
                Invoke(new Action(Ocultar));
                return;
            }

            _timerIconoCarga.Stop();

            Hide();

            TopMost = false;
        }

        public void Restaurar() {
            _timerIconoCarga.Stop();
        }

        public void Cerrar() {
            _timerIconoCarga.Stop();
            _timerIconoCarga.Tick -= Actualizar;
            Close();
        }

        private void Actualizar(object? sender, EventArgs e) {
            ActualizarIconoCarga();
            ActualizarTextoProgreso();
        }

        private void ActualizarIconoCarga() {
            if (fieldIconoCarga != null && fieldIconoCarga.InvokeRequired) {
                fieldIconoCarga.Invoke(new Action(ActualizarIconoCarga));
                return;
            }

            _iconoActual = _iconoActual == 8 ? 1 : ++_iconoActual;

            var numeroImagen = _iconoActual;
            var nombreImagen = _icono.Replace("X", numeroImagen.ToString());
            var imagen = (Image)Resources.ResourceManager.GetObject(nombreImagen);

            fieldIconoCarga.BackgroundImage = imagen;
        }

        private void ActualizarTextoProgreso() {
            if (fieldTextoCarga != null && fieldTextoCarga.InvokeRequired) {
                fieldTextoCarga.Invoke(new Action(ActualizarTextoProgreso));
                return;
            }

            fieldTextoCarga.Text = _textoProgreso;
        }
    }
}
