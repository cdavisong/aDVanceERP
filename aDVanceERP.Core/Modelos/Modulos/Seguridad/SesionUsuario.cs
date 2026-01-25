using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    [JsonSerializable(typeof(SesionUsuario), TypeInfoPropertyName = "SesionUsuarioJsonContext")]
    public class SesionUsuario : IEntidadBaseDatos {
        public SesionUsuario() {
            Id = 0;
            IdCuentaUsuario = 0;
            Token = string.Empty;
            FechaInicio = DateTime.MinValue;
            FechaFin = null;
        }

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
}