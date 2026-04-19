// ╔══════════════════════════════════════════════════════════════════════╗
// ║  CONTRATO JSON — MIRROR DE CORE                                       ║
// ║  Espejo de: Core/Modelos/Modulos/Venta/DetalleExportacion.cs
// ║                                                                        ║
// ║  Android no puede referenciar la DLL de Core, por lo que esta copia   ║
// ║  es necesaria. Si modificas el modelo original en Core, actualiza      ║
// ║  este archivo manualmente para mantener el contrato sincronizado.      ║
// ╚══════════════════════════════════════════════════════════════════════╝

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
