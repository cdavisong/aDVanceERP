using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Taller.Modelos {
    public class OrdenGastoIndirecto : IEntidadBaseDatos {
        public OrdenGastoIndirecto() {
            IdOrdenProduccion = 0;
            Concepto = string.Empty;
            Cantidad = 0.0m;
            Monto = 0.0m;
            Total = 0.0m;
            FechaRegistro = DateTime.Now;
        }

        public OrdenGastoIndirecto(long id, long idOrdenProduccion, string concepto, decimal cantidad,
            decimal monto, decimal costoTotal) {
            Id = id;
            IdOrdenProduccion = idOrdenProduccion;
            Concepto = concepto;
            Cantidad = cantidad;
            Monto = monto;
            Total = costoTotal;
            FechaRegistro = DateTime.Now;
        }

        public long Id { get; set; }
        public long IdOrdenProduccion { get; set; }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Monto { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }

    public enum FiltroBusquedaOrdenGastoIndirecto {
        Todos,
        Id,
        OrdenProduccion,
        Concepto,
        FechaRegistro
    }

    public static class UtilesBusquedaOrdenGastoIndirecto {
        public static object[] FiltroBusquedaOrdenGastoIndirecto =
        {
            "Todos los gastos indirectos",
            "Identificador de BD",
            "Orden de producción asociada",
            "Concepto del gasto",
            "Fecha de registro"
        };
    }
}