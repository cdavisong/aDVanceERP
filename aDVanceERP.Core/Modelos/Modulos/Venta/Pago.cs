using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Ventas {
    public sealed class Pago : IEntidadBaseDatos {
        public Pago() {
            MontoPagado = 0.0m;
            EstadoPago = EstadoPagoEnum.Pendiente;
        }

        public Pago(long id, long idVenta, MetodoPagoEnum metodoPago, decimal montoPagado,
                   DateTime? fechaPagoCliente, DateTime? fechaConfirmacionPago,
                   EstadoPagoEnum estadoPago) {
            Id = id;
            IdVenta = idVenta;
            MetodoPago = metodoPago;
            MontoPagado = montoPagado;
            FechaPagoCliente = fechaPagoCliente;
            FechaConfirmacionPago = fechaConfirmacionPago;
            EstadoPago = estadoPago;
        }

        public long Id { get; set; }
        public long IdVenta { get; set; }
        public MetodoPagoEnum MetodoPago { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime? FechaPagoCliente { get; set; }
        public DateTime? FechaConfirmacionPago { get; set; }
        public EstadoPagoEnum EstadoPago { get; set; }
    }

    public enum MetodoPagoEnum {
        Efectivo,
        [Display(Name = "Transferencia Bancaria")]
        TransferenciaBancaria
    }

    public enum EstadoPagoEnum {
        Pendiente,
        Confirmado,
        Fallido,
        Anulado
    }

    public enum FiltroBusquedaPago {
        Todos,
        Id,
        IdVenta,
        Estado
    }

    public static class UtilesBusquedaPago {
        public static object[] FiltroBusquedaPago = {
            "Todos los pagos",
            "Identificador de BD",
            "Identificador de la venta",
            "Estado del pago"
        };
    }
}