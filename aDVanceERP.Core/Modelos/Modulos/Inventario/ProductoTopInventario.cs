using System.Globalization;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class ProductoTopInventario {
        public string Nombre { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal ValorTotal { get; set; }

        public override string ToString() {
            return $"{Nombre}: Cantidad={Cantidad}, ValorTotal={ValorTotal.ToString("N2", CultureInfo.InvariantCulture)}";
        }
    }
}
