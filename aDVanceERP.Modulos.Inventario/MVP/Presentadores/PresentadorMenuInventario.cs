using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorMenuInventario : PresentadorVistaBase<IVistaMenuInventario> {
    public PresentadorMenuInventario(IVistaMenuInventario vista) : base(vista) { }

    public override void Dispose() {
        //...
    }
}