// ============================================================
//  aDVanceSTOCK.Mobile — ProductoSesion
//  Archivo: Modelos/ProductoSesion.cs
//
//  Representa un producto que el almacenero registra durante
//  la sesión actual. Puede ser nuevo o una entrada de stock
//  de un producto ya existente en el ERP.
// ============================================================

using System.Text.Json.Serialization;

namespace aDVanceSTOCK.Mobile.Modelos {

    /// <summary>
    /// Tipo de operación que el ERP realizará al importar.
    /// </summary>
    public enum TipoRegistro {
        Nuevo,
        EntradaStock
    }

    /// <summary>
    /// Producto registrado en la sesión actual.
    /// Es el ítem central que circula entre Activities vía SesionService.
    /// </summary>
    public class ProductoSesion {

        // ── Identificación ────────────────────────────────────────────
        public string Codigo     { get; set; } = "";
        public string Nombre     { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public TipoRegistro Tipo { get; set; } = TipoRegistro.Nuevo;

        // ── Clasificación (solo para Nuevo) ───────────────────────────
        public string Categoria  { get; set; } = "Mercancia"; // enum ERP
        public bool   EsVendible { get; set; } = true;

        public long   IdProveedor            { get; set; }
        public string NombreProveedor        { get; set; } = "";
        public long   IdUnidadMedida         { get; set; }
        public string NombreUnidadMedida     { get; set; } = "";
        public long   IdClasificacion        { get; set; }
        public string NombreClasificacion    { get; set; } = "";

        // ── Precios (solo para Nuevo) ─────────────────────────────────
        public decimal CostoAdquisicionUnitario { get; set; }
        public decimal PrecioVentaBase          { get; set; }

        // ── Stock ─────────────────────────────────────────────────────
        /// <summary>
        /// Stock inicial (Nuevo) o cantidad adicional (EntradaStock).
        /// </summary>
        public decimal Cantidad { get; set; }

        // ── Imagen ────────────────────────────────────────────────────
        /// <summary>
        /// Ruta local de la foto tomada. Null si no se capturó imagen.
        /// Al exportar, el archivo se copia como {Codigo}.jpg en /imagenes/.
        /// </summary>
        public string? RutaImagenLocal { get; set; }

        public bool TieneImagen => !string.IsNullOrEmpty(RutaImagenLocal)
                                   && System.IO.File.Exists(RutaImagenLocal);

        // ── Metadatos ─────────────────────────────────────────────────
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        /// <summary>Etiqueta legible para mostrar en listas.</summary>
        public string EtiquetaTipo => Tipo == TipoRegistro.Nuevo
            ? "NUEVO"
            : "ENTRADA STOCK";
    }
}
