using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaRegistroPago : IVistaRegistro {
        string NumeroSolicitudCompra { get; set; }
        DateTime FechaPagoProveedor { get; set; }
        public MetodoPagoEnum MetodoPago { get; set; }
        decimal MontoPagado { get; set; }
        string NumeroTelefonoConfirmacion { get; set; }
        string NumeroTransaccion { get; set; }

        void CargarSolicitudesComprasPendientes(string[] numerosSolicitudesPendientes);
        void CargarMetodosPago(string[] metodosPago);
    }
}