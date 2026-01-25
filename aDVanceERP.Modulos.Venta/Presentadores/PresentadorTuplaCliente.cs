using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores;

public class PresentadorTuplaCliente : PresentadorVistaTupla<IVistaTuplaCliente, Cliente> {
    public PresentadorTuplaCliente(IVistaTuplaCliente vista, Cliente objeto) : base(vista, objeto) { }
}