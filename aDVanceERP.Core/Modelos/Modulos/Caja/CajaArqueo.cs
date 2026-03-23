using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Caja {

    /// <summary>
    /// Representa una fila del conteo físico de efectivo: una denominación y su cantidad de piezas.
    /// El Subtotal (denominacion × cantidad) es columna STORED en BD — se asigna al leer, nunca al escribir.
    /// </summary>
    public sealed class CajaArqueo : IEntidadBaseDatos {

        public CajaArqueo() { }

        public CajaArqueo(long id, long idTurno, decimal denominacion, int cantidad) {
            Id = id;
            IdTurno = idTurno;
            Denominacion = denominacion;
            Cantidad = cantidad;
            Subtotal = denominacion * cantidad;
        }

        public CajaArqueo(long id, long idTurno, decimal denominacion, int cantidad, decimal subtotal) {
            Id = id;
            IdTurno = idTurno;
            Denominacion = denominacion;
            Cantidad = cantidad;
            Subtotal = subtotal;
        }

        public long Id { get; set; }
        public long IdTurno { get; set; }

        /// <summary>Valor de billete o moneda. Ej: 500, 200, 100, 50, 20, 10, 5, 1, 0.50</summary>
        public decimal Denominacion { get; set; }

        /// <summary>Cantidad de piezas contadas.</summary>
        public int Cantidad { get; set; }

        /// <summary>Columna STORED en BD (Denominacion × Cantidad). Solo lectura desde la app.</summary>
        public decimal Subtotal { get; set; }
    }

    /// <summary>
    /// DTO de solo lectura: resultado consolidado del arqueo de un turno.
    /// Lo produce <see cref="RepoCajaTurno.ObtenerResumenArqueo"/>.
    /// </summary>
    public sealed class ResumenArqueo {
        public long IdTurno { get; set; }
        public List<CajaArqueo> Denominaciones { get; set; } = [];
        public decimal TotalContado { get; set; }
    }

    /// <summary>
    /// DTO de solo lectura: totales de movimientos por canal para el cierre.
    /// Lo produce <see cref="RepoCajaMovimiento.ObtenerTotalesPorCanal"/>.
    /// </summary>
    public sealed class TotalesCierreCaja {
        public long IdTurno { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTransferencias { get; set; }
    }
}