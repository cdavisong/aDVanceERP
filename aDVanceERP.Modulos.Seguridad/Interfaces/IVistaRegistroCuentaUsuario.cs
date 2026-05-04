using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

using System.Security;

namespace aDVanceERP.Modulos.Seguridad.Interfaces {
    public interface IVistaRegistroCuentaUsuario : IVistaRegistro {
        string NombreUsuario { get; set; }
        Rol? RolUsuario { get; set; }
        SecureString? Password { get; }
        string? NombreCompleto { get; set; }
        TipoDocumentoEnum TipoDocumento { get; set; }
        string? NumeroDocumento { get; set; }
        string? DireccionPrincipal { get; set; }
        CorreoContacto? CorreoContacto { get; set; }

        void CargarRoles(Rol[] roles);
    }
}