namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class VentaPendientePago {
        public long IdVenta { get; set; }
        public string NumeroFacturaTicket { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public int PagosPendientesConfirmacion { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
    }
}
