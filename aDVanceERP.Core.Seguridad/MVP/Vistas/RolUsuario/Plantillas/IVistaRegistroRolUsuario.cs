using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.RolUsuario.Plantillas;

public interface IVistaRegistroRolUsuario : IVistaRegistro {
    string? NombreRolUsuario { get; set; }
    string? NombreModulo { get; set; }
    string? NombrePermiso { get; set; }
    List<string[]>? Permisos { get; }

    event EventHandler? PermisoAgregado;
    event EventHandler? PermisoEliminado;

    void CargarNombresModulos(string[] nombresModulos);
}