using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Compra.Vistas {
    public partial class VistaTuplaCompra : Form, IVistaTuplaCompra {
        private EstadoCompraEnum _estadoCompra;

        public VistaTuplaCompra() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaCompra);

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
            set => layoutVista.BackColor = value == Color.Gainsboro
                ? value
                : ObtenerColorFondoTupla(EstadoCompra);
        }

        public bool EstadoSeleccion { get; set; }

        public long Id { get; set; }

        public string Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public long IdProveedor { get; set; }

        public long? IdSolicitudCompra { get; set; }

        public long? IdEmpleadoComprador { get; set; }

        public long IdAlmacenDestino { get; set; }

        public long? IdTipoCompra { get; set; }

        public DateTime FechaOrden {
            get => fieldFechaCompra.Text.Equals("-")
                        ? DateTime.MinValue
                        : DateTime.ParseExact(fieldFechaCompra.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaCompra.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public DateTime FechaEntregaEsperada { get; set; }

        public string CondicionesPago {
            get => fieldCondicionPago.Text;
            set => fieldCondicionPago.Text = value;
        }

        public decimal Subtotal {
            get => decimal.TryParse(fieldSubtotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                           out var value)
                           ? value
                           : 0m;
            set => fieldSubtotal.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal ImpuestoTotal {
            get => decimal.TryParse(fieldImpuestoTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                   out var value)
                                   ? value
                                   : 0m;
            set => fieldImpuestoTotal.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal TotalCompra {
            get => decimal.TryParse(fieldImporteTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                    out var value)
                                    ? value
                                    : 0m;
            set => fieldImporteTotal.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public EstadoCompraEnum EstadoCompra {
            get => _estadoCompra;
            set {
                _estadoCompra = value;
                fieldEstado.Text = value.ObtenerDisplayName();
                btnVerFactura.Visible = value == EstadoCompraEnum.Facturada;
                btnAnular.Enabled = value != EstadoCompraEnum.Recibida_Completa && value != EstadoCompraEnum.Facturada;
                layoutVista.BackColor = ObtenerColorFondoTupla(value);
            }
        }

        public DateTime FechaAprobacion { get; set; }

        public long? AprobadoPor { get; set; }

        public string Observaciones {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        public bool Activo { get; set; }

        public event EventHandler<(long, FormatoDocumento)>? ExportarFacturaCompra;
        public event EventHandler<long>? AnularCompra;
        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnVerFactura.Click += delegate { btnVerFactura.ContextMenuStrip?.Show(btnVerFactura, new Point(0, 40)); };
            btnExportarPdf.Click += delegate { ExportarFacturaCompra?.Invoke(this, (Id, FormatoDocumento.PDF)); };
            btnExportarXlsx.Click += delegate { ExportarFacturaCompra?.Invoke(this, (Id, FormatoDocumento.Excel)); };
            btnAnular.Click += delegate (object? sender, EventArgs e) {
                EstadoCompra = EstadoCompraEnum.Cancelada;
                AnularCompra?.Invoke(this, Id);
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

        private Color ObtenerColorFondoTupla(EstadoCompraEnum estado) {
            if (!Activo)
                return BackColor;

            return estado switch {
                EstadoCompraEnum.Pendiente_Aprobacion => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoCompraEnum.Cancelada => ContextoAplicacion.ColorErrorTupla,
                EstadoCompraEnum.Recibida_Parcial => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoCompraEnum.Recibida_Completa => ContextoAplicacion.ColorAdvertenciaTupla,
                _ => BackColor
            };
        }
    }
}