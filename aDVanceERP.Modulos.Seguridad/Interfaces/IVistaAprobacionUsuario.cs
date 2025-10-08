using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Interfaces;

public interface IVistaAprobacionUsuario : IVistaBase {
    string NombreUsuario { get; set; }
}