namespace aDVancePOS.Mobile.Modelos {
    /// <summary>
    /// Ítem del carrito. Se crea cuando el cajero pulsa "+" en
    /// un producto. No se serializa directamente; se usa para
    /// construir VentaExportacion al cerrar la venta.
    /// </summary>
    public class ItemCarrito {
        public ProductoCatalogo Producto { get; set; } = null!;
        public decimal Cantidad { get; set; } = 1;
        public long IdPresentacion { get; set; } = 0;
        public decimal PrecioUnitario { get; set; } = 0;

        public decimal Subtotal =>
            Math.Round(PrecioUnitario * Cantidad, 2);
    }
}
