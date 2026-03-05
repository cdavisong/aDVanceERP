using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Compra.Interfaces;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorTuplaCompra : PresentadorVistaTupla<IVistaTuplaCompra, Core.Modelos.Modulos.Compra.Compra> {
        public PresentadorTuplaCompra(IVistaTuplaCompra vista, Core.Modelos.Modulos.Compra.Compra entidad) : base(vista, entidad) { }
    }
}