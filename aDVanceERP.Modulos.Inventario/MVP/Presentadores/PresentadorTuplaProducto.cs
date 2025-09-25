using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto.Plantillas;

namespace aDVanceERP.Modulos.Inventario.MVP.Presentadores;

public class PresentadorTuplaProducto : PresentadorVistaTupla<IVistaTuplaProducto, Producto> {
    public PresentadorTuplaProducto(IVistaTuplaProducto vista, Producto objeto) : base(vista, objeto) { }
}