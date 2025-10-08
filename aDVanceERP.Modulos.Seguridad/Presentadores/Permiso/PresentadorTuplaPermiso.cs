using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.Permiso;

public class PresentadorTuplaPermiso : PresentadorVistaTupla<IVistaTuplaPermiso, Core.Modelos.Modulos.Seguridad.Permiso> {
    public PresentadorTuplaPermiso(IVistaTuplaPermiso vista, Core.Modelos.Modulos.Seguridad.Permiso entidad) : base(vista, entidad) {
    }
}