using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores.Menu;

public class PresentadorMenuInventario : PresentadorVistaBase<IVistaMenuInventario> {
    public PresentadorMenuInventario(IVistaMenuInventario vista) : base(vista) {
        AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
        AgregadorEventos.Suscribir("MostrarVistaMenuInventario", OnMostrarVistaMenuInventario);
    }

    private void OnEventoCambioMenu(string obj) {
        Vista.Ocultar();
    }

    private void OnMostrarVistaMenuInventario(string obj) {
        Vista.Restaurar();
        Vista.Mostrar();
        Vista.SeleccionarVistaInicial();
    }

    public override void Dispose() {
        //...
    }
}