using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Vistas.Autenticacion.Plantillas;

public interface IVistaAprobacionUsuario : IVistaBase {
    event EventHandler? CambiarDeUsuario;
}