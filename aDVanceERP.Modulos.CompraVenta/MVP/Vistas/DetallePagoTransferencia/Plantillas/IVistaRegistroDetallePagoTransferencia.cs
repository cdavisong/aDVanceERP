using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetallePagoTransferencia.Plantillas;

public interface IVistaRegistroDetallePagoTransferencia : IVistaRegistro {
    string Alias { get; set; }
    string NumeroConfirmacion { get; set; }
    string NumeroTransaccion { get; set; }
    bool RecordarNumeroConfirmacion { set; }
    Image? QR { get; set; }

    void CargarAliasTarjetas(string[] aliasTarjetas);
    void CargarCodigoQR(string datos);
}