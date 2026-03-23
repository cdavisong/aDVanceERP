using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Caja {

    public sealed class CajaMovimiento : IEntidadBaseDatos {

        public CajaMovimiento() {
            Tipo = TipoMovimientoCajaEnum.EntradaManual;
            CanalPago = CanalPagoCajaEnum.NA;
            Monto = 0m;
            FechaMovimiento = DateTime.Now;
        }

        public CajaMovimiento(
            long id,
            long idTurno,
            TipoMovimientoCajaEnum tipo,
            CanalPagoCajaEnum canalPago,
            long? idVenta,
            decimal monto,
            string? descripcion,
            long idCuentaUsuario,
            DateTime fechaMovimiento) {

            Id = id;
            IdTurno = idTurno;
            Tipo = tipo;
            CanalPago = canalPago;
            IdVenta = idVenta;
            Monto = monto;
            Descripcion = descripcion;
            IdCuentaUsuario = idCuentaUsuario;
            FechaMovimiento = fechaMovimiento;
        }

        // ── Identidad ──────────────────────────────────────────────
        public long Id { get; set; }

        // ── Referencias ───────────────────────────────────────────
        public long IdTurno { get; set; }
        public long? IdVenta { get; set; }
        public long IdCuentaUsuario { get; set; }

        // ── Clasificación ─────────────────────────────────────────
        public TipoMovimientoCajaEnum Tipo { get; set; }

        /// <summary>
        /// Canal por donde fluyó el dinero.
        /// NA para salidas manuales sin canal específico (gastos, retiros).
        /// </summary>
        public CanalPagoCajaEnum CanalPago { get; set; }

        // ── Valor ─────────────────────────────────────────────────
        /// <summary>Positivo = entrada a la caja. Negativo = salida de la caja.</summary>
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }

        // ── Auditoría ─────────────────────────────────────────────
        public DateTime FechaMovimiento { get; set; }

        // ── Datos auxiliares de tupla (JOIN en historial) ─────────
        public string? NumeroFactura { get; set; }
        public string? NombreUsuario { get; set; }
    }

    // ── Enums ──────────────────────────────────────────────────────

    public enum TipoMovimientoCajaEnum {
        Venta,
        [Display(Name = "Devolución venta")]
        DevolucionVenta,
        [Display(Name = "Entrada manual")]
        EntradaManual,
        [Display(Name = "Salida manual")]
        SalidaManual,
        [Display(Name = "Ajuste arqueo")]
        AjusteArqueo
    }

    public enum CanalPagoCajaEnum {
        Efectivo,
        Transferencia,
        Mixto,
        [Display(Name = "N/A")]
        NA
    }

    public enum FiltroBusquedaCajaMovimiento {
        Todos,
        IdTurno,
        Tipo,
        Canal,
        Fecha
    }

    public static class UtilesBusquedaCajaMovimiento {
        public static object[] FiltroBusquedaCajaMovimiento = {
            "Todos los movimientos",
            "Por turno",
            "Por tipo",
            "Por canal de pago",
            "Por fecha"
        };
    }
}