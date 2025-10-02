using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.RolUsuario.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.MVP.Presentadores;

public class PresentadorTuplaRolUsuario : PresentadorVistaTupla<IVistaTuplaRolUsuario, RolUsuario> {
    public PresentadorTuplaRolUsuario(IVistaTuplaRolUsuario vista, RolUsuario objeto) : base(vista, objeto) { }
}