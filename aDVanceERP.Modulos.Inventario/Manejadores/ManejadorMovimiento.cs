using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Eventos.Modulos.Venta;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

namespace aDVanceERP.Modulos.Inventario.Manejadores {
    internal class ManejadorMovimiento {
        private readonly RepoAlmacen _repoAlmacen = null!;
        private readonly RepoProducto _repoProducto = null!;
        private readonly RepoPresentacionProducto _repoPresentacionProducto = null!;
        private readonly RepoInventario _repoInventario = null!;
        private readonly RepoMovimiento _repoMovimiento = null!;

        internal ManejadorMovimiento() {
            _repoAlmacen = RepoAlmacen.Instancia;
            _repoProducto = RepoProducto.Instancia;
            _repoPresentacionProducto = RepoPresentacionProducto.Instancia;
            _repoMovimiento = RepoMovimiento.Instancia;
        }

        internal void Manejar(EventoProductoRegistrado e) {
            const long IdTipoMovimiento = 11; // Carga Inicial
            var almacen = _repoAlmacen.ObtenerPorId(e.IdAlmacenDestino);

            if (almacen != null) {
                if (e.Producto != null) {
                    var movimiento = new Movimiento {
                        Id = 0,
                        IdProducto = e.Producto.Id,
                        CostoUnitario = e.Producto.ObtenerCostoUnitario(),
                        IdAlmacenOrigen = almacen.Id,
                        IdAlmacenDestino = 0,
                        Estado = EstadoMovimientoEnum.Completado,
                        FechaCreacion = DateTime.Now,
                        SaldoInicial = 0,
                        FechaTermino = DateTime.Now,
                        CantidadMovida = e.Cantidad,
                        SaldoFinal = e.Cantidad,
                        IdTipoMovimiento = IdTipoMovimiento,
                        IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                        Notas = $"Carga inicial del producto {e.Producto.Nombre} al almacen {almacen.Nombre}",
                    };

                    _repoMovimiento.Adicionar(movimiento);

                    AgregadorEventos.Publicar(new EventoInventarioModificado() {
                        Producto = e.Producto,
                        AlmacenOrigen = null!,
                        AlmacenDestino = almacen,
                        Cantidad = e.Cantidad
                    });
                }
            }
        }

        internal void Manejar(EventoVentaRegistrada e) {
            const long IdTipoMovimiento = 2;
            var almacen = _repoAlmacen.ObtenerPorId(e.IdAlmacenOrigen);

            if (almacen != null) {
                foreach (var detalle in e.Detalles) {
                    var producto = _repoProducto.ObtenerPorId(detalle.IdProducto);

                    if (producto != null) {
                        var inventarioProducto = _repoInventario.Buscar(FiltroBusquedaInventario.IdProducto)
                                .resultadosBusqueda
                                .FirstOrDefault(r => r.entidadBase.IdAlmacen.Equals(almacen.Id))
                                .entidadBase;

                        if (inventarioProducto != null) {
                            var presentacionProducto = _repoPresentacionProducto.ObtenerPorId(detalle.IdPresentacion);
                            var cantidad = presentacionProducto != null
                                ? detalle.Cantidad * presentacionProducto.Cantidad
                                : detalle.Cantidad;

                            var movimiento = new Movimiento() {
                                Id = 0,
                                IdProducto = producto.Id,
                                CostoUnitario = producto.ObtenerCostoUnitario(),
                                IdAlmacenOrigen = almacen.Id,
                                IdAlmacenDestino = 0,
                                Estado = EstadoMovimientoEnum.Completado,
                                FechaCreacion = DateTime.Now,
                                SaldoInicial = inventarioProducto.Cantidad,
                                FechaTermino = DateTime.Now,
                                CantidadMovida = cantidad, // Conversión en unidades base
                                SaldoFinal = inventarioProducto.Cantidad - cantidad,
                                IdTipoMovimiento = IdTipoMovimiento,
                                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                Notas = $"Descarga automática por venta {e.Venta.NumeroFacturaTicket}."
                            };

                            _repoMovimiento.Adicionar(movimiento);

                            AgregadorEventos.Publicar(new EventoInventarioModificado() {
                                Producto = producto,
                                AlmacenOrigen = almacen,
                                AlmacenDestino = null!,
                                Cantidad = cantidad
                            });
                        }
                    }
                }
            }
        }

        internal void Manejar(EventoVentaAnulada evento) {
            const long IdTipoMovimiento = 3;
            var almacen = _repoAlmacen.ObtenerPorId(evento.IdAlmacenDestino);

            if (almacen != null) {
                foreach (var detalle in evento.Detalles) {
                    var producto = _repoProducto.ObtenerPorId(detalle.IdProducto);

                    if (producto != null) {
                        var inventarioProducto = _repoInventario.Buscar(FiltroBusquedaInventario.IdProducto)
                                .resultadosBusqueda
                                .FirstOrDefault(r => r.entidadBase.IdAlmacen.Equals(almacen.Id))
                                .entidadBase;

                        if (inventarioProducto != null) {
                            var presentacionProducto = _repoPresentacionProducto.ObtenerPorId(detalle.IdPresentacion);
                            var cantidad = presentacionProducto != null
                                ? detalle.Cantidad * presentacionProducto.Cantidad
                                : detalle.Cantidad;

                            var movimiento = new Movimiento() {
                                Id = 0,
                                IdProducto = producto.Id,
                                CostoUnitario = producto.ObtenerCostoUnitario(),
                                IdAlmacenOrigen = 0,
                                IdAlmacenDestino = almacen.Id,
                                Estado = EstadoMovimientoEnum.Completado,
                                FechaCreacion = DateTime.Now,
                                SaldoInicial = inventarioProducto.Cantidad,
                                FechaTermino = DateTime.Now,
                                CantidadMovida = cantidad,
                                SaldoFinal = inventarioProducto.Cantidad + cantidad,
                                IdTipoMovimiento = IdTipoMovimiento,
                                IdCuentaUsuario = ContextoSeguridad.UsuarioAutenticado?.Id ?? 0,
                                Notas = $"Ingreso automático por anulación de venta {evento.Venta.NumeroFacturaTicket}."
                            };

                            _repoMovimiento.Adicionar(movimiento);

                            AgregadorEventos.Publicar(new EventoInventarioModificado() {
                                Producto = producto,
                                AlmacenOrigen = null!,
                                AlmacenDestino = almacen,
                                Cantidad = cantidad
                            });
                        }
                    }
                }
            }
        }
    }
}
