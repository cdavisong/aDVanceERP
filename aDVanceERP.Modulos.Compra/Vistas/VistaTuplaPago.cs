using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;
using System.Globalization;

namespace aDVanceERP.Modulos.Compra.Vistas {
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

        public long IdCompra { get; set; }

        public string NumeroSolicitudCompra {
            get => fieldNumeroFactura.Text;
            set => fieldNumeroFactura.Text = value;
        }

        public MetodoPagoEnum MetodoPago {
            get => (MetodoPagoEnum)Enum.Parse(typeof(MetodoPagoEnum), fieldMetodoPago.Text);
            set => fieldMetodoPago.Text = value.ObtenerNombreDescripcion();
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

        public DateTime FechaPagoProveedor {
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
                fieldEstado.Text = value.ObtenerNombreDescripcion();
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
                var repoCompra = RepoCompra.Instancia;
                
                RepoPago.Instancia.CambiarEstadoPago(Id, EstadoPagoEnum.Confirmado);

                //if (repoCompra.CompraEstaPagadaCompletamente(IdCompra))
                //    repoCompra.CambiarEstadoCompra(IdCompra, EstadoCompraEnum.Completada);
                //repoCompra.ActualizarMetodoPagoPrincipal(IdCompra);

                FechaConfirmacionPago = DateTime.Now;
                EstadoPago = EstadoPagoEnum.Confirmado;
            };
            btnCancelar.Click += delegate (object? sender, EventArgs e) {
                var repoCompra = RepoCompra.Instancia;

                RepoPago.Instancia.CambiarEstadoPago(Id, EstadoPagoEnum.Anulado);
                
                //repoCompra.CambiarEstadoCompra(IdCompra, EstadoCompraEnum.Pendiente);
                //repoCompra.ActualizarMetodoPagoPrincipal(IdCompra);

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