using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Documentos;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorGestionProductos : PresentadorVistaGestion<PresentadorTuplaProducto, IVistaGestionProductos, IVistaTuplaProducto, Producto, RepoProducto, FiltroBusquedaProducto> {
        public PresentadorGestionProductos(IVistaGestionProductos vista) : base(vista) {
            vista.GenerarCatalogoProductos += OnGenerarCatalogoProductos;

            RegistrarEntidad += OnRegistrarProducto;
            EditarEntidad += OnEditarProducto;

            AgregadorEventos.Suscribir("MostrarVistaGestionProductos", OnMostrarVistaGestionProductos);
            AgregadorEventos.Suscribir("HabilitarDeshabilitarProducto", OnHabilitarDeshabilitarProducto);
        }

        private void OnRegistrarProducto(object? sender, EventArgs e) {
            if (RepoAlmacen.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo producto porque no hay almacenes registrados en el sistema. Por favor, registre al menos un almacén antes de continuar.", TipoNotificacion.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroProducto", string.Empty);
        }

        private void OnEditarProducto(object? sender, Producto e) {
            AgregadorEventos.Publicar("MostrarVistaEdicionProducto", AgregadorEventos.SerializarPayload(new object[] { e, sender }));
        }

        private void OnMostrarVistaGestionProductos(string obj) {
            Vista.CargarFiltroAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre).Prepend("Todos los almacenes")]);
            Vista.CargarFiltrosBusqueda(UtilesBusquedaProducto.FiltroBusquedaProducto);
            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void OnGenerarCatalogoProductos(object? sender, EventArgs e) {
            var docCatalogoProductos = new DocCatalogoComercial();

            docCatalogoProductos.GenerarDocumento();
        }

        private void OnHabilitarDeshabilitarProducto(string obj) {
            var idProductoSeleccionado = _tuplasEntidades.FirstOrDefault(t => t.EstadoSeleccion)?.Vista.Id ?? 0;

            if (idProductoSeleccionado != 0) {
                var estado = RepoProducto.Instancia.HabilitarDeshabilitarProducto(idProductoSeleccionado);

                ActualizarResultadosBusqueda();

                CentroNotificaciones.MostrarNotificacion($"El producto ha sido {(estado ? "habilitado" : "deshabilitado")} satisfactoriamente.", TipoNotificacion.Info);
            }
        }

        protected override PresentadorTuplaProducto ObtenerValoresTupla(Producto entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaProducto(new VistaTuplaProducto(), entidad);
            var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
            var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, entidad.Id.ToString());

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Codigo = entidad.Codigo ?? string.Empty;
            presentadorTupla.Vista.FechaUltimoMovimiento = inventarioProducto.cantidad > 0 ? inventarioProducto.resultadosBusqueda.Min(inv => inv.entidadBase.UltimaActualizacion) : DateTime.MinValue;
            presentadorTupla.Vista.NombreAlmacen = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos") ? "-" : Vista.NombreAlmacen;
            presentadorTupla.Vista.NombreProducto = entidad.Nombre ?? string.Empty;
            presentadorTupla.Vista.Descripcion = entidad.Descripcion ?? "No hay descripción disponible";
            presentadorTupla.Vista.CostoUnitario = entidad.Categoria == CategoriaProducto.ProductoTerminado ? entidad.CostoProduccionUnitario : entidad.CostoAdquisicionUnitario;
            presentadorTupla.Vista.PrecioVentaBase = entidad.PrecioVentaBase;
            presentadorTupla.Vista.UnidadMedida = unidadMedidaProducto?.Abreviatura ?? "U";
            presentadorTupla.Vista.Stock = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos") ? inventarioProducto.resultadosBusqueda.Sum(inv => inv.entidadBase.Cantidad) : inventarioProducto.resultadosBusqueda.Find(inv => RepoAlmacen.Instancia.ObtenerPorId(inv.entidadBase.IdAlmacen)?.Nombre.Equals(Vista.NombreAlmacen) ?? false).entidadBase?.Cantidad ?? 0;
            presentadorTupla.Vista.MovimientoPositivoStock += delegate (object? sender, EventArgs args) {
                var nombreAlmacen = sender as string;
                var objetoPos = new object[] { entidad, "+" };

                AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoPos));
            };
            presentadorTupla.Vista.MovimientoNegativoStock += delegate (object? sender, EventArgs args) {
                var nombreAlmacen = sender as string;
                var objetoNeg = new object[] { entidad, "-" };

                AgregadorEventos.Publicar("MostrarVistaRegistroMovimiento", AgregadorEventos.SerializarPayload(objetoNeg));
            };

            return presentadorTupla;
        }
    }
}