using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaRegistroMovimiento : IVistaRegistro {
        Producto? Producto { get; set; }
        Almacen? AlmacenOrigen { get; set; }
        Almacen? AlmacenDestino { get; set; }
        DateTime Fecha { get; set; }
        decimal CantidadMovida { get; set; }
        TipoMovimiento? TipoMovimiento { get; set; }
        string Notas { get; set; }

        event EventHandler? RegistrarProducto;

        void ActualizarInformacionProductoSeleccionado(Producto producto);
        void CargarProductos(Producto[] productos);
        void CargarAlmacenes(Almacen[] almacenes);
        void CargarTiposMovimientos(TipoMovimiento[] tiposMovimientos);
    
    }
}