using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaTuplaCarrito : Form, IVistaTuplaCarrito {
        public VistaTuplaCarrito() {
            InitializeComponent();

            NombreVista = nameof(VistaTuplaCarrito);

            Inicializar();
        }

        public string NombreVista {
            get => $"{IdProducto:0000}{IdPresentacion:0000}{Name}";
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

        public string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
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

        public decimal PrecioUnitario { get; set; }

        public decimal SubTotal {
            get => decimal.TryParse(fieldSubtotal.Text.Replace("$ ", "").Trim(), NumberStyles.Any, CultureInfo.InvariantCulture,
                            out var value)
                            ? value
                            : 0m;
            set => fieldSubtotal.Text = value > 0
                    ? $"$ {value.ToString("N2", CultureInfo.InvariantCulture)}"
                    : "$ -";
        }

        public long IdPresentacion { get; set; }

        public decimal Descuento { get; set; }

        public decimal ImpuestoAdicional { get; set; }

        public event EventHandler? EditarDatosTupla;
        public event EventHandler? EliminarDatosTupla;

        public void Inicializar() {
            // Eventos
            btnEliminar.Click += delegate (object? sender, EventArgs e) { 
                EliminarDatosTupla?.Invoke(this, e); 
            };
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
    }
}