using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Vistas {
    public partial class VistaRegistroVenta : Form, IVistaRegistroVenta {
        private bool _modoEdicion = false;
        private Pedido? _pedidoSeleccionado = null!;
        private Almacen? _almacenSeleccionado = null!;
        private Producto? _productoSeleccionado = null;
        private UnidadMedida? _unidadMedidaProductoSeleccionado = null;
        private Dictionary<long, VistaTuplaCarrito> _carrito = new Dictionary<long, VistaTuplaCarrito>();
        private Dictionary<Pago, DetallePagoTransferencia> _pagos = new Dictionary<Pago, DetallePagoTransferencia>();

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

        public string ObservacionesVenta {
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

        public decimal TotalBruto {
            get => decimal.TryParse(fieldTotalBruto.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldTotalBruto.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal DescuentoTotal {
            get => decimal.TryParse(fieldDescuentoTotal.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldDescuentoTotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal ImpuestoTotal {
            get => decimal.TryParse(fieldImpuestoTotal.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldImpuestoTotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal MontoPagado {
            get => decimal.TryParse(fieldMontoPagado.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldMontoPagado.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public decimal ImporteTotal {
            get => decimal.TryParse(fieldImporteTotal.Text, CultureInfo.InvariantCulture, out var value) ? value : 0m;
            private set => fieldImporteTotal.Text = value.ToString("N2", CultureInfo.InvariantCulture);
        }

        public Dictionary<Pago, DetallePagoTransferencia> Pagos {
            get => _pagos;
        }

        public Dictionary<long, VistaTuplaCarrito> Carrito { get => _carrito; }

        public event EventHandler? RegistrarEntidad;
        public event EventHandler? EditarEntidad;
        public event EventHandler? EliminarEntidad;

        public void Inicializar() {
            fieldNumeroPedido.SelectedValueChanged += delegate {
                ObtenerPedidoSeleccionado();
            };
            fieldNombreProducto.KeyDown += delegate (object? sender, KeyEventArgs args) {
                if (args.KeyCode != Keys.Enter)
                    return;

                ObtenerProductoSeleccionado();

                args.SuppressKeyPress = true;
            };
            fieldDescuento.Click += delegate {
                ObtenerProductoSeleccionado();
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
            btnPagoEfectivo.Click += delegate {
                if (Carrito.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion("Debe agregar al menos un producto al carrito para registrar el pago correspondiente a la venta actual.", TipoNotificacion.Advertencia);
                    return;
                }

                var vistaPagoEfectivo = new VistaRegistroPagoEfectivo();

                if (vistaPagoEfectivo.ShowDialog() == DialogResult.OK) {
                    var pago = new Pago() {
                        Id = 0,
                        IdVenta = 0, // Se asignará al registrar la venta
                        MetodoPago = MetodoPagoEnum.Efectivo,
                        MontoPagado = vistaPagoEfectivo.MontoPagado,
                        FechaPagoCliente = DateTime.Now,
                        FechaConfirmacionPago = vistaPagoEfectivo.EstadoPendiente ? DateTime.MinValue : DateTime.Now,
                        EstadoPago = vistaPagoEfectivo.EstadoPendiente ? EstadoPagoEnum.Pendiente : EstadoPagoEnum.Confirmado
                    };

                    _pagos.Add(pago, null!);
                }

                // Calcular totales de la venta
                CalcularTotales();

                // Verificar que los pagos satisfacen el monto total de la venta
                btnPagoEfectivo.Enabled = MontoPagado < ImporteTotal;
                btnPagoTransferencia.Enabled = MontoPagado < ImporteTotal;
            };
            btnPagoTransferencia.Click += delegate {
                if (Carrito.Count == 0) {
                    CentroNotificaciones.MostrarNotificacion("Debe agregar al menos un producto al carrito para registrar el pago correspondiente a la venta actual.", TipoNotificacion.Advertencia);
                    return;
                }

                var vistaPagoTransferencia = new VistaRegistroPagoTransferencia();

                if (vistaPagoTransferencia.ShowDialog() == DialogResult.OK) {
                    var pago = new Pago() {
                        Id = 0,
                        IdVenta = 0, // Se asignará al registrar la venta
                        MetodoPago = MetodoPagoEnum.TransferenciaBancaria,
                        MontoPagado = vistaPagoTransferencia.MontoPagado,
                        FechaPagoCliente = DateTime.Now,
                        FechaConfirmacionPago = vistaPagoTransferencia.EstadoPendiente ? DateTime.MinValue : DateTime.Now,
                        EstadoPago = vistaPagoTransferencia.EstadoPendiente ? EstadoPagoEnum.Pendiente : EstadoPagoEnum.Confirmado
                    };

                    var detallePagoTransferencia = new DetallePagoTransferencia() {
                        Id = 0,
                        IdPago = 0, // Se asignará al registrar el pago
                        NumeroConfirmacion = vistaPagoTransferencia.NumeroConfirmacion,
                        NumeroTransaccion = vistaPagoTransferencia.NumeroTransaccion,
                        MontoTransferencia = vistaPagoTransferencia.MontoPagado
                    };

                    _pagos.Add(pago, detallePagoTransferencia);
                }

                // Calcular totales de la venta
                CalcularTotales();

                // Verificar que los pagos satisfacen el monto total de la venta
                btnPagoEfectivo.Enabled = MontoPagado < ImporteTotal;
                btnPagoTransferencia.Enabled = MontoPagado < ImporteTotal;
            };
            btnRegistrarActualizar.Click += delegate (object? sender, EventArgs args) {
                if (ModoEdicion)
                    EditarEntidad?.Invoke(sender, args);
                else
                    RegistrarEntidad?.Invoke(sender, args);
            };
            panelProductosVenta.Resize += delegate {
                foreach (var control in panelProductosVenta.Controls)
                    if (control is IVistaTuplaCarrito tuplaCarrito)
                        tuplaCarrito.Dimensiones = new Size(panelProductosVenta.Width - 20, 42);
            };
            btnSalir.Click += delegate (object? sender, EventArgs args) { Ocultar(); };
        }

        private bool ObtenerPedidoSeleccionado() {
            _pedidoSeleccionado = RepoPedido.Instancia.Buscar(FiltroBusquedaPedido.Codigo, NumeroPedido).resultadosBusqueda.FirstOrDefault().entidadBase;

            if (_pedidoSeleccionado == null) {
                CentroNotificaciones.MostrarNotificacion("El número de pedido seleccionado no es válido u ocurrió un error durante la selección.", TipoNotificacion.Advertencia);

                _pedidoSeleccionado = null;
                return false;
            }

            // Rellenar automáticamente los campos relacionados con el pedido
            var cliente = RepoCliente.Instancia.ObtenerPorId(_pedidoSeleccionado.IdCliente.ToString());
            var persona = RepoPersona.Instancia.ObtenerPorId(cliente?.IdPersona.ToString());

            NombreCliente = persona?.NombreCompleto ?? string.Empty;
            ObservacionesVenta = _pedidoSeleccionado.ObservacionesPedido ?? string.Empty;

            // Limpiar el carrito
            foreach (var control in panelProductosVenta.Controls) {
                if (control is VistaTuplaCarrito tuplaCarrito) {
                    tuplaCarrito.EliminarDatosTupla -= EliminarProductoCarrito;
                    tuplaCarrito.Cerrar();

                    _carrito.Remove(tuplaCarrito.IdProducto);
                }
            }

            AgregarPedidoAlCarrito();

            fieldNombreProducto.Text = string.Empty;
            fieldNombreProducto.Focus();

            return true;
        }

        private bool ObtenerAlmacenSeleccionado() {
            if (_almacenSeleccionado != null)
                return true;

            _almacenSeleccionado = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, NombreAlmacenOrigen).resultadosBusqueda.FirstOrDefault().entidadBase;

            if (_almacenSeleccionado == null) {
                CentroNotificaciones.MostrarNotificacion("Debe seleccionar un almacén de origen para la venta antes de agregar productos al carrito.", TipoNotificacion.Advertencia);
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
                CentroNotificaciones.MostrarNotificacion("No se ha especificado un nombre de producto válido. Corrija los datos antes de rellenar otro campo del carrito.", TipoNotificacion.Advertencia);

                _productoSeleccionado = null;
                _unidadMedidaProductoSeleccionado = null;

                fieldNombreProducto.Text = string.Empty;
                fieldNombreProducto.Focus();
                return false;
            }

            // Obtener el producto desde el carrito si está disponible para actualizar descuento e impuesto
            var productoCarrito = _carrito.TryGetValue(_productoSeleccionado.Id, out var pc) ? pc : null;

            if (productoCarrito != null) {
                Descuento = productoCarrito.Descuento;
                ImpuestoAdicional = productoCarrito.ImpuestoAdicional;
            }

            fieldAbreviaturaUM1.Text = _unidadMedidaProductoSeleccionado != null ? _unidadMedidaProductoSeleccionado.Abreviatura : "u";
            fieldCantidad.Focus();

            return true;
        }

        private void AgregarPedidoAlCarrito() {
            if (!ObtenerAlmacenSeleccionado())
                return;

            var disponibilidadPedido = RepoPedido.Instancia.VerificarDisponibilidadPedidoCompleta(_pedidoSeleccionado!.Id, _almacenSeleccionado!.Id);

            // Verificar si todos los productos están disponibles
            if (!disponibilidadPedido.todosDisponibles) {
                var productosInsuficientes = new List<string>();

                foreach (var item in disponibilidadPedido.disponibilidad) {
                    var disponible = item.Value.disponible;
                    var comprometido = item.Value.comprometido;
                    var solicitado = item.Value.solicitado;
                    var stockReal = disponible - comprometido;

                    if (solicitado > stockReal) {
                        productosInsuficientes.Add(
                            $"{item.Value.nombre} ({item.Value.codigo}): solicitado {solicitado}, disponible {stockReal}"
                        );
                    }
                }

                var mensaje = "Los siguientes productos no tienen stock suficiente:\n\n" +
                              string.Join("\n", productosInsuficientes) +
                              "\n\n¿Desea agregar solo los productos disponibles?";

                var resultado = CentroNotificaciones.MostrarMensaje(
                    mensaje,
                    TipoMensaje.Advertencia,
                    BotonesMensaje.SiNo
                );

                if (resultado != DialogResult.Yes)
                    return;
            }

            // Agregar productos del pedido al carrito
            foreach (var item in disponibilidadPedido.disponibilidad) {
                var idProducto = item.Key;
                var disponible = item.Value.disponible;
                var comprometido = item.Value.comprometido;
                var solicitado = item.Value.solicitado;
                var stockReal = disponible - comprometido;

                // Determinar la cantidad a agregar
                var cantidadAAgregar = solicitado;
                if (solicitado > stockReal) {
                    if (stockReal <= 0)
                        continue; // Saltar productos sin stock
                    cantidadAAgregar = stockReal;
                }

                var cantidadEnCarrito = _carrito.ContainsKey(idProducto) ? _carrito[idProducto].Cantidad : 0;

                // Agregar o actualizar el carrito
                if (_carrito.ContainsKey(idProducto)) {
                    _carrito[idProducto].Cantidad += cantidadAAgregar;
                } else {
                    // Obtener información adicional del producto (precio, unidad medida, etc.)
                    var producto = RepoProducto.Instancia.ObtenerPorId(idProducto);

                    if (producto == null)
                        continue;

                    var unidadMedida = RepoUnidadMedida.Instancia.Buscar(FiltroBusquedaUnidadMedida.Id, producto.IdUnidadMedida.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;

                    var tuplaCarrito = new VistaTuplaCarrito() {
                        IdProducto = idProducto,
                        Codigo = item.Value.codigo,
                        NombreProducto = item.Value.nombre,
                        CostoGeneral = producto.PrecioVentaBase,
                        Cantidad = cantidadAAgregar,
                        UnidadMedida = unidadMedida,
                        Descuento = 0,
                        ImpuestoAdicional = 0,
                        TopLevel = false,
                        Location = new Point(0, _carrito.Count * 42),
                        Size = new Size(panelProductosVenta.Width - 20, 42),
                        Visible = true
                    };

                    tuplaCarrito.EliminarDatosTupla += EliminarProductoCarrito;

                    _carrito.Add(idProducto, tuplaCarrito);
                    panelProductosVenta.Controls.Add(tuplaCarrito);
                }
            }

            // Mostrar notificación de éxito
            CentroNotificaciones.MostrarNotificacion(
                $"Se agregaron {disponibilidadPedido.disponibilidad.Count} productos del pedido al carrito",
                TipoNotificacion.Info
            );

            // Calcular totales de la venta
            CalcularTotales();

            // Habilitar botones de pago
            btnPagoEfectivo.Enabled = true;
            btnPagoTransferencia.Enabled = true;

            // Eliminar pagos al modificar el carrito
            _pagos.Clear();
        }

        private void AgregarProductoAlCarrito() {
            if (!ObtenerAlmacenSeleccionado() || !ObtenerProductoSeleccionado())
                return;

            void LimpiarDatos() {
                // Limpiar datos
                _pedidoSeleccionado = null;
                _productoSeleccionado = null;
                NombreProducto = string.Empty;
                Descuento = 0;
                ImpuestoAdicional = 0;
                Cantidad = 0;

                fieldNombreProducto.Focus();
            }

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
                } else {
                    LimpiarDatos();
                    return;
                }
            }

            // Agregar o actualizar el carrito
            if (_carrito.ContainsKey(_productoSeleccionado.Id)) {
                var productoCarrito = _carrito[_productoSeleccionado.Id];

                productoCarrito.Descuento = Descuento;
                productoCarrito.ImpuestoAdicional = ImpuestoAdicional;
                productoCarrito.Cantidad += Cantidad;
            } else {
                var tuplaCarrito = new VistaTuplaCarrito() {
                    IdProducto = _productoSeleccionado.Id,
                    Codigo = _productoSeleccionado.Codigo,
                    NombreProducto = _productoSeleccionado.Nombre,
                    CostoGeneral = _productoSeleccionado.PrecioVentaBase,
                    Cantidad = Cantidad,
                    UnidadMedida = _unidadMedidaProductoSeleccionado ?? null,
                    Descuento = Descuento,
                    ImpuestoAdicional = ImpuestoAdicional,
                    TopLevel = false,
                    Location = new Point(0, _carrito.Count * 42),
                    Size = new Size(panelProductosVenta.Width - 20, 42),
                    Visible = true
                };

                tuplaCarrito.EliminarDatosTupla += EliminarProductoCarrito;

                _carrito.Add(_productoSeleccionado.Id, tuplaCarrito);
                panelProductosVenta.Controls.Add(tuplaCarrito);
            }

            LimpiarDatos();

            // Calcular totales de la venta
            CalcularTotales();

            // Habilitar botones de pago
            btnPagoEfectivo.Enabled = true;
            btnPagoTransferencia.Enabled = true;

            // Eliminar pagos al modificar el carrito
            _pagos.Clear();
        }

        private void EliminarProductoCarrito(object? sender, EventArgs e) {
            if (sender is VistaTuplaCarrito tuplaCarrito) {
                tuplaCarrito.EliminarDatosTupla -= EliminarProductoCarrito;
                tuplaCarrito.Cerrar();

                _carrito.Remove(tuplaCarrito.IdProducto);
            }

            // Ajustar posiciones de elementos del carrito
            for (int i = 0; i < panelProductosVenta.Controls.Count; i++) {
                object? control = panelProductosVenta.Controls[i];

                if (control is IVistaTuplaCarrito tupla)
                    tupla.Coordenadas = new Point(0, i * 42);
            }

            fieldNombreProducto.Focus();

            // Calcular totales de la venta
            CalcularTotales();

            // Deshabilitar botones de pago si el carrito está vacío
            if (_carrito.Count == 0) {
                btnPagoEfectivo.Enabled = false;
                btnPagoTransferencia.Enabled = false;
            }

            // Eliminar pagos al modificar el carrito
            _pagos.Clear();
        }

        private void CalcularTotales() {
            decimal totalBruto = 0m;
            decimal descuentoTotal = 0m;
            decimal impuestoTotal = 0m;

            foreach (var producto in _carrito.Values) {
                var subTotal = producto.CostoGeneral * producto.Cantidad;
                var descuento = subTotal * (producto.Descuento / 100);
                var impuestoAdicional = subTotal * (producto.ImpuestoAdicional / 100);

                totalBruto += subTotal;
                descuentoTotal += descuento;
                impuestoTotal += impuestoAdicional;
            }

            // Actualizar campos correspondientes
            TotalBruto = totalBruto;
            DescuentoTotal = descuentoTotal;
            ImpuestoTotal = impuestoTotal;
            MontoPagado = _pagos.Sum(p => p.Key.MontoPagado);
            ImporteTotal = totalBruto - descuentoTotal + impuestoTotal;
        }

        public void Mostrar() {
            BringToFront();
            Show();
        }

        public void Ocultar() {
            Hide();
        }

        public void Restaurar() {
            FechaVenta = DateTime.Today;
            NumeroPedido = string.Empty;
            NombreCliente = string.Empty;
            ObservacionesVenta = string.Empty;
            NombreProducto = string.Empty;
            TotalBruto = 0;
            Descuento = 0;
            DescuentoTotal = 0;
            ImpuestoAdicional = 0;
            ImpuestoTotal = 0;
            Cantidad = 0;
            ImporteTotal = 0;

            // Limpiar el carrito
            foreach (var control in panelProductosVenta.Controls) {
                if (control is VistaTuplaCarrito tuplaCarrito) {
                    tuplaCarrito.EliminarDatosTupla -= EliminarProductoCarrito;
                    tuplaCarrito.Cerrar();

                    _carrito.Remove(tuplaCarrito.IdProducto);
                }
            }

            // Eliminar pagos
            _pagos.Clear();
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
