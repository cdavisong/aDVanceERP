using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Compra.Interfaces;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorTuplaPago : PresentadorVistaTupla<IVistaTuplaPago, Pago> {
        public PresentadorTuplaPago(IVistaTuplaPago vista, Pago entidad) : base(vista, entidad) { }
    }
}
