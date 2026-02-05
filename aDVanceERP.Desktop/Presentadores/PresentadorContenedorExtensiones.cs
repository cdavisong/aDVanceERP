using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Desktop.Interfaces;

namespace aDVanceERP.Desktop.Presentadores {
    public class PresentadorContenedorExtensiones : PresentadorVistaBase<IVistaContenedorExtensiones> {
        public PresentadorContenedorExtensiones(IVistaContenedorExtensiones vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaContenedorExtensiones", OnMostrarVistaContenedorExtensiones);
        }

        private void OnMostrarVistaContenedorExtensiones(string obj) {
            Vista.Restaurar();
            Vista.Mostrar();
        }

        public override void Dispose() {
            throw new NotImplementedException();
        }
    }
}
