namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public class CompraSinPago {
        public long IdCompra { get; set; }
        public string CodigoCompra { get; set; } = string.Empty;
        public DateTime FechaOrden { get; set; }
        public decimal TotalCompra { get; set; }
        public string CodigoProveedor { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        public int DiasSinPago { get; set; }
        public string EstadoCompra { get; set; } = string.Empty;

        public string Urgencia => DiasSinPago switch {
            >= 60 => "Crítica",
            >= 30 => "Alta",
            >= 15 => "Media",
            _ => "Normal"
        };

        public bool EsVencida => DiasSinPago > 30;
    }
}
