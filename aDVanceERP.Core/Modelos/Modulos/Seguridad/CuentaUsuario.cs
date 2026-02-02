using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    [JsonSerializable(typeof(CuentaUsuario), TypeInfoPropertyName = "CuentaUsuarioJsonContext")]
    public class CuentaUsuario : IEntidadBaseDatos {
        public CuentaUsuario() {
            Id = 0;
            Nombre = string.Empty;
            PasswordHash = null;
            PasswordSalt = null;
            Administrador = false;
            Aprobado = false;
        }

        public CuentaUsuario(long id, string nombre, string passwordHash, string passwordSalt) {
            Id = id;
            Nombre = nombre;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Administrador = false;
            Aprobado = false;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public bool Administrador { get; set; }
        public bool Aprobado { get; set; }
    }

    public enum FiltroBusquedaCuentaUsuario {
        Todos,
        Nombre
    }

    public static class UtilesBusquedaCuentaUsuario {
        public static string[] FiltroBusquedaCuentaUsuario = {
            "Todos los usuarios",
            "Identificador de BD",
            "Nombre del usuario"
        };
    }
}