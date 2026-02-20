using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Venta;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorRegistroVenta : PresentadorVistaRegistro<IVistaRegistroVenta, Core.Modelos.Modulos.Venta.Venta, RepoVenta, FiltroBusquedaVenta> {
        public PresentadorRegistroVenta(IVistaRegistroVenta vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroVenta", OnMostrarVistaRegistroVenta);
            AgregadorEventos.Suscribir("MostrarVistaEdicionVenta", OnMostrarVistaEdicionVenta);
        }

        private void OnMostrarVistaRegistroVenta(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarNumerosPedidos([.. RepoPedido.Instancia.ObtenerCodigosPedidos()]);
            Vista.CargarNombresClientes([.. RepoCliente.Instancia.ObtenerNombres()]);
            Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

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
            Vista.CargarNumerosPedidos([.. RepoPedido.Instancia.ObtenerCodigosPedidos()]);
            Vista.CargarNombresClientes([.. RepoCliente.Instancia.ObtenerNombres()]);
            Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

            PopularVistaDesdeEntidad(venta);

            Vista.Mostrar();
        }

        protected override Core.Modelos.Modulos.Venta.Venta? ObtenerEntidadDesdeVista() {
            var pedido = RepoPedido.Instancia.Buscar(FiltroBusquedaPedido.Codigo, Vista.NumeroPedido).resultadosBusqueda.FirstOrDefault().entidadBase;
            var persona = RepoPersona.Instancia.Buscar(Core.Modelos.Modulos.Maestros.FiltroBusquedaPersona.NombreCompleto, Vista.NombreCliente).resultadosBusqueda.FirstOrDefault().entidadBase;
            var cliente = RepoCliente.Instancia.Buscar(FiltroBusquedaCliente.IdPersona, persona?.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
            var almacenOrigen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenOrigen).resultadosBusqueda.FirstOrDefault().entidadBase;

            return new Core.Modelos.Modulos.Venta.Venta() {
                Id = 0,
                IdPedido = pedido?.Id ?? 0,
                IdCliente = cliente?.Id ?? 0,
                IdEmpleadoVendedor = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0, // TODO: Integrar con empleados autenticados
                IdAlmacen = almacenOrigen?.Id ?? throw new ArgumentException("El almacén especificado no es válido", nameof(Vista.NombreAlmacenOrigen)),
                NumeroFacturaTicket = $"F{DateTime.Today:yyyyMMdd}{(RepoVenta.Instancia.Cantidad() + 1):000000}",
                FechaVenta = Vista.FechaVenta,
                TotalBruto = Vista.TotalBruto,
                DescuentoTotal = Vista.DescuentoTotal,
                ImpuestoTotal = Vista.ImpuestoTotal,
                ImporteTotal = Vista.ImporteTotal,
                MetodoPagoPrincipal = string.Empty,
                EstadoVenta = EstadoVentaEnum.Pendiente, // TODO: Implementar estados de venta dependiendo de pagos y estados de entregas
                ObservacionesVenta = Vista.ObservacionesVenta,
                Activo = true
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoVenta repositorio, long id) {
            var repoProducto = RepoProducto.Instancia;
            var repoDetalleVenta = RepoDetalleVentaProducto.Instancia;
            var almacenOrigen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenOrigen).resultadosBusqueda.FirstOrDefault().entidadBase;

            foreach (var productoCarrito in Vista.Carrito) {
                // Detalles de venta
                var producto = repoProducto.ObtenerPorId(productoCarrito.Key);
                var subtotal = productoCarrito.Value.CostoGeneral * productoCarrito.Value.Cantidad;
                var detalleVenta = new DetalleVentaProducto() {
                    Id = 0,
                    IdVenta = id,
                    IdProducto = producto?.Id ?? throw new ArgumentException("Ha ocurrido un error al tratar de registrar los detalles de la venta, uno de los productos del carrito no se encuentra registrado en la base de datos.", nameof(Vista.Carrito)),
                    Cantidad = productoCarrito.Value.Cantidad,
                    PrecioCompraVigente = producto.Categoria == CategoriaProducto.ProductoTerminado ? producto.CostoProduccionUnitario : producto.CostoAdquisicionUnitario,
                    PrecioVentaUnitario = producto.PrecioVentaBase,
                    DescuentoItem = productoCarrito.Value.Descuento,
                    Subtotal = subtotal - (subtotal * (productoCarrito.Value.Descuento / 100))
                };

                repoDetalleVenta.Adicionar(detalleVenta);

                // Crear movimiento de inventario
                var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString()).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(almacenOrigen.Id)).entidadBase;
                var movimiento = new Movimiento() {
                    Id = 0,
                    IdProducto = producto?.Id ?? throw new ArgumentException("Ha ocurrido un error al tratar de registrar los detalles de la venta, uno de los productos del carrito no se encuentra registrado en la base de datos.", nameof(Vista.Carrito)),
                    CostoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado ? producto.CostoProduccionUnitario : producto.CostoAdquisicionUnitario,
                    IdAlmacenOrigen = almacenOrigen?.Id ?? throw new ArgumentException("El almacén especificado no es válido", nameof(Vista.NombreAlmacenOrigen)),
                    IdAlmacenDestino = 0,
                    Estado = EstadoMovimiento.Completado,
                    FechaCreacion = DateTime.Now,
                    SaldoInicial = inventarioProducto.Cantidad,
                    FechaTermino = Entidad?.EstadoVenta == EstadoVentaEnum.Completada ? DateTime.Now : DateTime.MinValue,
                    CantidadMovida = productoCarrito.Value.Cantidad,
                    SaldoFinal = inventarioProducto.Cantidad - productoCarrito.Value.Cantidad,
                    IdTipoMovimiento = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Venta").resultadosBusqueda.FirstOrDefault().entidadBase?.Id ?? 0,
                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                    Notas = "Venta de producto.",
                };

                RepoMovimiento.Instancia.Adicionar(movimiento);

                // Modificar inventario
                RepoInventario.Instancia.ModificarInventario(
                    producto?.Id ?? throw new ArgumentException("Ha ocurrido un error al tratar de registrar los detalles de la venta, uno de los productos del carrito no se encuentra registrado en la base de datos.", nameof(Vista.Carrito)),
                    almacenOrigen?.Id ?? throw new ArgumentException("El almacén especificado no es válido", nameof(Vista.NombreAlmacenOrigen)),
                    0,
                    productoCarrito.Value.Cantidad);

                // Actualizar el estado del pedido
                if (Entidad?.IdPedido != 0) {
                    var repoPedido = RepoPedido.Instancia;
                    var pedido = repoPedido.Buscar(FiltroBusquedaPedido.Codigo, Vista.NumeroPedido).resultadosBusqueda.FirstOrDefault().entidadBase;

                    pedido.EstadoPedido = EstadoPedidoEnum.Retirado;
                    repoPedido.Editar(pedido);                    
                }
            }

            // Registrar los pagos asociados a la venta
            var repoPago = RepoPago.Instancia;
            var repoDetallePagoTransferencia = RepoDetallePagoTransferencia.Instancia;

            foreach (var pago in Vista.Pagos) {
                pago.Key.IdVenta = id;
                var idPago = repoPago.Adicionar(pago.Key);

                if (pago.Key.MetodoPago == MetodoPagoEnum.TransferenciaBancaria && pago.Value != null) {
                    pago.Value.IdPago = idPago;
                    repoDetallePagoTransferencia.Adicionar(pago.Value);
                }
            }

            // Actualizar el estado de la venta a completada si el total pagado cubre el importe total de la venta
            if (repositorio.VentaEstaPagadaCompletamente(id))
                repositorio.CambiarEstadoVenta(id, EstadoVentaEnum.Completada);

            // Actualizar el método de pago principal de la venta
            repositorio.ActualizarMetodoPagoPrincipal(id);
        }
    }
}
