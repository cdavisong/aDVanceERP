using aDVanceERP.Core.Modelos.Modulos.Caja;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.CajaRegistradora.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Presentadores {
    internal class PresentadorTuplaTurno : PresentadorVistaTupla<IVistaTuplaTurno, CajaTurno> {
        public PresentadorTuplaTurno(IVistaTuplaTurno vista, CajaTurno entidad) : base(vista, entidad) {
        }
    }
}
