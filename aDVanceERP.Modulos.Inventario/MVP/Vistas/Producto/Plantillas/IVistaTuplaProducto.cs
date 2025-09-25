using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

public interface IVistaTuplaProducto : IVistaTupla {
    string Id { get; set; }
    string NombreAlmacen { get; set; }
    string Codigo { get; set; }
    DateTime FechaUltimoMovimiento { get; set; }
    string NombreProducto { get; set; }
    string Descripcion { get; set; }
    decimal CostoUnitario { get; set; }
    decimal PrecioVentaBase { get; set; }
    string UnidadMedida { get; set; }
    decimal Stock { get; set; }

    event EventHandler? MovimientoPositivoStock;
    event EventHandler? MovimientoNegativoStock;
}