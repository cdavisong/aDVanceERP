using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Modelos {
    public class OrdenMateriaPrima : IEntidadBaseDatos {
        public OrdenMateriaPrima() {
            IdOrdenProduccion = 0;
            IdProducto = 0; // Materia prima consumida
            Cantidad = 0.0m;
            CostoUnitario = 0.0m;
            Total = 0.0m;
            FechaRegistro = DateTime.Now;
        }

        public OrdenMateriaPrima(long id, long idOrdenProduccion, long idAlmacen, long idProducto, decimal cantidad, 
            decimal costoUnitario, decimal costoTotal) {
            Id = id;
            IdOrdenProduccion = idOrdenProduccion;
            IdAlmacen = idAlmacen; // Almacén donde se registra el consumo
            IdProducto = idProducto; // Materia prima consumida
            Cantidad = cantidad;
            CostoUnitario = costoUnitario;
            Total = costoTotal;
            FechaRegistro = DateTime.Now;
        }

        public long Id { get; set; }
        public long IdOrdenProduccion { get; set; }
        public long IdAlmacen { get; set; } // Almacén donde se registra el consumo
        public long IdProducto { get; set; } // Materia prima consumida
        public decimal Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }

    public enum FiltroBusquedaOrdenMateriaPrima {
        Todos,
        Id,
        OrdenProduccion,
        Producto,
        FechaRegistro
    }

    public static class UtilesBusquedaOrdenMateriaPrima {
        public static object[] FiltroBusquedaOrdenMateriaPrima =
        {
            "Todos los materiales consumidos",
            "Identificador de BD",
            "Orden de producción asociada",
            "Material utilizado",
            "Fecha de registro"
        };
    }
}