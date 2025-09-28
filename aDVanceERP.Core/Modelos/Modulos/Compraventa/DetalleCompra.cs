using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compraventa;

public class DetalleCompra : IEntidadBaseDatos {
    public DetalleCompra() { }

    public DetalleCompra(long id, long idCompra, long idProducto, decimal cantidad, decimal precioCompra) {
        Id = id;
        IdCompra = idCompra;
        IdProducto = idProducto;
        Cantidad = cantidad;
        PrecioCompra = precioCompra;
    }

    public long IdCompra { get; set; }
    public long IdProducto { get; set; }
    public decimal Cantidad { get; set; }
    public decimal PrecioCompra { get; set; }

    public long Id { get; set; }
}

public enum CriterioDetalleCompraProducto {
    Todos,
    Id,
    IdCompra,
    IdProducto
}