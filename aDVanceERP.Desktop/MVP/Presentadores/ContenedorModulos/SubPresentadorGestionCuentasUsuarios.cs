using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.MVP.Presentadores;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.CuentaUsuario;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorGestionCuentasUsuarios? _gestionCuentasUsuarios;

    private async void InicializarVistaGestionCuentasUsuarios() {
        _gestionCuentasUsuarios = new PresentadorGestionCuentasUsuarios(new VistaGestionCuentasUsuarios());
        _gestionCuentasUsuarios.EditarEntidad += MostrarVistaEdicionCuentaUsuario;
        _gestionCuentasUsuarios.Vista.RegistrarEntidad += MostrarVistaRegistroCuentaUsuario;

        Vista.PanelCentral.Registrar(_gestionCuentasUsuarios.Vista);
    }

    private  void MostrarVistaGestionCuentasUsuarios(object? sender, EventArgs e) {
        if (_gestionCuentasUsuarios?.Vista == null)
            return;

        _gestionCuentasUsuarios.Vista.CargarFiltrosBusqueda(UtilesBusquedaCuentaUsuario.FiltroBusquedaBusquedaCuentaUsuario);
        _gestionCuentasUsuarios.Vista.Restaurar();
        _gestionCuentasUsuarios.Vista.Mostrar();

        _gestionCuentasUsuarios.ActualizarResultadosBusqueda();
    }
}