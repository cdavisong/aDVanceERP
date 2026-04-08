using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Modulos.Compra.Interfaces;
using aDVanceERP.Modulos.Compra.Vistas;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorRegistroCompra : PresentadorVistaRegistro<IVistaRegistroCompra, Core.Modelos.Modulos.Compra.Compra, RepoCompra, FiltroBusquedaCompra> {
        public PresentadorRegistroCompra(IVistaRegistroCompra vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroCompra", OnMostrarVistaRegistroCompra);
            AgregadorEventos.Suscribir("MostrarVistaEdicionCompra", OnMostrarVistaEdicionCompra);
        }

        private void OnMostrarVistaRegistroCompra(string obj) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            // Carga inicial de datos
            Vista.CargarCodigosSolicitudesCompra([.. RepoSolicitudCompra.Instancia.ObtenerCodigosSolicitudes()]);
            Vista.CargarNombresProveedores([.. RepoProveedor.Instancia.ObtenerNombres()]);
            Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionCompra(string obj) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            if (string.IsNullOrEmpty(obj))
                return;

            var compra = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Compra.Compra>(obj);

            if (compra == null)
                return;

            // Carga inicial de datos
            Vista.CargarCodigosSolicitudesCompra([.. RepoSolicitudCompra.Instancia.ObtenerCodigosSolicitudes()]);
            Vista.CargarNombresProveedores([.. RepoProveedor.Instancia.ObtenerNombres()]);
            Vista.CargarNombresAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre)]);
            Vista.CargarNombresProductos([.. RepoProducto.Instancia.ObtenerTodos().Select(p => p.entidadBase.Nombre)]);

            PopularVistaDesdeEntidad(compra);

            Vista.Mostrar();
        }

        protected override Core.Modelos.Modulos.Compra.Compra? ObtenerEntidadDesdeVista() {
            var solicitudCompra = RepoSolicitudCompra.Instancia.Buscar(FiltroBusquedaSolicitudCompra.Codigo, Vista.CodigoSolicitud).resultadosBusqueda.FirstOrDefault().entidadBase;
            var persona = RepoPersona.Instancia.Buscar(Core.Modelos.Modulos.Maestros.FiltroBusquedaPersona.NombreCompleto, Vista.NombreProveedor).resultadosBusqueda.FirstOrDefault().entidadBase;
            var proveedor = RepoProveedor.Instancia.Buscar(FiltroBusquedaProveedor.IdPersona, persona?.Id.ToString()).resultadosBusqueda.FirstOrDefault().entidadBase;
            var almacenDestino = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenDestino).resultadosBusqueda.FirstOrDefault().entidadBase;

            return new Core.Modelos.Modulos.Compra.Compra() {
                Id = 0,
                Codigo = $"C{DateTime.Today:yyyyMMdd}{(RepoCompra.Instancia.Cantidad() + 1):000000}",
                IdProveedor = proveedor?.Id ?? 0,
                IdSolicitudCompra = solicitudCompra?.Id ?? 0,
                IdEmpleadoComprador = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0, // TODO: Integrar con empleados autenticados
                IdAlmacenDestino = almacenDestino?.Id ?? throw new ArgumentException("El almacén especificado no es válido", nameof(Vista.NombreAlmacenDestino)),
                IdTipoCompra = 0,
                FechaOrden = Vista.FechaCompra,
                FechaEntregaEsperada = Vista.FechaCompra.AddDays(1),
                CondicionesPago = "Contado",
                Subtotal = Vista.TotalBruto,
                ImpuestoTotal = Vista.ImpuestoTotal,
                TotalCompra = Vista.ImporteTotal,
                EstadoCompra = EstadoCompraEnum.Pendiente_Aprobacion,
                FechaAprobacion = DateTime.MinValue,
                AprobadoPor = 0,
                Activo = true
            };
        }

        protected override void RegistroEdicionAuxiliar(RepoCompra repositorio, long id) {
            var repoProducto = RepoProducto.Instancia;
            var repoDetalleCompra = RepoDetalleCompraProducto.Instancia;
            var repoInventario = RepoInventario.Instancia;
            var repoPrecioPresentacion = RepoPrecioPresentacion.Instancia;
            var tipoMovimientoCompra = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Compra")
                .resultadosBusqueda.FirstOrDefault().entidadBase;
            var almacenDestino = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenDestino)
                .resultadosBusqueda.FirstOrDefault().entidadBase;

            foreach (var productoCarrito in Vista.Carrito) {
                var tuplaCarrito = productoCarrito.Value as VistaTuplaCarrito; // O la vista correspondiente de compra
                var idPresentacion = tuplaCarrito?.IdPresentacion ?? 0;

                // Obtener cantidad de unidades por presentación
                decimal unidadesPorPresentacion = idPresentacion > 0
                    ? repoPrecioPresentacion.ObtenerCantidadPorPresentacion(idPresentacion)
                    : 1m;

                // Calcular cantidad total en unidades base para inventario
                decimal cantidadTotalUnidades = productoCarrito.Value.Cantidad * unidadesPorPresentacion;

                // Detalles de compra
                var producto = repoProducto.ObtenerPorId(productoCarrito.Key);
                var subtotal = productoCarrito.Value.PrecioUnitario * productoCarrito.Value.Cantidad;
                var detalleCompra = new DetalleCompraProducto() {
                    Id = 0,
                    IdCompra = id,
                    IdProducto = producto?.Id ?? throw new ArgumentException("...", nameof(Vista.Carrito)),
                    CantidadOrdenada = cantidadTotalUnidades, // GUARDAR EN UNIDADES BASE
                    CantidadRecibida = 0,
                    CostoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado
                        ? producto.CostoProduccionUnitario
                        : producto.CostoAdquisicionUnitario,
                    Descuento = productoCarrito.Value.Descuento,
                    ImpuestoPorcentaje = productoCarrito.Value.ImpuestoAdicional,
                    IdPresentacion = idPresentacion // AGREGAR ESTO
                };

                repoDetalleCompra.Adicionar(detalleCompra);

                // Crear movimiento de inventario
                var inventarioProducto = repoInventario.Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString())
                    .resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(almacenDestino.Id)).entidadBase;
                var movimiento = new Movimiento() {
                    Id = 0,
                    IdProducto = producto?.Id ?? throw new ArgumentException("...", nameof(Vista.Carrito)),
                    CostoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado
                        ? producto.CostoProduccionUnitario
                        : producto.CostoAdquisicionUnitario,
                    IdAlmacenOrigen = 0,
                    IdAlmacenDestino = almacenDestino?.Id ?? throw new ArgumentException("...", nameof(Vista.NombreAlmacenDestino)),
                    Estado = EstadoMovimiento.Pendiente,
                    FechaCreacion = DateTime.Now,
                    SaldoInicial = inventarioProducto.Cantidad,
                    FechaTermino = Entidad?.EstadoCompra == EstadoCompraEnum.Facturada ? DateTime.Now : DateTime.MinValue,
                    CantidadMovida = cantidadTotalUnidades, // AGREGAR EN UNIDADES BASE
                    SaldoFinal = inventarioProducto.Cantidad + cantidadTotalUnidades,
                    IdTipoMovimiento = tipoMovimientoCompra?.Id ?? 0,
                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                    Notas = $"Compra de producto. Presentación: {(idPresentacion > 0 ? idPresentacion.ToString() : "Unidad base")}.",
                };

                RepoMovimiento.Instancia.Adicionar(movimiento);
            }
        }

        protected override bool EntidadCorrecta() {
            var almacen = RepoAlmacen.Instancia
                .Buscar(FiltroBusquedaAlmacen.Nombre, Vista.NombreAlmacenDestino)
                .resultadosBusqueda.FirstOrDefault().entidadBase;

            if (almacen == null) {
                CentroNotificaciones.MostrarNotificacion(
                    "El almacén de destino no es válido.", TipoNotificacionEnum.Advertencia);
                return false;
            }

            return true;
        }
    }
}
