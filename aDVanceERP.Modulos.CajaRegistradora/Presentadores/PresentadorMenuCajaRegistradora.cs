using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CajaRegistradora.Presentadores {
    internal class PresentadorMenuCajaRegistradora : PresentadorVistaBase<IVistaMenu> {
        public PresentadorMenuCajaRegistradora(IVistaMenu vista) : base(vista) {
            AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
            AgregadorEventos.Suscribir("MostrarVistaMenuCajaRegistradora", OnMostrarVistaMenuCaja);
        }

        private void OnEventoCambioMenu(string obj) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuCaja(string obj) {
            Vista.Restaurar();
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            //...
        }
    }
}
