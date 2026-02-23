namespace aDVancePOS.Mobile.Modelos {
    /// <summary>
    /// Ítem del carrito. Se crea cuando el cajero pulsa "+" en
    /// un producto. No se serializa directamente; se usa para
    /// construir VentaExportacion al cerrar la venta.
    /// </summary>
    public class ItemCarrito {
        public ProductoCatalogo Producto { get; set; } = null!;
        public decimal Cantidad { get; set; } = 1;

        public decimal Subtotal =>
            Math.Round(Producto.PrecioConImpuesto * Cantidad, 2);
    }
}
