using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorGestionProductos : PresentadorVistaGestion<PresentadorTuplaProducto, IVistaGestionProductos,
    IVistaTuplaProducto, Producto, RepoProducto, FiltroBusquedaProducto> {
    public PresentadorGestionProductos(IVistaGestionProductos vista) : base(vista) {
        vista.HabilitarDeshabilitarProducto += IntercambiarHabilitacionProducto;
        vista.EditarEntidad += delegate {
            Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
        };
    }

    public event EventHandler? MovimientoPositivoStock;
    public event EventHandler? MovimientoNegativoStock;

    protected override PresentadorTuplaProducto ObtenerValoresTupla(Producto entidad) {
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

    public override void ActualizarResultadosBusqueda() {
        // Cambiar la visibilidad de los botones
        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;

        base.ActualizarResultadosBusqueda();
    }

    private void IntercambiarHabilitacionProducto(object? sender, EventArgs e) {
        // 1. Filtrar primero las tuplas seleccionadas para evitar procesamiento innecesario
        var tuplasSeleccionadas = _tuplasEntidades.Where(t => t.EstadoSeleccion).ToList();

        if (!tuplasSeleccionadas.Any()) {
            Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
            return;
        }

        // 2. Mover la instancia de RepoDetalleProducto fuera del bucle
        using (var repoDetalleProducto = new RepoDetalleProducto()) {
            foreach (var tupla in tuplasSeleccionadas) {
                // 3. Actualizar el producto 1 vez por tupla
                RepoDetalleProducto.HabilitarDeshabilitarProducto(tupla.Entidad.Id);
            }
        }

        Vista.MostrarBtnHabilitarDeshabilitarProducto = false;
        ActualizarResultadosBusqueda();
    }

    private void CambiarVisibilidadBtnHabilitacionProducto(object? sender, Producto e) {
        Vista.MostrarBtnHabilitarDeshabilitarProducto = _tuplasEntidades.Any(t => t.EstadoSeleccion);
    }
}