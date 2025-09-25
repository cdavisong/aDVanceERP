using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario;

public class Producto : IEntidadBaseDatos {
    public Producto() {
        Categoria = CategoriaProducto.Mercancia;
        Nombre = "Producto genérico";
    }

    public Producto(long id, CategoriaProducto categoria, string nombre, string codigo, long idDetalleProducto, long idProveedor,
        long idTipoMateriaPrima, bool esVendible, decimal precioCompra, decimal costoProduccionUnitario, decimal precioVentaBase) {
        Id = id;
        Categoria = categoria;
        Nombre = nombre;
        Codigo = codigo;
        IdDetalleProducto = idDetalleProducto;
        IdProveedor = idProveedor;
        IdTipoMateriaPrima = idTipoMateriaPrima;
        EsVendible = esVendible;
        PrecioCompra = precioCompra;
        CostoProduccionUnitario = costoProduccionUnitario;
        PrecioVentaBase = precioVentaBase;
    }

    public long Id { get; set; }
    public CategoriaProducto Categoria { get; set; }
    public string Nombre { get; set; }
    public string? Codigo { get; set; }
    public long IdDetalleProducto { get; set; }
    public long IdProveedor { get; set; }
    public long IdTipoMateriaPrima { get; set; } = 0; // Solo para materias primas
    public bool EsVendible { get; set; } = true;
    public decimal PrecioCompra { get; set; }
    public decimal CostoProduccionUnitario { get; set; } = 0.0m; // Solo para productos terminados
    public decimal PrecioVentaBase { get; set; }
}

public enum FiltroBusquedaProducto {
    Todos,
    Id,
    Codigo,
    Nombre,
    Descripcion,
    Inactivos
}

public static class UtilesBusquedaProducto {
    public static object[] FiltroBusquedaProducto = {
        "Todos los productos",
        "Identificador de BD",
        "Código del producto",
        "Nombre del producto",
        "Descripción del producto",
        "Productos inactivos"
    };
}

public static class UtilesCategoriaProducto {
    public static object[] CategoriaProducto = {
        "Mercancía (Producto revendido)",
        "Producto terminado",
        "Materia prima"
    };

    public static string[] DescripcionesProducto = {
        "Artículos comprados a proveedores para ser vendidos directamente sin modificaciones. No requieren proceso de fabricación",
        "Artículos elaborados por la empresa a partir de materias primas y mano de obra. Su costo incluye materiales, actividades de producción y costos asociados",
        "Insumos o materiales utilizados para fabricar productos terminados. Pueden venderse directamente si están configurados para ello"
    };
}