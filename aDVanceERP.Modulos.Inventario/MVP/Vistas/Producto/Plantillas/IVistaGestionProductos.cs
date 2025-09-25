using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

public interface IVistaGestionProductos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaProducto>,
    INavegadorTuplasEntidades {
    string? NombreAlmacen { get; }
    decimal ValorTotalInventario { get; }
    bool MostrarBtnHabilitarDeshabilitarProducto { get; set; }

    event EventHandler? HabilitarDeshabilitarProducto;

    void CargarNombresAlmacenes(object[] nombresAlmacenes);
}