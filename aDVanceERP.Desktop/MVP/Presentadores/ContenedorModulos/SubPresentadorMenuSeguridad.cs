using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Menu;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorMenuSeguridad? _menuSeguridad;

    private void InicializarVistaMenuSeguridad() {
        _menuSeguridad = new PresentadorMenuSeguridad(new VistaMenuSeguridad());
        _menuSeguridad.Vista.VerCuentasUsuarios += MostrarVistaGestionCuentasUsuarios;
        _menuSeguridad.Vista.VerRolesUsuarios += MostrarVistaGestionRolesUsuarios;
        _menuSeguridad.Vista.CambioMenu += delegate { 
            Vista.PanelCentral?.OcultarTodos(); 
        };

        VistaPrincipal.BarraTitulo.Registrar(_menuSeguridad.Vista);
    }

    private void MostrarVistaMenuSeguridad(object? sender, EventArgs e) {
        if (_menuSeguridad == null)
            return;

        _menuSeguridad.Vista.Restaurar();
        _menuSeguridad.Vista.Mostrar();
        _menuSeguridad.Vista.MostrarCaracteristicaInicial();
    }
}