using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaClasificacion : PresentadorVistaTupla<IVistaTuplaClasificacion, ClasificacionProducto> {
        public PresentadorTuplaClasificacion(IVistaTuplaClasificacion vista, ClasificacionProducto entidad) : base(vista, entidad) {
        }
    }
}