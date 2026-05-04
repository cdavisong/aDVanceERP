using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroVenta : Form, IVistaRegistroVenta {
        private bool _modoEdicion = false;
        private decimal _faltanteVuelto;

        public VistaRegistroVenta() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroVenta);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistro.Text = value ? "Actualizar la venta" : "Registrar la venta";
            }
        }

        public string NombreVista {
            get => Name;
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

        public DateTime FechaVenta { get; set; }

        public Cliente Cliente { get; set; }

        public Almacen? AlmacenOrigen {
            get => fieldAlmacenOrigen.SelectedItem as Almacen;
        }

        public string Observaciones {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        public Producto? ProductoSeleccionado { get; set; }

        public string NombreProducto {
            get => fieldNombreProducto.Text;
        }

        public decimal DescuentoProducto {
            get => decimal.TryParse(fieldDescuento.Text, CultureInfo.InvariantCulture, out var descuento) ? descuento : 0m;
        }

        public decimal ImpuestoAdicionalProducto {
            get => decimal.TryParse(fieldImpuestoAdicional.Text, CultureInfo.InvariantCulture, out var impuesto) ? impuesto : 0m;
        }

        public decimal CantidadProducto {
            get => decimal.TryParse(fieldCantidad.Text, CultureInfo.InvariantCulture, out var cantidad) ? cantidad : 0m;
        }

        public FlowLayoutPanel PanelProductosRapidos => panelProductosRapidos;

        public FlowLayoutPanel PanelCarritoVenta => panelCarritoVenta;

        public decimal TotalBruto {
            get {
                var totalTexto = fieldTotalBruto.Text.Replace("$ ", "").Trim();
                return decimal.TryParse(totalTexto, out var total) ? total : 0m;
            }
            set => fieldTotalBruto.Text = $"$ {value:N2}";
        }

        public decimal TotalDescuento {
            get {
                var descuentoTexto = fieldDescuentoTotal.Text.Replace("$ ", "").Trim();
                return decimal.TryParse(descuentoTexto, out var descuento) ? descuento : 0m;
            }
            set => fieldDescuentoTotal.Text = $"$ {value:N2}";
        }

        public decimal ImporteTotal {
            get {
                var importeTexto = fieldImporteTotal.Text.Replace("$ ", "").Trim();
                return decimal.TryParse(importeTexto, out var importe) ? importe : 0m;
            }
            set => fieldImporteTotal.Text = $"$ {value:N2}";
        }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public event EventHandler<string>? BuscarProducto;
        public event EventHandler? BuscarProductosRapidos;
        public event EventHandler? AgregarProductoAlCarrito;


        public void Inicializar() {
            // Eventos
            btnRegistrarCliente.Click += (s, e) => AgregadorEventos.Publicar("MostrarVistaRegistroCliente", string.Empty);
            btnAgregarAlCarrito.Click += (s, e) => AgregarProductoAlCarrito?.Invoke(this, EventArgs.Empty);
            btnRegistro.Click += (s, e) => {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(this, EventArgs.Empty);
                else
                    RegistrarEntidad?.Invoke(this, EventArgs.Empty);
            };
            fieldNombreProducto.TextChanged += (s, e) => {
                if (string.IsNullOrWhiteSpace(fieldNombreProducto.Text))
                    BuscarProductosRapidos?.Invoke(this, EventArgs.Empty);
            };
            fieldNombreProducto.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(fieldNombreProducto.Text))
                    BuscarProductosRapidos?.Invoke(this, EventArgs.Empty);
            };
            fieldNombreProducto.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter) {
                    BuscarProducto?.Invoke(this, fieldNombreProducto.Text);

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            fieldNombreProducto.Text = string.Empty;
            fieldCantidad.Text = string.Empty;
            fieldDescuento.Text = "0";
            fieldImpuestoAdicional.Text = "0";
            fieldObservaciones.Text = string.Empty;

            // Resetear totales
            TotalBruto = 0m;
            TotalDescuento = 0m;
            ImporteTotal = 0m;

            // Resetear selección de almacén al primero disponible
            if (fieldAlmacenOrigen.Items.Count > 0)
                fieldAlmacenOrigen.SelectedIndex = 0;

            ProductoSeleccionado = null;
        }

        public void Cerrar() {

        }

        public void CargarProductos(Producto[] productos) {
            fieldNombreProducto.AutoCompleteCustomSource.Clear();
            fieldNombreProducto.AutoCompleteCustomSource.AddRange([.. productos.Select(p => p.Nombre)]);
            fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarAlmacenes(Almacen[] almacenes) {
            fieldAlmacenOrigen.Items.Clear();
            fieldAlmacenOrigen.Items.AddRange(almacenes);
            fieldAlmacenOrigen.SelectedIndex = almacenes.Length > 0 ? 0 : -1;
        }

        private (Color colorBorde, Color colorFondo, Color colorFuente) ObtenerColorEstadoFaltanteVuelto(bool estado) {
            return estado
                ? (Color.FromArgb(239, 83, 80), Color.FromArgb(255, 235, 238), Color.FromArgb(183, 28, 28))     // Rojo
                : (Color.FromArgb(129, 199, 132), Color.FromArgb(232, 245, 233), Color.FromArgb(46, 125, 50));  // Verde
        }
    }
}
