using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaRegistroPago : IVistaRegistro {
        string NumeroFacturaVenta { get; set; }
        DateTime FechaPagoCliente { get; set; }
        public MetodoPagoEnum MetodoPago { get; set; }
        decimal MontoPagado { get; set; }
        bool EstadoPendiente { get; set; }
        string NumeroConfirmacion { get; set; }
        string NumeroTransaccion { get; set; }

        void CargarFacturasVentasPendientes(string[] numerosFacturasPendientes);
        void CargarMetodosPago(string[] metodosPago);
    }
}