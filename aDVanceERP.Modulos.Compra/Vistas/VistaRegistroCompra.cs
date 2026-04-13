using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Modulos.Compra.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Compra.Vistas {
    public partial class VistaRegistroCompra : Form, IVistaRegistroCompra {
        private bool _modoEdicion = false;
        private SolicitudCompra? _solicitudCompraSeleccionada = null!;
        private Almacen? _almacenSeleccionado = null!;
        private Producto? _productoSeleccionado = null;
        private UnidadMedida? _unidadMedidaProductoSeleccionado = null;
        private Dictionary<long, VistaTuplaCarrito> _carrito = new Dictionary<long, VistaTuplaCarrito>();
        private bool _eventosInicializados = false;

        public VistaRegistroCompra() {
            InitializeComponent();

            NombreVista = nameof(VistaRegistroCompra);

            Inicializar();
        }

        public bool ModoEdicion {
            get => _modoEdicion;
            set {
                _modoEdicion = value;

                fieldSubtitulo.Text = value ? "Detalles y actualización" : "Registro";
                btnRegistrarActualizar.Text = value ? "Actualizar la venta" : "Registrar la venta";
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

        public DateTime FechaCompra {
            get => fieldFechaCompra.Value;
            set => fieldFechaCompra.Value = value;
        }

        public string CodigoSolicitud {
            get => fieldCodigoSolicitudCompra.Text;
            set => fieldCodigoSolicitudCompra.Text = value;
        }

        public string NombreProveedor {
            get => fieldNombreCompletoProveedor.Text;
            set => fieldNombreCompletoProveedor.Text = value;
        }

        public string NombreAlmacenDestino {
            get => fieldAlmacenDestino.Text;
            set => fieldAlmacenDestino.Text = value;
        }

        public string ObservacionesCompra {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        private string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        private decimal ImpuestoAdicional {
            get {
                var impuestoAdicionalPorcentaje = !string.IsNullOrEmpty(fieldImpuestoAdicional.Text)
                    ? fieldImpuestoAdicional.Text
                    : fieldImpuestoAdicional.PlaceholderText;

                return decimal.TryParse(impuestoAdicionalPorcentaje, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            }
            set {
                fieldImpuestoAdicional.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : string.Empty;
            }
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

        public decimal TotalBruto {
            get => decimal.TryParse(fieldTotalBruto.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldTotalBruto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal ImpuestoTotal {
            get => decimal.TryParse(fieldImpuestoTotal.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldImpuestoTotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal ImporteTotal {
            get => decimal.TryParse(fieldImporteTotal.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldImporteTotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public Dictionary<long, VistaTuplaCarrito> Carrito { get => _carrito; }

        public event EventHandler? RegistrarNuevoProducto;
        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            if (_eventosInicializados)
                return;

            fieldCodigoSolicitudCompra.SelectedValueChanged += OnSolicitudCompraSeleccionado;
            fieldAlmacenDestino.SelectedValueChanged += OnAlmacenDestinoSeleccionado;
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
            fieldImpuestoAdicional.Click += delegate {
                ObtenerProductoSeleccionado();
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

            _eventosInicializados = true;
        }

        private void OnSolicitudCompraSeleccionado(object? sender, EventArgs e) {
            ObtenerSolicitudCompraSeleccionada();
        }

        private void OnAlmacenDestinoSeleccionado(object? sender, EventArgs e) {
            _almacenSeleccionado = null;   // invalidar caché

            if (_carrito.Count > 0) {
                LimpiarCarrito();

                CentroNotificaciones.MostrarNotificacion(
                    "El carrito fue limpiado porque cambió el almacén de destino.",
                    TipoNotificacionEnum.Advertencia);
            }
        }

        private void LimpiarCarrito() {
            var tuplas = panelProductosCompra.Controls
                .OfType<VistaTuplaCarrito>()
                .ToList();

            foreach (var tupla in tuplas) {
                tupla.EliminarDatosTupla -= EliminarProductoCarrito;
                tupla.Cerrar();
            }

            panelProductosCompra.Controls.Clear();
            _carrito.Clear();
        }

        private bool ObtenerSolicitudCompraSeleccionada() {
            _solicitudCompraSeleccionada = RepoSolicitudCompra.Instancia.Buscar(FiltroBusquedaSolicitudCompra.Codigo, CodigoSolicitud).resultadosBusqueda.FirstOrDefault().entidadBase;

            if (_solicitudCompraSeleccionada == null) {
                CentroNotificaciones.MostrarNotificacion("El código de solicitud de compra seleccionado no es válido u ocurrió un error durante la selección.", TipoNotificacionEnum.Advertencia);
                return false;
            }

            // Rellenar automáticamente los campos relacionados con la solicitud
            ObservacionesCompra = _solicitudCompraSeleccionada.Observaciones ?? string.Empty;

            LimpiarCarrito();
            AgregarSolicitudCompraAlCarrito();

            fieldNombreProducto.Text = string.Empty;
            fieldNombreProducto.Focus();

            return true;
        }

        private bool ObtenerAlmacenSeleccionado() {
            if (_almacenSeleccionado != null)
                return true;

            _almacenSeleccionado = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, NombreAlmacenDestino).resultadosBusqueda.FirstOrDefault().entidadBase;

            if (_almacenSeleccionado == null) {
                CentroNotificaciones.MostrarNotificacion("Debe seleccionar un almacén de destino para la compra antes de agregar productos al carrito.", TipoNotificacionEnum.Advertencia);
                return false;
            }

            return true;
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

            // Obtener el producto desde el carrito si está disponible para actualizar descuento e impuesto
            var productoCarrito = _carrito.TryGetValue(_productoSeleccionado.Id, out var pc) ? pc : null;

            if (productoCarrito != null) {
                ImpuestoAdicional = productoCarrito.ImpuestoAdicional;
            }

            fieldAbreviaturaUM1.Text = _unidadMedidaProductoSeleccionado != null ? _unidadMedidaProductoSeleccionado.Abreviatura : "u";
            fieldCantidad.Focus();

            return true;
        }

        private void AgregarSolicitudCompraAlCarrito() {
            if (!ObtenerAlmacenSeleccionado())
                return;

            var detallesSolicitud = RepoDetalleSolicitudCompra.Instancia.Buscar(FiltroBusquedaDetalleSolicitudCompra.IdSolicitudCompra, _solicitudCompraSeleccionada?.Id.ToString()).resultadosBusqueda.Select(r => r.entidadBase).ToList();

            // Agregar productos de la solicitud al carrito
            foreach (var detalleSolicitud in detallesSolicitud) {
                var idProducto = detalleSolicitud.IdProducto;

                // Agregar o actualizar el carrito
                if (_carrito.ContainsKey(idProducto)) {
                    _carrito[idProducto].Cantidad += detalleSolicitud.CantidadSolicitada;
                } else {
                    // Obtener información adicional del producto (precio, unidad medida, etc.)
                    var producto = RepoProducto.Instancia.ObtenerPorId(idProducto);

                    if (producto == null)
                        continue;

                    var presentaciones = RepoPrecioPresentacion.Instancia.Buscar(FiltroBusquedaPrecioPresentacion.IdProducto, idProducto.ToString()).resultadosBusqueda.Select(r => r.entidadBase).ToArray();
                    var unidadMedida = RepoUnidadMedida.Instancia.Buscar(FiltroBusquedaUnidadMedida.Id, producto.IdUnidadMedida.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

                    var tuplaCarrito = new VistaTuplaCarrito() {
                        IdProducto = idProducto,
                        Codigo = producto.Codigo,
                        NombreProducto = producto.Nombre,
                        PrecioUnitario = producto.Categoria == CategoriaProductoEnum.ProductoTerminado
                            ? producto.CostoProduccionUnitario
                            : producto.CostoAdquisicionUnitario,
                        Cantidad = detalleSolicitud.CantidadSolicitada,
                        UnidadMedida = unidadMedida,
                        Descuento = 0,
                        ImpuestoAdicional = 0,
                        TopLevel = false,
                        Location = new Point(0, _carrito.Count * 42),
                        Size = new Size(panelProductosCompra.Width - 20, 42),
                        Visible = true
                    };

                    tuplaCarrito.CargarPresentacionesVenta(presentaciones);
                    tuplaCarrito.EditarDatosTupla += ReCalcularTotales;
                    tuplaCarrito.EliminarDatosTupla += EliminarProductoCarrito;

                    _carrito.Add(idProducto, tuplaCarrito);

                    panelProductosCompra.Controls.Add(tuplaCarrito);
                }
            }

            // Mostrar notificación de éxito
            CentroNotificaciones.MostrarNotificacion(
                $"Se agregaron {detallesSolicitud.Count} productos de la solicitud al carrito",
                TipoNotificacionEnum.Info
            );

            // Calcular totales de la compra
            CalcularTotales();
        }

        private void AgregarProductoAlCarrito() {
            if (!ObtenerAlmacenSeleccionado() || !ObtenerProductoSeleccionado())
                return;

            void LimpiarDatos() {
                // Limpiar datos
                _productoSeleccionado = null;
                NombreProducto = string.Empty;
                ImpuestoAdicional = 0;
                Cantidad = 0;

                fieldNombreProducto.Focus();
            }

            // Agregar o actualizar el carrito
            var presentaciones = RepoPrecioPresentacion.Instancia.Buscar(FiltroBusquedaPrecioPresentacion.IdProducto, _productoSeleccionado.Id.ToString()).resultadosBusqueda.Select(r => r.entidadBase).ToArray();

            if (_carrito.ContainsKey(_productoSeleccionado!.Id)) {
                var productoCarrito = _carrito[_productoSeleccionado.Id];

                productoCarrito.ImpuestoAdicional = ImpuestoAdicional;
                productoCarrito.Cantidad += Cantidad;
            } else {
                var tuplaCarrito = new VistaTuplaCarrito() {
                    IdProducto = _productoSeleccionado.Id,
                    Codigo = _productoSeleccionado.Codigo,
                    NombreProducto = _productoSeleccionado.Nombre,
                    PrecioUnitario = _productoSeleccionado.Categoria == CategoriaProductoEnum.ProductoTerminado
                        ? _productoSeleccionado.CostoProduccionUnitario
                        : _productoSeleccionado.CostoAdquisicionUnitario,
                    Cantidad = Cantidad,
                    UnidadMedida = _unidadMedidaProductoSeleccionado ?? null,
                    Descuento = 0,
                    ImpuestoAdicional = ImpuestoAdicional,
                    TopLevel = false,
                    Location = new Point(0, _carrito.Count * 42),
                    Size = new Size(panelProductosCompra.Width - 20, 42),
                    Visible = true
                };

                tuplaCarrito.CargarPresentacionesVenta(presentaciones);
                tuplaCarrito.EditarDatosTupla += ReCalcularTotales;
                tuplaCarrito.EliminarDatosTupla += EliminarProductoCarrito;

                _carrito.Add(_productoSeleccionado.Id, tuplaCarrito);

                panelProductosCompra.Controls.Add(tuplaCarrito);
            }

            LimpiarDatos();

            // Calcular totales de la compra
            CalcularTotales();
        }

        private void EliminarProductoCarrito(object? sender, EventArgs e) {
            if (sender is not VistaTuplaCarrito tuplaCarrito)
                return;

            tuplaCarrito.EliminarDatosTupla -= EliminarProductoCarrito;

            _carrito.Remove(tuplaCarrito.IdProducto);

            tuplaCarrito.Cerrar();

            // Reindexar SOLO los que quedan, después del Dispose
            var restantes = panelProductosCompra.Controls
                .OfType<VistaTuplaCarrito>()
                .ToList();

            for (int i = 0; i < restantes.Count; i++)
                restantes[i].Coordenadas = new Point(0, i * 42);

            fieldNombreProducto.Focus();

            CalcularTotales();
        }

        private void ReCalcularTotales(object? sender, EventArgs e) {
            CalcularTotales();
        }

        private void CalcularTotales() {
            decimal totalBruto = 0m;
            decimal impuestoTotal = 0m;

            foreach (var producto in _carrito.Values) {
                var subTotal = producto.PrecioUnitario * producto.Cantidad;
                var impuestoAdicional = subTotal * (producto.ImpuestoAdicional / 100);

                totalBruto += subTotal;
                impuestoTotal += impuestoAdicional;
            }

            // Actualizar campos correspondientes
            TotalBruto = totalBruto;
            ImpuestoTotal = impuestoTotal;
            ImporteTotal = totalBruto + impuestoTotal;
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            _solicitudCompraSeleccionada = null;
            _almacenSeleccionado = null;
            _productoSeleccionado = null;
            _unidadMedidaProductoSeleccionado = null;

            FechaCompra = DateTime.Today;
            CodigoSolicitud = string.Empty;
            NombreProveedor = string.Empty;
            ObservacionesCompra = string.Empty;
            NombreProducto = string.Empty;
            TotalBruto = 0;
            ImpuestoAdicional = 0;
            ImpuestoTotal = 0;
            Cantidad = 0;
            ImporteTotal = 0;

            LimpiarCarrito();
        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarCodigosSolicitudesCompra(object[] codigosSolicitudes) {
            fieldCodigoSolicitudCompra.SelectedValueChanged -= OnSolicitudCompraSeleccionado;
            fieldCodigoSolicitudCompra.Items.Clear();
            fieldCodigoSolicitudCompra.Items.AddRange(codigosSolicitudes);
            fieldCodigoSolicitudCompra.SelectedIndex = -1;
            fieldCodigoSolicitudCompra.SelectedValueChanged += OnSolicitudCompraSeleccionado;
        }

        public void CargarNombresProveedores(string[] nombresProveedores) {
            fieldNombreCompletoProveedor.AutoCompleteCustomSource.Clear();
            fieldNombreCompletoProveedor.AutoCompleteCustomSource.AddRange(nombresProveedores);
            fieldNombreCompletoProveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreCompletoProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarNombresAlmacenes(object[] nombresAlmacenes) {
            fieldAlmacenDestino.SelectedValueChanged -= OnAlmacenDestinoSeleccionado;
            fieldAlmacenDestino.Items.Clear();
            fieldAlmacenDestino.Items.AddRange(nombresAlmacenes);
            fieldAlmacenDestino.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;
            fieldAlmacenDestino.SelectedValueChanged += OnAlmacenDestinoSeleccionado;
        }

        public void CargarNombresProductos(string[] nombresProductos) {
            fieldNombreProducto.AutoCompleteCustomSource.Clear();
            fieldNombreProducto.AutoCompleteCustomSource.AddRange(nombresProductos);
            fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}
