using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorTuplaUnidadMedida : PresentadorVistaTupla<IVistaTuplaUnidadMedida, UnidadMedida> {
        public PresentadorTuplaUnidadMedida(IVistaTuplaUnidadMedida vista, UnidadMedida entidad) : base(vista, entidad) {
            vista.EditarDatosTupla += MostrarVistaEdicionUnidadMedida;
        }

        private void MostrarVistaEdicionUnidadMedida(object? sender, EventArgs e) {
            if (sender is not long id)
                return;

            var entidad = RepoUnidadMedida.Instancia.ObtenerPorId(id);

            AgregadorEventos.Publicar(new EventoMostrarVistaEdicionUnidadMedida() { 
                UnidadMedida = entidad!
            });
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionUnidadMedida;

            base.Dispose();
        }
    }
}