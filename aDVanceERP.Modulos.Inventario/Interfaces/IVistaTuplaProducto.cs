using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaTuplaProducto : IVistaTupla {
        long Id { get; set; }
        string NombreAlmacen { get; set; }
        string Codigo { get; set; }
        DateTime FechaUltimoMovimiento { get; set; }
        string NombreDescripcion { get; set; }
        decimal CostoUnitario { get; set; }
        decimal PrecioVentaBase { get; set; }
        int Presentaciones { get; set; }
        decimal Stock { get; set; }
        string UnidadMedida { get; set; }
        
        event EventHandler? GestionarPresentaciones;
        event EventHandler? MovimientoPositivoStock;
        event EventHandler? MovimientoNegativoStock;
    }
}