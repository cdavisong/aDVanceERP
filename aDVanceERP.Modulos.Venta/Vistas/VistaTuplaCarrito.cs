using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaCarrito : Form, IVistaTuplaCarrito {
        private PrecioPresentacion[] _presentacionesVenta = null!;
        private UnidadMedida? _unidadMedida = null!;

        public VistaTuplaCarrito() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaCarrito);

            Inicializar();
        }

        public string NombreVista {
            get => $"{Name}{Codigo}";
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

        public long IdProducto { get; set; }

        public string Codigo {
            get => fieldCodigo.Text;
            set => fieldCodigo.Text = value;
        }

        public string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        public decimal CostoGeneral {
            get => decimal.TryParse(fieldCostoGeneral.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var value)
                        ? value
                        : 0m;
            set => fieldCostoGeneral.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal Cantidad {
            get => decimal.TryParse(fieldCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                            out var value)
                            ? value
                            : 0m;
            set => fieldCantidad.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public UnidadMedida? UnidadMedida {
            get => _unidadMedida;
            set {
                _unidadMedida = value;
                                
                var (borde, fondo, fuente) = ObtenerColorUnidadMedida(_presentacionesVenta != null && _presentacionesVenta.Length > 0);

                fieldUnidadMedida.Text = value?.Abreviatura ?? "u";
                fieldUnidadMedida.BorderColor = borde;
                fieldUnidadMedida.FillColor = fondo;
                fieldUnidadMedida.ForeColor = fuente;
            }
        }

        public decimal Descuento { get; set; }

        public decimal ImpuestoAdicional { get; set; }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Restaurar() {
        }

        public void Ocultar() {
            Hide();
        }

        public void Cerrar() {
            Dispose();
        }

        private (Color borde, Color fondo, Color fuente) ObtenerColorUnidadMedida(bool estado) {
            return estado
                ? (Color.FromArgb(253, 224, 196), Color.FromArgb(255, 248, 242), Color.FromArgb(232, 149, 74))  // Naranja
                : (Color.FromArgb(228, 228, 228), Color.FromArgb(240, 240, 240), Color.FromArgb(136, 136, 136));// Gris
        }
    }
}