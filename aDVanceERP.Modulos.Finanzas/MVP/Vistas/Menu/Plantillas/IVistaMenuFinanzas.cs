using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Vistas.Menu.Plantillas;

public interface IVistaMenuFinanzas : IVistaMenu {
    event EventHandler? VerCuentas;
    event EventHandler? VerCajas;
}