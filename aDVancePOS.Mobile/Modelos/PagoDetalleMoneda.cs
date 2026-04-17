using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    /// <summary>
    /// Fragmento de un pago expresado en una moneda específica.
    /// Un pago puede tener varios de estos (ej: 50 USD + 500 CUP).
    /// Corresponde a adv__pago_detalle_moneda en la BD.
    /// </summary>
    public class PagoDetalleMoneda {
        [JsonPropertyName("idMoneda")]
        public int IdMoneda { get; set; }

        [JsonPropertyName("codigoMoneda")]
        public string CodigoMoneda { get; set; } = string.Empty;   // "CUP", "USD"

        [JsonPropertyName("simboloMoneda")]
        public string SimboloMoneda { get; set; } = string.Empty;

        [JsonPropertyName("montoMoneda")]
        public decimal MontoMoneda { get; set; }        // monto en la moneda seleccionada

        [JsonPropertyName("montoMonedaBase")]
        public decimal MontoMonedaBase { get; set; }    // equivalente en moneda base

        [JsonPropertyName("tasaCambioAplicada")]
        public decimal TasaCambioAplicada { get; set; } = 1m;

        // ── Solo para transferencias ──────────────────────────
        [JsonPropertyName("numeroTransaccion")]
        public string? NumeroTransaccion { get; set; }

        [JsonPropertyName("telefonoRemitente")]
        public string? TelefonoRemitente { get; set; }

        // ── Display helper ────────────────────────────────────
        [JsonIgnore]
        public string Resumen => SimboloMoneda == CodigoMoneda
            ? $"{MontoMoneda:N2} {CodigoMoneda}"
            : $"{SimboloMoneda}{MontoMoneda:N2} ({MontoMonedaBase:N2} base)";
    }
}
