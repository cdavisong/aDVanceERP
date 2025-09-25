using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.QR.Plantillas;

public interface IVistaQR : IVistaBase {
    Image? QR { get; set; }

    void CargarCodigoQR(string? datos);
}