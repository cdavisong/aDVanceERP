using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Estadisticas;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;
using aDVanceERP.Modulos.Venta.Vistas;

using System.Globalization;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorRegistroVenta : PresentadorVistaRegistro<IVistaRegistroVenta, Core.Modelos.Modulos.Venta.Venta, RepoVenta, FiltroBusquedaVenta> {
        private bool _buscandoProductos = false;
        private List<Pago> _pagos = new();

        public PresentadorRegistroVenta(IVistaRegistroVenta vista) : base(vista) {
            vista.BuscarProducto += OnBuscarProductos;
            vista.BuscarProductosRapidos += OnBuscarProductosRapidos;
            vista.AgregarProductoAlCarrito += OnAgregarProductoCarrito;
            vista.AgregarPagoVenta += OnAgregarPagoVenta;

            AgregadorEventos.Suscribir("MostrarVistaRegistroVenta", OnMostrarVistaRegistroVenta);
            AgregadorEventos.Suscribir("MostrarVistaEdicionVenta", OnMostrarVistaEdicionVenta);
        }

        private void OnMostrarVistaRegistroVenta(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            CargarDatosComunes();

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionVenta(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var venta = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Venta.Venta>(obj);

            if (venta == null)
                return;

            // Carga inicial de datos
            CargarDatosComunes();

            PopularVistaDesdeEntidad(venta);

            Vista.Mostrar();
        }

        private void CargarDatosComunes() {
            Vista.CargarAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
        }

        protected override Core.Modelos.Modulos.Venta.Venta? ObtenerEntidadDesdeVista() {
            var totalBruto = _carrito.Values.Sum(d => d.PrecioVentaUnitario * d.Cantidad);
            var totalDescuento = _carrito.Values.Sum(d => d.PrecioVentaUnitario * d.Cantidad * (d.DescuentoItem / 100));
            var importeTotal = totalBruto - totalDescuento;

            return new Core.Modelos.Modulos.Venta.Venta {
                Id = 0,
                IdCliente = Vista.Cliente?.Id ?? 0,
                IdEmpleadoVendedor = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                IdAlmacen = Vista.AlmacenOrigen?.Id ?? 0,
                NumeroFacturaTicket = $"V{DateTime.Today:yyyyMMdd}{(RepoVenta.Instancia.Cantidad() + 1):000000}",
                FechaVenta = DateTime.Now,
                TotalBruto = totalBruto,
                DescuentoTotal = totalDescuento,
                ImpuestoTotal = 0m,
                ImporteTotal = importeTotal,
                CanalPagoPrincipal = ObtenerCanalPagoPrincipal(),
                EstadoVenta = _pagos.Sum(p => p.MontoPagado) >= importeTotal
                    ? EstadoVentaEnum.Completada
                    : EstadoVentaEnum.Pendiente,
                ObservacionesVenta = Vista.Observaciones,
                Activo = true
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoVenta repositorio, long id) {
            if (Vista.ModoEdicion)
                return;

            var repoDetalle = RepoDetalleVentaProducto.Instancia;
            var detallesVenta = _carrito.Values.Select(d => new DetalleVentaProducto {
                Id = 0,
                IdVenta = id,
                IdProducto = d.IdProducto,
                Cantidad = d.Cantidad,
                PrecioCompraVigente = d.PrecioCompraVigente,
                PrecioVentaUnitario = d.PrecioVentaUnitario,
                DescuentoItem = d.DescuentoItem,
                Subtotal = d.Subtotal,
                IdPresentacion = d.IdPresentacion
            }).ToList();

            foreach (var detalle in detallesVenta)
                repoDetalle.Adicionar(detalle);

            AgregadorEventos.Publicar(
                "VentaRegistrada",
                AgregadorEventos.SerializarPayload(new object[] { Entidad!, detallesVenta, _pagos })
            );

            _carrito.Clear();
            _pagos.Clear();         // ← limpiar pagos junto con el carrito

            ActualizarCarritoVenta();
            ActualizarTotalesVista(); // llama ActualizarBalancePagos internamente → FaltanteVuelto = 0
        }

        protected override bool EntidadCorrecta() {
            //if (Vista.Cliente == null) {
            //    CentroNotificaciones.MostrarNotificacion(
            //        "Debe seleccionar un cliente para registrar la venta.",
            //        TipoNotificacionEnum.Advertencia);
            //    return false;
            //}

            if (_carrito.Count == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "El carrito está vacío. Agregue al menos un producto.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            if (Vista.AlmacenOrigen == null || Vista.AlmacenOrigen.Id == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe seleccionar un almacén de origen.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            var repoInventario = RepoInventario.Instancia;
            var repoProducto = RepoProducto.Instancia;
            var repoUm = RepoUnidadMedida.Instancia;

            // Agrupar por producto: suma de unidades base de todas sus presentaciones en el carrito
            var comprometidoPorProducto = _carrito
                .GroupBy(kv => kv.Key.IdProducto)
                .ToDictionary(g => g.Key, g => g.Sum(kv => kv.Value.Cantidad));

            foreach (var (idProducto, cantidadTotalBase) in comprometidoPorProducto) {
                var producto = repoProducto.ObtenerPorId(idProducto);
                if (producto == null) continue;

                var inventario = repoInventario
                    .Buscar(FiltroBusquedaInventario.IdProducto, idProducto.ToString())
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .FirstOrDefault(i => i.IdAlmacen == (Vista.AlmacenOrigen?.Id ?? 0));

                if (inventario == null) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"No se encontró inventario para '{producto.Nombre}' en el almacén seleccionado.",
                        TipoNotificacionEnum.Advertencia);
                    return false;
                }

                if (cantidadTotalBase > inventario.Cantidad) {
                    var abreviatura = repoUm.ObtenerPorId(producto.IdUnidadMedida)?.Abreviatura ?? "u";
                    CentroNotificaciones.MostrarNotificacion(
                        $"Stock insuficiente para '{producto.Nombre}' al momento de guardar.\n" +
                        $"Disponible: {inventario.Cantidad.ToString("N1", CultureInfo.InvariantCulture)} {abreviatura}\n" +
                        $"Requerido en carrito: {cantidadTotalBase.ToString("N1", CultureInfo.InvariantCulture)} {abreviatura}.",
                        TipoNotificacionEnum.Advertencia);
                    return false;
                }
            }

            return true;
        }

        public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Venta.Venta venta) {
            _carrito.Clear();

            // Reconstruir carrito desde los detalles guardados
            var detalles = RepoDetalleVentaProducto.Instancia
                .Buscar(FiltroBusquedaDetalleVenta.PorVenta, venta.Id.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase);

            foreach (var detalle in detalles)
                _carrito[(detalle.IdProducto, detalle.IdPresentacion)] = detalle;

            ActualizarCarritoVenta();
            ActualizarTotalesVista();
        }

        private void OnBuscarProductos(object? sender, string nombreProducto) {
            PopularProductosBusqueda(nombreProducto);
        }

        private void OnBuscarProductosRapidos(object? sender, EventArgs e) {
            PopularProductosRapidos();
        }

        #region PRODUCTOS RÁPIDOS Y BÚSQUEDA:

        private void PopularProductosRapidos() {
            LimpiarPanelProductos();

            var repoProducto = RepoProducto.Instancia;
            var topProductos = RepoEstadisticasVenta.Instancia
                .ObtenerTopProductosMes(ObtenerTotalTarjetas());
            var productosEncontrados = topProductos
                .Select(tp => repoProducto
                    .Buscar(FiltroBusquedaProducto.Nombre, tp.Nombre)
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .FirstOrDefault(p => p.Nombre.Equals(tp.Nombre, StringComparison.OrdinalIgnoreCase)))
                .Where(p => p != null)
                .ToList();

            ProcesarProductosEncontrados(productosEncontrados!);
        }

        private void PopularProductosBusqueda(string nombreProducto) {
            if (_buscandoProductos) return;
            _buscandoProductos = true;

            try {
                LimpiarPanelProductos();

                if (string.IsNullOrWhiteSpace(nombreProducto))
                    return;

                var productosEncontrados = RepoProducto.Instancia
                    .Buscar(FiltroBusquedaProducto.Nombre, nombreProducto)
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .Take(ObtenerTotalTarjetas())
                    .ToList();

                ProcesarProductosEncontrados(productosEncontrados);

                Vista.ProductoSeleccionado = productosEncontrados.Count == 1
                    ? productosEncontrados[0]
                    : null;
            } finally {
                _buscandoProductos = false;
            }
        }

        #endregion

        #region CARRITO:

        private Dictionary<(long IdProducto, long IdPresentacion), DetalleVentaProducto> _carrito = new Dictionary<(long, long), DetalleVentaProducto>();

        private void OnAgregarProductoCarrito(object? sender, EventArgs e) {
            #region REPOS:

            var repoProducto = RepoProducto.Instancia;

            #endregion

            var productoSeleccionado = Vista.ProductoSeleccionado;
            var cantidad = Vista.CantidadProducto;
            var precioUnitario = productoSeleccionado != null ? productoSeleccionado.PrecioVentaBase : 0m;
            var descuento = Vista.DescuentoProducto;
            var impuestoAdicional = Vista.ImpuestoAdicionalProducto;
            var idPresentacion = 0L;

            if (productoSeleccionado == null && !string.IsNullOrEmpty(Vista.NombreProducto)) {
                productoSeleccionado = repoProducto
                    .Buscar(FiltroBusquedaProducto.Nombre, Vista.NombreProducto)
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .FirstOrDefault(p => p.Nombre.Equals(Vista.NombreProducto));
                precioUnitario = productoSeleccionado != null ? productoSeleccionado.PrecioVentaBase : 0m;
            }

            if (sender is VistaProductoCarritoCard productoCard) {
                productoSeleccionado = repoProducto.ObtenerPorId(productoCard.IdProducto);
                cantidad = 1;
                precioUnitario = productoCard.PrecioVenta;
                idPresentacion = productoCard.IdPresentacion;
            }

            if (productoSeleccionado == null) {
                CentroNotificaciones.MostrarNotificacion(
                    "No se ha seleccionado un producto válido para agregar al carrito.",
                    TipoNotificacionEnum.Advertencia
                );
                return;
            }

            if (VerificarCantidadValida(productoSeleccionado, idPresentacion, cantidad, out var cantidadTotalUnidades)) {
                AgregarProductoAlCarrito(productoSeleccionado, cantidadTotalUnidades, precioUnitario, descuento, impuestoAdicional, idPresentacion);
            }
        }

        private bool VerificarCantidadValida(Producto productoSeleccionado, long idPresentacion, decimal cantidad, out decimal cantidadTotalUnidades) {
            var abreviaturaUmBase = RepoUnidadMedida.Instancia.ObtenerPorId(productoSeleccionado.IdUnidadMedida)?.Abreviatura ?? "u";
            var repoPresentacionProducto = RepoPresentacionProducto.Instancia;
            var repoInventario = RepoInventario.Instancia;

            var presentacionProducto = repoPresentacionProducto.ObtenerPorId(idPresentacion);
            var unidadesPorPresentacion = presentacionProducto != null
                ? presentacionProducto.Cantidad
                : 1m;
            cantidadTotalUnidades = cantidad * unidadesPorPresentacion;

            var inventario = repoInventario
                .Buscar(FiltroBusquedaInventario.IdProducto, productoSeleccionado.Id.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase)
                .FirstOrDefault(i => i.IdAlmacen == (Vista.AlmacenOrigen?.Id ?? 0));

            if (inventario == null) {
                CentroNotificaciones.MostrarNotificacion(
                    $"No se encontró inventario para '{productoSeleccionado.Nombre}' en el almacén seleccionado.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            // Unidades ya comprometidas en el carrito para este producto (todas las presentaciones,
            // porque todas consumen del mismo stock en unidades base)
            var unidadesComprometidas = _carrito
                .Where(kv => kv.Key.IdProducto == productoSeleccionado.Id)
                .Sum(kv => kv.Value.Cantidad);

            var stockDisponible = inventario.Cantidad;
            var stockEfectivo = stockDisponible - unidadesComprometidas;

            if (cantidadTotalUnidades > stockEfectivo) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Stock insuficiente para '{productoSeleccionado.Nombre}'\n" +
                    $"Disponible: {stockEfectivo.ToString("N1", CultureInfo.InvariantCulture)} {abreviaturaUmBase} / {stockDisponible.ToString("N1", CultureInfo.InvariantCulture)} {abreviaturaUmBase}\n" +
                    $"Solicitado: {cantidadTotalUnidades.ToString("N1", CultureInfo.InvariantCulture)} {abreviaturaUmBase}\n" +
                    $"({cantidad.ToString("N1", CultureInfo.InvariantCulture)} × {unidadesPorPresentacion.ToString("N1", CultureInfo.InvariantCulture)} por presentación)",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            return true;
        }

        private void AgregarProductoAlCarrito(Producto producto, decimal cantidad, decimal precioUnitario, decimal descuento, decimal impuestoAdicional, long idPresentacion) {
            if (_carrito.ContainsKey((producto.Id, idPresentacion))) {
                var item = _carrito[(producto.Id, idPresentacion)];

                item.Cantidad += cantidad;
                item.PrecioVentaUnitario = precioUnitario;
                item.DescuentoItem = descuento;
                item.IdPresentacion = idPresentacion;
                item.Subtotal = (item.PrecioVentaUnitario * item.Cantidad)
                    - (item.PrecioVentaUnitario * item.Cantidad * (descuento / 100));
            } else {
                var bruto = precioUnitario * cantidad;
                _carrito[(producto.Id, idPresentacion)] = new DetalleVentaProducto {
                    IdProducto = producto.Id,
                    Cantidad = cantidad,
                    PrecioCompraVigente = producto.Categoria == CategoriaProductoEnum.ProductoTerminado
                                             ? producto.CostoProduccionUnitario
                                             : producto.CostoAdquisicionUnitario,
                    PrecioVentaUnitario = precioUnitario,
                    DescuentoItem = descuento,
                    Subtotal = bruto - (bruto * (descuento / 100)),
                    IdPresentacion = idPresentacion
                };
            }

            ActualizarCarritoVenta();
            ActualizarTotalesVista();
        }

        #endregion

        #region PAGOS:

        private void OnAgregarPagoVenta(object? sender, EventArgs e) {
            var montoPago = Vista.MontoPagado;

            if (montoPago <= 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "El monto del pago debe ser mayor a cero.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }

            if (Vista.CanalPago == CanalPagoEnum.NA) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe seleccionar un método de pago.",
                    TipoNotificacionEnum.Advertencia);
                return;
            }

            _pagos.Add(new Pago {
                IdVenta = 0,
                MetodoPago = Vista.CanalPago,
                MontoPagado = montoPago,
                FechaPago = DateTime.Now,
                EstadoPago = EstadoPagoEnum.Confirmado
            });

            ActualizarBalancePagos();
        }

        private void ActualizarBalancePagos() {
            var importeTotal = _carrito.Values.Sum(d => d.Subtotal);
            var totalPagado = _pagos.Sum(p => p.MontoPagado);
            var diferencia = totalPagado - importeTotal;

            // diferencia negativa → cliente aún debe → valor positivo → vista muestra "FALTANTE" en rojo
            // diferencia positiva → overpagado → valor negativo → vista muestra "VUELTO" en verde
            // diferencia cero → exacto → valor cero → vista muestra "VUELTO $0.00" en verde
            Vista.FaltanteVuelto = -diferencia;
        }

        private string ObtenerCanalPagoPrincipal() {
            if (_pagos.Count == 0)
                return CanalPagoEnum.NA.ObtenerNombreDescripcion().Nombre;

            // Buscar el método de pago predominante según el monto acumulado correspondiente
            var acumuladoEfectivo = _pagos.Where(p => p.MetodoPago == CanalPagoEnum.Efectivo).Sum(p => p.MontoPagado);
            var acumuladoTransferencia = _pagos.Where(p => p.MetodoPago == CanalPagoEnum.Transferencia).Sum(p => p.MontoPagado);

            if (acumuladoEfectivo > acumuladoTransferencia)
                return CanalPagoEnum.Efectivo.ObtenerNombreDescripcion().Nombre;
            else if (acumuladoTransferencia > acumuladoEfectivo)
                return CanalPagoEnum.Transferencia.ObtenerNombreDescripcion().Nombre;
            else
                return CanalPagoEnum.Mixto.ObtenerNombreDescripcion().Nombre;
        }

        #endregion

        #region AUX:

        private int ObtenerTotalTarjetas() {
            var dimensionesCard = new Size(157, 208);
            var dimensionesPanel = Vista.PanelProductosRapidos.Size;
            var espacioHorizontal = 5;
            var espacioVertical = 5;

            // Calcular cuántas tarjetas caben horizontal y verticalmente
            var tarjetasPorFila = Math.Max(1, (dimensionesPanel.Width + espacioHorizontal) / (dimensionesCard.Width + espacioHorizontal));
            var tarjetasPorColumna = Math.Max(1, (dimensionesPanel.Height + espacioVertical) / (dimensionesCard.Height + espacioVertical));
            var totalTarjetas = tarjetasPorFila * tarjetasPorColumna;

            return totalTarjetas;
        }

        private void ProcesarProductosEncontrados(List<Producto> productosEncontrados) {
            if (productosEncontrados == null)
                return;

            #region REPOS:

            var repoProducto = RepoProducto.Instancia;
            var repoClasificacionProducto = RepoClasificacionProducto.Instancia;
            var repoUnidadMedida = RepoUnidadMedida.Instancia;
            var repoPresentacionProducto = RepoPresentacionProducto.Instancia;
            var repoInventario = RepoInventario.Instancia;

            #endregion

            LimpiarPanelProductos();

            foreach (var producto in productosEncontrados) {
                var unidadesMedidaProducto = new List<UnidadMedida>();
                var presentaciones = repoPresentacionProducto
                    .Buscar(FiltroBusquedaPresentacionProducto.IdProducto, producto.Id.ToString())
                    .resultadosBusqueda
                    .Select(r => r.entidadBase);
                var inventario = repoInventario
                    .Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString())
                    .resultadosBusqueda
                    .Select(r => r.entidadBase)
                    .FirstOrDefault(p => p.IdAlmacen == (Vista.AlmacenOrigen?.Id ?? 0));
                var card = new VistaProductoCarritoCard() {
                    IdProducto = producto.Id,
                    Nombre = producto.Nombre,
                    Clasificacion = repoClasificacionProducto.ObtenerPorId(producto.IdClasificacionProducto)?.Nombre ?? "N/A",
                    Codigo = producto.Codigo,
                    PrecioVenta = producto.PrecioVentaBase,
                    Cantidad = inventario?.Cantidad ?? 0
                };


                unidadesMedidaProducto.Add(repoUnidadMedida.ObtenerPorId(producto.IdUnidadMedida));
                if (presentaciones != null)
                    unidadesMedidaProducto.AddRange(presentaciones.Select(p => repoUnidadMedida.ObtenerPorId(p.IdUnidadMedida))!);

                // Inicializar card
                card.CargarPresentaciones([.. unidadesMedidaProducto]);
                card.CambioPresentacion += OnCambioPresentacionCard;
                card.ProductoSeleccionado += OnSeleccionarProductoCard;

                AgregarProductoCard(card);
            }
        }

        private void OnCambioPresentacionCard(object? sender, UnidadMedida e) {
            if (sender is not VistaProductoCarritoCard card)
                return;

            var producto = RepoProducto.Instancia.ObtenerPorId(card.IdProducto);
            if (producto == null) return;

            var presentacion = RepoPresentacionProducto.Instancia
                .Buscar(FiltroBusquedaPresentacionProducto.IdProducto, card.IdProducto.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase)
                .FirstOrDefault(c => c.IdUnidadMedida == e.Id);

            // Actualizar presentación y precio
            card.IdPresentacion = presentacion?.Id ?? 0;
            card.PrecioVenta = presentacion != null
                ? presentacion.PrecioVenta
                : producto.PrecioVentaBase;

            // Calcular stock efectivo en unidades de ESTA presentación
            var inventario = RepoInventario.Instancia
                .Buscar(FiltroBusquedaInventario.IdProducto, card.IdProducto.ToString())
                .resultadosBusqueda
                .Select(r => r.entidadBase)
                .FirstOrDefault(i => i.IdAlmacen == (Vista.AlmacenOrigen?.Id ?? 0));

            var stockBase = inventario?.Cantidad ?? 0m;

            // Unidades base ya comprometidas en carrito para este producto
            var comprometido = _carrito
                .Where(kv => kv.Key.IdProducto == card.IdProducto)
                .Sum(kv => kv.Value.Cantidad);

            var unidadesPorPresentacion = presentacion?.Cantidad ?? 1m;

            // Cuántas unidades de esta presentación caben con el stock restante
            card.Cantidad = Math.Floor((stockBase - comprometido) / unidadesPorPresentacion);
        }

        private void OnSeleccionarProductoCard(object? sender, long e) {
            OnAgregarProductoCarrito(sender, EventArgs.Empty);
        }

        private void AgregarProductoCard(VistaProductoCarritoCard card) {
            card.Name = card.NombreVista;
            card.TopLevel = false;

            Vista.PanelProductosRapidos.Controls.Add(card);

            card.Mostrar();
        }

        private void LimpiarPanelProductos() {
            var panel = Vista.PanelProductosRapidos;
            panel.SuspendLayout();

            try {
                for (int i = panel.Controls.Count - 1; i >= 0; i--) {
                    var control = panel.Controls[i];

                    panel.Controls.RemoveAt(i);

                    try {
                        if (control is VistaProductoCarritoCard card) {
                            card.CambioPresentacion -= OnCambioPresentacionCard;

                            card.Cerrar();
                            card.Dispose();
                        } else {
                            control.Dispose();
                        }
                    } catch {
                        try { control.Dispose(); } catch { }
                    }
                }
            } finally {
                panel.ResumeLayout(false);
            }
        }

        private int ObtenerTotalItemsCarrito() {
            var alturaItem = 30;
            var dimensionesPanel = Vista.PanelCarritoVenta.Size;
            var espacioVertical = 0;

            // Calcular cuántos items caben verticalmente
            var itemsPorColumna = Math.Max(1, (dimensionesPanel.Height + espacioVertical) / (alturaItem + espacioVertical));

            return (int) itemsPorColumna;
        }

        private void ActualizarCarritoVenta() {
            if (_carrito == null || _carrito.Count == 0)
                return;

            #region REPOS:

            var repoProducto = RepoProducto.Instancia;

            #endregion

            LimpiarPanelCarrito();

            for (int i = 0; i < _carrito.Count; i++) {
                var detalleVenta = _carrito.ElementAt(i).Value;
                var tupla = new VistaTuplaCarrito() {
                    IdProducto = detalleVenta.IdProducto,
                    IdPresentacion = detalleVenta.IdPresentacion,
                    NombreProducto = repoProducto.ObtenerPorId(detalleVenta.IdProducto)?.Nombre ?? "N/A",
                    Cantidad = detalleVenta.Cantidad,
                    PrecioUnitario = detalleVenta.PrecioVentaUnitario,
                    Descuento = detalleVenta.DescuentoItem,
                    ImpuestoAdicional = 0,
                    SubTotal = detalleVenta.Subtotal
                };

                AgregarProductoTupla(tupla);

                // Inicializar tupla
                tupla.EliminarDatosTupla += OnEliminarDatosTupla;
            }
        }

        private void OnEliminarDatosTupla(object? sender, EventArgs e) {
            if (sender is not VistaTuplaCarrito tupla)
                return;

            var panel = Vista.PanelCarritoVenta;
            panel.SuspendLayout();

            _carrito.Remove((tupla.IdProducto, tupla.IdPresentacion));

            tupla.EliminarDatosTupla -= OnEliminarDatosTupla;
            tupla.Cerrar();
            tupla.Dispose();

            panel.ResumeLayout(false);

            ActualizarCarritoVenta();
            ActualizarTotalesVista();
        }

        private void AgregarProductoTupla(VistaTuplaCarrito tupla) {
            tupla.Name = tupla.NombreVista;
            tupla.Dimensiones = new Size(Vista.PanelCarritoVenta.Width - 25, 30);
            tupla.TopLevel = false;

            Vista.PanelCarritoVenta.Controls.Add(tupla);

            tupla.Mostrar();
        }

        private void LimpiarPanelCarrito() {
            var panel = Vista.PanelCarritoVenta;
            panel.SuspendLayout();

            try {
                for (int i = panel.Controls.Count - 1; i >= 0; i--) {
                    var control = panel.Controls[i];
                    panel.Controls.RemoveAt(i);

                    try {
                        if (control is VistaTuplaCarrito item) {
                            item.Cerrar();
                            item.Dispose();
                        } else {
                            control.Dispose();
                        }
                    } catch {
                        try { control.Dispose(); } catch { }
                    }
                }
            } finally {
                panel.ResumeLayout(false);
            }
        }

        private void ActualizarTotalesVista() {
            var totalBruto = _carrito.Values.Sum(d => d.PrecioVentaUnitario * d.Cantidad);
            var totalDescuento = _carrito.Values.Sum(d => d.PrecioVentaUnitario * d.Cantidad * (d.DescuentoItem / 100));
            var importeTotal = totalBruto - totalDescuento;

            Vista.TotalBruto = totalBruto;
            Vista.TotalDescuento = totalDescuento;
            Vista.ImporteTotal = importeTotal;

            ActualizarBalancePagos(); // ← el importe cambió, recalcular faltante/vuelto
        }

        #endregion
    }
}
