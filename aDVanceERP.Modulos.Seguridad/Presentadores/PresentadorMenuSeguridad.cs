using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorMenuSeguridad : PresentadorVistaBase<IVistaMenuSeguridad> {
        public PresentadorMenuSeguridad(IVistaMenuSeguridad vista) : base(vista) { 
            AgregadorEventos.Suscribir("EventoCambioMenu", OnEventoCambioMenu);
            AgregadorEventos.Suscribir("MostrarVistaMenuSeguridad", OnMostrarVistaMenuSeguridad);
        }

        private void OnEventoCambioMenu(string obj) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuSeguridad(string obj) {
            Vista.Restaurar();  
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            //...
        }
    }
}