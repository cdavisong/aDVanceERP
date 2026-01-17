using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.RecursosHumanos {
    public sealed class CorreoContacto : IEntidadBaseDatos {
        public CorreoContacto() {
            DireccionCorreo = "N/A";
            Categoria = CategoriaCorreoContacto.Personal;
            IdPersona = 0;
        }

        public CorreoContacto(long id, string direccionCorreo, CategoriaCorreoContacto categoria, long idPersona) {
            Id = id;
            DireccionCorreo = direccionCorreo;
            Categoria = categoria;
            IdPersona = idPersona;
        }

        public long Id { get; set; }
        public string DireccionCorreo { get; set; }
        public CategoriaCorreoContacto Categoria { get; set; }
        public long IdPersona { get; set; }
    }

    public enum FiltroBusquedaCorreoContacto {
        Todos,
        Id,
        DireccionCorreo,
        Categoria,
        IdPersona
    }

    public static class UtilesBusquedaCorreoContacto {
        public static object[] FiltroBusquedaCorreoContacto = {
            "Todos los correos de contacto",
            "Identificador de BD",
            "Dirección de correo",
            "Categoría del correo de contacto",
            "Identificador de la persona"
        };
    }
}
