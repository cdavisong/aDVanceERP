using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Modelos {
    public enum EstadoOrdenProduccion {
        Abierta,
        EnProceso,
        Cerrada,
        Cancelada
    }

    public class OrdenProduccion : IEntidadBaseDatos {
        public OrdenProduccion() {
            NumeroOrden = "-";
            FechaApertura = DateTime.Now;
            Estado = EstadoOrdenProduccion.Abierta;
            Observaciones = "No hay observaciones para la orden de producción actual";
        }

        public OrdenProduccion(long id, string numeroOrden, DateTime fechaApertura, DateTime? fechaCierre, long idAlmacen, string nombreProducto,
            decimal cantidad, EstadoOrdenProduccion estado, string observaciones, decimal costoTotal, decimal precioUnitario,
            decimal margenGanancia) {
            Id = id;
            NumeroOrden = numeroOrden;
            FechaApertura = fechaApertura;
            FechaCierre = fechaCierre;
            IdAlmacen = idAlmacen; // Almacén destino luego de la producción
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            Estado = estado;
            Observaciones = observaciones;
            CostoTotal = costoTotal;
            PrecioUnitario = precioUnitario;
            MargenGanancia = margenGanancia;
        }

        public long Id { get; set; }
        public string NumeroOrden { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public long IdAlmacen { get; set; } // Almacén destino luego de la producción
        public string NombreProducto { get; set; } // Producto terminado
        public decimal Cantidad { get; set; }
        public EstadoOrdenProduccion Estado { get; set; }
        public string Observaciones { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal MargenGanancia { get; set; }
    }

    public enum FiltroBusquedaOrdenProduccion {
        Todas,
        NumeroOrden,
        Producto,
        Estado,
        FechaApertura,
        FechaCierre
    }

    public static class UtilesBusquedaOrdenProduccion {
        public static object[] FiltroBusquedaOrdenProduccion =
        {
            "Todas las órdenes",
            "Número de orden",
            "Producto terminado",
            "Estado de producción",
            "Fecha de apertura",
            "Fecha de cierre"
        };

        public static object[] EstadosOrdenProduccion =
        {
            "Abierta",
            "En proceso",
            "Cerrada",
            "Cancelada"
        };
    }
}