using aDVanceERP.Core.Seguridad.MVP.Modelos;
using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorGestionRolesUsuarios? _gestionRolesUsuarios;

    private async void InicializarVistaGestionRolesUsuarios() {
        _gestionRolesUsuarios = new PresentadorGestionRolesUsuarios(new VistaGestionRolesUsuarios());
        _gestionRolesUsuarios.EditarEntidad += MostrarVistaEdicionRolUsuario;
        _gestionRolesUsuarios.Vista.RegistrarEntidad += MostrarVistaRegistroRolUsuario;

        Vista.PanelCentral.Registrar(_gestionRolesUsuarios.Vista);
    }

    private void MostrarVistaGestionRolesUsuarios(object? sender, EventArgs e) {
        if (_gestionRolesUsuarios?.Vista == null)
            return;

        _gestionRolesUsuarios.Vista.CargarFiltrosBusqueda(UtilesBusquedaRolUsuario.FiltroBusquedaBusquedaRolUsuario);
        _gestionRolesUsuarios.Vista.Restaurar();
        _gestionRolesUsuarios.Vista.Mostrar();

        _gestionRolesUsuarios.ActualizarResultadosBusqueda();
    }
}