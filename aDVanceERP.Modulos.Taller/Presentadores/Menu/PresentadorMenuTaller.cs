using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Taller.Interfaces;

namespace aDVanceERP.Modulos.Taller.Presentadores.Menu
{
    public class PresentadorMenuTaller : PresentadorVistaBase<IVistaMenuTaller> {
        public PresentadorMenuTaller(IVistaMenuTaller vista) : base(vista) { }

        public override void Dispose() {
            //...
        }
    }
}