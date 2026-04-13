using System.Globalization;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class StockPorAlmacen {
        public string NombreAlmacen { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public int CantidadSkus { get; set; }

        public override string ToString() {
            return $"{NombreAlmacen}: {CantidadSkus} SKUs, Valor Total: {ValorTotal.ToString("N2", CultureInfo.InvariantCulture)}";
        }
    }
}
