using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    /// <summary>
    /// Raíz del JSON de ventas exportado desde el móvil.
    /// Ruta de escritura:
    ///   /data/data/aDVancePOS.Mobile/files/ventas_YYYYMMDD.json
    /// </summary>
    public class VentasExportacionJson {
        [JsonPropertyName("meta")]
        public ExportacionMeta Meta { get; set; } = new();

        [JsonPropertyName("ventas")]
        public List<VentaExportacion> Ventas { get; set; } = new();
    }
}
