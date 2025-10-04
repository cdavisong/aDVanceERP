using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Vistas.RolUsuario.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.RolUsuario;

public class PresentadorTuplaRolUsuario : PresentadorVistaTupla<IVistaTuplaRolUsuario, Core.Modelos.Modulos.Seguridad.RolUsuario> {
    public PresentadorTuplaRolUsuario(IVistaTuplaRolUsuario vista, Core.Modelos.Modulos.Seguridad.RolUsuario objeto) : base(vista, objeto) { }
}