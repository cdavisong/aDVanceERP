using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.MVP.Vistas.RolUsuario.Plantillas;

public interface IVistaTuplaRolUsuario : IVistaTupla {
    string? Id { get; set; }
    string? NombreRolUsuario { get; set; }
    string? CantidadPermisos { get; set; }
    string? CantidadUsuarios { get; set; }
}