using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorTuplaVenta : PresentadorVistaTupla<IVistaTuplaVenta, Core.Modelos.Modulos.Ventas.Venta> {
        public PresentadorTuplaVenta(IVistaTuplaVenta vista, Core.Modelos.Modulos.Ventas.Venta objeto) : base(vista, objeto) { }
    }
}