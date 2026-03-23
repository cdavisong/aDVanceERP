using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaAperturaTurno : Form, IVistaAperturaTurno {
        private bool _modoEdicion = false;

        public VistaAperturaTurno() {
            InitializeComponent();

            NombreVista = nameof(VistaAperturaTurno);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "" : "Registro de fondo inicial de caja";
                btnRegistrarActualizar.Text = value ? "" : "Abrir turno";
            }
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
        public long IdAlmacen { get; set; }

        public string NombreAlmacen { 
            get => fieldAlmacen.Text;
            set => fieldAlmacen.Text = value;
        }

        public decimal MontoApertura {
            get => decimal.TryParse(fieldMontoEfectivo.Text, out var monto) ? monto : 0m;
            set => fieldMontoEfectivo.Text = value.ToString("N2");
        }

        public string? Observaciones {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        public void Mostrar() {
            fieldOperador.Text = ContextoSeguridad.UsuarioAutenticado?.Nombre;

            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            IdAlmacen = 0L;
            NombreAlmacen = string.Empty;
            fieldMontoEfectivo.Text = string.Empty;
            Observaciones = string.Empty;
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
