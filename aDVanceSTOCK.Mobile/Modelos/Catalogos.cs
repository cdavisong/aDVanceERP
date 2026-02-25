// ============================================================
//  aDVanceSTOCK.Mobile — Modelos de catálogos de apoyo
//  Archivo: Modelos/Catalogos.cs
//
//  Estos modelos representan los datos que el ERP exporta
//  al teléfono vía adb push antes de iniciar el registro.
// ============================================================

using System.Text.Json.Serialization;

namespace aDVanceSTOCK.Mobile.Modelos {

    // ── Producto existente en el ERP (para detección de duplicados) ──
    public class ProductoExistente {
        [JsonPropertyName("id")]     public long   Id     { get; set; }
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = "";
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = "";
    }

    // ── Proveedor ─────────────────────────────────────────────────────
    public class ProveedorCatalogo {
        [JsonPropertyName("id")]          public long   Id         { get; set; }
        [JsonPropertyName("razonSocial")] public string RazonSocial { get; set; } = "";
        [JsonPropertyName("codigo")]      public string Codigo     { get; set; } = "";

        // Para mostrar en Spinner: "Proveedor X (PV-001)"
        public override string ToString() => $"{RazonSocial} ({Codigo})";
    }

    // ── Unidad de medida ──────────────────────────────────────────────
    public class UnidadMedidaCatalogo {
        [JsonPropertyName("id")]           public long   Id          { get; set; }
        [JsonPropertyName("nombre")]       public string Nombre      { get; set; } = "";
        [JsonPropertyName("abreviatura")]  public string Abreviatura { get; set; } = "";

        public override string ToString() => $"{Nombre} ({Abreviatura})";
    }

    // ── Clasificación de producto ─────────────────────────────────────
    public class ClasificacionCatalogo {
        [JsonPropertyName("id")]          public long   Id          { get; set; }
        [JsonPropertyName("nombre")]      public string Nombre      { get; set; } = "";
        [JsonPropertyName("descripcion")] public string Descripcion { get; set; } = "";

        public override string ToString() => Nombre;
    }

    // ── Almacén ───────────────────────────────────────────────────────
    public class AlmacenCatalogo {
        [JsonPropertyName("id")]     public long   Id     { get; set; }
        [JsonPropertyName("nombre")] public string Nombre { get; set; } = "";
        [JsonPropertyName("codigo")] public string Codigo { get; set; } = "";

        public override string ToString() => Nombre;
    }
}
