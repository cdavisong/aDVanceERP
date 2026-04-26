using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Comun {
    public sealed class Pago : IEntidadBaseDatos {
        public Pago() {
            MontoPagado = 0.0m;
            EstadoPago = EstadoPagoEnum.Pendiente;
        }

        public Pago(long id, long idCompra, long idVenta, CanalPagoEnum metodoPago, decimal montoPagado,
                   DateTime? fechaPagoCliente, DateTime? fechaConfirmacionPago,
                   EstadoPagoEnum estadoPago) {
            Id = id;
            IdCompra = idCompra;
            IdVenta = idVenta;
            MetodoPago = metodoPago;
            MontoPagado = montoPagado;
            FechaPago = fechaPagoCliente;
            FechaConfirmacionPago = fechaConfirmacionPago;
            EstadoPago = estadoPago;
        }

        public long Id { get; set; }
        public long IdCompra { get; set; }
        public long IdVenta { get; set; }
        public CanalPagoEnum MetodoPago { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaConfirmacionPago { get; set; }
        public EstadoPagoEnum EstadoPago { get; set; }
    }

    public enum CanalPagoEnum {
        Efectivo,
        [Display(Name = "Transferencia")]
        TransferenciaBancaria,
        Mixto,
        [Display(Name = "N/A")]
        NA
    }

    public enum EstadoPagoEnum {
        Pendiente,
        Confirmado,
        Fallido,
        Anulado
    }

    public enum FiltroBusquedaPago {
        Todos,
        [Display(Name = "ID")]
        Id,
        [Display(Name = "ID Compra / Venta")]
        IdCompraVenta,
        Estado
    }

    public static class UtilesBusquedaPago {
        public static object[] FiltroBusquedaPagoCompra = {
            "Todos los pagos",
            "Identificador de BD",
            "Identificador de la compra",
            "Estado del pago"
        };

        public static object[] FiltroBusquedaPagoVenta = {
            "Todos los pagos",
            "Identificador de BD",
            "Identificador de la venta",
            "Estado del pago"
        };
    }
}