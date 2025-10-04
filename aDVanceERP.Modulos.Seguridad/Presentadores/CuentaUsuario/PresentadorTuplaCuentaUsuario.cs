using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Vistas.CuentaUsuario.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.CuentaUsuario;

public class PresentadorTuplaCuentaUsuario : PresentadorVistaTupla<IVistaTuplaCuentaUsuario, aDVanceERP.Core.Modelos.Modulos.Seguridad.CuentaUsuario>
{
    public PresentadorTuplaCuentaUsuario(IVistaTuplaCuentaUsuario vista, aDVanceERP.Core.Modelos.Modulos.Seguridad.CuentaUsuario objeto) : base(vista, objeto) { }
}