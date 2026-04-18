using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class DetalleTransferencia {
        [JsonPropertyName("numeroTransaccion")]
        public string? NumeroTransaccion { get; set; }

        [JsonPropertyName("numeroConfirmacion")]
        public string? NumeroConfirmacion { get; set; }
    }
}
