using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorMenuInventario : PresentadorVistaBase<IVistaMenuInventario> {
        public PresentadorMenuInventario(IVistaMenuInventario vista) : base(vista) {
            AgregadorEventos.Suscribir<EventoCambioMenu>(OnEventoCambioMenu);
            AgregadorEventos.Suscribir<EventoMostrarVistaMenuInventario>(OnMostrarVistaMenuInventario);
        }

        private void OnEventoCambioMenu(EventoCambioMenu e) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuInventario(EventoMostrarVistaMenuInventario e) {
            Vista.Restaurar();
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir<EventoCambioMenu>(OnEventoCambioMenu);
            AgregadorEventos.Desuscribir<EventoMostrarVistaMenuInventario>(OnMostrarVistaMenuInventario);

            Vista.Cerrar();
        }
    }
}