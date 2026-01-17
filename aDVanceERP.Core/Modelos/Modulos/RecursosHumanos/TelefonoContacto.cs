using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.RecursosHumanos {
    public sealed class TelefonoContacto : IEntidadBaseDatos {
        public TelefonoContacto() {
            PrefijoPais = "+0";
            NumeroTelefono = "N/A";
            Categoria = CategoriaTelefonoContacto.Movil;
            IdPersona = 0;
        }

        public TelefonoContacto(long id, string prefijoPais, string numeroTelefono, CategoriaTelefonoContacto categoria, long idPersona) {
            Id = id;
            PrefijoPais = prefijoPais;
            NumeroTelefono = numeroTelefono;
            Categoria = categoria;
            IdPersona = idPersona;
        }

        public long Id { get; set; }
        public string PrefijoPais { get; set; }
        public string NumeroTelefono { get; set; }
        public CategoriaTelefonoContacto Categoria { get; set; }
        public long IdPersona { get; set; }
    }

    public enum FiltroBusquedaTelefonoContacto {
        Todos,
        Id,
        PrefijoPais,
        NumeroTelefono,
        Categoria,
        IdPersona
    }

    public static class UtilesBusquedaTelefonoContacto {
        public static object[] FiltroBusquedaTelefonoContacto = {
            "Todos los teléfonos de contacto",
            "Identificador de BD",
            "Prefijo del país",
            "Número de teléfono",
            "Categoría del teléfono de contacto",
            "Identificador de la persona"
        };
    }
}
