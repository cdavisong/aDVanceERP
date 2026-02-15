namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class EstadoPagoVenta {
        public decimal ImporteTotal { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal Saldo { get; set; }
        public bool EstaPagadaCompletamente { get; set; }
        public bool TienePagosPendientes { get; set; }
        public bool TieneSobrepago { get; set; }
    }
}
