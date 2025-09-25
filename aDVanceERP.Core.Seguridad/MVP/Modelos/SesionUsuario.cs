using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Seguridad.MVP.Modelos; 

public class SesionUsuario : IEntidadBaseDatos {
    public SesionUsuario() { }

    public SesionUsuario(long id, int idCuentaUsuario, string? token, DateTime fechaInicio) {
        Id = id;
        IdCuentaUsuario = idCuentaUsuario;
        Token = token;
        FechaInicio = fechaInicio;
    }

    public int IdCuentaUsuario { get; set; }
    public string? Token { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }

    public long Id { get; set; }
}

public enum FiltroBusquedaSesionUsuario {
    Todos,
    NombreUsuario,
    SesionActiva
}

public static class UtilesBusquedaSesionUsuario {
    public static string[] FiltroBusquedaBusquedaSesionUsuario = {
        "Todas las sesiones",
        "Identificador de BD",
        "Nombre del usuario",
        "Sesión activa"
    };
}