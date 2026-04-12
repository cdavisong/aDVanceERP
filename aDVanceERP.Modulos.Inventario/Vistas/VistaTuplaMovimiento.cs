using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Properties;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaMovimiento : Form, IVistaTuplaMovimiento {
        private EstadoMovimiento _estadoMovimiento;
        private EfectoMovimientoEnum _efecto;

        public VistaTuplaMovimiento() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaMovimiento);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Name}{Id}";
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

        public Color ColorFondoTupla {
            get => layoutBase.BackColor;
            set => layoutBase.BackColor = value;
        }

        public bool EstadoSeleccion { get; set; }

        public string Id {
            get => fieldId.Text;
            set => fieldId.Text = value;
        }

        public string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public string NombreAlmacenOrigen {
            get => fieldNombreAlmacenOrigen.Text;
            set {
                fieldNombreAlmacenOrigen.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
                fieldNombreAlmacenOrigen.Margin = new Padding(1, value?.Length > 16 ? 10 : 1, 1, 1);
            }
        }

        public string NombreAlmacenDestino {
            get => fieldNombreAlmacenDestino.Text;
            set {
                fieldNombreAlmacenDestino.Text = string.IsNullOrEmpty(value) ? "Ninguno" : value;
                fieldNombreAlmacenDestino.Margin = new Padding(1, value?.Length > 16 ? 10 : 1, 1, 1);
            }
        }

        public string SaldoInicial {
            get => fieldSaldoInicial.Text;
            set => fieldSaldoInicial.Text = value;
        }

        public string CantidadMovida {
            get => fieldCantidadMovida.Text;
            set => fieldCantidadMovida.Text = value;
        }

        public string SaldoFinal {
            get => fieldSaldoFinal.Text;
            set => fieldSaldoFinal.Text = value;
        }

        public string TipoMovimiento {
            get => fieldTipoMovimiento.Text;
            set {
                var (colorFondo, colorFuente) = ObtenerColorEfecto(_efecto);

                fieldTipoMovimiento.DisabledState.BorderColor = colorFondo;
                fieldTipoMovimiento.DisabledState.FillColor = colorFondo;
                fieldTipoMovimiento.DisabledState.ForeColor = colorFuente;
                fieldTipoMovimiento.Text = string.IsNullOrEmpty(value) ? "ERROR" : value;
            }
        }

        public string Fecha {
            get => fieldFecha.Text;
            set => fieldFecha.Text = value;
        }

        public EstadoMovimiento EstadoMovimiento { get; set; }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
    
        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate(object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
            btnEliminar.Click += delegate(object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
        }

        public void ActualizarIconoStock(EfectoMovimientoEnum efecto) {
            _efecto = efecto;
            fieldIcono.BackgroundImage = efecto switch {
                EfectoMovimientoEnum.Carga => Resources.load_cargo_20px,
                EfectoMovimientoEnum.Descarga => Resources.unload_cargo_20px,
                EfectoMovimientoEnum.Transferencia => Resources.transfer_20px,
                _ => fieldIcono.BackgroundImage
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
            ColorFondoTupla = BackColor;
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        private (Color colorFondo, Color colorFuente) ObtenerColorEfecto(EfectoMovimientoEnum efecto) {
            return efecto switch {
                EfectoMovimientoEnum.Carga => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),         // Verde
                EfectoMovimientoEnum.Descarga => (Color.FromArgb(252, 228, 236), Color.FromArgb(198, 40, 40)),      // Rojo
                EfectoMovimientoEnum.Transferencia => (Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0)),  // Ambar
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }
    }
}