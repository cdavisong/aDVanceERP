using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaEnvio : Form, IVistaTuplaEnvio {
        private EstadoEntregaEnum _estadoEntrega;

        public VistaTuplaEnvio() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaEnvio);

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
                : ObtenerColorFondoTupla(EstadoEntrega);
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

        public long? IdMensajero { get; set; }

        public string? NombreMensajero {
            get => fieldNombreMensajero.Text;
            set {
                fieldNombreMensajero.Text = value;
                fieldNombreMensajero.Margin = fieldObservaciones.AjusteAutomaticoMargenTexto();
            }
        }

        public TipoEnvioEnum TipoEnvio {
            get => (TipoEnvioEnum)Enum.Parse(typeof(TipoEnvioEnum), fieldTipoEnvio.Text);
            set => fieldTipoEnvio.Text = value.ObtenerDisplayName();
        }

        public DateTime FechaAsignacion {
            get => fieldFechaAsignacion.Text.Equals("-")
                                ? DateTime.MinValue
                                : DateTime.ParseExact(fieldFechaAsignacion.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaAsignacion.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public DateTime FechaEntregaRealizada {
            get => fieldFechaEntrega.Text.Equals("-")
                                ? DateTime.MinValue
                                : DateTime.ParseExact(fieldFechaEntrega.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaEntrega.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string? ObservacionesEntrega {
            get => fieldObservaciones.Text;
            set { 
                fieldObservaciones.Text = value;
                fieldObservaciones.Margin = fieldObservaciones.AjusteAutomaticoMargenTexto();
            }
        }

        public decimal MontoCobradoAlCliente {
            get => decimal.TryParse(fieldMonto.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                    out var value)
                                    ? value
                                    : 0m;
            set => fieldMonto.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public EstadoEntregaEnum EstadoEntrega {
            get => _estadoEntrega;
            set {
                _estadoEntrega = value;
                fieldEstado.Text = value.ObtenerDisplayName();
                btnConfirmar.Enabled = value == EstadoEntregaEnum.PendienteAsignacion;
                btnCancelar.Enabled = value != EstadoEntregaEnum.Cancelado && value != EstadoEntregaEnum.PagoRecibido;
                ColorFondoTupla = ObtenerColorFondoTupla(_estadoEntrega);
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnConfirmar.Click += delegate (object? sender, EventArgs e) {
                
            };
            btnCancelar.Click += delegate (object? sender, EventArgs e) {
                
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

        private Color ObtenerColorFondoTupla(EstadoEntregaEnum estado) {
            return estado switch {
                EstadoEntregaEnum.PendienteAsignacion => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoEntregaEnum.Fallido => ContextoAplicacion.ColorErrorTupla,
                EstadoEntregaEnum.Cancelado => ContextoAplicacion.ColorErrorTupla,
                _ => BackColor
            };
        }
    }
}