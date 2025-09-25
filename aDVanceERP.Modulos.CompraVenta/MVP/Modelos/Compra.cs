using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos; 

public class Compra : IEntidadBaseDatos {
    public Compra() { }

    public Compra(long id, DateTime fecha, long idAlmacen, long idProveedor, decimal total) {
        Id = id;
        Fecha = fecha;
        IdAlmacen = idAlmacen;
        IdProveedor = idProveedor;
        Total = total;
    }

    public DateTime Fecha { get; set; }
    public long IdAlmacen { get; set; }
    public long IdProveedor { get; set; }
    public decimal Total { get; set; }

    public long Id { get; set; }
}

public enum FiltroBusquedaCompra {
    Todos,
    Id,
    NombreAlmacen,
    RazonSocialProveedor,
    Fecha
}

public static class UtilesBusquedaCompra {
    public static readonly object[] FiltroBusquedaCompra = {
        "Todas las compras",
        "Identificador de BD",
        "Nombre del almacén",
        "Razón social del proveedor",
        "Fecha de la compra"
    };
}