using aDVanceERP.Core.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;

using aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVancePOS.Modulos.TerminalVenta.MVP.Presentadores;

public class PresentadorTuplaVentaProducto : PresentadorTuplaBase<IVistaTuplaVentaProducto, Venta> {
    public PresentadorTuplaVentaProducto(IVistaTuplaVentaProducto vista, Venta objeto) : base(vista, objeto) { }
}

