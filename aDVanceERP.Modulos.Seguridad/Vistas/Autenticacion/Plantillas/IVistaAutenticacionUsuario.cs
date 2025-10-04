using System.Security;

using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas.Autenticacion.Plantillas;

public interface IVistaAutenticacionUsuario : IVistaBase {
    string NombreUsuario { get; }
    SecureString Password { get; }
}