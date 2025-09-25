using System.Security;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;

public interface IVistaAutenticacionUsuario : IVistaBase {
    string NombreUsuario { get; }
    SecureString Password { get; }

    event EventHandler? Autenticar;
    event EventHandler? RegistrarCuenta;
}