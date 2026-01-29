using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    public class PresentadorTuplaPedido : PresentadorVistaTupla<IVistaTuplaPedido, Pedido> {
        public PresentadorTuplaPedido(IVistaTuplaPedido vista, Pedido entidad) : base(vista, entidad) { }
    }
}