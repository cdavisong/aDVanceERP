using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion;
using aDVanceERP.Core.Seguridad.Utiles;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorSeguridad; 

public partial class PresentadorContenedorSeguridad {
    private PresentadorAprobacionUsuario _aprobacionUsuario;

    public event EventHandler? UsuarioAutenticado;

    private void InicializarVistaAprobacionUsuario() {
        _aprobacionUsuario = new PresentadorAprobacionUsuario(new VistaAprobacionUsuario());
        _aprobacionUsuario.Vista.CambiarDeUsuario += MostrarVistaAutenticacionUsuario;

        Vista.Vistas?.Registrar("vistaAprobacionUsuario", _aprobacionUsuario.Vista);
    }

    private void MostrarVistaAprobacionUsuario(object? sender, EventArgs e) {
        _aprobacionUsuario.Vista.Mostrar();
    }

    private void VerificarAprobacionUsuario(object? sender, EventArgs e) {
        if (UtilesCuentaUsuario.UsuarioAutenticado == null)
            return;

        if (UtilesCuentaUsuario.UsuarioAutenticado != null && UtilesCuentaUsuario.UsuarioAutenticado.Aprobado)
            UsuarioAutenticado?.Invoke(sender, e);

        else MostrarVistaAprobacionUsuario(sender, e);
    }
}