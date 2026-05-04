using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Core.Eventos.Modulos.Inventario {
    public class EventoInventarioModificado {
        public Producto Producto { get; set; } = null!;
        public Almacen AlmacenOrigen { get; set; } = null!;
        public Almacen AlmacenDestino { get; set; } = null!;
        public decimal Cantidad { get; set; }
    }
}
