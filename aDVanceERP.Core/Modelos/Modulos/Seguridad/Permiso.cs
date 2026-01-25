using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    [JsonSerializable(typeof(Permiso), TypeInfoPropertyName = "PermisoJsonContext")]
    public class Permiso : IEntidadBaseDatos {
        public Permiso() {
            Id = 0;
            IdModulo = 0;
            Nombre = string.Empty;
        }

        public Permiso(long id, long idModulo, string nombre) {
            Id = id;
            IdModulo = idModulo;
            Nombre = nombre;
        }

        public long Id { get; set; }
        public long IdModulo { get; }
        public string Nombre { get; }
    }

    public enum FiltroBusquedaPermiso {
        Todos,
        Id,
        IdModulo,
        Nombre
    }

    public static class UtilesBusquedaPermiso {
        public static string[] FiltroBusquedaPermiso = {
            "Todos los permisos",
            "Identificador de BD",
            "Identificador del módulo",
            "Nombre del permiso"
        };
    }
}
