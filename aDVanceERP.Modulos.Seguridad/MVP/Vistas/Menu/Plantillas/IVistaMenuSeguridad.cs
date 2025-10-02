using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.MVP.Vistas.Menu.Plantillas;

public interface IVistaMenuSeguridad : IVistaMenu {
    event EventHandler? VerCuentasUsuarios;
    event EventHandler? VerRolesUsuarios;
}