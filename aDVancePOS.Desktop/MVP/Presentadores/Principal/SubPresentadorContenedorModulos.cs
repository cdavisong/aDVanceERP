using aDVancePOS.Desktop.MVP.Vistas.ContenedorModulos;
using aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos;

namespace aDVancePOS.Desktop.MVP.Presentadores.Principal;

public partial class PresentadorPrincipal {
    private PresentadorContenedorModulos? _contenedorModulos;

    private void InicializarVistaContenedorModulos() {
        _contenedorModulos = new PresentadorContenedorModulos(Vista, new VistaContenedorModulos());
        _contenedorModulos.Vista.CambioModulo += delegate { Vista.Menus.Ocultar(true); };

        Vista.Vistas?.Registrar("vistaContenedorModulos", _contenedorModulos.Vista);
    }

    private void MostrarVistaContenedorModulos(object? sender, EventArgs e) {
        if (_contenedorModulos == null)
            return;

        _contenedorModulos.Vista.Restaurar();
        _contenedorModulos.Vista.Mostrar();

        // TODO: Mostrar el botón de sub-menu para usuarios
        //Vista.BtnSubmenuUsuarioDisponible = true;
    }
}