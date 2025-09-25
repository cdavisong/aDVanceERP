using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorSeguridad; 

public partial class PresentadorContenedorSeguridad {
    private PresentadorAutenticacionUsuario? _autenticacionUsuario;

    private void InicializarVistaAutenticacionUsuario() {
        _autenticacionUsuario = new PresentadorAutenticacionUsuario(new VistaAutenticacionUsuario());
        _autenticacionUsuario.MostrarVistaRegistroCuentaUsuario += MostrarVistaRegistroUsuario;
        _autenticacionUsuario.UsuarioAutenticado += VerificarAprobacionUsuario;

        Vista.Vistas?.Registrar("vistaAutenticacionUsuario", _autenticacionUsuario.Vista);
    }

    private void MostrarVistaAutenticacionUsuario(object? sender, EventArgs e) {
        var message = sender as string;

        if (string.IsNullOrEmpty(message) || message.Equals("register-user"))
            return;

        _autenticacionUsuario?.Vista.Restaurar();
        _autenticacionUsuario?.Vista.Mostrar();
    }
}