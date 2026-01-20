using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.RecursosHumanos {
    public class Persona : IEntidadBaseDatos {
        public Persona() {
            NombreCompleto = "N/A";
            TipoDocumento = TipoDocumento.CI;
            NumeroDocumento = "N/A";
            FechaRegistro = DateTime.UtcNow;
            Activo = true;
        }
        public Persona(long id, string nombreCompleto, TipoDocumento tipoDocumento, string numeroDocumento, string? direccionPrincipal, DateTime fechaRegistro,  bool activo) {
            Id = id;
            NombreCompleto = nombreCompleto;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            DireccionPrincipal = direccionPrincipal;
            FechaRegistro = fechaRegistro;
            Activo = activo;
        }

        public long Id { get; set; }
        public string NombreCompleto { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string? DireccionPrincipal { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }

    public enum FiltroBusquedaPersona {
        Todos,
        Id,
        NombreCompleto,
        NumeroDocumento
    }

    public static class UtilesBusquedaPersona {
        public static object[] FiltroBusquedaPersona = {
            "Todas las personas",
            "Identificador de BD",
            "Nombre completo",
            "Número de documento"
        };
    }
}
