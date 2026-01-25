using System.Security;

using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Interfaces {
    public interface IVistaAutenticacionUsuario : IVistaBase {
        string NombreUsuario { get; }
        SecureString Password { get; }
    }
}