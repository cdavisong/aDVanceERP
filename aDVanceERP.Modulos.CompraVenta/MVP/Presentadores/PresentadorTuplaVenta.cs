using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Venta.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class PresentadorTuplaVenta : PresentadorVistaTupla<IVistaTuplaVenta, Venta> {
    public PresentadorTuplaVenta(IVistaTuplaVenta vista, Venta objeto) : base(vista, objeto) { }
}