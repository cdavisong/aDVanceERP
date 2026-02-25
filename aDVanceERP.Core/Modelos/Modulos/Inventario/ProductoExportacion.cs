using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    /// <summary>
    /// Un ítem dentro del JSON de exportación.
    /// El campo "tipo" le dice al ERP qué operación realizar:
    ///   "nuevo"        → crear producto y registrar stock inicial
    ///   "entrada_stock" → solo sumar cantidad al inventario existente
    /// </summary>
    public class ProductoExportacion {
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = "nuevo";

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; } = "";

        // ── Campos para tipo = "nuevo" ────────────────────────────────
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("categoria")]
        public string? Categoria { get; set; }

        [JsonPropertyName("esVendible")] 
        public bool? EsVendible { get; set; }

        [JsonPropertyName("idProveedor")] 
        public long? IdProveedor { get; set; }

        [JsonPropertyName("idUnidadMedida")] 
        public long? IdUnidadMedida { get; set; }

        [JsonPropertyName("idClasificacion")] 
        public long? IdClasificacion { get; set; }

        [JsonPropertyName("costoAdquisicionUnitario")] 
        public decimal? CostoAdquisicionUnitario { get; set; }

        [JsonPropertyName("precioVentaBase")] 
        public decimal? PrecioVentaBase { get; set; }

        // ── Campos comunes ────────────────────────────────────────────
        /// <summary>
        /// Stock inicial (nuevo) o cantidad adicional (entrada_stock).
        /// </summary>
        [JsonPropertyName("cantidad")] 
        public decimal Cantidad { get; set; }

        [JsonPropertyName("tieneImagen")] 
        public bool TieneImagen { get; set; }

        [JsonPropertyName("fechaRegistro")] 
        public DateTime FechaRegistro { get; set; }
    }
}
