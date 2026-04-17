using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    public class DetalleTransferencia {
        [JsonPropertyName("numeroTransaccion")]
        public string? NumeroTransaccion { get; set; }
    }
}
