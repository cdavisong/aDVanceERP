using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaMovimiento : PresentadorVistaTupla<IVistaTuplaMovimiento, Core.Modelos.Modulos.Inventario.Movimiento> {
        public PresentadorTuplaMovimiento(IVistaTuplaMovimiento vista, Core.Modelos.Modulos.Inventario.Movimiento objeto) : base(vista, objeto) {
            vista.EditarDatosTupla += MostrarVistaEdicionMovimiento;
        }

        private void MostrarVistaEdicionMovimiento(object? sender, EventArgs e) {
            if (sender is not long id)
                return;

            var entidad = RepoMovimiento.Instancia.ObtenerPorId(id);

            AgregadorEventos.Publicar("MostrarVistaEdicionMovimiento", AgregadorEventos.SerializarPayload(entidad));
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionMovimiento;

            base.Dispose();
        }
    }
}