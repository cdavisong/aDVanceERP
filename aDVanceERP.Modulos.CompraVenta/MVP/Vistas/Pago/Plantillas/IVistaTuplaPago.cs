using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago.Plantillas;

public interface IVistaTuplaPago : IVistaTupla {
    int Indice {  get; set; }
    string MetodoPago { get; set; }
    string Monto { get; set; }
}