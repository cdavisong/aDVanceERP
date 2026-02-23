
using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    public class CatalogoMeta {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0";

        [JsonPropertyName("generadoEn")]
        public DateTime GeneradoEn { get; set; }

        [JsonPropertyName("aplicacion")]
        public string Aplicacion { get; set; } = string.Empty;

        [JsonPropertyName("idAlmacen")]
        public long IdAlmacen { get; set; }

        [JsonPropertyName("nombreAlmacen")]
        public string NombreAlmacen { get; set; } = string.Empty;
    }
}
