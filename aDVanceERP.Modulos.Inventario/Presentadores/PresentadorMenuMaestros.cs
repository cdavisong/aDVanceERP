using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorMenuMaestros : PresentadorVistaBase<IVistaMenuMaestros> {
        public PresentadorMenuMaestros(IVistaMenuMaestros vista) : base(vista) {
            AgregadorEventos.Suscribir<EventoCambioMenu>(OnEventoCambioMenu);
            AgregadorEventos.Suscribir<EventoMostrarVistaMenuMaestrosInventario>(OnMostrarVistaMenuMaestros);
        }

        private void OnEventoCambioMenu(EventoCambioMenu e) {
            Vista.Ocultar();
        }

        private void OnMostrarVistaMenuMaestros(EventoMostrarVistaMenuMaestrosInventario e) {
            Vista.Restaurar();
            Vista.Mostrar();
            Vista.SeleccionarVistaInicial();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir<EventoCambioMenu>(OnEventoCambioMenu);
            AgregadorEventos.Desuscribir<EventoMostrarVistaMenuMaestrosInventario>(OnMostrarVistaMenuMaestros);

            Vista.Cerrar();
        }
    }
}