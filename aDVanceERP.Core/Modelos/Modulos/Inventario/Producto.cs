using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class Producto : IEntidadBaseDatos {
        public Producto() {
            Categoria = CategoriaProducto.Mercancia;
            Nombre = "N/A";
            Codigo = "N/A";
            Descripcion = "N/A";
            IdUnidadMedida = 0;
            IdClasificacionProducto = 1; // Valor por defecto acorde a la BD
            EsVendible = true;
            CostoAdquisicionUnitario = 0.0m;
            CostoProduccionUnitario = 0.0m;
            ImpuestoVentaPorcentaje = 10.00m; // Valor por defecto acorde a la BD
            MargenGananciaDeseado = 0.00m; // Valor por defecto acorde a la BD
            PrecioVentaBase = 0.0m;
            Activo = true; // Valor por defecto acorde a la BD
            RutaImagen = null;
        }

        public Producto(long id, CategoriaProducto categoria, string nombre, string? codigo, long idProveedor,
            string descripcion, long idUnidadMedida, long idClasificacionProducto, bool esVendible,
            decimal costoAdquisicionUnitario, decimal costoProduccionUnitario,
            decimal impuestoVentaPorcentaje, decimal margenGananciaDeseado, decimal precioVentaBase, bool activo,
            string? rutaImagen = null) // RutaImagen puede ser nula
        {
            Id = id;
            Categoria = categoria;
            Nombre = nombre;
            Codigo = codigo;
            IdProveedor = idProveedor;
            Descripcion = descripcion;
            IdUnidadMedida = idUnidadMedida;
            IdClasificacionProducto = idClasificacionProducto;
            EsVendible = esVendible;
            CostoAdquisicionUnitario = costoAdquisicionUnitario;
            CostoProduccionUnitario = costoProduccionUnitario;
            ImpuestoVentaPorcentaje = impuestoVentaPorcentaje;
            MargenGananciaDeseado = margenGananciaDeseado;
            PrecioVentaBase = precioVentaBase;
            Activo = activo;
            RutaImagen = rutaImagen;
        }

        public long Id { get; set; }
        public string? RutaImagen { get; set; }
        public CategoriaProducto Categoria { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public long IdProveedor { get; set; }
        public string Descripcion { get; set; } = "";
        public long IdUnidadMedida { get; set; } = 0;
        public long IdClasificacionProducto { get; set; } = 1;
        public bool EsVendible { get; set; } = true;
        public decimal CostoAdquisicionUnitario { get; set; } = 0.0m;
        public decimal CostoProduccionUnitario { get; set; } = 0.0m;
        public decimal ImpuestoVentaPorcentaje { get; set; } = 10.00m;
        public decimal MargenGananciaDeseado { get; set; } = 0.00m;
        public decimal PrecioVentaBase { get; set; }
        public bool Activo { get; set; } = true;
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
}