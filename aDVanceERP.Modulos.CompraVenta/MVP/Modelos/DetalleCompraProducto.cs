using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos; 

public class DetalleCompraProducto : IEntidadBaseDatos {
    public DetalleCompraProducto() { }

    public DetalleCompraProducto(long id, long idCompra, long idProducto, decimal cantidad, decimal precioCompra) {
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