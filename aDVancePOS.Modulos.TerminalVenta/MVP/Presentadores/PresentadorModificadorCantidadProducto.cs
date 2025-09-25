using aDVanceERP.Core.MVP.Presentadores;

using aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Presentadores {
    public class PresentadorModificadorCantidadProducto : PresentadorBase<IVistaModificadorCantidadProducto> {
        public PresentadorModificadorCantidadProducto(IVistaModificadorCantidadProducto vista) : base(vista) {
            
        }

        public override void Dispose() {
            //...
        }
    }
}
