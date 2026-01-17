using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.RecursosHumanos {
    public sealed class RolContacto : IEntidadBaseDatos {
        public RolContacto() {
            Nombre = "N/A";
            Descripcion = string.Empty;
        }

        public RolContacto(long id, string nombre, string descripcion) {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public enum FiltroBusquedaRolContacto {
        Todos,
        Id,
        Nombre
    }

    public static class UtilesBusquedaRolContacto {
        public static object[] FiltroBusquedaRolContacto = {
            "Todos los roles de contacto",
            "Identificador de BD",
            "Nombre del rol de contacto"
        };
    }
}
