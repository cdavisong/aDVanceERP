using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    internal class PresentadorTuplaVentaPresentacion : PresentadorVistaTupla<IVistaTuplaVentaPresentacion, PrecioPresentacion> {
        public PresentadorTuplaVentaPresentacion(IVistaTuplaVentaPresentacion vista, PrecioPresentacion entidad) : base(vista, entidad) {
        }
    }
}
