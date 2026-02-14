using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public sealed class Venta : IEntidadBaseDatos {
        public Venta() {
            FechaVenta = DateTime.UtcNow;
            TotalBruto = 0.0m;
            DescuentoTotal = 0.0m;
            ImpuestoTotal = 0.0m;
            ImporteTotal = 0.0m;
            EstadoVenta = EstadoVenta.Pendiente;
            Activo = true;
        }

        public Venta(long id, long? idPedido, long idCliente, long? idEmpleadoVendedor, long idAlmacen,
                    string numeroFacturaTicket, DateTime fechaVenta, decimal totalBruto,
                    decimal descuentoTotal, decimal impuestoTotal, decimal importeTotal,
                    string metodoPagoPrincipal, EstadoVenta estadoVenta,
                    string observacionesVenta, bool activo) {
            Id = id;
            IdPedido = idPedido;
            IdCliente = idCliente;
            IdEmpleadoVendedor = idEmpleadoVendedor;
            IdAlmacen = idAlmacen;
            NumeroFacturaTicket = numeroFacturaTicket;
            FechaVenta = fechaVenta;
            TotalBruto = totalBruto;
            DescuentoTotal = descuentoTotal;
            ImpuestoTotal = impuestoTotal;
            ImporteTotal = importeTotal;
            MetodoPagoPrincipal = metodoPagoPrincipal;
            EstadoVenta = estadoVenta;
            ObservacionesVenta = observacionesVenta;
            Activo = activo;
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
        public string? MetodoPagoPrincipal { get; set; }
        public EstadoVenta EstadoVenta { get; set; }
        public string? ObservacionesVenta { get; set; }
        public bool Activo { get; set; }
    }

    public enum EstadoVenta {
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