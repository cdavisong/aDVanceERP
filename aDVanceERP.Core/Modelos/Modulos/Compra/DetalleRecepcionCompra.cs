using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class DetalleRecepcionCompra : IEntidadBaseDatos {
        public DetalleRecepcionCompra() {
            CantidadRecibida = 0;
        }

        public DetalleRecepcionCompra(
            long id,
            long idRecepcionCompra,
            long idDetalleCompraProducto,
            decimal cantidadRecibida) {
            Id = id;
            IdRecepcionCompra = idRecepcionCompra;
            IdDetalleCompraProducto = idDetalleCompraProducto;
            CantidadRecibida = cantidadRecibida;
        }

        public long Id { get; set; }
        public long IdRecepcionCompra { get; set; }
        public long IdDetalleCompraProducto { get; set; }
        public decimal CantidadRecibida { get; set; }
    }

    public enum FiltroBusquedaDetalleRecepcion {
        Todos,
        Id,
        IdRecepcionCompra,
        IdDetalleCompraProducto
    }

    public static class UtilesBusquedaDetalleRecepcion {
        public static object[] Filtros = {
            "Todos los detalles",
            "Identificador de BD",
            "ID de recepción",
            "ID del detalle de compra"
        };
    }
}