using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos;

public enum TipoMoneda {
    CUP,
    MLC,
    USD
}

public class Pago : IEntidadBaseDatos {
    public Pago() { }

    public Pago(long id, long idVenta, string metodoPago, decimal monto) {
        Id = id;
        IdVenta = idVenta;
        MetodoPago = metodoPago;
        Monto = monto;
        FechaConfirmacion = DateTime.Now;
        Estado = "Confirmado";
    }

    public long IdVenta { get; set; }
    public string? MetodoPago { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaConfirmacion { get; set; }
    public string? Estado { get; set; }

    public long Id { get; set; }
}

public enum FiltroBusquedaPago {
    Todos,
    Id,
    IdVenta
}