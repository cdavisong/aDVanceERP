using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaVenta : Form, IVistaTuplaVenta {
        private CanalPagoEnum? _canalPago;
        private EstadoVentaEnum _estadoVenta;

        public VistaTuplaVenta() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaVenta);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Id:0000}{Name}";
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

        public string NumeroFacturaVenta {
            get => fieldNumeroFactura.Text;
            set => fieldNumeroFactura.Text = value;
        }

        public DateTime FechaVenta {
            get => fieldFechaVenta.Text.Equals("-")
                    ? DateTime.MinValue
                    : DateTime.ParseExact(fieldFechaVenta.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaVenta.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string NombreCliente { get; set; }

        public CanalPagoEnum? CanalPagoPrincipal {
            get => _canalPago;
            set {
                _canalPago = value;

                var (colorFondo, colorFuente) = ObtenerColorCanal(value ?? CanalPagoEnum.NA);

                fieldCanalPagoPrincipal.Text = value.ObtenerNombreDescripcion().Nombre;
                fieldCanalPagoPrincipal.DisabledState.BorderColor = colorFondo;
                fieldCanalPagoPrincipal.DisabledState.FillColor = colorFondo;
                fieldCanalPagoPrincipal.DisabledState.ForeColor = colorFuente;                
            }
        }

        public decimal TotalBruto {
            get => decimal.TryParse(fieldTotalBruto.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var value)
                        ? value
                        : 0m;
            set => fieldTotalBruto.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal DescuentoTotal {
            get => decimal.TryParse(fieldDescuentoTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                            out var value)
                            ? value
                            : 0m;
            set => fieldDescuentoTotal.Text = value > 0
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

        public decimal ImporteTotal {
            get => decimal.TryParse(fieldImporteTotal.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                                    out var value)
                                    ? value
                                    : 0m;
            set => fieldImporteTotal.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public EstadoVentaEnum EstadoVenta {
            get => _estadoVenta;
            set {
                _estadoVenta = value;

                var (colorFondo, colorFuente) = ObtenerColorEstado(value);
                                
                fieldEstado.Text = value.ObtenerNombreDescripcion().Nombre;
                fieldEstado.DisabledState.BorderColor = colorFondo;
                fieldEstado.DisabledState.FillColor = colorFondo;
                fieldEstado.DisabledState.ForeColor = colorFuente;

                btnVerFactura.Visible = value == EstadoVentaEnum.Completada;
                btnAnular.Enabled = value == EstadoVentaEnum.Pendiente || value == EstadoVentaEnum.Entregada;
            }
        }

        public bool Activo { get; set; }

        public event EventHandler<(long, FormatoDocumento)>? ExportarFacturaVenta;
        public event EventHandler<long>? AnularVenta;
        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnVerFactura.Click += delegate { btnVerFactura.ContextMenuStrip?.Show(btnVerFactura, new Point(0, 40)); };
            btnExportarPdf.Click += delegate { ExportarFacturaVenta?.Invoke(this, (Id, FormatoDocumento.PDF)); };
            btnExportarXlsx.Click += delegate { ExportarFacturaVenta?.Invoke(this, (Id, FormatoDocumento.Excel)); };
            btnAnular.Click += delegate (object? sender, EventArgs e) {
                EstadoVenta = EstadoVentaEnum.Anulada;
                AnularVenta?.Invoke(this, Id);
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
                CanalPagoEnum.Transferencia => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 196)),   // Azul
                CanalPagoEnum.Mixto => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),            // Verde
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }

        private (Color colorFondo, Color colorFuente) ObtenerColorEstado(EstadoVentaEnum estado) {
            return estado switch {
                EstadoVentaEnum.Pendiente => (Color.FromArgb(255, 243, 224), Color.FromArgb(230, 81, 0)),           // Ambar
                EstadoVentaEnum.Completada => (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50)),         // Verde
                EstadoVentaEnum.Anulada => (Color.FromArgb(252, 228, 236), Color.FromArgb(198, 40, 40)),            // Rojo
                EstadoVentaEnum.Entregada => (Color.FromArgb(227, 242, 253), Color.FromArgb(21, 101, 196)),         // Azul
                _ => (Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136))
            };
        }
    }
}