using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos; 

public class Venta : IEntidadBaseDatos {
    public Venta() { }

    public Venta(long id, DateTime fecha, long idAlmacen, long idCliente, long idTipoEntrega, string? direccionEntrega,
        string? estadoEntrega, decimal total) {
        Id = id;
        Fecha = fecha;
        IdAlmacen = idAlmacen;
        IdCliente = idCliente;
        IdTipoEntrega = idTipoEntrega;
        DireccionEntrega = direccionEntrega;
        EstadoEntrega = estadoEntrega;
        Total = total;
    }

    public DateTime Fecha { get; set; }
    public long IdAlmacen { get; set; }
    public long IdCliente { get; set; }
    public long IdTipoEntrega { get; set; }
    public string? DireccionEntrega { get; set; }
    public string? EstadoEntrega { get; set; }
    public decimal Total { get; set; }

    public long Id { get; set; }
}

public enum FiltroBusquedaVenta {
    Todos,
    Id,
    NombreAlmacen,
    RazonSocialCliente,
    Fecha
}

public static class UtilesBusquedaVenta {
    public static readonly object[] FiltroBusquedaVenta = {
        "Todas las ventas",
        "Identificador de BD",
        "Nombre del almacén",
        "Razón social del cliente",
        "Fecha de la venta"
    };
}