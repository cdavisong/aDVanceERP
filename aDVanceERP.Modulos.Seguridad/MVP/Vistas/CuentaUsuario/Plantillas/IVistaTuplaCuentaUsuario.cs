using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.MVP.Vistas.CuentaUsuario.Plantillas;

public interface IVistaTuplaCuentaUsuario : IVistaTupla {
    string? Id { get; set; }
    string? NombreUsuario { get; set; }
    string? NombreRolUsuario { get; set; }
    string? EstadoCuentaUsuario { get; set; }
}