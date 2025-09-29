using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compraventa;

public class Pago : IEntidadBaseDatos {
    public Pago() { }

    public Pago(long id, long idVenta, string metodoPago, decimal monto) {
        Id = id;
        IdVenta = idVenta;
        MetodoPago = metodoPago;
        Monto = monto;
        FechaConfirmacion = DateTime.Now;
        Estado = EstadoPago.Pendiente;
    }

    public long Id { get; set; }
    public long IdVenta { get; set; }
    public string? MetodoPago { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaConfirmacion { get; set; }
    public EstadoPago Estado { get; set; }
}

public enum FiltroBusquedaPago {
    Todos,
    Id,
    IdVenta
}