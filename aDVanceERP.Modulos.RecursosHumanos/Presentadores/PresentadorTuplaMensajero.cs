using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorTuplaMensajero : PresentadorVistaTupla<IVistaTuplaMensajero, Mensajero> {
    public PresentadorTuplaMensajero(IVistaTuplaMensajero vista, Mensajero objeto) : base(vista, objeto) { }
}