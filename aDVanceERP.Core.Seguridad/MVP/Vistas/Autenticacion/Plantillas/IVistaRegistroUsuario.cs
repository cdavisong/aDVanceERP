using System.Security;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;

public interface IVistaRegistroUsuario : IVistaRegistro {
    string NombreUsuario { get; }
    SecureString? Password { get; }
    bool ConfirmacionTerminosServicio { get; }

    event EventHandler? AutenticarUsuario;
}