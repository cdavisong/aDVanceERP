using System.Globalization;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class MovimientoDiario {
        public DateTime Fecha { get; set; }
        public decimal Entradas { get; set; }
        public decimal Salidas { get; set; }

        public override string ToString() {
            return $"{Fecha:yyyy-MM-dd} - Entradas: {Entradas.ToString("N1", CultureInfo.InvariantCulture)}, Salidas: {Salidas.ToString("N1", CultureInfo.InvariantCulture)}";
        }
    }
}
