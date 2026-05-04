using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaMovimiento : PresentadorVistaTupla<IVistaTuplaMovimiento, Movimiento> {
        public PresentadorTuplaMovimiento(IVistaTuplaMovimiento vista, Movimiento objeto) : base(vista, objeto) {
            vista.EditarDatosTupla += MostrarVistaEdicionMovimiento;
        }

        private void MostrarVistaEdicionMovimiento(object? sender, EventArgs e) {
           var movimiento = RepoMovimiento.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaEdicionMovimiento() {
                Movimiento = movimiento!
            });
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionMovimiento;

            base.Dispose();
        }
    }
}