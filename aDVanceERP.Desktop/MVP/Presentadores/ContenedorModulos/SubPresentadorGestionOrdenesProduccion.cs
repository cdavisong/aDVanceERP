using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Core.Seguridad.Utiles;
using aDVanceERP.Core.Utiles.Datos;

using aDVanceERP.Modulos.Taller.Modelos;
using aDVanceERP.Modulos.Taller.Presentadores.OrdenProduccion;
using aDVanceERP.Modulos.Taller.Repositorios;
using aDVanceERP.Modulos.Taller.Vistas.OrdenProduccion;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos {
    public partial class PresentadorModulos {
        private PresentadorGestionOrdenesProduccion? _gestionOrdenesProduccion;

        private void InicializarVistaGestionOrdenesProduccion() {
            _gestionOrdenesProduccion = new PresentadorGestionOrdenesProduccion(new VistaGestionOrdenesProduccion());
            _gestionOrdenesProduccion.OrdenProduccionCerrada += RegistrarNuevoProducto;
            _gestionOrdenesProduccion.OrdenProduccionCerrada += RegistrarMovimientosOrdenProduccionCerrada;
            _gestionOrdenesProduccion.EditarEntidad += MostrarVistaEdicionOrdenProduccion;
            _gestionOrdenesProduccion.Vista.RegistrarEntidad += MostrarVistaRegistroOrdenProduccion;

            Vista.PanelCentral.Registrar(_gestionOrdenesProduccion.Vista);
        }

        private void MostrarVistaGestionOrdenesProduccion(object? sender, EventArgs e) {
            if (_gestionOrdenesProduccion?.Vista == null)
                return;

            _gestionOrdenesProduccion.Vista.CargarFiltrosBusqueda(UtilesBusquedaOrdenProduccion.FiltroBusquedaOrdenProduccion);
            _gestionOrdenesProduccion.Vista.Restaurar();
            _gestionOrdenesProduccion.Vista.Mostrar();

            _gestionOrdenesProduccion.ActualizarResultadosBusqueda();
        }

        private void RegistrarNuevoProducto(object? sender, OrdenProduccion e) {
            using (var repoProducto = new RepoProducto()) {
                var productoExistente = repoProducto.Buscar(FiltroBusquedaProducto.Nombre, e.NombreProducto);

                if (productoExistente.cantidad > 0)
                    return;

                var idDetalleProducto = 0L;

                using (var repoDetalleProducto = new RepoDetalleProducto()) {
                    var detalleProducto = new DetalleProducto() {
                        Id = 0,
                        IdUnidadMedida = UtilesUnidadMedida.ObtenerIdUnidadMedida("Unidad").Result,
                        Descripcion = "No hay descripción disponible"
                    };

                    idDetalleProducto = repoDetalleProducto.Adicionar(detalleProducto);
                }

                var producto = new Producto() {
                    Id = 0,
                    Categoria = CategoriaProducto.ProductoTerminado,
                    Nombre = e.NombreProducto,
                    Codigo = UtilesCodigoBarras.GenerarEan13(e.NombreProducto),
                    IdDetalleProducto = idDetalleProducto,
                    IdProveedor = 0,
                    PrecioCompra = 0,
                    CostoProduccionUnitario = e.PrecioUnitario,
                    PrecioVentaBase = e.PrecioUnitario
                };

                repoProducto.Adicionar(producto);
            }
        }

        private void RegistrarMovimientosOrdenProduccionCerrada(object? sender, OrdenProduccion e) {
            var producto = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, e.NombreProducto).resultados.FirstOrDefault(p => p.Nombre.Equals(e.NombreProducto));
            var almacenDestino = RepoAlmacen.Instancia.ObtenerPorId(e.IdAlmacen);
            var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, producto.Id.ToString()).resultados.FirstOrDefault(i => i.IdAlmacen.Equals(e.IdAlmacen));
            var tipoMovimientoProducto = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Gasto material").resultados.FirstOrDefault();
            var saldoFinalProducto = inventarioProducto.Cantidad + (e.Cantidad * (tipoMovimientoProducto?.Efecto == EfectoMovimiento.Carga ? 1 : -1));

            // Actualizar el costo unitario de producción en el producto correspondiente
            UtilesProducto.ActualizarCostoProduccionUnitario(producto?.Id ?? 0, e.PrecioUnitario);

            // Movimiento de materiales utilizados en la orden de producción
            using (var repoOrdenMateriaPrima = new RepoOrdenMateriaPrima()) {
                var materiasPrimas = repoOrdenMateriaPrima.Buscar(FiltroBusquedaOrdenMateriaPrima.OrdenProduccion, e.Id.ToString()).resultados;

                if (materiasPrimas != null && materiasPrimas.Count() > 0) {
                    using (var repoMovimiento = new RepoMovimiento()) {
                        foreach (var materiaPrima in materiasPrimas) {
                            var inventarioMateriaPrima = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, materiaPrima.IdProducto.ToString()).resultados.FirstOrDefault(i => i.IdAlmacen.Equals(materiaPrima.IdAlmacen));
                            var tipoMovimientoMateriaPrima = RepoTipoMovimiento.Instancia.Buscar(FiltroBusquedaTipoMovimiento.Nombre, "Gasto material").resultados.FirstOrDefault();
                            var saldoFinalMateriaPrima = inventarioMateriaPrima.Cantidad + (materiaPrima.Cantidad * (tipoMovimientoMateriaPrima?.Efecto == EfectoMovimiento.Carga ? 1 : -1));

                            repoMovimiento.Adicionar(new Movimiento(
                                0,
                                materiaPrima.IdProducto,
                                materiaPrima.CostoUnitario,
                                materiaPrima.CostoUnitario * materiaPrima.Cantidad,
                                materiaPrima.IdAlmacen,
                                0,
                                materiaPrima.FechaRegistro,
                                EstadoMovimiento.Completado,
                                DateTime.MinValue,
                                inventarioMateriaPrima?.Cantidad ?? 0,
                                materiaPrima.Cantidad,
                                saldoFinalMateriaPrima,
                                tipoMovimientoMateriaPrima?.Id ?? 0,
                                UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0
                            ));

                            // Disminuir el cantidad de materiales utilizados en la orden de producción
                            RepoInventario.Instancia.ModificarInventario(
                                materiaPrima.IdProducto,
                                materiaPrima.IdAlmacen,
                                0,
                                materiaPrima.Cantidad
                            );
                        }
                    }
                }
            }

            // Movimiento generado por la orden de producción
            using (var repoMovimiento = new RepoMovimiento()) {
                repoMovimiento.Adicionar(new Movimiento(
                    0,
                    producto.Id,
                    e.PrecioUnitario,
                    e.CostoTotal,
                    0,
                    e.IdAlmacen,
                    e.FechaCierre ?? DateTime.Now,
                    EstadoMovimiento.Completado,
                    DateTime.MinValue,
                    inventarioProducto?.Cantidad ?? 0,
                    e.Cantidad,
                    saldoFinalProducto,
                    tipoMovimientoProducto?.Id ?? 0,
                    UtilesCuentaUsuario.UsuarioAutenticado?.Id ?? 0
                ));
            }

            // Aumentar el cantidad del producto terminado en el almacén seleccionado
            RepoInventario.Instancia.ModificarInventario(
                producto.Id,
                0,
                e.IdAlmacen,
                e.Cantidad
            );
        }
    }
}