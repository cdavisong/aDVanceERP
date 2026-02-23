using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    /// <summary>
    /// Raíz del JSON de catálogo exportado por el ERP desktop.
    /// Ruta esperada en el dispositivo:
    ///   /data/data/aDVancePOS.Mobile/files/catalogo.json
    /// </summary>
    public class CatalogoJson {
        [JsonPropertyName("meta")]
        public CatalogoMeta Meta { get; set; } = new();

        [JsonPropertyName("productos")]
        public List<ProductoCatalogo> Productos { get; set; } = new();
    }
}
