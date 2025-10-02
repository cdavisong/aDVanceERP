using System.Security;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.MVP.Vistas.CuentaUsuario.Plantillas;

public interface IVistaRegistroCuentaUsuario : IVistaRegistro {
    string? NombreUsuario { get; set; }
    SecureString? Password { get; }
    string? NombreRolUsuario { get; set; }

    void CargarRolesUsuarios(string[] rolesUsuarios);
}