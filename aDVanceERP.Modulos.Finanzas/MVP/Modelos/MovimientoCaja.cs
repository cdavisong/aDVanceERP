using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.Finanzas.MVP.Modelos {
    public enum TipoMovimientoCaja {
        Ingreso,
        Egreso
    }

    public class MovimientoCaja : IEntidadBaseDatos {
        public MovimientoCaja() { }

        public MovimientoCaja(long id, long idCaja, DateTime fecha, decimal monto, TipoMovimientoCaja tipo, string? concepto, long idPago, long idUsuario, string? observaciones) {
            Id = id;
            IdCaja = idCaja;
            Fecha = fecha;
            Monto = monto;
            Tipo = tipo;
            Concepto = concepto;
            IdPago = idPago;
            IdUsuario = idUsuario;
            Observaciones = observaciones;
        }

        public long Id { get; set; }
        public long IdCaja { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public TipoMovimientoCaja Tipo { get; set; }
        public string Concepto { get; set; }
        public long IdPago { get; set; }
        public long IdUsuario { get; set; }
        public string? Observaciones { get; set; }
    }

    public enum FiltroBusquedaMovimientoCaja {
        Todos,
        Id,
        IdCaja,
        IdPago,
        Fecha,
        Tipo,
        Concepto
    }

    public static class UtilesBusquedaMovimientoCaja {
        public static string[] FiltroBusquedaMovimientoCaja = {
            "Todos los movimientos de caja",
            "Identificador de BD",
            "Identificador de BD de caja",
            "Identificador de BD del pago",
            "Fecha",
            "Tipo de movimiento",
            "Concepto"
        };
    }
}
