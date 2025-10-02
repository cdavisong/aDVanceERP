using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.CuentaUsuario.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.MVP.Presentadores;

public class PresentadorTuplaCuentaUsuario : PresentadorVistaTupla<IVistaTuplaCuentaUsuario, CuentaUsuario> {
    public PresentadorTuplaCuentaUsuario(IVistaTuplaCuentaUsuario vista, CuentaUsuario objeto) : base(vista, objeto) { }
}