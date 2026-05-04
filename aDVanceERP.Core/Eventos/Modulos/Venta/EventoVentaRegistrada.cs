using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;

namespace aDVanceERP.Core.Eventos.Modulos.Venta {
    public class EventoVentaRegistrada {
        public Modelos.Modulos.Venta.Venta Venta { get; init; } = null!;
        public IEnumerable<DetalleVentaProducto> Detalles { get; init; } = new List<DetalleVentaProducto>();
        public long IdAlmacenOrigen { get; init; } = 0;
    }
}
