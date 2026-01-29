using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Venta {
    public sealed class DetalleVentaProducto : IEntidadBaseDatos {
        public DetalleVentaProducto() {
            Cantidad = 0.0m;
            PrecioCompraVigente = 0.0m;
            PrecioVentaUnitario = 0.0m;
            DescuentoItem = 0.0m;
        }

        public DetalleVentaProducto(long id, long idVenta, long idProducto, decimal cantidad,
                                   decimal precioCompraVigente, decimal precioVentaUnitario,
                                   decimal descuentoItem, decimal subtotal) {
            Id = id;
            IdVenta = idVenta;
            IdProducto = idProducto;
            Cantidad = cantidad;
            PrecioCompraVigente = precioCompraVigente;
            PrecioVentaUnitario = precioVentaUnitario;
            DescuentoItem = descuentoItem;
            Subtotal = subtotal;
        }

        public long Id { get; set; }
        public long IdVenta { get; set; }
        public long IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCompraVigente { get; set; }
        public decimal PrecioVentaUnitario { get; set; }
        public decimal DescuentoItem { get; set; }
        public decimal Subtotal { get; set; }
    }

    public enum FiltroBusquedaDetalleVenta {
        PorVenta,
        PorProducto
    }
}