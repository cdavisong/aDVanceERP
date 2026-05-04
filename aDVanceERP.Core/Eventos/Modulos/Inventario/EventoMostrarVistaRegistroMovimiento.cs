using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Core.Eventos.Modulos.Inventario {
    public class EventoMostrarVistaRegistroMovimiento {
        public EfectoMovimientoEnum EfectoMovimiento { get; set; } = EfectoMovimientoEnum.Ninguno;
        public Almacen AlmacenOrigen { get; set; } = null!;
        public Almacen AlmacenDestino { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
    }
}
