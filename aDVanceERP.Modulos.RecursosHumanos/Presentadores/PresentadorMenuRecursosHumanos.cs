using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorMenuRecursosHumanos : PresentadorVistaBase<IVistaMenuRecursosHumanos> {
    public PresentadorMenuRecursosHumanos(IVistaMenuRecursosHumanos vista) : base(vista) { }

    public override void Dispose() {
        //...
    }
}