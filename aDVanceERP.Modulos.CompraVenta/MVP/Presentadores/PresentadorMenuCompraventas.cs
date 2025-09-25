using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Menu.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class PresentadorMenuCompraventas : PresentadorVistaBase<IVistaMenuVentas> {
    public PresentadorMenuCompraventas(IVistaMenuVentas vista) : base(vista) { }

    public override void Dispose() {
        //...
    }
}