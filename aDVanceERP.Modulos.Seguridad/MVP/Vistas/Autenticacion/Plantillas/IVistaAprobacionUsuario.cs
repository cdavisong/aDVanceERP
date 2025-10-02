using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.MVP.Vistas.Autenticacion.Plantillas;

public interface IVistaAprobacionUsuario : IVistaBase {
    event EventHandler? CambiarDeUsuario;
}