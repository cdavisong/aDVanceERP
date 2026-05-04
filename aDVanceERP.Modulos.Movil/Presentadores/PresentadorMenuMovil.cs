using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Movil.Interfaces;

namespace aDVanceERP.Modulos.Movil.Presentadores {
    internal class PresentadorMenuMovil : PresentadorVistaBase<IVistaMenuMovil> {
        public PresentadorMenuMovil(IVistaMenuMovil vista) : base(vista) {
            AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
            AgregadorEventos.Suscribir("MostrarVistaMenuMovil", OnMostrarVistaMenuMovil);
        }

        private void OnEventoCambioMenu(string obj) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuMovil(string obj) {
            Vista.Restaurar();
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir("EventoCambioMenu", OnEventoCambioMenu);
            AgregadorEventos.Desuscribir("MostrarVistaMenuMovil", OnMostrarVistaMenuMovil);

            Vista.Cerrar();
        }
    }
}
