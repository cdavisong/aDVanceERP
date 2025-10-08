using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.Autenticacion;

public class PresentadorAprobacionUsuario : PresentadorVistaBase<IVistaAprobacionUsuario> {
    public PresentadorAprobacionUsuario(IVistaAprobacionUsuario vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaAprobacionUsuario", OnMostrarVistaAprobacionUsuario);
    }

    private void OnMostrarVistaAprobacionUsuario(string obj) {
        var cuentaUsuario = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Seguridad.CuentaUsuario>(obj);

        Vista.NombreUsuario = cuentaUsuario.Nombre ?? "usuario";
        Vista.Mostrar();
    }

    public override void Dispose() {
        AgregadorEventos.Desuscribir("MostrarVistaAprobacionUsuario", OnMostrarVistaAprobacionUsuario);
    }
}