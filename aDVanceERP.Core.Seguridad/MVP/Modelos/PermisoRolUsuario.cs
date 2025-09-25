using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Modelos; 

public class PermisoRolUsuario : IEntidadBaseDatos {
    public PermisoRolUsuario() { }

    public PermisoRolUsuario(long id, long idRolUsuario, long idPermiso) {
        Id = id;
        IdRolUsuario = idRolUsuario;
        IdPermiso = idPermiso;
    }

    public long IdRolUsuario { get; }
    public long IdPermiso { get; }

    public long Id { get; set; }
}

public enum FiltroBusquedaPermisoRolUsuario {
    Todos,
    Id
}