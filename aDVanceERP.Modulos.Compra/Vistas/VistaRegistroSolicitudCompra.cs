using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Compra.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Compra.Vistas {
    public partial class VistaRegistroSolicitudCompra : Form, IVistaRegistroSolicitudCompra {
        private bool _modoEdicion = false;
        private Producto? _productoSeleccionado = null;
        private UnidadMedida? _unidadMedidaProductoSeleccionado = null;
        private Dictionary<long, VistaTuplaCarrito> _carrito = new Dictionary<long, VistaTuplaCarrito>();

        public VistaRegistroSolicitudCompra() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroSolicitudCompra);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar la solicitud" : "Registrar la solicitud";
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

        public string Codigo {
            get => $"SOL{DateTime.Now:yyMMddHHmmss}{Random.Shared.Next(10, 99)}";
        }

        public DateTime FechaRequerida {
            get => fieldFechaRequerida.Value;
            set => fieldFechaRequerida.Value = value;
        }

        public string Observaciones {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        private string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        private decimal Cantidad {
            get {
                var cantidad = !string.IsNullOrEmpty(fieldCantidad.Text)
                    ? fieldCantidad.Text
                    : fieldCantidad.PlaceholderText;

                return decimal.TryParse(cantidad, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            }
            set {
                fieldCantidad.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : string.Empty;
            }
        }

        public decimal SubTotal {
            get => decimal.TryParse(fieldSubTotal.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m; 
            private set => fieldSubTotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal ImporteEstimado {
            get => decimal.TryParse(fieldImporteEstimado.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldImporteEstimado.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public Dictionary<long, VistaTuplaCarrito> Carrito { get => _carrito; }

        public event EventHandler? RegistrarNuevoProducto;
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            // Eventos
            fieldNombreProducto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                ObtenerProductoSeleccionado();                

                args.SuppressKeyPress = true;
            };
            btnRegistrarNuevoProducto.Click += delegate {
                RegistrarNuevoProducto?.Invoke(this, EventArgs.Empty);

                _productoSeleccionado = null;
                _unidadMedidaProductoSeleccionado = null;

                fieldNombreProducto.Text = string.Empty;
                fieldNombreProducto.Focus();
            };
            fieldCantidad.Click += delegate {
                ObtenerProductoSeleccionado();
            };
            fieldCantidad.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                AgregarProductoAlCarrito();

                args.SuppressKeyPress = true;
            };
            btnAdicionarAlCarrito.Click += delegate {
                AgregarProductoAlCarrito();
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            panelProductosCompra.Resize += delegate {
                foreach (var control in panelProductosCompra.Controls)
                    if (control is IVistaTuplaCarrito tuplaCarrito)
                        tuplaCarrito.Dimensiones = new Size(panelProductosCompra.Width - 20, 42);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private bool ObtenerProductoSeleccionado() {
            if (_productoSeleccionado != null)
                return true;

            _productoSeleccionado = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, NombreProducto).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.Nombre.Equals(NombreProducto)).entidadBase;
            _unidadMedidaProductoSeleccionado = _productoSeleccionado != null ? RepoUnidadMedida.Instancia.Buscar(FiltroBusquedaUnidadMedida.Id, _productoSeleccionado.IdUnidadMedida.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase : null;

            if (_productoSeleccionado == null) {
                CentroNotificaciones.MostrarNotificacion("No se ha especificado un nombre de producto válido. Corrija los datos antes de rellenar otro campo del carrito.", TipoNotificacionEnum.Advertencia);

                _productoSeleccionado = null;
                _unidadMedidaProductoSeleccionado = null;

                fieldNombreProducto.Text = string.Empty;
                fieldNombreProducto.Focus();
                return false;
            }

            fieldAbreviaturaUM1.Text = _unidadMedidaProductoSeleccionado != null ? _unidadMedidaProductoSeleccionado.Abreviatura : "u";
            fieldCantidad.Focus();

            return true;
        }

        private void AgregarProductoAlCarrito() {
            if (!ObtenerProductoSeleccionado())
                return;

            // Agregar o actualizar el carrito
            if (_carrito.ContainsKey(_productoSeleccionado!.Id)) {
                var productoCarrito = _carrito[_productoSeleccionado.Id];

                productoCarrito.Cantidad += Cantidad;
            } else {
                var tuplaCarrito = new VistaTuplaCarrito() {
                    IdProducto = _productoSeleccionado.Id,
                    Codigo = _productoSeleccionado.Codigo,
                    NombreProducto = _productoSeleccionado.Nombre,
                    CostoGeneral = _productoSeleccionado.Categoria == CategoriaProducto.ProductoTerminado 
                        ? _productoSeleccionado.CostoProduccionUnitario
                        : _productoSeleccionado.CostoAdquisicionUnitario,
                    Cantidad = Cantidad,
                    UnidadMedida = _unidadMedidaProductoSeleccionado ?? null,
                    TopLevel = false,
                    Location = new Point(0, _carrito.Count * 42),
                    Size = new Size(panelProductosCompra.Width - 20, 42),
                    Visible = true
                };

                tuplaCarrito.EliminarDatosTupla += EliminarProductoCarrito;
                
                _carrito.Add(_productoSeleccionado.Id, tuplaCarrito);                
                panelProductosCompra.Controls.Add(tuplaCarrito);
            }

            // Limpiar datos
            _productoSeleccionado = null;
            NombreProducto = string.Empty;
            Cantidad = 0;

            fieldNombreProducto.Focus();

            // Calcular totales de la venta
            CalcularTotales();
        }

        private void EliminarProductoCarrito(object? sender, EventArgs e) {
            if (sender is VistaTuplaCarrito tuplaCarrito) {
                tuplaCarrito.EliminarDatosTupla -= EliminarProductoCarrito;
                tuplaCarrito.Cerrar();

                _carrito.Remove(tuplaCarrito.IdProducto);
            }

            // Ajustar posiciones de elementos del carrito
            for (int i = 0; i < panelProductosCompra.Controls.Count; i++) {
                object? control = panelProductosCompra.Controls[i];
                
                if (control is IVistaTuplaCarrito tupla)
                    tupla.Coordenadas = new Point(0, i * 42);
            }

            fieldNombreProducto.Focus();

            // Calcular totales de la venta
            CalcularTotales();
        }

        private void CalcularTotales() {
            decimal subtotal = 0m;

            foreach (var producto in _carrito.Values) {
                var subTotal = producto.CostoGeneral * producto.Cantidad;

                subtotal += subTotal;
            }

            // Actualizar campos correspondientes
            SubTotal = subtotal;
            ImporteEstimado = subtotal;
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            FechaRequerida = DateTime.Today;
            NombreProducto = string.Empty;
            SubTotal = 0;
            Cantidad = 0;
            ImporteEstimado = 0;

            // Limpiar el carrito
            foreach (var control in panelProductosCompra.Controls) {
                if (control is VistaTuplaCarrito tuplaCarrito) {
                    tuplaCarrito.EliminarDatosTupla -= EliminarProductoCarrito;
                    tuplaCarrito.Cerrar();

                    _carrito.Remove(tuplaCarrito.IdProducto);
                }
            }            
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarNombresProductos(string[] nombresProductos) {
            fieldNombreProducto.AutoCompleteCustomSource.Clear();
            fieldNombreProducto.AutoCompleteCustomSource.AddRange(nombresProductos);
            fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}
