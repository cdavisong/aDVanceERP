using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorEstadisticasGenerales : PresentadorVistaBase<IVistaEstadisticasGenerales> {
        public PresentadorEstadisticasGenerales(IVistaEstadisticasGenerales vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaEstadisticasGenerales", OnMostrarVistaEstadisticasGenerales);
        }

        private void OnMostrarVistaEstadisticasGenerales(string obj) {
            Vista.Restaurar();
            Vista.Mostrar();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir("MostrarVistaEstadisticasGenerales", OnMostrarVistaEstadisticasGenerales);

            Vista.Cerrar();
        }
    }
}
