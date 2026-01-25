using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Ventas {
    public sealed class DetallePedidoProducto : IEntidadBaseDatos {
        public DetallePedidoProducto() {
            CantidadSolicitada = 0.0m;
            PrecioVentaReferencia = 0.0m;
        }

        public DetallePedidoProducto(long id, long idPedido, long idProducto, decimal cantidadSolicitada,
                                    decimal precioVentaReferencia, decimal subtotal) {
            Id = id;
            IdPedido = idPedido;
            IdProducto = idProducto;
            CantidadSolicitada = cantidadSolicitada;
            PrecioVentaReferencia = precioVentaReferencia;
            Subtotal = subtotal;
        }

        public long Id { get; set; }
        public long IdPedido { get; set; }
        public long IdProducto { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal PrecioVentaReferencia { get; set; }
        public decimal Subtotal { get; set; }
    }

    public enum FiltroBusquedaDetallePedido {
        PorPedido,
        PorProducto
    }
}