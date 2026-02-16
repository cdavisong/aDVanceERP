namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public class VentaSinPago {
        public long IdVenta { get; set; }
        public string NumeroFacturaTicket { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal ImporteTotal { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public int DiasSinPago { get; set; }
        public string Urgencia => DiasSinPago switch {
            >= 60 => "Crítica",
            >= 30 => "Alta",
            >= 15 => "Media",
            _ => "Normal"
        };
        public bool EsVencida => DiasSinPago > 30;
    }
}
