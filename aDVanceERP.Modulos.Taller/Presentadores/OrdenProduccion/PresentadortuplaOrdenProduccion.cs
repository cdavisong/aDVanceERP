using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Taller.Interfaces;

namespace aDVanceERP.Modulos.Taller.Presentadores.OrdenProduccion
{
    public class PresentadortuplaOrdenProduccion : PresentadorVistaTupla<IVistaTuplaOrdenProduccion, Modelos.OrdenProduccion> {
        public PresentadortuplaOrdenProduccion(IVistaTuplaOrdenProduccion vista, Modelos.OrdenProduccion objeto) : base(vista, objeto) {
        }
    }
}