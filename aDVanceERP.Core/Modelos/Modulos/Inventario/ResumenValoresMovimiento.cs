namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class ResumenValoresMovimiento {
        public decimal TotalEntradas { get; set; }
        public decimal TotalSalidas { get; set; }
        public decimal Balance { get; set; }
        public int CantidadEntradas { get; set; }
        public int CantidadSalidas { get; set; }
        public decimal TotalUnidadesMovidas { get; set; }

        public override string ToString() {
            return $"Entradas: {TotalEntradas:C2}, Salidas: {TotalSalidas:C2}, Balance: {Balance:C2}";
        }
    }
}
