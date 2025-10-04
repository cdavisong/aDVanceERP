using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad;

public class PermisoRolUsuario : IEntidadBaseDatos {
    public PermisoRolUsuario() { }

    public PermisoRolUsuario(long id, long idRolUsuario, long idPermiso) {
        Id = id;
        IdRolUsuario = idRolUsuario;
        IdPermiso = idPermiso;
    }

    public long Id { get; set; }
    public long IdRolUsuario { get; }
    public long IdPermiso { get; }
    public long IdModulo { get; set; }
    public string NombrePermiso { get; set; }
}

public enum FiltroBusquedaPermisoRolUsuario {
    Todos,
    Id,
    IdRolUsuario
}