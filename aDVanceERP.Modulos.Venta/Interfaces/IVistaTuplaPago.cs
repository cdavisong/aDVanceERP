using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaTuplaPago : IVistaTupla {
        public long Id { get; set; }
        long IdVenta { get; set; }
        string NumeroFacturaVenta { get; set; }
        public MetodoPagoEnum MetodoPago { get; set; }
        string NumeroConfirmacion { get; set; }
        string NumeroTransaccion { get; set; }
        decimal MontoPagado { get; set; }
        DateTime FechaPagoCliente { get; set; }
        DateTime FechaConfirmacionPago { get; set; }
        EstadoPagoEnum EstadoPago { get; set; }
    }
}