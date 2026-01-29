using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorTuplaVenta : PresentadorVistaTupla<IVistaTuplaVenta, Core.Modelos.Modulos.Venta.Venta> {
        public PresentadorTuplaVenta(IVistaTuplaVenta vista, Core.Modelos.Modulos.Venta.Venta entidad) : base(vista, entidad) { }
    }
}