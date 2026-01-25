using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.RecursosHumanos.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores {
    public class PresentadorMenuMaestros : PresentadorVistaBase<IVistaMenuMaestros> {
        public PresentadorMenuMaestros(IVistaMenuMaestros vista) : base(vista) {
            AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
            AgregadorEventos.Suscribir("MostrarVistaMenuMaestrosRecursosHumanos", OnMostrarVistaMenuMaestros);
        }

        private void OnEventoCambioMenu(string obj) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuMaestros(string obj) {
            Vista.Restaurar();
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            //...
        }
    }
}