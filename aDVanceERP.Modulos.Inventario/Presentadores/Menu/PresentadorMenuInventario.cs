using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores.Menu;

public class PresentadorMenuInventario : PresentadorVistaBase<IVistaMenuInventario>
{
    public PresentadorMenuInventario(IVistaMenuInventario vista) : base(vista) { }

    public override void Dispose()
    {
        //...
    }
}