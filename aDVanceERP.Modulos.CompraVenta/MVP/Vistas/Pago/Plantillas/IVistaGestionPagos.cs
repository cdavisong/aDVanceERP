using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Pago.Plantillas;

public interface IVistaGestionPagos : IVistaContenedor, IGestorEntidades {
    void AdicionarPago(long id, long idVenta, string metodoPago, decimal monto);
}