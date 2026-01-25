using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Ventas {
    public sealed class DetallePagoTransferencia : IEntidadBaseDatos {
        public DetallePagoTransferencia() {
            MontoTransferencia = 0.0m;
        }

        public DetallePagoTransferencia(long id, long idPago, string numeroConfirmacion,
                                       string numeroTransaccion, decimal montoTransferencia) {
            Id = id;
            IdPago = idPago;
            NumeroConfirmacion = numeroConfirmacion;
            NumeroTransaccion = numeroTransaccion;
            MontoTransferencia = montoTransferencia;
        }

        public long Id { get; set; }
        public long IdPago { get; set; }
        public string NumeroConfirmacion { get; set; }
        public string NumeroTransaccion { get; set; }
        public decimal MontoTransferencia { get; set; }
    }

    public enum FiltroBusquedaDetalleTransferencia {
        PorPago,
        PorNumeroConfirmacion,
        PorNumeroTransaccion
    }
}