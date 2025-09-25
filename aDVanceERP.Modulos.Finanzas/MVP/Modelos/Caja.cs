using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Modelos {
    public enum EstadoCaja {
        Inactiva,
        Abierta,
        Cerrada
    }

    public class Caja : IEntidadBaseDatos {
        public Caja() { }

        public Caja(long id, DateTime fechaApertura, decimal saldoInicial, decimal saldoActual, DateTime fechaCierre, long idCuentaUsuario) {
            Id = id;
            FechaApertura = fechaApertura;
            SaldoInicial = saldoInicial;
            SaldoActual = saldoActual;
            FechaCierre = fechaCierre;
            IdCuentaUsuario = idCuentaUsuario;
        }

        public long Id { get; set; }
        public EstadoCaja Estado { get; set; } = EstadoCaja.Abierta;
        public DateTime FechaApertura { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime FechaCierre { get; set; }
        public long IdCuentaUsuario { get; set; }
    }

    public enum FiltroBusquedaCaja {
        Todos,
        Id,
        FechaApertura,
        Estado,
        FechaCierre
    }

    public static class UtilesBusquedaCaja {
        public static string[] FiltroBusquedaCaja = {
            "Todas las cajas",
            "Identificador de BD",
            "Fecha de apertura",
            "Estado de la caja",
            "Fecha de cierre"
        };
    }
}
