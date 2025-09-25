using aDVanceERP.Core.MVP.Vistas.Plantillas;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas;

public interface IVistaTuplaVentaProducto : IVistaTupla {
    string IdProducto { get; set; }
    string NombreProducto { get; set; }
    string PrecioVentaFinal { get; set; }
    string Cantidad { get; set; }
    string Subtotal { get; set; }

    event EventHandler? PrecioVentaModificado;
}

