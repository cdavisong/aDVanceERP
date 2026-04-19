// ╔══════════════════════════════════════════════════════════════════════╗
// ║  CONTRATO JSON — MIRROR DE CORE                                       ║
// ║  Espejo de: Core/Modelos/Modulos/Venta/VentasExportacionJson.cs
// ║                                                                        ║
// ║  Android no puede referenciar la DLL de Core, por lo que esta copia   ║
// ║  es necesaria. Si modificas el modelo original en Core, actualiza      ║
// ║  este archivo manualmente para mantener el contrato sincronizado.      ║
// ╚══════════════════════════════════════════════════════════════════════╝

using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
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
