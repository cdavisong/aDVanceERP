namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class DetalleValoresPorTipoMovimiento {
        public int IdTipoMovimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public string Efecto { get; set; } // "Carga", "Descarga", "Transferencia"
        public int CantidadMovimientos { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalUnidades { get; set; }
        public DateTime PrimerMovimiento { get; set; }
        public DateTime UltimoMovimiento { get; set; }

        public override string ToString() {
            return $"{TipoMovimiento} ({Efecto}): {ValorTotal:C2} - {CantidadMovimientos} movimientos";
        }
    }
}
