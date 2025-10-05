using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Presentadores.Comun;

public class PresentadorVistaNotificacion : PresentadorVistaBase<IVistaNotificacion> {
    private readonly Notificacion _modelo;

    public PresentadorVistaNotificacion(IVistaNotificacion vista, Notificacion modelo) : base(vista) {
        _modelo = modelo;
    }

    public override void Dispose() {
        //...
    }
}