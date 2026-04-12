using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaRegistroMovimiento : IVistaRegistro {
        string NombreProducto { get; set; }
        string? NombreAlmacenOrigen { get; set; }
        string? NombreAlmacenDestino { get; set; }
        DateTime Fecha { get; set; }
        decimal CantidadMovida { get; set; }
        string NombreTipoMovimiento { get; set; }
        string Notas { get; set; }

        void ActualizarInformacionProductoSeleccionado(Producto producto);
        void CargarNombresProductos(string[] nombresProductos);
        void CargarNombresAlmacenes(string[] nombresAlmacenes);
        void CargarTiposMovimientos(TipoMovimiento[] tiposMovimientos);
    
    }
}