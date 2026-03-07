namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public class ResumenPagosProveedor {
        public long IdProveedor { get; set; }
        public string CodigoProveedor { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        public int TotalCompras { get; set; }
        public int ComprasPendientesPago { get; set; }
        public decimal MontoTotalComprado { get; set; }
        public decimal MontoTotalPagado { get; set; }
        public decimal SaldoTotalPendiente { get; set; }

        public decimal PorcentajePagado => MontoTotalComprado > 0
            ? (MontoTotalPagado / MontoTotalComprado) * 100
            : 0;
    }
}
