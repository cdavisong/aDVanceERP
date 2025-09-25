using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Movimiento.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorTuplaMovimiento : PresentadorVistaTupla<IVistaTuplaMovimiento, Movimiento> {
    public PresentadorTuplaMovimiento(IVistaTuplaMovimiento vista, Movimiento objeto) : base(vista, objeto) { }
}