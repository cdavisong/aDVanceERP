using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
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
                btnConfirmar.Enabled = value == EstadoEntregaEnum.PendienteAsignacion;
                btnCambiarEstado.Enabled = value != EstadoEntregaEnum.PendienteAsignacion && value != EstadoEntregaEnum.Cancelado && value != EstadoEntregaEnum.Completado;
                btnCancelar.Enabled = value != EstadoEntregaEnum.Cancelado && value != EstadoEntregaEnum.PagoRecibido && value != EstadoEntregaEnum.Completado;
                ColorFondoTupla = ObtenerColorFondoTupla(_estadoEntrega);
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnConfirmar.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Asignado;
                RepoSeguimientoEntrega.Instancia.CambiarEstadoEntrega(Id, EstadoEntregaEnum.Asignado);                
            };
            btnCambiarEstado.Click += delegate {
                if (TipoEnvio == TipoEnvioEnum.MensajeriaConFondo) {
                    btnCambiarEstado.ContextMenuStrip?.Items.Remove(btnEstadoPagoRecibido);
                }

                btnCambiarEstado.ContextMenuStrip?.Show(btnCambiarEstado, new Point(0, 40)); 
            };
            btnEstadoEnRuta.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.EnRuta;
                RepoSeguimientoEntrega.Instancia.CambiarEstadoEntrega(Id, EstadoEntregaEnum.EnRuta);
            };
            btnEstadoEntregado.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Entregado;
                RepoSeguimientoEntrega.Instancia.CambiarEstadoEntrega(Id, EstadoEntregaEnum.Entregado);
            };
            btnEstadoPagoRecibido.Click += delegate (object? sender, EventArgs e) {
                if (CentroNotificaciones.MostrarMensaje(
                    "Está seguro de confirmar los pagos recibidos por la venta?",
                    Core.Modelos.Comun.TipoMensaje.Info,
                    Core.Modelos.Comun.BotonesMensaje.SiNo) == DialogResult.Yes) {
                    EstadoEntrega = EstadoEntregaEnum.PagoRecibido;
                    RepoSeguimientoEntrega.Instancia.CambiarEstadoEntrega(Id, EstadoEntregaEnum.PagoRecibido);

                    // Confirmar pagos pendientes de la venta
                    var venta = RepoVenta.Instancia.ObtenerPorId(IdVenta);
                    var pagosVenta = RepoPago.Instancia.Buscar(FiltroBusquedaPago.IdVenta, venta?.Id.ToString() ?? "0").resultadosBusqueda.Select(r => r.entidadBase).ToList();

                    if (pagosVenta.Count == 0) {
                        // Registrar nuevo pago
                        var pago = new Pago() {
                            Id = 0,
                            IdVenta = venta?.Id ?? 0,
                            MetodoPago = MetodoPagoEnum.Efectivo,
                            MontoPagado = venta?.ImporteTotal ?? 0,
                            FechaPagoCliente = DateTime.Now,
                            FechaConfirmacionPago = DateTime.Now,
                            EstadoPago = TipoEnvio == TipoEnvioEnum.MensajeriaConFondo
                                ? EstadoPagoEnum.Confirmado
                                : EstadoPagoEnum.Pendiente
                        };

                        RepoPago.Instancia.Adicionar(pago);
                    } else {
                        foreach (var pago in pagosVenta) {
                            if (pago.EstadoPago == EstadoPagoEnum.Pendiente)
                                RepoPago.Instancia.CambiarEstadoPago(pago.Id, EstadoPagoEnum.Confirmado);
                        }
                    }
                }
            };
            btnEstadoCompletado.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Completado;
                RepoSeguimientoEntrega.Instancia.CambiarEstadoEntrega(Id, EstadoEntregaEnum.Completado);
            };
            btnEstadoFallido.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Fallido;
                RepoSeguimientoEntrega.Instancia.CambiarEstadoEntrega(Id, EstadoEntregaEnum.Fallido);
            };
            btnCancelar.Click += delegate (object? sender, EventArgs e) {
                EstadoEntrega = EstadoEntregaEnum.Cancelado;
                RepoSeguimientoEntrega.Instancia.CambiarEstadoEntrega(Id, EstadoEntregaEnum.Cancelado);
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