using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    /// <summary>
    /// Moneda activa con su tasa vigente del día.
    /// El ERP la incluye en el catalogo.json para que el POS
    /// pueda calcular equivalencias sin conexión.
    /// </summary>
    public class MonedaCatalogo {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; } = string.Empty;   // "CUP", "USD", "MLC"

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("simbolo")]
        public string Simbolo { get; set; } = string.Empty;  // "$", "€", "MLC"

        [JsonPropertyName("esBase")]
        public bool EsBase { get; set; }                      // true solo para CUP (o la moneda base)

        [JsonPropertyName("tasaHoy")]
        public decimal TasaHoy { get; set; } = 1m;           // cuántas unidades de moneda base = 1 de esta

        [JsonPropertyName("aplicaEfectivo")]
        public bool AplicaEfectivo { get; set; } = true;     // false = solo transferencia (tasa oficial BCC)

        [JsonPropertyName("precisionDecimal")]
        public int PrecisionDecimal { get; set; } = 2;

        // ── Calculado en runtime ───────────────────────────────
        [JsonIgnore]
        public string Etiqueta => EsBase
            ? $"{Nombre} ({Simbolo})"
            : $"{Nombre} ({Simbolo})  ×{TasaHoy:N2}";
    }
}
