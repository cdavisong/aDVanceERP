using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaPago : Form, IVistaTuplaPago {
        private EstadoPagoEnum _estadoPago;

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
            set => layoutVista.BackColor = layoutVista.BackColor = value == Color.Gainsboro
                ? value
                : ObtenerColorFondoTupla(EstadoPago);
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

        public MetodoPagoEnum MetodoPago {
            get => (MetodoPagoEnum)Enum.Parse(typeof(MetodoPagoEnum), fieldMetodoPago.Text);
            set => fieldMetodoPago.Text = value.ObtenerDisplayName();
        }

        public string NumeroConfirmacion { 
            get => fieldNumeroConfirmacion.Text;
            set => fieldNumeroConfirmacion.Text = string.IsNullOrEmpty(value)
                ? "-"
                : value;
        }

        public string NumeroTransaccion {
            get => fieldNumeroTransferencia.Text;
            set => fieldNumeroTransferencia.Text = string.IsNullOrEmpty(value)
                ? "-"
                : value;
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
                fieldEstado.Text = value.ObtenerDisplayName();
                btnConfirmar.Enabled = value == EstadoPagoEnum.Pendiente;
                btnCancelar.Enabled = value == EstadoPagoEnum.Pendiente || value == EstadoPagoEnum.Confirmado;
                ColorFondoTupla = ObtenerColorFondoTupla(_estadoPago);
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
                    repoVenta.CambiarEstadoVenta(IdVenta, EstadoVenta.Completada);
                repoVenta.ActualizarMetodoPagoPrincipal(IdVenta);

                FechaConfirmacionPago = DateTime.Now;
                EstadoPago = EstadoPagoEnum.Confirmado;
            };
            btnCancelar.Click += delegate (object? sender, EventArgs e) {
                var repoVenta = RepoVenta.Instancia;

                RepoPago.Instancia.CambiarEstadoPago(Id, EstadoPagoEnum.Anulado);
                
                repoVenta.CambiarEstadoVenta(IdVenta, EstadoVenta.Pendiente);
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

        private Color ObtenerColorFondoTupla(EstadoPagoEnum estado) {
            return estado switch {
                EstadoPagoEnum.Pendiente => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoPagoEnum.Fallido => ContextoAplicacion.ColorErrorTupla,
                EstadoPagoEnum.Anulado => ContextoAplicacion.ColorErrorTupla,
                _ => BackColor
            };
        }
    }
}