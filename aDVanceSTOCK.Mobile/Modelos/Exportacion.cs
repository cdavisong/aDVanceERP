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
        [JsonPropertyName("version")]       public string Version     { get; set; } = "1.0";
        [JsonPropertyName("app")]           public string App         { get; set; } = "aDVance Stock Mobile";
        [JsonPropertyName("fechaSesion")]   public DateTime FechaSesion { get; set; } = DateTime.Now;
        [JsonPropertyName("idAlmacen")]     public long   IdAlmacen   { get; set; }
        [JsonPropertyName("nombreAlmacen")] public string NombreAlmacen { get; set; } = "";
        [JsonPropertyName("totalItems")]    public int    TotalItems   { get; set; }
        [JsonPropertyName("productos")]     public List<ProductoExportacion> Productos { get; set; } = new();
    }

    /// <summary>
    /// Un ítem dentro del JSON de exportación.
    /// El campo "tipo" le dice al ERP qué operación realizar:
    ///   "nuevo"        → crear producto y registrar stock inicial
    ///   "entrada_stock" → solo sumar cantidad al inventario existente
    /// </summary>
    public class ProductoExportacion {
        [JsonPropertyName("tipo")]   public string Tipo   { get; set; } = "nuevo";
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = "";

        // ── Campos para tipo = "nuevo" ────────────────────────────────
        [JsonPropertyName("nombre")]       public string? Nombre      { get; set; }
        [JsonPropertyName("descripcion")]  public string? Descripcion { get; set; }
        [JsonPropertyName("categoria")]    public string? Categoria   { get; set; }
        [JsonPropertyName("esVendible")]   public bool?   EsVendible  { get; set; }

        [JsonPropertyName("idProveedor")]         public long? IdProveedor         { get; set; }
        [JsonPropertyName("idUnidadMedida")]      public long? IdUnidadMedida      { get; set; }
        [JsonPropertyName("idClasificacion")]     public long? IdClasificacion     { get; set; }

        [JsonPropertyName("costoAdquisicionUnitario")] public decimal? CostoAdquisicionUnitario { get; set; }
        [JsonPropertyName("precioVentaBase")]           public decimal? PrecioVentaBase          { get; set; }

        // ── Campos comunes ────────────────────────────────────────────
        /// <summary>
        /// Stock inicial (nuevo) o cantidad adicional (entrada_stock).
        /// </summary>
        [JsonPropertyName("cantidad")]    public decimal Cantidad    { get; set; }
        [JsonPropertyName("tieneImagen")] public bool   TieneImagen { get; set; }
        [JsonPropertyName("fechaRegistro")] public DateTime FechaRegistro { get; set; }
    }
}
