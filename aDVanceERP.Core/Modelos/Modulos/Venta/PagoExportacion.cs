using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    /// <summary>
    /// Representa un pago completo (efectivo o transferencia) con
    /// soporte para múltiples monedas por fragmento.
    /// Corresponde a adv__pago en la BD.
    /// </summary>
    public class PagoExportacion {
        [JsonPropertyName("metodoPago")]
        public string MetodoPago { get; set; } = string.Empty;
        // "Efectivo" | "TransferenciaBancaria"

        /// <summary>Monto total en moneda BASE.</summary>
        [JsonPropertyName("montoPagado")]
        public decimal MontoPagado { get; set; }

        [JsonPropertyName("fechaPagoCliente")]
        public DateTime FechaPagoCliente { get; set; }

        [JsonPropertyName("estadoPago")]
        public string EstadoPago { get; set; } = "Confirmado";
        // "Confirmado" = efectivo | "Pendiente" = transferencia sin confirmar

        [JsonPropertyName("detalleTransferencia")]
        public DetalleTransferencia? DetalleTransferencia { get; set; }

        /// <summary>
        /// Desglose por moneda. El ERP crea registros en adv__pago_detalle_moneda con esto.
        /// </summary>
        [JsonPropertyName("detallesMoneda")]
        public List<PagoDetalleMoneda> DetallesMoneda { get; set; } = new();

        [JsonPropertyName("idMoneda")]
        public int? IdMoneda { get; set; }          // moneda principal si solo hay una

        [JsonPropertyName("montoMonedaBase")]
        public decimal MontoMonedaBase { get; set; }

        [JsonPropertyName("tasaCambioAplicada")]
        public decimal TasaCambioAplicada { get; set; } = 1m;
    }
}
