using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class PagoExportacion {
        [JsonPropertyName("metodoPago")]
        public string MetodoPago { get; set; } = string.Empty; // "Efectivo" | "TransferenciaBancaria"

        [JsonPropertyName("montoPagado")]
        public decimal MontoPagado { get; set; }

        [JsonPropertyName("fechaPagoCliente")]
        public DateTime FechaPagoCliente { get; set; }

        [JsonPropertyName("estadoPago")]
        public string EstadoPago { get; set; } = "Confirmado";

        /// <summary>Null si el pago fue en efectivo.</summary>
        [JsonPropertyName("detalleTransferencia")]
        public DetalleTransferenciaExportacion? DetalleTransferencia { get; set; }
    }
}
