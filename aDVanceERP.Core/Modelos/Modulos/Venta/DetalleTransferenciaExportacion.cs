using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class DetalleTransferenciaExportacion {
        [JsonPropertyName("numeroConfirmacion")]
        public string NumeroConfirmacion { get; set; } = string.Empty;

        [JsonPropertyName("numeroTransaccion")]
        public string NumeroTransaccion { get; set; } = string.Empty;
    }
}
