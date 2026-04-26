using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaPago : Form, IVistaTuplaPago {
        private EstadoPagoEnum _estadoPago;
        private CanalPagoEnum? _canalPago;

        public VistaTuplaPago() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaPago);

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
            get => layoutVista.BackColor;
            set => layoutVista.BackColor = value;
        }

        public bool EstadoSeleccion { get; set; }

        public long Id {
            get => Convert.ToInt64(fieldId.Text);
            set => fieldId.Text = value.ToString();
        }

        public long IdVenta { get; set; }

        public string NumeroFacturaVenta {
            get => fieldNumeroFactura.Text;
            set => fieldNumeroFactura.Text = value;
        }

        public CanalPagoEnum? CanalPago {
            get => _canalPago;
            set {
                _canalPago = value;

                var (colorFondo, colorFuente) = ObtenerColorCanal(value ?? CanalPagoEnum.NA);

                fieldCanalPago.Text = value.ObtenerNombreDescripcion().Nombre;
                fieldCanalPago.DisabledState.BorderColor = colorFondo;
                fieldCanalPago.DisabledState.FillColor = colorFondo;
                fieldCanalPago.DisabledState.ForeColor = colorFuente;
            }
        }

        public string NumeroTelefonoRemitente { 
            get => fieldNumeroTelefonoRemitente.Text;
            set => fieldNumeroTelefonoRemitente.Text = string.IsNullOrEmpty(value)
                ? "-"
                : value;
        }

        public string NumeroTransaccion {
            get => fieldNumeroTransferencia.Text;
            set {
                fieldNumeroTransferencia.Text = string.IsNullOrEmpty(value)
                ? "-"
                : value;
                fieldNumeroTransferencia.Margin = fieldNumeroTransferencia.AjusteAutomaticoMargenTexto();
            }
        }

        public decimal MontoPagado {
            get => decimal.TryParse(fieldMonto.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                    out var value)
                                    ? value
                                    : 0m;
            set => fieldMonto.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public DateTime FechaPagoCliente {
            get => fieldFechaPago.Text.Equals("-")
                            ? DateTime.MinValue
                            : DateTime.ParseExact(fieldFechaPago.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaPago.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public DateTime FechaConfirmacionPago {
            get => fieldFechaConfirmacion.Text.Equals("-")
                            ? DateTime.MinValue
                            : DateTime.ParseExact(fieldFechaConfirmacion.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaConfirmacion.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public EstadoPagoEnum EstadoPago {
            get => _estadoPago;
            set {
                _estadoPago = value;

                var (colorFondo, colorFuente) = ObtenerColorEstado(value);

                fieldEstado.Text = value.ObtenerNombreDescripcion().Nombre;
                fieldEstado.DisabledState.BorderColor = colorFondo;
                fieldEstado.DisabledState.FillColor = colorFondo;
                fieldEstado.DisabledState.ForeColor = colorFuente;

                btnConfirmar.Enabled = value != EstadoPagoEnum.Anulado && value != EstadoPagoEnum.Confirmado;
                btnCancelar.Enabled = value == EstadoPagoEnum.Pendiente || value == EstadoPagoEnum.Confirmado;
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnConfirmar.Click += delegate (object? sender, EventArgs e) {
                var repoVenta = RepoVenta.Instancia;
                
                RepoPago.Instancia.CambiarEstadoPago(Id, EstadoPagoEnum.Confirmado);

                if (repoVenta.VentaEstaPagadaCompletamente(IdVenta))
                    repoVenta.CambiarEstadoVenta(IdVenta, EstadoVentaEnum.Completada);
                repoVenta.ActualizarMetodoPagoPrincipal(IdVenta);

                FechaConfirmacionPago = DateTime.Now;
                EstadoPago = EstadoPagoEnum.Confirmado;
            };
            btnCancelar.Click += delegate (object? sender, EventArgs e) {
                var repoVenta = RepoVenta.Instancia;

                RepoPago.Instancia.CambiarEstadoPago(Id, EstadoPagoEnum.Anulado);
                
                repoVenta.CambiarEstadoVenta(IdVenta, EstadoVentaEnum.Pendiente);
                repoVenta.ActualizarMetodoPagoPrincipal(IdVenta);

                FechaConfirmacionPago = DateTime.MinValue;
                EstadoPago = EstadoPagoEnum.Anulado;
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

        private (Color colorFondo, Color colorFuente) ObtenerColorCanal(CanalPagoEnum estado) {
            return estado switch {
                CanalPagoEnum.Efectivo => (Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0)),          // Ambar
                CanalPagoEnum.TransferenciaBancaria => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 196)),   // Azul
                CanalPagoEnum.Mixto => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),            // Verde
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }

        private (Color colorFondo, Color colorFuente) ObtenerColorEstado(EstadoPagoEnum estado) {
            return estado switch {
                EstadoPagoEnum.Pendiente => (Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0)),           // Ambar
                EstadoPagoEnum.Confirmado => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),         // Verde
                EstadoPagoEnum.Anulado => (Color.FromArgb(252, 228, 236), Color.FromArgb(198, 40, 40)),            // Rojo
                EstadoPagoEnum.Fallido => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 196)),           // Azul
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }
    }
}