using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Compra.Vistas {
    public partial class VistaTuplaSolicitudCompra : Form, IVistaTuplaSolicitudCompra {
        private EstadoSolicitudCompraEnum _estadoSolicitudCompra;
        private bool _activo;

        public VistaTuplaSolicitudCompra() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaSolicitudCompra);

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

        public long Id { get; set; }

        public string Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public string NombreSolicitante {
            get => fieldNombreSolicitante.Text;
            set {
                fieldNombreSolicitante.Text = value;
                fieldNombreSolicitante.Margin = fieldNombreSolicitante.AjusteAutomaticoMargenTexto();
            }
        }

        public DateTime FechaSolicitud {
            get => fieldFechaSolicitud.Text.Equals("-")
                        ? DateTime.MinValue
                        : DateTime.ParseExact(fieldFechaSolicitud.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaSolicitud.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public DateTime FechaRequerida {
            get => fieldFechaRequerida.Text.Equals("-")
                        ? DateTime.MinValue
                        : DateTime.ParseExact(fieldFechaRequerida.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            set => fieldFechaRequerida.Text = value.Equals(DateTime.MinValue)
                ? "-"
                : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public string Observaciones {
            get => fieldObservaciones.Text;
            set {
                fieldObservaciones.Text = value;
                fieldObservaciones.Margin = fieldObservaciones.AjusteAutomaticoMargenTexto();
            }
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

        public EstadoSolicitudCompraEnum Estado {
            get => _estadoSolicitudCompra;
            set {
                _estadoSolicitudCompra = value;
                fieldEstado.Text = value.ObtenerDisplayName();
                fieldEstado.Font = value == EstadoSolicitudCompraEnum.Convertida || value == EstadoSolicitudCompraEnum.Cancelada ? new Font(new FontFamily("Segoe UI"), 11.25f, FontStyle.Regular) : new Font(new FontFamily("Segoe UI"), 11.25f, FontStyle.Underline);
                fieldEstado.ForeColor = value == EstadoSolicitudCompraEnum.Convertida || value == EstadoSolicitudCompraEnum.Cancelada ? Color.DimGray : Color.DodgerBlue;
                fieldEstado.Cursor = value == EstadoSolicitudCompraEnum.Convertida || value == EstadoSolicitudCompraEnum.Cancelada ? Cursors.Default : Cursors.Hand;
                btnEditar.Enabled = value == EstadoSolicitudCompraEnum.Borrador || value == EstadoSolicitudCompraEnum.Pendiente_Aprobacion;
                layoutVista.BackColor = ObtenerColorFondoTupla(value);
            }
        }

        public bool Activo {
            get => _activo;
            set {
                _activo = value;
                btnEditar.Enabled = value;
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;
        public event EventHandler<(long idSolicitudCompra, EstadoSolicitudCompraEnum estado)> CambioEstadoSolicitudCompra;

        public void Inicializar() {
            // Eventos
            fieldEstado.Click += delegate {
                if (Estado == EstadoSolicitudCompraEnum.Convertida || Estado == EstadoSolicitudCompraEnum.Cancelada)
                    return;

                btnEstadoAprobada.Visible = Estado == EstadoSolicitudCompraEnum.Pendiente_Aprobacion;
                btnEstadoRechazada.Visible = Estado == EstadoSolicitudCompraEnum.Pendiente_Aprobacion;
                fieldEstado.ContextMenuStrip?.Show(fieldEstado, new Point(0, 40));
            };
            btnEstadoAprobada.Click += delegate (object? sender, EventArgs e) {
                Estado = EstadoSolicitudCompraEnum.Aprobada;
                CambioEstadoSolicitudCompra?.Invoke(this, (Id, Estado));
            };
            btnEstadoRechazada.Click += delegate (object? sender, EventArgs e) {
                Estado = EstadoSolicitudCompraEnum.Rechazada;
                CambioEstadoSolicitudCompra?.Invoke(this, (Id, Estado));
            };
            btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(this, e); };
            btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
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

        private Color ObtenerColorFondoTupla(EstadoSolicitudCompraEnum estado) {
            if (!Activo)
                return BackColor;

            return estado switch {
                EstadoSolicitudCompraEnum.Pendiente_Aprobacion => ContextoAplicacion.ColorAdvertenciaTupla,
                EstadoSolicitudCompraEnum.Cancelada => ContextoAplicacion.ColorErrorTupla,
                _ => BackColor
            };
        }
    }
}