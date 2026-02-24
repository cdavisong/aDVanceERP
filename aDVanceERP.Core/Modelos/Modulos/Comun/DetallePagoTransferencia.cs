using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Comun {
    public sealed class DetallePagoTransferencia : IEntidadBaseDatos {
        public DetallePagoTransferencia() {
            MontoTransferencia = 0.0m;
        }

        public DetallePagoTransferencia(long id, long idPago, string numeroTelefonoRemitente,
                                       string numeroTransaccion, decimal montoTransferencia) {
            Id = id;
            IdPago = idPago;
            NumeroTelefonoRemitente = numeroTelefonoRemitente;
            NumeroTransaccion = numeroTransaccion;
            MontoTransferencia = montoTransferencia;
        }

        public long Id { get; set; }
        public long IdPago { get; set; }
        public string NumeroTelefonoRemitente { get; set; }
        public string NumeroTransaccion { get; set; }
        public decimal MontoTransferencia { get; set; }
    }

    public enum FiltroBusquedaDetalleTransferencia {
        PorPago,
        PorNumeroTelefonoRemitente,
        PorNumeroTransaccion
    }
}