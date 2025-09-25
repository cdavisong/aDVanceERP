using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion;
using aDVanceERP.Core.Seguridad.Utiles;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorSeguridad; 

public partial class PresentadorSeguridad {
    private PresentadorAprobacionUsuario? _aprobacionUsuario;

    private void InicializarVistaAprobacionUsuario() {
        _aprobacionUsuario = new PresentadorAprobacionUsuario(new VistaAprobacionUsuario());
        _aprobacionUsuario.Vista.CambiarDeUsuario += MostrarVistaAutenticacionUsuario;

        Vista.PanelCentral.Registrar(_aprobacionUsuario.Vista);
    }

    private void MostrarVistaAprobacionUsuario(object? sender, EventArgs e) {
        if (_aprobacionUsuario == null)
            return;

        _aprobacionUsuario.Vista.Mostrar();
    }

    private void VerificarAprobacionUsuario(object? sender, EventArgs e) {
        if (UtilesCuentaUsuario.UsuarioAutenticado == null)
            return;

        if (UtilesCuentaUsuario.UsuarioAutenticado != null && UtilesCuentaUsuario.UsuarioAutenticado.Aprobado) {
            AgregadorEventos.Publicar("EventoUsuarioAutenticado", string.Empty);
        }
        else MostrarVistaAprobacionUsuario(sender, e);
    }
}