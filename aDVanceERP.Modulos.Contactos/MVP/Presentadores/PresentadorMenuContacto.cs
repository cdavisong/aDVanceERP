using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores;

public class PresentadorMenuContacto : PresentadorVistaBase<IVistaMenuContacto> {
    public PresentadorMenuContacto(IVistaMenuContacto vista) : base(vista) { }

    public override void Dispose() {
        //...
    }
}