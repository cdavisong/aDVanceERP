namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class StockPorAlmacen {
        public string NombreAlmacen { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public int CantidadSkus { get; set; }
    }
}
