using System.Text.Json.Serialization;

namespace aDVancePOS.Mobile.Modelos {
    /// <summary>
    /// Producto del catálogo. Solo incluye los campos necesarios
    /// para el POS móvil (no costos de adquisición, etc.).
    /// Solo aparece en el catálogo si stockDisponible > 0.
    /// </summary>
    public class ProductoCatalogo {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("categoria")]
        public string Categoria { get; set; } = string.Empty;

        [JsonPropertyName("precioVentaBase")]
        public decimal PrecioVentaBase { get; set; }

        [JsonPropertyName("impuestoVentaPorcentaje")]
        public decimal ImpuestoVentaPorcentaje { get; set; }

        [JsonPropertyName("stockDisponible")]
        public decimal StockDisponible { get; set; }

        [JsonPropertyName("unidadMedida")]
        public string UnidadMedida { get; set; } = string.Empty;

        [JsonPropertyName("presentaciones")]
        public List<PresentacionVenta> Presentaciones { get; set; } = new();

        // ── Calculados en runtime (no vienen en el JSON) ──────

        /// <summary>Precio final = PrecioVentaBase * (1 + Impuesto/100)</summary>
        [JsonIgnore]
        public decimal PrecioConImpuesto => PrecioVentaBase;

        /// <summary>Stock restante tras agregar unidades al carrito en sesión.</summary>
        [JsonIgnore]
        public decimal StockEnSesion { get; set; }
    }

    /// <summary>
    /// Representa una presentación de venta para un producto.
    /// </summary>
    public class PresentacionVenta {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("cantidad")]
        public decimal Cantidad { get; set; }

        [JsonPropertyName("precioVenta")]
        public decimal PrecioVenta { get; set; }

        [JsonPropertyName("activo")]
        public bool Activo { get; set; }

        [JsonPropertyName("unidadMedida")]
        public string UnidadMedida { get; set; } = string.Empty;

        [JsonIgnore]
        public decimal PrecioPorUnidad => Cantidad > 0 ? PrecioVenta / Cantidad : 0m;
    }
}
