using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class ProductoProveedor : IEntidadBaseDatos {
    public ProductoProveedor() { }

    public ProductoProveedor(long id, long idProveedor, decimal precioAdquisicion, decimal precioVenta) {
        Id = id;
        IdProveedor = idProveedor;
        PrecioAdquisicion = precioAdquisicion;
        PrecioVenta = precioVenta;
    }

    public long Id { get; set; }
    public long IdProducto { get; set; }
    public long IdProveedor { get; set; }
    public decimal PrecioAdquisicion { get; }
    public decimal PrecioVenta { get; }
}

public enum FiltroBusquedaProductoProveedor {
    Todos,
    Id
}

public static class UtilesBusquedaProductoProveedor {
    public static string[] FiltroBusquedaProducto = {
        "Todos los productos",
        "Identificador de BD"
    };
}