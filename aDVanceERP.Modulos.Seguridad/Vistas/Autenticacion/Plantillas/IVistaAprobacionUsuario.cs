using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas.Autenticacion.Plantillas;

public interface IVistaAprobacionUsuario : IVistaBase {
    string NombreUsuario { get; set; }
}