using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Modelos; 

public class CuentaUsuario : IEntidadBaseDatos {
    public CuentaUsuario() { }

    public CuentaUsuario(long id, string nombre, string passwordHash, string passwordSalt, long idRolUsuario) {
        Id = id;
        Nombre = nombre;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        IdRolUsuario = idRolUsuario;
        Administrador = false;
        Aprobado = false;
    }

    public string? Nombre { get; }
    public string? PasswordHash { get; private set; }
    public string? PasswordSalt { get; private set; }
    public long IdRolUsuario { get; set; }
    public bool Administrador { get; set; }
    public bool Aprobado { get; set; }

    public long Id { get; set; }
}

public enum FiltroBusquedaCuentaUsuario {
    Todos,
    Nombre,
    Rol
}

public static class UtilesBusquedaCuentaUsuario {
    public static string[] FiltroBusquedaBusquedaCuentaUsuario = {
        "Todos los usuarios",
        "Identificador de BD",
        "Nombre del usuario",
        "Rol de usuario"
    };
}