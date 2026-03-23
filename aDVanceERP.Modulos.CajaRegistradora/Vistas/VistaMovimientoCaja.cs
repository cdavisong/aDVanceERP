using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaMovimientoCaja : Form, IVistaMovimientoCaja {
        private bool _modoEdicion = false;
        private string _codigo = string.Empty;
        private string _nombreAlmacen = string.Empty;

        public VistaMovimientoCaja() {
            InitializeComponent();

            NombreVista = nameof(VistaMovimientoCaja);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "" : $"Turno _ · _";
                btnRegistrarActualizar.Text = value ? "" : "Registrar movimiento";
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

        public string Codigo {
            get => _codigo;
            set {
                _codigo = value;
                fieldSubtitulo.Text = $"Turno {_codigo} · {_nombreAlmacen}";
            }
        }

        public long IdAlmacen { get; set; }

        public string NombreAlmacen {
            get => _nombreAlmacen;
            set {
                _nombreAlmacen = value;
                fieldSubtitulo.Text = $"Turno {_codigo} · {_nombreAlmacen}";
            }
        }

        public TipoMovimientoCajaEnum Tipo { get; set; }

        public CanalPagoCajaEnum CanalPago { get; set; }

        public decimal Monto {
            get => decimal.TryParse(fieldMonto.Text, out var monto) ? monto : 0m;
            set => fieldMonto.Text = value.ToString("N2");
        }

        public string? Descripcion {
            get => fieldDescripcion.Text;
            set => fieldDescripcion.Text = value;
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldTipoMovimiento.CheckedChanged += delegate {
                fieldTipoMovimiento.Text = fieldTipoMovimiento.Checked
                    ? "↓  Entrada manual"
                    : "↑  Salida manual";
                fieldTipoMovimiento.FillColor = fieldTipoMovimiento.Checked
                    ? Color.FromArgb(232, 245, 233)
                    : Color.FromArgb(252, 228, 236);
                fieldTipoMovimiento.BorderColor = fieldTipoMovimiento.Checked
                    ? Color.FromArgb(129, 199, 132)
                    : Color.FromArgb(239, 154, 154);
                fieldTipoMovimiento.ForeColor = fieldTipoMovimiento.Checked
                    ? Color.FromArgb(46, 125, 50)
                    : Color.FromArgb(198, 40, 40);
                Tipo = fieldTipoMovimiento.Checked
                    ? TipoMovimientoCajaEnum.EntradaManual
                    : TipoMovimientoCajaEnum.SalidaManual;
            };
            fieldCanal.SelectedIndexChanged += delegate {
                CanalPago = (CanalPagoCajaEnum) fieldCanal.SelectedIndex;
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            IdAlmacen = 0L;
            NombreAlmacen = string.Empty;
            fieldTipoMovimiento.Checked = true;
        }

        public void Cerrar() {
            Dispose();
        }
    }
}
