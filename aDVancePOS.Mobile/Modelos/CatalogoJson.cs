using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    /// <summary>
    /// Raíz del JSON de catálogo exportado por el ERP desktop.
    /// Incluye productos, monedas activas con tasas del día.
    /// </summary>
    public class CatalogoJson {
        [JsonPropertyName("meta")]
        public CatalogoMeta Meta { get; set; } = new();

        [JsonPropertyName("productos")]
        public List<ProductoCatalogo> Productos { get; set; } = new();

        /// <summary>
        /// Monedas activas con su tasa vigente del día.
        /// El POS las usa en CobroActivity para ofrecer opciones de pago.
        /// </summary>
        [JsonPropertyName("monedas")]
        public List<MonedaCatalogo> Monedas { get; set; } = new();
    }
}
