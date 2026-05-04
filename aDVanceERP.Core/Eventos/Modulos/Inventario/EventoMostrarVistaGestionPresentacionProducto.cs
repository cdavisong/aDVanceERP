using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Core.Eventos.Modulos.Inventario {
    public class EventoMostrarVistaGestionPresentacionProducto {
        public Almacen Almacen { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
    }
}
