using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorSeguridad; 

public partial class PresentadorSeguridad {
    private PresentadorAutenticacionUsuario? _autenticacionUsuario;

    private void InicializarVistaAutenticacionUsuario() {
        _autenticacionUsuario = new PresentadorAutenticacionUsuario(new VistaAutenticacionUsuario());
        _autenticacionUsuario.MostrarVistaRegistroCuentaUsuario += MostrarVistaRegistroUsuario;
        _autenticacionUsuario.UsuarioAutenticado += VerificarAprobacionUsuario;

        Vista.PanelCentral.Registrar(_autenticacionUsuario.Vista);
    }

    private void MostrarVistaAutenticacionUsuario(object? sender, EventArgs e) {
        var message = sender as string;

        if (string.IsNullOrEmpty(message) || message.Equals("register-user"))
            return;

        if (_autenticacionUsuario == null)
            return;

        _autenticacionUsuario.Vista.Restaurar();
        _autenticacionUsuario.Vista.Mostrar();
    }
}