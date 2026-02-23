using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class VentaExportacion {
        /// <summary>
        /// GUID generado en el móvil. El ERP desktop lo usa para
        /// detectar duplicados si el archivo se importa dos veces.
        /// </summary>
        [JsonPropertyName("idLocal")]
        public string IdLocal { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("idCliente")]
        public long IdCliente { get; set; } = 1; // cliente anónimo

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

        [JsonPropertyName("estadoVenta")]
        public string EstadoVenta { get; set; } = "Completada";

        [JsonPropertyName("observaciones")]
        public string? Observaciones { get; set; }

        [JsonPropertyName("detalles")]
        public List<DetalleExportacion> Detalles { get; set; } = new();

        [JsonPropertyName("pagos")]
        public List<PagoExportacion> Pagos { get; set; } = new();
    }
}
