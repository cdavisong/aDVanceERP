using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorSeguridad; 

public partial class PresentadorContenedorSeguridad {
    private PresentadorRegistroUsuario? _registroUsuario;

    private void InicializarVistaRegistroUsuario() {
        _registroUsuario = new PresentadorRegistroUsuario(new VistaRegistroUsuario());
        _registroUsuario.MostrarVistaAutenticacionUsuario += MostrarVistaAutenticacionUsuario;
        _registroUsuario.UsuarioRegistrado += MostrarVistaAutenticacionUsuario;
        _registroUsuario.DatosRegistradosActualizados += MostrarVistaAprobacionUsuario;

        Vista.Vistas?.Registrar("vistaRegistroUsuario", _registroUsuario.Vista);
    }

    private void MostrarVistaRegistroUsuario(object? sender, EventArgs e) {
        _registroUsuario?.Vista.Restaurar();
        _registroUsuario?.Vista.Mostrar();
    }
}