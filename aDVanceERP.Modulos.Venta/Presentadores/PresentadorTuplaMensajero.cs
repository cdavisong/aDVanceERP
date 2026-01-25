using aDVanceERP.Core.Modelos.Modulos.Ventas;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorTuplaMensajero : PresentadorVistaTupla<IVistaTuplaMensajero, Mensajero> {
        public PresentadorTuplaMensajero(IVistaTuplaMensajero vista, Mensajero objeto) : base(vista, objeto) { }
    }
}