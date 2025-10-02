using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.MVP.Presentadores;

public class PresentadorMenuSeguridad : PresentadorVistaBase<IVistaMenuSeguridad> {
    public PresentadorMenuSeguridad(IVistaMenuSeguridad vista) : base(vista) { }

    public override void Dispose() {
        //...
    }
}