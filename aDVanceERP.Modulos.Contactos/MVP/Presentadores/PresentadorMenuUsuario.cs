using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Usuario.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Presentadores {
    public class PresentadorMenuUsuario : PresentadorVistaBase<IVistaMenuUsuario> {
        public PresentadorMenuUsuario(IVistaMenuUsuario vista) : base(vista) { }

        public override void Dispose() {
            //...
        }
    }
}
