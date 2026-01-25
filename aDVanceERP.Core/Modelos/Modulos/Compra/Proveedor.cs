using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class Proveedor : IEntidadBaseDatos {
        public Proveedor() {
            CodigoProveedor = "N/A";
            RazonSocial = "N/A";
            NIT = "N/A";
            CondicionesPago = "N/A";
            FechaRegistro = DateTime.UtcNow;
            Activo = true;
        }

        public Proveedor(long id, long idPersona, string codigoProveedor, string razonSocial, string nit, string condicionesPago, DateTime fechaRegistro, bool activo) {
            Id = id;
            IdPersona = idPersona;
            CodigoProveedor = codigoProveedor;
            RazonSocial = razonSocial;
            NIT = nit;
            CondicionesPago = condicionesPago;
            FechaRegistro = fechaRegistro;
            Activo = activo;
        }

        public long Id { get; set; }
        public long IdPersona { get; set; }
        public string CodigoProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string NIT { get; set; }
        public string CondicionesPago { get; set; } // Condiciones de pago acordadas (Neto 30, Contado, etc.)
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }

    public enum FiltroBusquedaProveedor {
        Todos,
        Id,
        IdPersona,
        CodigoProveedor,
        RazonSocial,
        NIT
    }

    public static class UtilesBusquedaProveedor {
        public static object[] FiltroBusquedaProveedor = {
            "Todos los proveedores",
            "Identificador de BD",
            "Identificador de la persona",
            "Código del proveedor",
            "Razón social",
            "NIT"
        };
    }
}
