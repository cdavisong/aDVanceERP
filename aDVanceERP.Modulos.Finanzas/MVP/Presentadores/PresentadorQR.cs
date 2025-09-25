using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.QR.Plantillas;

namespace aDVanceERP.Modulos.Finanzas.MVP.Presentadores;

public class PresentadorQR : PresentadorVistaBase<IVistaQR> {
    public PresentadorQR(IVistaQR vista) : base(vista) {
    }

    public override void Dispose() {
        
    }
}