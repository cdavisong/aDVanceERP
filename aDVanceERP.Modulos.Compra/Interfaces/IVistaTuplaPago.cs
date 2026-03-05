using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaTuplaPago : IVistaTupla {
        public long Id { get; set; }
        long IdCompra { get; set; }
        string NumeroSolicitudCompra { get; set; }
        public MetodoPagoEnum MetodoPago { get; set; }
        string NumeroTelefonoRemitente { get; set; }
        string NumeroTransaccion { get; set; }
        decimal MontoPagado { get; set; }
        DateTime FechaPagoProveedor { get; set; }
        DateTime FechaConfirmacionPago { get; set; }
        EstadoPagoEnum EstadoPago { get; set; }
    }
}