using System.Text.Json.Serialization;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {

    /// <summary>
    /// Raíz del JSON de sesión exportado por aDVance Stock Mobile.
    ///
    /// CONTRATO DE IMPORTACIÓN — mirror del modelo de la app Android.
    /// Si modificas esta clase actualiza también el modelo correspondiente
    /// en aDVanceSTOCK.Mobile para mantener el contrato sincronizado.
    /// </summary>
    public class SesionStockJson {
        [JsonPropertyName("nombreSesion")]
        public string NombreSesion { get; set; } = string.Empty;

        [JsonPropertyName("fechaSesion")]
        public DateTime FechaSesion { get; set; }

        [JsonPropertyName("items")]
        public List<ItemStockJson> Items { get; set; } = [];
    }

    /// <summary>
    /// Ítem individual dentro de una sesión de inventario de Stock Mobile.
    /// IdProducto = 0 indica un producto nuevo registrado en campo.
    /// </summary>
    public class ItemStockJson {
        [JsonPropertyName("idProducto")]
        public long IdProducto { get; set; }   // 0 si es producto nuevo

        [JsonPropertyName("codigo")]
        public string? Codigo { get; set; }

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("idClasificacion")]
        public long IdClasificacion { get; set; }

        [JsonPropertyName("idUnidadMedida")]
        public long IdUnidadMedida { get; set; }

        [JsonPropertyName("idAlmacen")]
        public long IdAlmacen { get; set; }

        [JsonPropertyName("cantidadRegistrada")]
        public decimal CantidadRegistrada { get; set; }

        [JsonPropertyName("costoUnitario")]
        public decimal CostoUnitario { get; set; }

        [JsonPropertyName("imagenNombreArchivo")]
        public string? ImagenNombreArchivo { get; set; }
    }
}
