using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Taller.Interfaces;

namespace aDVanceERP.Modulos.Taller.Presentadores.OrdenProduccion {
    public class PresentadortuplaOrdenProduccion : PresentadorVistaTupla<IVistaTuplaOrdenProduccion, Core.Modelos.Modulos.Taller.OrdenProduccion> {
        public PresentadortuplaOrdenProduccion(IVistaTuplaOrdenProduccion vista, Core.Modelos.Modulos.Taller.OrdenProduccion objeto) : base(vista, objeto) {
        }
    }
}