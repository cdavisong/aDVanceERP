using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad;

public class RolUsuario : IEntidadBaseDatos {
    public RolUsuario() { }

    public RolUsuario(long id, string nombre) {
        Id = id;
        Nombre = nombre;
    }

    public long Id { get; set; }
    public string? Nombre { get; set; }

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