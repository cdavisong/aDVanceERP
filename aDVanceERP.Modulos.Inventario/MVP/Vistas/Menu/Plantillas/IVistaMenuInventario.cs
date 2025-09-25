using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.MVP.Vistas.Menu.Plantillas;

public interface IVistaMenuInventario : IVistaMenu {
    event EventHandler? VerProductos;
    event EventHandler? VerMovimientos;
    event EventHandler? VerAlmacenes;
}