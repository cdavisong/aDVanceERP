using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    internal interface IVistaProductoCarrito : IVistaBase {
        long IdProducto { get; set; }
        long IdPresentacion { get; set; }
        string Nombre { get; set; }
        string Clasificacion { get; set; }
        string Codigo { get; set; }
        decimal PrecioVenta { get; set; }
        decimal Cantidad { get; set; }

        event EventHandler<UnidadMedida>? CambioPresentacion;
        event EventHandler<long>? ProductoSeleccionado;

        void CargarPresentaciones(UnidadMedida[] unidadesMedida);
    }
}
