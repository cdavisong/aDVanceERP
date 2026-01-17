using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores;

public class PresentadorMenuRecursosHumanos : PresentadorVistaBase<IVistaMenuRecursosHumanos> {
    public PresentadorMenuRecursosHumanos(IVistaMenuRecursosHumanos vista) : base(vista) {
        AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
        AgregadorEventos.Suscribir("MostrarVistaMenuRecursosHumanos", OnMostrarVistaMenuRecursosHumanos);
    }

    private void OnEventoCambioMenu(string obj) {
        Vista.Ocultar();
    }

    private void OnMostrarVistaMenuRecursosHumanos(string obj) {
        Vista.Restaurar();
        Vista.Mostrar();
        Vista.SeleccionarVistaInicial();
    }

    public override void Dispose() {
        //...
    }
}