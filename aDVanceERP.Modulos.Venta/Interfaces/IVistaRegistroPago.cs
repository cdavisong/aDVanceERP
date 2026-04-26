using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaRegistroPago : IVistaRegistro {
        Core.Modelos.Modulos.Venta.Venta? Venta { get; set; }
        string NumeroFacturaVenta { get; set; }
        DateTime FechaPagoCliente { get; set; }
        public CanalPagoEnum MetodoPago { get; set; }
        decimal MontoPagado { get; set; }
        string NumeroTelefonoRemitente { get; set; }
        string NumeroTransaccion { get; set; }
        decimal MontoPendiente { get; set; }

        void CargarFacturasVentasPendientes(string[] numerosFacturasPendientes);
        void CargarMetodosPago(string[] metodosPago);
    }
}