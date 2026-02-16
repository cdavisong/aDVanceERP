namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class VentaPagoParcial {
        public long IdVenta { get; set; }
        public string NumeroFacturaTicket { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public int CantidadPagos { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal PorcentajePagado => ImporteTotal > 0 ? (TotalPagado / ImporteTotal) * 100 : 0;
        public string EstadoPago => PorcentajePagado switch {
            >= 75 => "Casi Completo",
            >= 50 => "Medio Pagado",
            >= 25 => "Poco Avance",
            _ => "Inicial"
        };
    }
}
