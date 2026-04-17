using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaClasificacion : PresentadorVistaTupla<IVistaTuplaClasificacion, ClasificacionProducto> {
        public PresentadorTuplaClasificacion(IVistaTuplaClasificacion vista, ClasificacionProducto entidad) : base(vista, entidad) {
            vista.EditarDatosTupla += MostrarVistaEdicionClasificacion;
        }

        private void MostrarVistaEdicionClasificacion(object? sender, EventArgs e) {
            if (sender is not long id)
                return;

            var entidad = RepoClasificacionProducto.Instancia.ObtenerPorId(id);

            AgregadorEventos.Publicar("MostrarVistaEdicionClasificacion", AgregadorEventos.SerializarPayload(entidad));
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionClasificacion;

            base.Dispose();
        }
    }
}