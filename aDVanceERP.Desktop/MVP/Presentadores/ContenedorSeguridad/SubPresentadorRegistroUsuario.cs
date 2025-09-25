using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorSeguridad; 

public partial class PresentadorSeguridad {
    private PresentadorRegistroUsuario? _registroUsuario;

    private void InicializarVistaRegistroUsuario() {
        _registroUsuario = new PresentadorRegistroUsuario(new VistaRegistroUsuario());
        _registroUsuario.MostrarVistaAutenticacionUsuario += MostrarVistaAutenticacionUsuario;
        _registroUsuario.UsuarioRegistrado += MostrarVistaAutenticacionUsuario;
        _registroUsuario.EntidadRegistradaActualizada += MostrarVistaAprobacionUsuario;

        Vista.PanelCentral.Registrar(_registroUsuario.Vista);
    }

    private void MostrarVistaRegistroUsuario(object? sender, EventArgs e) {
        if (_registroUsuario == null)
            return;

        _registroUsuario.Vista.Restaurar();
        _registroUsuario.Vista.Mostrar();
    }
}