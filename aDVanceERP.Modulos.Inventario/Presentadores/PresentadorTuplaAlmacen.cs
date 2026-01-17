using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores;

public class PresentadorTuplaAlmacen : PresentadorVistaTupla<IVistaTuplaAlmacen, Core.Modelos.Modulos.Inventario.Almacen> {
    public PresentadorTuplaAlmacen(IVistaTuplaAlmacen vista, Core.Modelos.Modulos.Inventario.Almacen objeto) : base(vista, objeto) { }
}