using aDVanceERP.Core.MVP.Vistas.Plantillas;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas; 

public interface IVistaModificadorCantidadProducto : IVista {
    decimal CantidadProducto { get; set; }
}