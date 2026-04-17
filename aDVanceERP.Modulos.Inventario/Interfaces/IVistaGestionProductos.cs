using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaGestionProductos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaProducto>, INavegadorTuplasEntidades {
        Almacen? Almacen { get; }
        decimal ValorTotalInventario { get; }

        event EventHandler? GenerarCatalogoProductos;

        void CargarFiltroAlmacenes(Almacen[] almacenes);
    }
}