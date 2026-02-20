using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaVenta : Form, IVistaTuplaVenta {
        private EstadoVentaEnum _estadoVenta;

        public VistaTuplaVenta() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaVenta);

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
                : ObtenerColorFondoTupla(EstadoVenta);
        }

        public bool EstadoSeleccion { get; set; }

        public long Id { get; set; }

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

        public string? MetodoPagoPrincipal {
            get => fieldMetodoPagoPrincipal.Text;
            set {
                fieldMetodoPagoPrincipal.Text = value;
                fieldMetodoPagoPrincipal.Margin = fieldMetodoPagoPrincipal.AjusteAutomaticoMargenTexto();
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
                fieldEstado.Text = value.ObtenerDisplayName();
                btnVerFactura.Visible = value == EstadoVentaEnum.Completada;
                btnAnular.Enabled = value == EstadoVentaEnum.Pendiente || value == EstadoVentaEnum.Entregada;
                layoutVista.BackColor = ObtenerColorFondoTupla(value);
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

        private Color ObtenerColorFondoTupla(EstadoVentaEnum estado) {
            if (!Activo)
                return BackColor;

            return estado switch {
                EstadoVentaEnum.Pendiente => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoVentaEnum.Anulada => ContextoAplicacion.ColorErrorTupla,
                EstadoVentaEnum.Entregada => ContextoAplicacion.ColorAdvertenciaTupla,
                _ => BackColor
            };
        }
    }
}