using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Caja {

    public sealed class CajaTurno : IEntidadBaseDatos {

        public CajaTurno() {
            Codigo = string.Empty;
            FechaApertura = DateTime.Now;
            MontoApertura = 0m;
            Estado = EstadoCajaTurnoEnum.Abierto;
        }

        public CajaTurno(
            long id,
            string codigo,
            long idAlmacen,
            long idCuentaApertura,
            long? idCuentaCierre,
            DateTime fechaApertura,
            DateTime? fechaCierre,
            decimal montoApertura,
            decimal? montoEfectivoCalculado,
            decimal? montoEfectivoDeclarado,
            decimal? montoTransferenciasCalculado,
            decimal? montoTransferenciasDeclarado,
            EstadoCajaTurnoEnum estado,
            string? observacionesApertura,
            string? observacionesCierre) {
            Id = id;
            Codigo = codigo;
            IdAlmacen = idAlmacen;
            IdCuentaApertura = idCuentaApertura;
            IdCuentaCierre = idCuentaCierre;
            FechaApertura = fechaApertura;
            FechaCierre = fechaCierre;
            MontoApertura = montoApertura;
            MontoEfectivoCalculado = montoEfectivoCalculado;
            MontoEfectivoDeclarado = montoEfectivoDeclarado;
            MontoTransferenciasCalculado = montoTransferenciasCalculado;
            MontoTransferenciasDeclarado = montoTransferenciasDeclarado;
            Estado = estado;
            ObservacionesApertura = observacionesApertura;
            ObservacionesCierre = observacionesCierre;
        }

        // ── Identidad ──────────────────────────────────────────────
        public long Id { get; set; }
        public string Codigo { get; set; }

        // ── Referencias ───────────────────────────────────────────
        public long IdAlmacen { get; set; }
        public long IdCuentaApertura { get; set; }
        public long? IdCuentaCierre { get; set; }

        // ── Fechas ────────────────────────────────────────────────
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }

        // ── Montos de apertura ────────────────────────────────────
        public decimal MontoApertura { get; set; }

        // ── Efectivo al cierre ────────────────────────────────────
        /// <summary>Calculado por el presentador sumando movimientos Efectivo.</summary>
        public decimal? MontoEfectivoCalculado { get; set; }
        /// <summary>Conteo físico declarado por el cajero.</summary>
        public decimal? MontoEfectivoDeclarado { get; set; }
        /// <summary>Columna STORED en BD: Declarado − Calculado. Solo lectura desde la app.</summary>
        public decimal? DiferenciaEfectivo { get; set; }

        // ── Transferencias al cierre ──────────────────────────────
        /// <summary>Calculado por el presentador sumando movimientos Transferencia.</summary>
        public decimal? MontoTransferenciasCalculado { get; set; }
        /// <summary>Total transferencias declarado por el cajero.</summary>
        public decimal? MontoTransferenciasDeclarado { get; set; }
        /// <summary>Columna STORED en BD: Declarado − Calculado. Solo lectura desde la app.</summary>
        public decimal? DiferenciaTransferencias { get; set; }

        // ── Estado y notas ────────────────────────────────────────
        public EstadoCajaTurnoEnum Estado { get; set; }
        public string? ObservacionesApertura { get; set; }
        public string? ObservacionesCierre { get; set; }

        // ── Datos auxiliares de tupla (JOIN en listado) ───────────
        public string? NombreAlmacen { get; set; }
        public string? NombreUsuarioApertura { get; set; }
    }

    // ── Enums ──────────────────────────────────────────────────────

    public enum EstadoCajaTurnoEnum {
        Abierto,
        Cerrado,
        Anulado
    }

    public enum FiltroBusquedaCajaTurno {
        Todos,
        Id,
        Codigo,
        Estado,
        Fecha
    }

    // ── Utilidades de búsqueda ─────────────────────────────────────

    public static class UtilesBusquedaCajaTurno {
        public static object[] Filtros = {
            "Todos los turnos",
            "Identificador de BD",
            "Código de turno",
            "Almacén",
            "Estado",
            "Fecha de apertura"
        };
    }
}