using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class VentaExportacion {
        [JsonPropertyName("idLocal")]
        public string IdLocal { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("idCliente")]
        public long IdCliente { get; set; } = 1;

        [JsonPropertyName("idAlmacen")]
        public long IdAlmacen { get; set; }

        [JsonPropertyName("numeroTicket")]
        public string NumeroTicket { get; set; } = string.Empty;

        [JsonPropertyName("fechaVenta")]
        public DateTime FechaVenta { get; set; }

        [JsonPropertyName("totalBruto")]
        public decimal TotalBruto { get; set; }

        [JsonPropertyName("descuentoTotal")]
        public decimal DescuentoTotal { get; set; } = 0;

        [JsonPropertyName("impuestoTotal")]
        public decimal ImpuestoTotal { get; set; }

        [JsonPropertyName("importeTotal")]
        public decimal ImporteTotal { get; set; }

        /// <summary>
        /// "Completada" = venta cerrada normal.
        /// "EnEspera"   = venta archivada esperando confirmación de transferencia.
        /// El ERP solo importa ventas "Completadas".
        /// </summary>
        [JsonPropertyName("estadoVenta")]
        public string EstadoVenta { get; set; } = "Completada";

        [JsonPropertyName("observaciones")]
        public string? Observaciones { get; set; }

        [JsonPropertyName("detalles")]
        public List<DetalleExportacion> Detalles { get; set; } = new();

        [JsonPropertyName("pagos")]
        public List<PagoExportacion> Pagos { get; set; } = new();

        // ── Runtime helpers (no se serializan) ────────────────
        [JsonIgnore]
        public bool EstaEnEspera => EstadoVenta == "EnEspera";

        [JsonIgnore]
        public decimal TotalPagadoBase =>
            Pagos.Sum(p => p.MontoMonedaBase > 0 ? p.MontoMonedaBase : p.MontoPagado);

        [JsonIgnore]
        public decimal PendienteBase => ImporteTotal - TotalPagadoBase;
    }
}
