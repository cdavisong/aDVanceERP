using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    public class DetalleExportacion {
        [JsonPropertyName("idProducto")]
        public long IdProducto { get; set; }

        [JsonPropertyName("idPresentacion")]
        public long IdPresentacion { get; set; } = 0;

        [JsonPropertyName("cantidad")]
        public decimal Cantidad { get; set; }

        [JsonPropertyName("precioVentaUnitario")]
        public decimal PrecioVentaUnitario { get; set; }

        [JsonPropertyName("descuentoItem")]
        public decimal DescuentoItem { get; set; } = 0;

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }
    }
}
