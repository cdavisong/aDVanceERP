using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores;

public class PresentadorMenuFinanzas : PresentadorVistaBase<IVistaMenuFinanzas> {
    public PresentadorMenuFinanzas(IVistaMenuFinanzas vista) : base(vista) { }

    public override void Dispose() {
        //...
    }
}