using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Presentadores {
    public class PresentadorMenuVenta : PresentadorVistaBase<IVistaMenuVenta> {
        public PresentadorMenuVenta(IVistaMenuVenta vista) : base(vista) {
            AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
            AgregadorEventos.Suscribir("MostrarVistaMenuVenta", OnMostrarVistaMenuVenta);
        }

        private void OnEventoCambioMenu(string obj) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuVenta(string obj) {
            Vista.Restaurar();
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            //...
        }
    }
}