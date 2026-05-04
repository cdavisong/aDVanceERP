using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Core.Eventos.Modulos.Inventario {
    public class EventoMostrarVistaEdicionProducto {
        public Almacen Almacen { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
        public IEnumerable<Modelos.Modulos.Inventario.Inventario> Inventario { get; set; } = null!;
    }
}
