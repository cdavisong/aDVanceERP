using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaRegistroVenta : IVistaRegistro {
        DateTime FechaVenta { get; set; }
        Cliente Cliente { get; set; }
        Almacen? AlmacenOrigen { get; }
        string Observaciones { get; set; }

        string NombreProducto { get; }
        decimal DescuentoProducto { get; }
        decimal ImpuestoAdicionalProducto { get; }
        decimal CantidadProducto { get; }
        Producto? ProductoSeleccionado { get; set; }
        FlowLayoutPanel PanelProductosRapidos { get; }

        FlowLayoutPanel PanelCarritoVenta { get; }
        decimal TotalBruto { get; set; }
        decimal TotalDescuento { get; set; }
        decimal ImporteTotal { get; set; }

        CanalPagoEnum CanalPago { get; }
        decimal MontoPagado { get; }
        decimal FaltanteVuelto { get; set; }

        event EventHandler<string>? BuscarProducto;
        event EventHandler? BuscarProductosRapidos;
        event EventHandler? AgregarProductoAlCarrito;
        event EventHandler? AgregarPagoVenta;

        void CargarProductos(Producto[] productos);
        void CargarAlmacenes(Almacen[] almacenes);
    }
}