using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores {
    public class PresentadorTuplaMovimiento : PresentadorVistaTupla<IVistaTuplaMovimiento, Core.Modelos.Modulos.Inventario.Movimiento> {
        public PresentadorTuplaMovimiento(IVistaTuplaMovimiento vista, Core.Modelos.Modulos.Inventario.Movimiento objeto) : base(vista, objeto) { }
    }
}