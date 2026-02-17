using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Venta.Interfaces;

namespace aDVanceERP.Modulos.Venta.Presentadores {
    internal class PresentadorTuplaEnvio : PresentadorVistaTupla<IVistaTuplaEnvio, SeguimientoEntrega> {
        public PresentadorTuplaEnvio(IVistaTuplaEnvio vista, SeguimientoEntrega entidad) : base(vista, entidad) { }
    }
}
