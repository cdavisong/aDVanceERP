using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas;

namespace aDVanceERP.Modulos.Inventario.Presentadores;

public class PresentadorGestionProductos : PresentadorVistaGestion<PresentadorTuplaProducto, IVistaGestionProductos, IVistaTuplaProducto, Producto, RepoProducto, FiltroBusquedaProducto> {
    public PresentadorGestionProductos(IVistaGestionProductos vista) : base(vista) {
        vista.HabilitarDeshabilitarProducto += OnIntercambiarHabilitacionProducto;

        RegistrarEntidad += OnRegistrarProducto;
        EditarEntidad += OnEditarProducto;

        AgregadorEventos.Suscribir("MostrarVistaGestionProductos", OnMostrarVistaGestionProductos);
    }

    private void OnRegistrarProducto(object? sender, EventArgs e) {
        if (RepoAlmacen.Instancia.Cantidad() == 0) {
            CentroNotificaciones.Mostrar("No es posible registrar un nuevo producto porque no hay almacenes registrados en el sistema. Por favor, registre al menos un almacén antes de continuar.", TipoNotificacion.Advertencia);
            return;
        }

        AgregadorEventos.Publicar("MostrarVistaRegistroProducto", string.Empty);
        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
    }

    private void OnEditarProducto(object? sender, Producto e) {
        AgregadorEventos.Publicar("MostrarVistaEdicionProducto", AgregadorEventos.SerializarPayload(new object[] { e, sender }));
        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
    }

    private void OnMostrarVistaGestionProductos(string obj) {
        Vista.CargarFiltroAlmacenes([.. RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.entidadBase.Nombre).Prepend("Todos los almacenes")]);
        Vista.CargarFiltrosBusqueda(UtilesBusquedaProducto.FiltroBusquedaProducto);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    protected override PresentadorTuplaProducto ObtenerValoresTupla(Producto entidad, List<IEntidadBaseDatos> entidadesExtra) {
        var presentadorTupla = new PresentadorTuplaProducto(new VistaTuplaProducto(), entidad);
        var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(entidad.IdUnidadMedida);
        var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, entidad.Id.ToString());

        presentadorTupla.Vista.Id = entidad.Id.ToString();
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
        presentadorTupla.EntidadSeleccionada += CambiarVisibilidadBtnHabilitacionProducto;
        presentadorTupla.EntidadDeseleccionada += CambiarVisibilidadBtnHabilitacionProducto;

        return presentadorTupla;
    }



    private void OnIntercambiarHabilitacionProducto(object? sender, EventArgs e) {
        var tuplaSeleccionada = _tuplasEntidades.Where(t => t.EstadoSeleccion).FirstOrDefault();

        if (tuplaSeleccionada == null) {
            Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
            return;
        }

        var estadoActualProducto = RepoProducto.Instancia.HabilitarDeshabilitarProducto(tuplaSeleccionada.Entidad.Id);

        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;

        ActualizarResultadosBusqueda();

        CentroNotificaciones.Mostrar($"El producto '{tuplaSeleccionada.Entidad.Nombre}' ha sido {(estadoActualProducto ? "habilitado" : "deshabilitado")} satisfactoriamente.", TipoNotificacion.Info);
    }

    public override void ActualizarResultadosBusqueda() {
        // Cambiar la visibilidad de los botones
        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;

        base.ActualizarResultadosBusqueda();
    }

    private void CambiarVisibilidadBtnHabilitacionProducto(object? sender, Producto e) {
        Vista.MostrarBtnHabilitarDeshabilitarProducto = _tuplasEntidades.Any(t => t.EstadoSeleccion);
    }

    protected override void Dispose(bool disposing) {
        Vista.HabilitarDeshabilitarProducto += OnIntercambiarHabilitacionProducto;

        RegistrarEntidad += OnRegistrarProducto;
        EditarEntidad += OnEditarProducto;

        AgregadorEventos.Desuscribir("MostrarVistaGestionProductos", OnMostrarVistaGestionProductos);

        base.Dispose(disposing);
    }
}