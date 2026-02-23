using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    public class DetalleTransferenciaExportacion {
        [JsonPropertyName("numeroConfirmacion")]
        public string NumeroConfirmacion { get; set; } = string.Empty;

        [JsonPropertyName("numeroTransaccion")]
        public string NumeroTransaccion { get; set; } = string.Empty;
    }
}
