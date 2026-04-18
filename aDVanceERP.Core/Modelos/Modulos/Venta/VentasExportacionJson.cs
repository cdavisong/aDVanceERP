using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    /// <summary>
    /// Raíz del JSON de ventas del día.
    /// Incluye ventas completadas y ventas en espera.
    /// Solo las "Completadas" son importadas por el ERP.
    /// </summary>
    public class VentasExportacionJson {
        [JsonPropertyName("meta")]
        public ExportacionMeta Meta { get; set; } = new();

        [JsonPropertyName("ventas")]
        public List<VentaExportacion> Ventas { get; set; } = new();

        /// <summary>Ventas archivadas esperando confirmación de transferencia.</summary>
        [JsonPropertyName("ventasEnEspera")]
        public List<VentaExportacion>? VentasEnEspera { get; set; } = new();
    }
}
