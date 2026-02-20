using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaEnvio : Form, IVistaTuplaEnvio {
        private TipoEnvioEnum _tipoEnvio;
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
                fieldNombreMensajero.Margin = fieldNombreMensajero.AjusteAutomaticoMargenTexto();
            }
        }

        public TipoEnvioEnum TipoEnvio {
            get => _tipoEnvio;
            set {
                _tipoEnvio = value;
                fieldTipoEnvio.Text = value.ObtenerDisplayName();
            }
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
                fieldEstado.Font = value == EstadoEntregaEnum.Completado || value == EstadoEntregaEnum.Cancelado ? new Font(new FontFamily("Segoe UI"), 11.25f, FontStyle.Regular) : new Font(new FontFamily("Segoe UI"), 11.25f, FontStyle.Underline);
                fieldEstado.ForeColor = value == EstadoEntregaEnum.Completado || value == EstadoEntregaEnum.Cancelado ? Color.DimGray : Color.DodgerBlue;
                fieldEstado.Cursor = value == EstadoEntregaEnum.Completado || value == EstadoEntregaEnum.Cancelado ? Cursors.Default : Cursors.Hand;
                btnCancelar.Enabled = value != EstadoEntregaEnum.Cancelado && value != EstadoEntregaEnum.PagoRecibido && value != EstadoEntregaEnum.Completado;
                ColorFondoTupla = ObtenerColorFondoTupla(_estadoEntrega);
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        public event EventHandler<(long idEnvio, long idVenta, EstadoEntregaEnum estado)> CambioEstadoEnvio;

        public void Inicializar() {
            // Eventos
            fieldEstado.Click += delegate {
                if (EstadoEntrega == EstadoEntregaEnum.Completado || EstadoEntrega == EstadoEntregaEnum.Cancelado)
                    return;

                btnAsignado.Visible = EstadoEntrega == EstadoEntregaEnum.Fallido;
                btnEstadoEnRuta.Visible = EstadoEntrega == EstadoEntregaEnum.Asignado;
                btnEstadoEntregado.Visible = EstadoEntrega == EstadoEntregaEnum.EnRuta;
                btnEstadoPagoRecibido.Visible = TipoEnvio != TipoEnvioEnum.MensajeriaConFondo && (EstadoEntrega == EstadoEntregaEnum.Entregado || EstadoEntrega == EstadoEntregaEnum.EnEspera);
                btnEstadoCompletado.Visible = (TipoEnvio == TipoEnvioEnum.MensajeriaConFondo && EstadoEntrega == EstadoEntregaEnum.Entregado) || EstadoEntrega == EstadoEntregaEnum.PagoRecibido;
                btnEstadoFallido.Visible = EstadoEntrega == EstadoEntregaEnum.EnRuta;
                fieldEstado.ContextMenuStrip?.Show(fieldEstado, new Point(0, 40));
            };
            btnAsignado.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Asignado;
                CambioEstadoEnvio?.Invoke(this, (Id, IdVenta, EstadoEntrega));
            };
            btnEstadoEnRuta.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.EnRuta;
                CambioEstadoEnvio?.Invoke(this, (Id, IdVenta, EstadoEntrega));
            };
            btnEstadoEntregado.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Entregado;
                CambioEstadoEnvio?.Invoke(this, (Id, IdVenta, EstadoEntrega));
            };
            btnEstadoPagoRecibido.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.PagoRecibido;
                CambioEstadoEnvio?.Invoke(this, (Id, IdVenta, EstadoEntrega));
            };
            btnEstadoCompletado.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Completado;
                CambioEstadoEnvio?.Invoke(this, (Id, IdVenta, EstadoEntrega));
            };
            btnEstadoFallido.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Fallido;
                CambioEstadoEnvio?.Invoke(this, (Id, IdVenta, EstadoEntrega));
            };
            btnCancelar.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Cancelado;
                CambioEstadoEnvio?.Invoke(this, (Id, IdVenta, EstadoEntrega));
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
                EstadoEntregaEnum.Asignado => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoEntregaEnum.EnRuta => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoEntregaEnum.Entregado => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoEntregaEnum.PagoRecibido => ContextoAplicacion.ColorOkTupla,
                EstadoEntregaEnum.Fallido => ContextoAplicacion.ColorErrorTupla,
                EstadoEntregaEnum.Cancelado => ContextoAplicacion.ColorErrorTupla,
                _ => BackColor
            };
        }
    }
}