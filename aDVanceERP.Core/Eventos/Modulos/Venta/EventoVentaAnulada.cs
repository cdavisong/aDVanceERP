using aDVanceERP.Core.Modelos.Modulos.Venta;

namespace aDVanceERP.Core.Eventos.Modulos.Venta {
    public class EventoVentaAnulada {
        public Modelos.Modulos.Venta.Venta Venta { get; init; } = null!;
        public IEnumerable<DetalleVentaProducto> Detalles { get; init; } = new List<DetalleVentaProducto>();
        public long IdAlmacenDestino { get; init; } = 0;
    }
}
