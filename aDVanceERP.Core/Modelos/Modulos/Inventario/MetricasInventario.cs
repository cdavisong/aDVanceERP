namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class MetricasInventario {
        /// <summary>
        /// Número de productos activos (Activo = 1).
        /// </summary>
        public int TotalProductos { get; set; }

        /// <summary>
        /// Productos cuyo stock está en o por debajo del mínimo configurado.
        /// </summary>
        public int ProductosBajoStockMinimo { get; set; }

        /// <summary>
        /// Productos con cantidad = 0 en todos los almacenes.
        /// </summary>
        public int ProductosSinStock { get; set; }

        /// <summary>
        /// Suma de (cantidad × costo_promedio) de toda la tabla adv__inventario.
        /// </summary>
        public decimal ValorTotalInventario { get; set; }

        /// <summary>Número de almacenes activos (estado = 1).</summary>
        public int TotalAlmacenes { get; set; }

        /// <summary>
        /// Movimientos registrados en las últimas 24 h.
        /// </summary>
        public int MovimientosHoy { get; set; }

        /// <summary>
        /// Top 5 productos con mayor valor en inventario.
        /// </summary>
        public List<ProductoTopInventario> TopProductosValor { get; set; } = [];

        /// <summary>
        /// Distribución de stock por almacén (nombre + valor total).
        /// </summary>
        public List<StockPorAlmacen> StockPorAlmacen { get; set; } = [];

        /// <summary>
        /// Últimos 30 días — entradas y salidas agrupadas por día.
        /// </summary>
        public List<MovimientoDiario> EvolucionMovimientos { get; set; } = [];
    }
}
