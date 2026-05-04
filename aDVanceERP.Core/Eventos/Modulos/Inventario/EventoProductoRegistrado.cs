using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Core.Eventos.Modulos.Inventario {
    public class EventoProductoRegistrado {
        public Producto Producto { get; set; } = null!;
        public long IdAlmacenDestino { get; init; } = 0;
        public decimal Cantidad { get; init; } = 0;
    }
}
