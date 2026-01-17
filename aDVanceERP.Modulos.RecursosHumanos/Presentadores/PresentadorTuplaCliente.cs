using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorTuplaCliente : PresentadorVistaTupla<IVistaTuplaCliente, Cliente> {
    public PresentadorTuplaCliente(IVistaTuplaCliente vista, Cliente objeto) : base(vista, objeto) { }
}