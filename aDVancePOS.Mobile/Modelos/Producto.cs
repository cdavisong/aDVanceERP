namespace aDVancePOS.Mobile.Modelos {
    public class Producto {
        public int id_producto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string categoria { get; set; }
        public decimal precio_compra { get; set; }
        public decimal costo_produccion_unitario { get; set; }
        public decimal precio_venta_base { get; set; }
        public decimal cantidad { get; set; }
        public string nombre_almacen { get; set; }
        public string unidad_medida { get; set; }
        public string abreviatura_medida { get; set; }
    }
}