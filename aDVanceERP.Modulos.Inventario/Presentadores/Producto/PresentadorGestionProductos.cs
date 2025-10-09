using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;
using aDVanceERP.Modulos.Inventario.Vistas.Producto;

namespace aDVanceERP.Modulos.Inventario.Presentadores.Producto;

public class PresentadorGestionProductos : PresentadorVistaGestion<PresentadorTuplaProducto, IVistaGestionProductos, IVistaTuplaProducto, Core.Modelos.Modulos.Inventario.Producto, RepoProducto, FiltroBusquedaProducto> {
    public PresentadorGestionProductos(IVistaGestionProductos vista) : base(vista) {
        vista.HabilitarDeshabilitarProducto += OnIntercambiarHabilitacionProducto;

        RegistrarEntidad += OnRegistrarProducto;
        EditarEntidad += OnEditarProducto;

        AgregadorEventos.Suscribir("MostrarVistaGestionProductos", OnMostrarVistaGestionProductos);
    }


    public event EventHandler? MovimientoPositivoStock;
    public event EventHandler? MovimientoNegativoStock;

    private void OnRegistrarProducto(object? sender, EventArgs e) {
        AgregadorEventos.Publicar("MostrarVistaRegistroProducto", string.Empty);
        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
    }

    private void OnEditarProducto(object? sender, Core.Modelos.Modulos.Inventario.Producto e) {
        AgregadorEventos.Publicar("MostrarVistaEdicionProducto", AgregadorEventos.SerializarPayload(e));
        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
    }

    private void OnMostrarVistaGestionProductos(string obj) {
        Vista.CargarFiltroAlmacenes(RepoAlmacen.Instancia.ObtenerTodos().Select(a => a.Nombre).Prepend("Todos los almacenes").ToArray());
        Vista.CargarFiltrosBusqueda(UtilesBusquedaProducto.FiltroBusquedaProducto);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    protected override PresentadorTuplaProducto ObtenerValoresTupla(Core.Modelos.Modulos.Inventario.Producto entidad) {
        var presentadorTupla = new PresentadorTuplaProducto(new VistaTuplaProducto(), entidad);
        var detalleProducto = RepoDetalleProducto.Instancia.ObtenerPorId(entidad.IdDetalleProducto);
        var unidadMedidaProducto = RepoUnidadMedida.Instancia.ObtenerPorId(detalleProducto?.IdUnidadMedida ?? 0);
        var inventarioProducto = RepoInventario.Instancia.Buscar(FiltroBusquedaInventario.IdProducto, entidad.Id.ToString());

        presentadorTupla.Vista.Id = entidad.Id.ToString();
        presentadorTupla.Vista.Codigo = entidad.Codigo ?? string.Empty;
        presentadorTupla.Vista.FechaUltimoMovimiento = inventarioProducto.cantidad > 0 ? inventarioProducto.entidades.Min(inv => inv.UltimaActualizacion) : DateTime.MinValue;
        presentadorTupla.Vista.NombreAlmacen = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos")
            ? "-"
            : Vista.NombreAlmacen;
        presentadorTupla.Vista.NombreProducto = entidad.Nombre ?? string.Empty;
        presentadorTupla.Vista.Descripcion = detalleProducto?.Descripcion ?? "No hay descripción disponible";
        presentadorTupla.Vista.CostoUnitario = entidad.Categoria == CategoriaProducto.ProductoTerminado ? entidad.CostoProduccionUnitario : entidad.PrecioCompra;
        presentadorTupla.Vista.PrecioVentaBase = entidad.PrecioVentaBase;
        presentadorTupla.Vista.UnidadMedida = unidadMedidaProducto?.Abreviatura ?? "u";
        presentadorTupla.Vista.Stock = string.IsNullOrEmpty(Vista.NombreAlmacen) || Vista.NombreAlmacen.Contains("Todos")
            ? inventarioProducto.entidades.Sum(inv => inv.Cantidad)
            : inventarioProducto.entidades.Find(inv => RepoAlmacen.Instancia.ObtenerPorId(inv.IdAlmacen)?.Nombre.Equals(Vista.NombreAlmacen) ?? false)?.Cantidad ?? 0;
        presentadorTupla.Vista.MovimientoPositivoStock += delegate (object? sender, EventArgs args) {
            var nombreAlmacen = sender as string;
            var objetoPos = new object[] { "+", nombreAlmacen ?? string.Empty, entidad };

            MovimientoPositivoStock?.Invoke(objetoPos, EventArgs.Empty);
        };
        presentadorTupla.Vista.MovimientoNegativoStock += delegate (object? sender, EventArgs args) {
            var nombreAlmacen = sender as string;
            var objetoNeg = new object[] { "-", nombreAlmacen ?? string.Empty, entidad };

            MovimientoNegativoStock?.Invoke(objetoNeg, EventArgs.Empty);
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

        var estadoActualProducto = RepoDetalleProducto.HabilitarDeshabilitarProducto(tuplaSeleccionada.Entidad.IdDetalleProducto);

        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;

        ActualizarResultadosBusqueda();

        CentroNotificaciones.Mostrar($"El producto '{tuplaSeleccionada.Entidad.Nombre}' ha sido {(estadoActualProducto ? "habilitado" : "deshabilitado")} satisfactoriamente.", TipoNotificacion.Info);
    }

    public override void ActualizarResultadosBusqueda() {
        // Cambiar la visibilidad de los botones
        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;

        base.ActualizarResultadosBusqueda();
    }

    private void CambiarVisibilidadBtnHabilitacionProducto(object? sender, Core.Modelos.Modulos.Inventario.Producto e) {
        Vista.MostrarBtnHabilitarDeshabilitarProducto = _tuplasEntidades.Any(t => t.EstadoSeleccion);
    }
}