using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public sealed class Venta : IEntidadBaseDatos {
        public Venta() {
            FechaVenta = DateTime.Now;
            TotalBruto = 0.0m;
            DescuentoTotal = 0.0m;
            ImpuestoTotal = 0.0m;
            ImporteTotal = 0.0m;
            EstadoVenta = EstadoVentaEnum.Pendiente;
            Activo = true;
        }

        public long Id { get; set; }
        public long? IdPedido { get; set; }
        public long IdCliente { get; set; }
        public long? IdEmpleadoVendedor { get; set; }
        public long IdAlmacen { get; set; }
        public string? NumeroFacturaTicket { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalBruto { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal ImpuestoTotal { get; set; }
        public decimal ImporteTotal { get; set; }
        public string? CanalPagoPrincipal { get; set; }
        public EstadoVentaEnum EstadoVenta { get; set; }
        public string? ObservacionesVenta { get; set; }
        public bool Activo { get; set; }
    }

    public enum EstadoVentaEnum {
        Pendiente,
        Completada,
        Anulada,
        Entregada
    }

    public enum FiltroBusquedaVenta {
        Todas,
        Id,
        IdCliente,
        NumeroFactura,
        Estado,
        Inactivos
    }

    public static class UtilesBusquedaVenta {
        public static object[] FiltroBusquedaVenta = {
            "Todas las ventas",
            "Identificador de BD",
            "Identificador del cliente",
            "Número de factura/ticket",
            "Estado de la venta",
            "Ventas inactivas"
        };
    }
}