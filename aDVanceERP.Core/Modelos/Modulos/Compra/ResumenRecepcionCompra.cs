namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public class ResumenRecepcionCompra {
        public long IdCompra { get; set; }
        public string CodigoCompra { get; set; } = string.Empty;
        public int TotalRecepciones { get; set; }
        public DateTime? UltimaRecepcion { get; set; }
        public decimal CantidadTotalPedida { get; set; }
        public decimal CantidadTotalRecibida { get; set; }
        public decimal CantidadPendiente { get; set; }

        public decimal PorcentajeRecibido => CantidadTotalPedida > 0
            ? (CantidadTotalRecibida / CantidadTotalPedida) * 100
            : 0;

        public bool EstaCompletamenteRecibida => CantidadPendiente <= 0;
    }   
}
