using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    public class ExportacionMeta {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0";

        [JsonPropertyName("exportadoEn")]
        public DateTime ExportadoEn { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("aplicacion")]
        public string Aplicacion { get; set; } = "aDVance ERP Mobile";

        [JsonPropertyName("idAlmacen")]
        public long IdAlmacen { get; set; }

        [JsonPropertyName("totalVentas")]
        public int TotalVentas { get; set; }

        [JsonPropertyName("totalRecaudado")]
        public decimal TotalRecaudado { get; set; }
    }
}
