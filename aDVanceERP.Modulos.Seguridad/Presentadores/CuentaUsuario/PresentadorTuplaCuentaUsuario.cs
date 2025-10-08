using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.CuentaUsuario;

public class PresentadorTuplaCuentaUsuario : PresentadorVistaTupla<IVistaTuplaCuentaUsuario, Core.Modelos.Modulos.Seguridad.CuentaUsuario> {
    public PresentadorTuplaCuentaUsuario(IVistaTuplaCuentaUsuario vista, Core.Modelos.Modulos.Seguridad.CuentaUsuario objeto) : base(vista, objeto) { }
}