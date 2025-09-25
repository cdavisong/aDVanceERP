using aDVanceERP.Core.Mensajes.MVP.Modelos;
using aDVanceERP.Core.Mensajes.MVP.Vistas.Notificacion.Plantillas;
using aDVanceERP.Core.Presentadores.Comun;

namespace aDVanceERP.Core.Mensajes.MVP.Presentadores;

public class PresentadorNotificacion : PresentadorVistaBase<IVistaNotificacion> {
    private readonly Notificacion _modelo;

    public PresentadorNotificacion(IVistaNotificacion vista, Notificacion modelo) : base(vista) {
        _modelo = modelo;
    }

    public override void Dispose() {
        //...
    }
}