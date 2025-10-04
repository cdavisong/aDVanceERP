using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad;

public class SesionUsuario : IEntidadBaseDatos {
    public SesionUsuario() { }

    public SesionUsuario(long id, long idCuentaUsuario, string? token, DateTime fechaInicio) {
        Id = id;
        IdCuentaUsuario = idCuentaUsuario;
        Token = token;
        FechaInicio = fechaInicio;
    }

    public long Id { get; set; }
    public long IdCuentaUsuario { get; set; }
    public string? Token { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}

public enum FiltroBusquedaSesionUsuario {
    Todos,
    NombreUsuario,
    SesionActiva
}

public static class UtilesBusquedaSesionUsuario {
    public static string[] FiltroBusquedaSesionUsuario = {
        "Todas las sesiones",
        "Identificador de BD",
        "Nombre del usuario",
        "Sesión activa"
    };
}