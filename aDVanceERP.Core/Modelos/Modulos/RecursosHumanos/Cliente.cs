using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.RecursosHumanos {
    public sealed class Cliente : IEntidadBaseDatos {
        public Cliente() {
            CodigoCliente = "N/A";
            LimiteCredito = 0.0m;
            FechaRegistro = DateTime.UtcNow;
            Activo = true;
        }

        public Cliente(long id, long idPersona, string codigoCliente, decimal limiteCredito, DateTime fechaRegistro, bool activo) {
            Id = id;
            IdPersona = idPersona;
            CodigoCliente = codigoCliente;
            LimiteCredito = limiteCredito;
            FechaRegistro = fechaRegistro;
            Activo = activo;
        }

        public long Id { get; set; }
        public long IdPersona { get; set; }
        public string CodigoCliente { get; set; }
        public decimal LimiteCredito { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }

    public enum FiltroBusquedaCliente {
        Todos,
        Id,
        IdPersona,
        CodigoCliente
    }

    public static class UtilesBusquedaCliente {
        public static object[] FiltroBusquedaCliente = {
            "Todos los clientes",
            "Identificador de BD",
            "Identificador de la persona",
            "Código del cliente"
        };
    }
}
