using aDVanceERP.Desktop.MVP.Presentadores.ContenedorEstadisticas;
using aDVanceERP.Desktop.MVP.Vistas.ContenedorEstadisticas;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorContenedorEstadisticas? _contenedorEstadisticas;

    private async void InicializarVistaContenedorEstadisticas() {
        _contenedorEstadisticas = new PresentadorContenedorEstadisticas(new VistaContenedorEstadísticas());
        _contenedorEstadisticas.Vista.MostrarVistaGestionProductos += delegate {
            Vista.PresionarBotonModulo(5, EventArgs.Empty);
        };
        _contenedorEstadisticas.Vista.MostrarVistaGestionVentas += delegate {
            Vista.PresionarBotonModulo(6, EventArgs.Empty);
        };

        if (Vista.PanelCentral != null)
            await Task.Run(() => Vista.PanelCentral.Registrar(_contenedorEstadisticas.Vista));
    }

    private void MostrarVistaContenedorEstadisticas(object? sender, EventArgs e) {
        if (_contenedorEstadisticas?.Vista == null)
            return;

        _contenedorEstadisticas.Vista.Restaurar();
        _contenedorEstadisticas.Vista.Mostrar();
        _contenedorEstadisticas.RefrescarEstadísticas();
    }
}