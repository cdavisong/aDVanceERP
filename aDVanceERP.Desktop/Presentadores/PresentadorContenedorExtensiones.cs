using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Desktop.Interfaces;

namespace aDVanceERP.Desktop.Presentadores {
    public class PresentadorContenedorExtensiones : PresentadorVistaBase<IVistaContenedorExtensiones> {
        public PresentadorContenedorExtensiones(IVistaContenedorExtensiones vista) : base(vista) {
            AgregadorEventos.Suscribir<EventoMostrarVistaContenedorExtensiones>(OnMostrarVistaContenedorExtensiones);
        }

        private void OnMostrarVistaContenedorExtensiones(EventoMostrarVistaContenedorExtensiones e) {
            Vista.Restaurar();
            Vista.Mostrar();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir<EventoMostrarVistaContenedorExtensiones>(OnMostrarVistaContenedorExtensiones);

            Vista.Cerrar();
        }
    }
}
