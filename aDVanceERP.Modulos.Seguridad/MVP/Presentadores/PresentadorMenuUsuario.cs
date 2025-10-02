using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.MVP.Presentadores
{
    public class PresentadorMenuUsuario : PresentadorVistaBase<IVistaMenuUsuario> {
        public PresentadorMenuUsuario(IVistaMenuUsuario vista) : base(vista) { }

        public override void Dispose() {
            //...
        }
    }
}
