using aDVanceERP.Modulos.Finanzas.MVP.Presentadores;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Menu;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorMenuFinanzas? _menuFinanzas;

    private void InicializarVistaMenuFinanzas() {
        _menuFinanzas = new PresentadorMenuFinanzas(new VistaMenuFinanzas());
        _menuFinanzas.Vista.VerCuentas += MostrarVistaGestionCuentasBancarias;
        _menuFinanzas.Vista.VerCajas += MostrarVistaGestionCajas;
        _menuFinanzas.Vista.CambioMenu += delegate { Vista.PanelCentral?.OcultarTodos(); };

        VistaPrincipal.BarraTitulo.Registrar(_menuFinanzas.Vista);
    }

    private void MostrarVistaMenuFinanzas(object? sender, EventArgs e) {
        if (_menuFinanzas == null)
            return;

        _menuFinanzas.Vista.Restaurar();
        _menuFinanzas.Vista.Mostrar();
        _menuFinanzas.Vista.MostrarCaracteristicaInicial();
    }
}