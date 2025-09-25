using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Seguridad.MVP.Vistas.CuentaUsuario.Plantillas;

namespace aDVanceERP.Core.Seguridad.MVP.Presentadores;

public class PresentadorTuplaCuentaUsuario : PresentadorVistaTupla<IVistaTuplaCuentaUsuario, CuentaUsuario> {
    public PresentadorTuplaCuentaUsuario(IVistaTuplaCuentaUsuario vista, CuentaUsuario objeto) : base(vista, objeto) { }
}