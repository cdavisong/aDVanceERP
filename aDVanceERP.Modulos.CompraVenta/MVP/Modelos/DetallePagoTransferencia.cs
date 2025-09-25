using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos; 

public class DetallePagoTransferencia : IEntidadBaseDatos {
    public DetallePagoTransferencia() { }

    public DetallePagoTransferencia(long id, long idVenta, long idTarjeta, string numeroConfirmacion,
        string numeroTransaccion) {
        Id = id;
        IdVenta = idVenta;
        IdTarjeta = idTarjeta;
        NumeroConfirmacion = numeroConfirmacion;
        NumeroTransaccion = numeroTransaccion;
    }

    public long IdVenta { get; set; }
    public long IdTarjeta { get; set; }
    public string? NumeroConfirmacion { get; set; }
    public string? NumeroTransaccion { get; set; }

    public long Id { get; set; }
}

public enum FiltroBusquedaDetallePagoTransferencia {
    Todos,
    Id,
    IdVenta,
    IdTarjeta
}