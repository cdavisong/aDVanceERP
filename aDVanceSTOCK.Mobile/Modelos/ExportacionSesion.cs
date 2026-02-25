// ============================================================
//  aDVanceSTOCK.Mobile — Modelos de exportación
//  Archivo: Modelos/Exportacion.cs
//
//  Estructura del JSON que se genera al finalizar la sesión
//  y que el ERP desktop importa vía adb pull.
//
//  Nombre del archivo: stock_YYYYMMDD_HHmmss.json
// ============================================================

using System.Text.Json.Serialization;

namespace aDVanceSTOCK.Mobile.Modelos {

    /// <summary>Raíz del JSON de exportación.</summary>
    public class ExportacionSesion {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0";

        [JsonPropertyName("app")] 
        public string App { get; set; } = "aDVance Stock Mobile";

        [JsonPropertyName("fechaSesion")] 
        public DateTime FechaSesion { get; set; } = DateTime.Now;

        [JsonPropertyName("idAlmacen")] 
        public long IdAlmacen { get; set; }

        [JsonPropertyName("nombreAlmacen")] 
        public string NombreAlmacen { get; set; } = "";

        [JsonPropertyName("totalItems")] 
        public int TotalItems { get; set; }

        [JsonPropertyName("productos")] 
        public List<ProductoExportacion> Productos { get; set; } = new();
    }
}
