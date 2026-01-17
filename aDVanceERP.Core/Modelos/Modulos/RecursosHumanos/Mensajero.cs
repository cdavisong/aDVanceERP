using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.RecursosHumanos {
    public sealed class Mensajero : IEntidadBaseDatos {
        public Mensajero() {
            CodigoMensajero = "N/A";
            MatriculaVehiculo = "N/A";
            Activo = true;
        }

        public Mensajero(long idPersona, string codigoMensajero, string matriculaVehiculo, bool activo) {
            IdPersona = idPersona;
            CodigoMensajero = codigoMensajero;
            MatriculaVehiculo = matriculaVehiculo;
            Activo = activo;
        }

        public long Id { get; set; }
        public long IdPersona { get; set; }
        public string CodigoMensajero { get; set; }
        public string MatriculaVehiculo { get; set; }
        public bool Activo { get; set; }
    }

    public enum FiltroBusquedaMensajero {
        Todos,
        Id,
        IdPersona,
        CodigoMensajero,
        MatriculaVehiculo
    }

    public static class UtilesBusquedaMensajero {
        public static object[] FiltroBusquedaMensajero = {
            "Todos los mensajeros",
            "Identificador de BD",
            "Identificador de la persona",
            "Código del mensajero",
            "Matrícula del vehículo"
        };
    }
}
