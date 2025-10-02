using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.Autenticacion.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.MVP.Presentadores;

public class PresentadorAprobacionUsuario : PresentadorVistaBase<IVistaAprobacionUsuario> {
    public PresentadorAprobacionUsuario(IVistaAprobacionUsuario vista) : base(vista) { }

    public override void Dispose() {
        //...
    }
}