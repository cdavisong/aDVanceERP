using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorMenuSeguridad : PresentadorVistaBase<IVistaMenuSeguridad> {
        public PresentadorMenuSeguridad(IVistaMenuSeguridad vista) : base(vista) { 
            AgregadorEventos.Suscribir<EventoCambioMenu>(OnEventoCambioMenu);
            AgregadorEventos.Suscribir<EventoMostrarVistaMenuSeguridad>(OnMostrarVistaMenuSeguridad);
        }

        private void OnEventoCambioMenu(EventoCambioMenu e) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuSeguridad(EventoMostrarVistaMenuSeguridad e) {
            Vista.Restaurar();  
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir<EventoCambioMenu>(OnEventoCambioMenu);
            AgregadorEventos.Desuscribir<EventoMostrarVistaMenuSeguridad>(OnMostrarVistaMenuSeguridad);
        }
    }
}