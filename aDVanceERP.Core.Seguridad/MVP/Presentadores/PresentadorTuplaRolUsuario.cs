using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario.Plantillas;

namespace aDVanceERP.Core.Seguridad.MVP.Presentadores;

public class PresentadorTuplaRolUsuario : PresentadorVistaTupla<IVistaTuplaRolUsuario, RolUsuario> {
    public PresentadorTuplaRolUsuario(IVistaTuplaRolUsuario vista, RolUsuario objeto) : base(vista, objeto) { }
}