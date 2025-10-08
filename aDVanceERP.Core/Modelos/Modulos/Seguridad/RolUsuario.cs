using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad;

[JsonSerializable(typeof(RolUsuario), TypeInfoPropertyName = "RolUsuarioJsonContext")]
public class RolUsuario : IEntidadBaseDatos {
    public RolUsuario() {
        Id = 0;
        Nombre = string.Empty;
    }

    public RolUsuario(long id, string nombre) {
        Id = id;
        Nombre = nombre;
    }

    public long Id { get; set; }
    public string Nombre { get; set; }
    public int CantidadPermisos { get; set; }
    public int CantidadUsuariosAsignados { get; set; }
}

public enum FiltroBusquedaRolUsuario {
    Todos,
    Id,
    Nombre
}

public static class UtilesBusquedaRolUsuario {
    public static string[] FiltroBusquedaRolUsuario = {
        "Todos los roles",
        "Identificador de BD",
        "Nombre del rol"
    };
}