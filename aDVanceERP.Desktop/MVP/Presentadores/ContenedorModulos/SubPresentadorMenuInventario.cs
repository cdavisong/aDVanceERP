using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Menu;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorMenuInventario? _menuInventario;

    private void InicializarVistaMenuInventario() {
        _menuInventario = new PresentadorMenuInventario(new VistaMenuInventario());
        _menuInventario.Vista.VerProductos += MostrarVistaGestionProductos;
        _menuInventario.Vista.VerMovimientos += MostrarVistaGestionMovimientos;
        _menuInventario.Vista.VerAlmacenes += MostrarVistaGestionAlmacenes;
        _menuInventario.Vista.CambioMenu += delegate { Vista.PanelCentral?.OcultarTodos(); };

        VistaPrincipal.BarraTitulo.Registrar(_menuInventario.Vista);
    }

    private void MostrarVistaMenuInventario(object? sender, EventArgs e) {
        if (_menuInventario == null)
            return;

        _menuInventario.Vista.Restaurar();
        _menuInventario.Vista.Mostrar();
        _menuInventario.Vista.MostrarCaracteristicaInicial();
    }
}