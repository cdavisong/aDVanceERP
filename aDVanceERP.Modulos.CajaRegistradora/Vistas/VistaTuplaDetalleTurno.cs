using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Vistas {
    public partial class VistaTuplaDetalleTurno : Form, IVistaTuplaDetalleTurno {
        private TipoMovimientoCajaEnum _tipo;
        private CanalPagoCajaEnum _canalPago;
        private decimal _monto;

        public VistaTuplaDetalleTurno() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaDetalleTurno);

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

        public long Id { get; set; }

        public string FechaMovimiento {
            get => fieldFechaHora.Text;
            set => fieldFechaHora.Text = value;
        }

        public TipoMovimientoCajaEnum Tipo {
            get => _tipo;
            set {
                _tipo = value;

                var (colorFondo, colorFuente) = ObtenerColorTipo(value);

                fieldTipo.DisabledState.BorderColor = colorFondo;
                fieldTipo.DisabledState.FillColor = colorFondo;
                fieldTipo.DisabledState.ForeColor = colorFuente;
                fieldTipo.Text = $"● {value.ObtenerDisplayName()}";
            }
        }

        public CanalPagoCajaEnum CanalPago {
            get => _canalPago;
            set {
                _canalPago = value;

                var (colorFondo, colorFuente) = ObtenerColorCanal(value);

                fieldCanal.DisabledState.BorderColor = colorFondo;
                fieldCanal.DisabledState.FillColor = colorFondo;
                fieldCanal.DisabledState.ForeColor = colorFuente;
                fieldCanal.Text = $"● {value.ObtenerDisplayName()}";
            }
        }

        public string? DescripcionFactura {
            get => fieldDescripcionFactura.Text;
            set => fieldDescripcionFactura.Text = value;
        }

        public string Operador {
            get => fieldOperador.Text;
            set => fieldOperador.Text = value;
        }
        public decimal Monto {
            get => _monto;
            set {
                _monto = value;
                fieldMonto.Text = value > 0
                    ? $"+ $ {value:N2}"
                    : value < 0
                        ? $"- $ {(value * -1):N2}"
                        : $"$ {value:N2}";
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos

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

        private (Color colorFondo, Color colorFuente) ObtenerColorTipo(TipoMovimientoCajaEnum estado) {
            return estado switch {
                TipoMovimientoCajaEnum.Venta => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 196)),          // Azul
                TipoMovimientoCajaEnum.DevolucionVenta => (Color.FromArgb(243, 229, 245), Color.FromArgb(106, 27, 154)),// Magenta
                TipoMovimientoCajaEnum.EntradaManual => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),   // Verde
                TipoMovimientoCajaEnum.SalidaManual => (Color.FromArgb(252, 228, 236), Color.FromArgb(198, 40, 40)),    // Rojo
                TipoMovimientoCajaEnum.AjusteArqueo => (Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0)),     // Ambar
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }

        private (Color colorFondo, Color colorFuente) ObtenerColorCanal(CanalPagoCajaEnum estado) {
            return estado switch {
                CanalPagoCajaEnum.Efectivo => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),             // Verde
                CanalPagoCajaEnum.Transferencia => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 196)),       // Azul
                CanalPagoCajaEnum.Mixto => (Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0)),                 // Ambar
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }
    }
}