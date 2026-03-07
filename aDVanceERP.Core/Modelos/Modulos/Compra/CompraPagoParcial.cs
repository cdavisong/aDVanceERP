namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public class CompraPagoParcial {
        public long IdCompra { get; set; }
        public string CodigoCompra { get; set; } = string.Empty;
        public DateTime FechaOrden { get; set; }
        public decimal TotalCompra { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public int CantidadPagos { get; set; }
        public string CodigoProveedor { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;

        public decimal PorcentajePagado => TotalCompra > 0
            ? (TotalPagado / TotalCompra) * 100
            : 0;

        public string EstadoPago => PorcentajePagado switch {
            >= 75 => "Casi Completa",
            >= 50 => "Medio Pagada",
            >= 25 => "Poco Avance",
            _ => "Inicial"
        };
    }
}
