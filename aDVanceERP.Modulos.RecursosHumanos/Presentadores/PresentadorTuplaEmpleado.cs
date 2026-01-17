using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores {
    public class PresentadorTuplaEmpleado : PresentadorVistaTupla<IVistaTuplaEmpleado, Empleado> {
        public PresentadorTuplaEmpleado(IVistaTuplaEmpleado vista, Empleado entidad) : base(vista, entidad) { }
    }
}
