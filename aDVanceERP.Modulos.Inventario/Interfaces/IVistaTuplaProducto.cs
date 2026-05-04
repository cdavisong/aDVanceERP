using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaTuplaProducto : IVistaTupla {
        long Id { get; set; }
        Almacen? Almacen { get; set; }
        string Codigo { get; set; }
        DateTime FechaUltimoMovimiento { get; set; }
        string NombreDescripcion { get; set; }
        decimal CostoUnitario { get; set; }
        decimal PrecioVentaBase { get; set; }
        int Presentaciones { get; set; }
        decimal Stock { get; set; }
        UnidadMedida? UnidadMedida { get; set; }
        
        event EventHandler? GestionarPresentaciones;
        event EventHandler? MovimientoCarga;
        event EventHandler? MovimientoDescarga;
    }
}