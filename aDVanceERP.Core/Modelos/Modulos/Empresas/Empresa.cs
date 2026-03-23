using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Empresas {
    public class Empresa : IEntidadBaseDatos {
        public Empresa() {
            Nombre = string.Empty;
            FechaRegistro = DateTime.Now;
        }

        public Empresa(long id, string nombre, string? razonSocial, string? rif, string? direccion, string? telefono, string? email, string? web, string? rutaLogo, DateTime fechaRegistro) {
            Id = id;
            Nombre = nombre;
            RazonSocial = razonSocial;
            Rif = rif;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Web = web;
            RutaLogo = rutaLogo;
            FechaRegistro = fechaRegistro;
        }

        public long Id { get; set; }
        public string Nombre { get; set; } // Nombre comercial
        public string? RazonSocial { get; set; } // Razón social legal
        public string? Rif { get; set; } // RIF / NIT
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Web { get; set; }
        public string? RutaLogo { get; set; } // Ruta al archivo del logo
        public DateTime FechaRegistro { get; set; }
    }

    public enum FiltroBusquedaEmpresa {
        Todos,
        Id,
        NombreComercial,
        Rif
    }

    public static class UtilesBusquedaEmpresa {
        public static object[] Filtros = {
            "Todas las empresas",
            "Identificador de BD",
            "Nombre comercial",
            "RIF / NIT"
        };
    }


}
