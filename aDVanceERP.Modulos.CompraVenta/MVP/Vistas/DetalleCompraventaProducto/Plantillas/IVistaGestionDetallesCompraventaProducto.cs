using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetalleCompraventaProducto.Plantillas;

public interface IVistaGestionDetallesCompraventaProductos : IVistaContenedor, IGestorEntidades {
    void AdicionarProducto(string nombreAlmacen = "", string nombreProducto = "", string cantidad = "");
}