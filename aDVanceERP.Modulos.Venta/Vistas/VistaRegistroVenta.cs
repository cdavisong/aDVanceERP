using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using DVanceERP.Modulos.Venta.Vistas;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroVenta : Form, IVistaRegistroVenta {
        private bool _modoEdicion = false;
        private Pedido? _pedidoSeleccionado = null!;
        private Almacen? _almacenSeleccionado = null!;
        private Producto? _productoSeleccionado = null;
        private UnidadMedida? _unidadMedidaProductoSeleccionado = null;
        private Dictionary<long, VistaTuplaCarrito> _carrito = new Dictionary<long, VistaTuplaCarrito>();

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

        public DateTime FechaVenta {
            get => fieldFechaVenta.Value;
            set => fieldFechaVenta.Value = value;
        }

        public string NumeroPedido {
            get => fieldNumeroPedido.Text;
            set => fieldNumeroPedido.Text = value;
        }

        public string NombreCliente {
            get => fieldNombreCompletoCliente.Text;
            set => fieldNombreCompletoCliente.Text = value;
        }

        public string NombreAlmacenOrigen {
            get => fieldAlmacenOrigen.Text;
            set => fieldAlmacenOrigen.Text = value;
        }

        public string ObservacionesPedido {
            get => fieldObservaciones.Text;
            set => fieldObservaciones.Text = value;
        }

        private string NombreProducto {
            get => fieldNombreProducto.Text;
            set => fieldNombreProducto.Text = value;
        }

        private decimal Descuento {
            get {
                var descuentoPorcentaje = !string.IsNullOrEmpty(fieldDescuento.Text)
                    ? fieldDescuento.Text
                    : fieldDescuento.PlaceholderText;

                return decimal.TryParse(descuentoPorcentaje, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            }
            set {
                fieldDescuento.Text = value > 0
                    ? value.ToString("N2", CultureInfo.InvariantCulture)
                    : string.Empty;
            }
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

        public Dictionary<long, VistaTuplaCarrito> Carrito { get => _carrito; }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldNombreProducto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                _productoSeleccionado = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, NombreProducto).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.Nombre.Equals(NombreProducto)).entidadBase;
                _unidadMedidaProductoSeleccionado = _productoSeleccionado != null ? RepoUnidadMedida.Instancia.Buscar(FiltroBusquedaUnidadMedida.Id, _productoSeleccionado.IdUnidadMedida.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase : null;

                if (_productoSeleccionado == null) {
                    CentroNotificaciones.MostrarNotificacion("El producto entrado no se encuentra registrado en la base de datos. Entre el nombre de un producto válido antes de realizar el movimiento.", TipoNotificacion.Advertencia);

                    _productoSeleccionado = null;
                    _unidadMedidaProductoSeleccionado = null;
                    fieldNombreProducto.Text = string.Empty;
                    fieldNombreProducto.Focus();
                    return;
                }

                fieldAbreviaturaUM1.Text = _unidadMedidaProductoSeleccionado != null ? _unidadMedidaProductoSeleccionado.Abreviatura : "u";
                fieldCantidad.Focus();

                args.SuppressKeyPress = true;
            };
            fieldCantidad.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                _almacenSeleccionado = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, NombreAlmacenOrigen).resultadosBusqueda.FirstOrDefault().entidadBase;

                if (_almacenSeleccionado == null) {
                    CentroNotificaciones.MostrarNotificacion("Debe seleccionar un almacén de origen antes de agregar productos al carrito.", TipoNotificacion.Advertencia);
                    return;
                }

                if (_productoSeleccionado == null) {
                    CentroNotificaciones.MostrarNotificacion("No se ha especificado un producto válido. Especifique un valor de producto válido en el campo \"Nombre del producto\" antes de agregar una cantidad.", TipoNotificacion.Advertencia);

                    _productoSeleccionado = null;
                    fieldCantidad.Text = string.Empty;
                    fieldNombreProducto.Focus();
                    return;
                }

                AgregarProductoAlCarrito();

                args.SuppressKeyPress = true;
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private void AgregarPedidoAlCarrito() {
            // TODO: Agregar pedido al carrito
            // Verificar disponibilidad de TODOS los productos del pedido
            var verificacion = RepoPedido.Instancia.VerificarDisponibilidadPedidoCompleta(_pedidoSeleccionado!.Id, _almacenSeleccionado!.Id);
        }

        private void AgregarProductoAlCarrito() {
            var disponibilidadProducto = RepoProducto.Instancia.ObtenerDisponibilidadProducto(_productoSeleccionado!.Id, _almacenSeleccionado!.Id, _pedidoSeleccionado?.Id ?? 0);
            var disponible = disponibilidadProducto.disponible;
            var comprometido = disponibilidadProducto.comprometido;
            var cantidadReal = disponible - comprometido;
            var cantidadEnCarrito = _carrito.ContainsKey(_productoSeleccionado.Id) ? _carrito[_productoSeleccionado.Id].Cantidad : 0;
            var cantidadTotalFinal = cantidadEnCarrito + Cantidad;

            // Verificar la cantidad disponible
            if (cantidadTotalFinal > cantidadReal + cantidadEnCarrito) {
                var maximoPermitido = cantidadReal;

                CentroNotificaciones.MostrarNotificacion(
                    $"Stock insuficiente para {_productoSeleccionado.Nombre}. El máximo disponible es de {maximoPermitido} {_unidadMedidaProductoSeleccionado?.Abreviatura}, la cantidad en inventario es de {disponible} {_unidadMedidaProductoSeleccionado?.Abreviatura} y están comprometidas {comprometido} {_unidadMedidaProductoSeleccionado?.Abreviatura}",
                    TipoNotificacion.Advertencia
                );

                // Ofrecer agregar el máximo disponible
                if (maximoPermitido > 0) {
                    var resultado = CentroNotificaciones.MostrarMensaje(
                        $"¿Desea agregar la cantidad máxima disponible ({maximoPermitido} {_unidadMedidaProductoSeleccionado?.Abreviatura})?",
                        TipoMensaje.Info,
                        BotonesMensaje.SiNo
                    );

                    if (resultado == DialogResult.Yes) {
                        Cantidad = maximoPermitido - cantidadEnCarrito;
                        if (Cantidad <= 0)
                            return; // Ya está en carrito
                    } else return;
                } else return;
            }

            // Agregar o actualizar el carrito
            if (_carrito.ContainsKey(_productoSeleccionado.Id)) {
                _carrito[_productoSeleccionado.Id].Cantidad += Cantidad;
            } else {
                var tuplaCarrito = new VistaTuplaCarrito() {
                    Codigo = _productoSeleccionado.Codigo,
                    NombreProducto = _productoSeleccionado.Nombre,
                    CostoGeneral = _productoSeleccionado.PrecioVentaBase,
                    Cantidad = Cantidad,
                    UnidadMedida = _unidadMedidaProductoSeleccionado ?? null,
                    Descuento = Descuento,
                    ImpuestoAdicional = ImpuestoAdicional
                };

                _carrito.Add(_productoSeleccionado.Id, tuplaCarrito);
            }

            ActualizarCarrito();
        }

        private void ActualizarCarrito() {
            throw new NotImplementedException();
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {

        }

        public void Cerrar() {
            Dispose();
        }

        public void CargarNumerosPedidos(object[] numerosPedidos) {
            fieldNumeroPedido.Items.Clear();
            fieldNumeroPedido.Items.AddRange(numerosPedidos);
            fieldNumeroPedido.SelectedIndex = -1;
        }

        public void CargarNombresClientes(string[] nombresClientes) {
            fieldNombreCompletoCliente.AutoCompleteCustomSource.Clear();
            fieldNombreCompletoCliente.AutoCompleteCustomSource.AddRange(nombresClientes);
            fieldNombreCompletoCliente.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreCompletoCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void CargarNombresAlmacenes(object[] nombresAlmacenes) {
            fieldAlmacenOrigen.Items.Clear();
            fieldAlmacenOrigen.Items.AddRange(nombresAlmacenes);
            fieldAlmacenOrigen.SelectedIndex = nombresAlmacenes.Length > 0 ? 0 : -1;
        }

        public void CargarNombresProductos(string[] nombresProductos) {
            fieldNombreProducto.AutoCompleteCustomSource.Clear();
            fieldNombreProducto.AutoCompleteCustomSource.AddRange(nombresProductos);
            fieldNombreProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            fieldNombreProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}
