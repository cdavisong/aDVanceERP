using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Presentadores.Producto;

public class PresentadorTuplaProducto : PresentadorVistaTupla<IVistaTuplaProducto, Core.Modelos.Modulos.Inventario.Producto> {
    public PresentadorTuplaProducto(IVistaTuplaProducto vista, Core.Modelos.Modulos.Inventario.Producto objeto) : base(vista, objeto) { }
}