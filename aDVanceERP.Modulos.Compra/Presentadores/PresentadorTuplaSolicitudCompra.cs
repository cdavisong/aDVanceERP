using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Compra.Interfaces;

namespace aDVanceERP.Modulos.Compra.Presentadores {
    internal class PresentadorTuplaSolicitudCompra : PresentadorVistaTupla<IVistaTuplaSolicitudCompra, SolicitudCompra> {
        public PresentadorTuplaSolicitudCompra(IVistaTuplaSolicitudCompra vista, SolicitudCompra entidad) : base(vista, entidad) { }
    }
}