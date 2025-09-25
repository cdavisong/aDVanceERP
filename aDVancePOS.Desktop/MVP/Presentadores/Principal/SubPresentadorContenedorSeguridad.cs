using aDVancePOS.Desktop.MVP.Presentadores.ContenedorSeguridad;
using aDVancePOS.Desktop.MVP.Vistas.ContenedorSeguridad;

namespace aDVancePOS.Desktop.MVP.Presentadores.Principal;

public partial class PresentadorPrincipal {
    private PresentadorContenedorSeguridad? _contenedorSeguridad;

    private void InicializarVistaContenedorSeguridad() {
        _contenedorSeguridad = new PresentadorContenedorSeguridad(Vista, new VistaContenedorSeguridad());
        _contenedorSeguridad.UsuarioAutenticado += MostrarVistaContenedorModulos;

        Vista.Vistas?.Registrar("vistaContenedorSeguridad", _contenedorSeguridad.Vista);
    }

    private void MostrarVistaContenedorSeguridad(object sender, EventArgs e) {
        _contenedorSeguridad?.Vista.Restaurar();
        _contenedorSeguridad?.Vista.Mostrar();
    }
}