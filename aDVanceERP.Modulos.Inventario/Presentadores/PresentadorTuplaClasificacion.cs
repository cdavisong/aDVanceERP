using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
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
            var clasificacionProducto = RepoClasificacionProducto.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaEdicionClasificacion() {
                ClasificacionProducto = clasificacionProducto!
            });
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionClasificacion;

            base.Dispose();
        }
    }
}