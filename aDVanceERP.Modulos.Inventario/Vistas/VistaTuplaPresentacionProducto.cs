using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Properties;

using System.Globalization;

namespace aDVanceERP.Modulos.Inventario.Vistas {
    public partial class VistaTuplaPresentacionProducto : Form, IVistaTuplaVentaPresentacion {
        private decimal _descuento;
        private bool _activo;

        public VistaTuplaPresentacionProducto() {
            InitializeComponent();
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

        public long Id { get; set; }

        public string NombreUM {
            get => fieldNombreUM.Text;
            set => fieldNombreUM.Text = value;
        }

        public string AbreviaturaUM {
            get => fieldAbreviaturaUM.Text;
            set => fieldAbreviaturaUM.Text = value;
        }

        public decimal Cantidad {
            get => decimal.TryParse(fieldCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var value)
                    ? value
                    : 0m;
            set => fieldCantidad.Text = value > 0
                    ? value.ToString("N1", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal PrecioVenta {
            get => decimal.TryParse(fieldPrecioVenta.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var value)
                    ? value
                    : 0m;
            set => fieldPrecioVenta.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal PrecioPorUnidad {
            get => decimal.TryParse(fieldPrecioUnidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var value)
                    ? value
                    : 0m;
            set => fieldPrecioUnidad.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : "-";
        }

        public decimal Descuento {
            get => _descuento;
            set {
                _descuento = value;

                fieldDescuento.Text = value > 0 ? $"    {value:N1}% de descuento" : "Sin descuento";
                fieldDescuento.Image = value > 0 ? Resources.sort_down_green_16px : null;
                fieldDescuento.ForeColor = value > 0 ? Color.FromArgb(46, 125, 50) : Color.DimGray;
            }
        }

        public bool Estado {
            get => _activo;
            set {
                _activo = value;

                var (colorFondo, colorFuente) = ObtenerColorEstado(value);

                fieldEstado.Text = value ? "● Activo" : "X Inactivo";
                fieldEstado.DisabledState.BorderColor = colorFondo;
                fieldEstado.DisabledState.FillColor = colorFondo;
                fieldEstado.DisabledState.ForeColor = colorFuente;
            }
        }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnEditar.Click += delegate (object? sender, EventArgs e) { EditarDatosTupla?.Invoke(Id, e); };
            btnEliminar.Click += delegate (object? sender, EventArgs e) { EliminarDatosTupla?.Invoke(this, e); };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        private (Color colorFondo, Color colorFuente) ObtenerColorEstado(bool estado) {
            return estado
                ? (Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50))  // Verde
                : (Color.FromArgb(252, 228, 236), Color.FromArgb(198, 40, 40)); // Rojo
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
    }
}