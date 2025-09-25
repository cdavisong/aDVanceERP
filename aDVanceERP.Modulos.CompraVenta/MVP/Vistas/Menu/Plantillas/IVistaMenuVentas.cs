using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Menu.Plantillas;

public interface IVistaMenuVentas : IVistaMenu {
    event EventHandler? VerCompras;
    event EventHandler? VerVentas;
}