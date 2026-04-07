using aDVanceERP.Core.Documentos.Comun;
using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compra;
using aDVanceERP.Core.Repositorios.Modulos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Compra.Interfaces;
using aDVanceERP.Modulos.Compra.Vistas;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorGestionCompras : PresentadorVistaGestion<PresentadorTuplaCompra, IVistaGestionCompras, IVistaTuplaCompra, Core.Modelos.Modulos.Compra.Compra, RepoCompra, FiltroBusquedaCompra> {
        public PresentadorGestionCompras(IVistaGestionCompras vista) : base(vista) {
            RegistrarEntidad += OnRegistrarCompra;
            EditarEntidad += OnEditarCompra;

            AgregadorEventos.Suscribir("MostrarVistaGestionCompras", OnMostrarVistaGestionCompras);
        }

        private void OnRegistrarCompra(object? sender, EventArgs e) {
            AgregadorEventos.Publicar("MostrarVistaRegistroCompra", string.Empty);
        }

        private void OnEditarCompra(object? sender, Core.Modelos.Modulos.Compra.Compra e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionCompra", AgregadorEventos.SerializarPayload(e));
        }

        private void OnMostrarVistaGestionCompras(string obj) {
            Vista.CargarFiltrosBusqueda(UtilesBusquedaCompra.Filtros);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        public override void ActualizarResultadosBusqueda() {
            if (FiltroBusqueda == FiltroBusquedaCompra.Todos && (CriteriosBusqueda == null || CriteriosBusqueda.Length == 0))
                CriteriosBusqueda = [DateTime.Today.ToString("yyyy-MM-dd 00:00:00"), DateTime.Today.ToString("yyyy-MM-dd 23:59:59"), string.Empty];

            base.ActualizarResultadosBusqueda();
        }

        protected override PresentadorTuplaCompra ObtenerValoresTupla(Core.Modelos.Modulos.Compra.Compra entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaCompra(new VistaTuplaCompra(), entidad);

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Codigo = entidad.Codigo;
            presentadorTupla.Vista.IdProveedor = entidad.IdProveedor;
            presentadorTupla.Vista.IdSolicitudCompra = entidad.IdSolicitudCompra;
            presentadorTupla.Vista.IdEmpleadoComprador = entidad.IdEmpleadoComprador;
            presentadorTupla.Vista.IdAlmacenDestino = entidad.IdAlmacenDestino;
            presentadorTupla.Vista.IdTipoCompra = entidad.IdTipoCompra;
            presentadorTupla.Vista.FechaOrden = entidad.FechaOrden;
            presentadorTupla.Vista.FechaEntregaEsperada = entidad.FechaEntregaEsperada ?? DateTime.MinValue;
            presentadorTupla.Vista.CondicionesPago = entidad.CondicionesPago;
            presentadorTupla.Vista.Subtotal = entidad.Subtotal;
            presentadorTupla.Vista.ImpuestoTotal = entidad.ImpuestoTotal;
            presentadorTupla.Vista.TotalCompra = entidad.TotalCompra;
            presentadorTupla.Vista.EstadoCompra = entidad.EstadoCompra;
            presentadorTupla.Vista.FechaAprobacion = entidad.FechaAprobacion ?? DateTime.MinValue;
            presentadorTupla.Vista.AprobadoPor = entidad.AprobadoPor;
            presentadorTupla.Vista.Observaciones = entidad.Observaciones;
            presentadorTupla.Vista.Activo = entidad.Activo;
            presentadorTupla.Vista.CambioEstadoCompra += OnCambioEstadoCompra;
            presentadorTupla.Vista.ExportarFacturaCompra += OnExportarDocumentoFacturaCompra;
            presentadorTupla.Vista.AnularCompra += OnCancelarCompra;

            return presentadorTupla;
        }

        private void OnCambioEstadoCompra(object? sender, (long idCompra, EstadoCompraEnum estado) e) {
            var repoSolicitudCompra = RepoSolicitudCompra.Instancia;
            var repoCompra = RepoCompra.Instancia;
            var compra = repoCompra.ObtenerPorId(e.idCompra)!;
            var solicitudCompra = repoSolicitudCompra.ObtenerPorId(compra.IdSolicitudCompra)!;
            var repoDetalleCompraProducto = RepoDetalleCompraProducto.Instancia;
            var detallesCompra = repoDetalleCompraProducto.Buscar(FiltroBusquedaDetalleCompra.IdCompra, e.idCompra.ToString()).resultadosBusqueda.Select(dc => dc.entidadBase).ToList();
            var repoProducto = RepoProducto.Instancia;
            var repoMovimiento = RepoMovimiento.Instancia;
            var tipoMovimientoDevolucion = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Devolución a Proveedor").resultadosBusqueda.FirstOrDefault().entidadBase;
            var repoInventario = RepoInventario.Instancia;

            switch (e.estado) {
                case EstadoCompraEnum.Aprobada:
                    // Convertir la solicitud de compra si se consumió una en la compra actual
                    if (solicitudCompra != null)
                        repoSolicitudCompra.CambiarEstadoSolicitudCompra(solicitudCompra.Id, EstadoSolicitudCompraEnum.Convertida);

                    repoCompra.CambiarEstadoCompra(compra.Id, EstadoCompraEnum.Aprobada);
                    break;
                case EstadoCompraEnum.Recibida_Completa:
                    foreach (var detalleCompra in detallesCompra) {
                        // Actualizar la cantidad recibida
                        detalleCompra.CantidadRecibida = detalleCompra.CantidadOrdenada;

                        repoDetalleCompraProducto.Editar(detalleCompra);

                        // Completar los movimientos pendientes
                        var producto = repoProducto.ObtenerPorId(detalleCompra.IdProducto);
                        var inventarioProducto = repoInventario.Buscar(FiltroBusquedaInventario.IdProducto, producto!.Id.ToString()).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(compra.IdAlmacenDestino)).entidadBase;
                        var movimientoPendiente = repoMovimiento.Buscar(FiltroBusquedaMovimiento.IdProducto, producto!.Id.ToString()).resultadosBusqueda.FirstOrDefault(m => m.entidadBase.IdAlmacenDestino.Equals(compra.IdAlmacenDestino) && m.entidadBase.Estado == EstadoMovimiento.Pendiente).entidadBase;

                        if (movimientoPendiente != null) {
                            movimientoPendiente.Estado = EstadoMovimiento.Completado;

                            repoMovimiento.Editar(movimientoPendiente);

                            // Modificar inventario
                            repoInventario.ModificarInventario(
                                producto!.Id,
                                0,
                                compra.IdAlmacenDestino,
                                detalleCompra.CantidadRecibida);
                        }
                    }

                    repoCompra.CambiarEstadoCompra(compra.Id, EstadoCompraEnum.Recibida_Completa);
                    break;
                case EstadoCompraEnum.Cancelada:
                    // Verificar si la compra está asociada a alguna solicitud de compra y cancelarla
                    if (solicitudCompra != null)
                        repoSolicitudCompra.CambiarEstadoSolicitudCompra(solicitudCompra.Id, EstadoSolicitudCompraEnum.Cancelada);

                    // Verificar si la compra tiene pagos asociados (pendientes o no) y anularlos
                    var repoPago = RepoPago.Instancia;
                    var pagosCompra = repoPago.Buscar(FiltroBusquedaPago.IdCompraVenta, e.idCompra.ToString(), "Compra").resultadosBusqueda.Select(p => p.entidadBase).ToList();

                    if (pagosCompra?.Count > 0) {
                        foreach (var pago in pagosCompra)
                            repoPago.CambiarEstadoPago(pago.Id, EstadoPagoEnum.Anulado);
                    }

                    // Crear movimiento de inventario para cada detalle de compra y revertir el inventario
                    foreach (var detalleCompra in detallesCompra) {
                        var producto = RepoProducto.Instancia.ObtenerPorId(detalleCompra.IdProducto);
                        var inventarioProducto = repoInventario.Buscar(FiltroBusquedaInventario.IdProducto, producto!.Id.ToString()).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(compra.IdAlmacenDestino)).entidadBase;
                        var movimientoPendiente = repoMovimiento.Buscar(FiltroBusquedaMovimiento.IdProducto, producto!.Id.ToString()).resultadosBusqueda.FirstOrDefault(m => m.entidadBase.IdAlmacenDestino.Equals(compra.IdAlmacenDestino) && m.entidadBase.Estado == EstadoMovimiento.Pendiente).entidadBase;

                        if (movimientoPendiente != null) {
                            movimientoPendiente.Estado = EstadoMovimiento.Cancelado;

                            repoMovimiento.Editar(movimientoPendiente);
                        } else {
                            var movimiento = new Movimiento() {
                                Id = 0,
                                IdProducto = producto!.Id,
                                CostoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado
                                    ? producto.CostoProduccionUnitario
                                    : producto.CostoAdquisicionUnitario,
                                IdAlmacenOrigen = compra.IdAlmacenDestino,
                                IdAlmacenDestino = 0,
                                Estado = EstadoMovimiento.Completado,
                                FechaCreacion = DateTime.Now,
                                SaldoInicial = inventarioProducto.Cantidad,
                                FechaTermino = DateTime.Now,
                                CantidadMovida = detalleCompra.CantidadRecibida,
                                SaldoFinal = inventarioProducto.Cantidad + detalleCompra.CantidadRecibida,
                                IdTipoMovimiento = tipoMovimientoDevolucion?.Id ?? 0,
                                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                Notas = $"Devolución al proveedor para la compra del producto: {producto.Nombre}.",
                            };

                            // Adicionar a la base de datos local
                            repoMovimiento.Adicionar(movimiento);

                            // Modificar inventario
                            repoInventario.ModificarInventario(
                                producto!.Id,
                                compra.IdAlmacenDestino,
                                0,
                                detalleCompra.CantidadRecibida);
                        }
                    }

                    // Finalmente, cancelar la compra
                    repoCompra.CancelarCompra(compra.Id, $"Compra cancelada por el usuario.");
                    break;
            }
        }

        private void OnExportarDocumentoFacturaCompra(object? sender, (long id, FormatoDocumento formato) e) {

        }

        private void OnCancelarCompra(object? sender, long idCompra) {
            var repoCompra = RepoCompra.Instancia;
            var repoDetalleCompraProducto = RepoDetalleCompraProducto.Instancia;
            var compra = repoCompra.ObtenerPorId(idCompra)!;

            // Verificar si la compra está asociada a alguna solicitud y cancelarla
            if (compra.IdSolicitudCompra != 0) {
                var repoSolicitudCompra = RepoSolicitudCompra.Instancia;
                var solicitudCompra = repoSolicitudCompra.ObtenerPorId(compra.IdSolicitudCompra);

                if (solicitudCompra != null) {
                    solicitudCompra.Estado = EstadoSolicitudCompraEnum.Cancelada;
                    repoSolicitudCompra.Editar(solicitudCompra);
                }
            }


            // Verificar si la compra tiene pagos asociados (pendientes o no) y anularlos
            var repoPago = RepoPago.Instancia;
            var pagosCompra = repoPago.Buscar(FiltroBusquedaPago.IdCompraVenta, compra.Id.ToString(), "Compra").resultadosBusqueda.Select(p => p.entidadBase).ToList();

            if (pagosCompra?.Count > 0) {
                foreach (var pago in pagosCompra)
                    repoPago.CambiarEstadoPago(pago.Id, EstadoPagoEnum.Anulado);
            }

            // Crear movimiento de inventario para cada detalle de compra y revertir el inventario
            var repoMovimiento = RepoMovimiento.Instancia;
            var tipoMovimientoDevolucion = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Devolución a Proveedor").resultadosBusqueda.FirstOrDefault().entidadBase;
            var repoInventario = RepoInventario.Instancia;
            var detallesCompra = repoDetalleCompraProducto.Buscar(FiltroBusquedaDetalleCompra.IdCompra, compra.Id.ToString()).resultadosBusqueda.Select(dv => dv.entidadBase).ToList();

            foreach (var detalleCompra in detallesCompra) {
                var producto = RepoProducto.Instancia.ObtenerPorId(detalleCompra.IdProducto);
                var inventarioProducto = repoInventario.Buscar(FiltroBusquedaInventario.IdProducto, producto!.Id.ToString()).resultadosBusqueda.FirstOrDefault(p => p.entidadBase.IdAlmacen.Equals(compra.IdAlmacenDestino)).entidadBase;
                var movimiento = new Movimiento() {
                    Id = 0,
                    IdProducto = producto!.Id,
                    CostoUnitario = producto.Categoria == CategoriaProducto.ProductoTerminado ? producto.CostoProduccionUnitario : producto.CostoAdquisicionUnitario,
                    IdAlmacenOrigen = compra.IdAlmacenDestino,
                    IdAlmacenDestino = 0,
                    Estado = EstadoMovimiento.Completado,
                    FechaCreacion = DateTime.Now,
                    SaldoInicial = inventarioProducto.Cantidad,
                    FechaTermino = DateTime.Now,
                    CantidadMovida = detalleCompra.CantidadRecibida,
                    SaldoFinal = inventarioProducto.Cantidad - detalleCompra.CantidadRecibida,
                    IdTipoMovimiento = tipoMovimientoDevolucion?.Id ?? 0,
                    IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                    Notas = $"Devolución al proveedor para la compra del producto: {producto.Nombre}.",
                };

                // Adicionar a la base de datos local
                repoMovimiento.Adicionar(movimiento);

                // Modificar inventario
                repoInventario.ModificarInventario(
                    producto!.Id,
                    compra.IdAlmacenDestino,
                    0,
                    detalleCompra.CantidadRecibida);
            }

            // Finalmente, anular la compra
            repoCompra.CancelarCompra(compra.Id, $"Compra anulada por el usuario {ContextoSeguridad.UsuarioAutenticado?.Nombre}");
        }
    }
}