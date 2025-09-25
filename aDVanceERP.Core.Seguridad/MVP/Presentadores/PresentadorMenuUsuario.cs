using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Core.Seguridad.MVP.Presentadores
{
    public class PresentadorMenuUsuario : PresentadorVistaBase<IVistaMenuUsuario> {
        public PresentadorMenuUsuario(IVistaMenuUsuario vista) : base(vista) { }

        public override void Dispose() {
            //...
        }
    }
}
