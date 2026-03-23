using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Presentadores {
    internal class PresentadorTuplaDetalleTurno : PresentadorVistaTupla<IVistaTuplaDetalleTurno, CajaMovimiento> {
        public PresentadorTuplaDetalleTurno(IVistaTuplaDetalleTurno vista, CajaMovimiento entidad) : base(vista, entidad) {
        }
    }
}
