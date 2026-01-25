using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorMenuMaestros : PresentadorVistaBase<IVistaMenuMaestros> {
        public PresentadorMenuMaestros(IVistaMenuMaestros vista) : base(vista) {
            AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
            AgregadorEventos.Suscribir("MostrarVistaMenuMaestrosInventario", OnMostrarVistaMenuMaestros);
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