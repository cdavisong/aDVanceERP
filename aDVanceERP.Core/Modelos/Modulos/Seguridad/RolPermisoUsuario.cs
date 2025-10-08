using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad;

[JsonSerializable(typeof(RolPermisoUsuario), TypeInfoPropertyName = "RolPermisoUsuarioJsonContext")]
public class RolPermisoUsuario : IEntidadBaseDatos {
    public RolPermisoUsuario() {
        Id = 0;
        IdRolUsuario = 0;
        IdPermiso = 0;
        IdModulo = 0;
        NombrePermiso = string.Empty;
    }

    public RolPermisoUsuario(long id, long idRolUsuario, long idPermiso) {
        Id = id;
        IdRolUsuario = idRolUsuario;
        IdPermiso = idPermiso;
        IdModulo = 0;
        NombrePermiso = string.Empty;
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