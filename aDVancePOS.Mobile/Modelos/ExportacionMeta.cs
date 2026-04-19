// ╔══════════════════════════════════════════════════════════════════════╗
// ║  CONTRATO JSON — MIRROR DE CORE                                       ║
// ║  Espejo de: Core/Modelos/Modulos/Venta/ExportacionMeta.cs             ║
// ║                                                                        ║
// ║  Android no puede referenciar la DLL de Core, por lo que esta copia   ║
// ║  es necesaria. Si modificas el modelo original en Core, actualiza      ║
// ║  este archivo manualmente para mantener el contrato sincronizado.      ║
// ╚══════════════════════════════════════════════════════════════════════╝

using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    public class ExportacionMeta {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0";

        [JsonPropertyName("exportadoEn")]
        public DateTime ExportadoEn { get; set; } = DateTime.Now;  // corregido: era DateTime.UtcNow

        [JsonPropertyName("dispositivoId")]
        public string DispositivoId { get; set; } = string.Empty;

        [JsonPropertyName("idAlmacen")]
        public long IdAlmacen { get; set; }

        [JsonPropertyName("totalRecaudado")]
        public decimal TotalRecaudado { get; internal set; }

        [JsonPropertyName("totalVentas")]
        public int TotalVentas { get; internal set; }
    }
}
