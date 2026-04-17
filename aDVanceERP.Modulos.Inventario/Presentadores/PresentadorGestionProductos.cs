using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
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
            vista.RegistrarEntidad += OnRegistrarProducto;
            vista.GenerarCatalogoProductos += OnGenerarCatalogoProductos;

            AgregadorEventos.Suscribir("MostrarVistaGestionProductos", OnMostrarVistaGestionProductos);
        }

        private void OnMostrarVistaGestionProductos(string obj) {
            CargarDatosComunes();

            Vista.Restaurar();
            Vista.Mostrar();

            ActualizarResultadosBusqueda();
        }

        private void CargarDatosComunes() {
            Vista.CargarFiltroAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
            Vista.CargarFiltrosBusqueda([.. EnumExt.ObtenerNombresDescripciones<FiltroBusquedaProducto>()]);
        }

        private void OnRegistrarProducto(object? sender, EventArgs e) {
            if (RepoAlmacen.Instancia.Cantidad() == 0) {
                CentroNotificaciones.MostrarNotificacion("No es posible registrar un nuevo producto porque no hay almacenes registrados en el sistema. Por favor, registre al menos un almacén antes de continuar.", TipoNotificacionEnum.Advertencia);
                return;
            }

            AgregadorEventos.Publicar("MostrarVistaRegistroProducto", string.Empty);
        }

        private void OnGenerarCatalogoProductos(object? sender, EventArgs e) {
            var docCatalogoProductos = new DocCatalogoComercial();

            docCatalogoProductos.GenerarDocumento();
        }

        protected override PresentadorTuplaProducto ObtenerValoresTupla(Producto entidad, List<IEntidadBaseDatos> entidadesExtra) {
            var presentadorTupla = new PresentadorTuplaProducto(new VistaTuplaProducto(), entidad);
            var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
            var (cantidad, resultadosBusqueda) = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, entidad.Id.ToString());
            var presentaciones = RepoPresentacionProducto.Instancia.Buscar(FiltroBusquedaPresentacionProducto.IdProducto, entidad.Id.ToString());

            presentadorTupla.Vista.Id = entidad.Id;
            presentadorTupla.Vista.Codigo = entidad.Codigo ?? string.Empty;
            presentadorTupla.Vista.FechaUltimoMovimiento = cantidad > 0 
                    ? resultadosBusqueda.Min(inv => inv.entidadBase.UltimaActualizacion) 
                    : DateTime.MinValue;
            presentadorTupla.Vista.Almacen = Vista.Almacen;
            presentadorTupla.Vista.NombreDescripcion = string.IsNullOrEmpty(entidad.Nombre)
                ? string.IsNullOrEmpty(entidad.Descripcion)
                    ? "No existe un nombre o descripción disponible para el producto"
                    : entidad.Descripcion
                : $"{entidad.Nombre}{(string.IsNullOrEmpty(entidad.Descripcion) ? "" : $", {entidad.Descripcion}")}";
            presentadorTupla.Vista.CostoUnitario = entidad.Categoria == CategoriaProductoEnum.ProductoTerminado 
                ? entidad.CostoProduccionUnitario 
                : entidad.CostoAdquisicionUnitario;
            presentadorTupla.Vista.PrecioVentaBase = entidad.PrecioVentaBase;
            presentadorTupla.Vista.Presentaciones = presentaciones.cantidad;
            presentadorTupla.Vista.Stock = Vista.Almacen?.Nombre == "Todos"
                ? resultadosBusqueda.Sum(inv => inv.entidadBase.Cantidad)
                : resultadosBusqueda
                    .Find(inv => RepoAlmacen.Instancia
                    .ObtenerPorId(inv.entidadBase.IdAlmacen)?
                    .Nombre
                    .Equals(Vista.Almacen?.Nombre) ?? false)
                    .entidadBase?
                    .Cantidad ?? 0;
            presentadorTupla.Vista.UnidadMedida = unidadMedidaProducto;
            
            return presentadorTupla;
        }
    }
}