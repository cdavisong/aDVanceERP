using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Core.Eventos.Modulos.Inventario {
    public class EventoMovimientoRegistrado {
        public Movimiento Movimiento { get; set; } = null!;
    }
}
