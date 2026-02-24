using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class DetalleSolicitudCompra : IEntidadBaseDatos {
        public DetalleSolicitudCompra() {
            CantidadSolicitada = 0;
            PrecioAdquisicionReferencia = 0;
        }

        public DetalleSolicitudCompra(
            long id,
            long idSolicitudCompra,
            long idProducto,
            decimal cantidadSolicitada,
            decimal precioAdquisicionReferencia) {
            Id = id;
            IdSolicitudCompra = idSolicitudCompra;
            IdProducto = idProducto;
            CantidadSolicitada = cantidadSolicitada;
            PrecioAdquisicionReferencia = precioAdquisicionReferencia;
        }

        public long Id { get; set; }
        public long IdSolicitudCompra { get; set; }
        public long IdProducto { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal PrecioAdquisicionReferencia { get; set; }

        // Propiedad calculada
        public decimal Subtotal => CantidadSolicitada * PrecioAdquisicionReferencia;
    }

    public enum FiltroBusquedaDetalleSolicitudCompra {
        Todos,
        Id,
        IdSolicitudCompra,
        IdProducto
    }

    public static class UtilesBusquedaDetalleSolicitudCompra {
        public static object[] Filtros = {
            "Todos los detalles",
            "Identificador de BD",
            "ID de solicitud de compra",
            "ID del producto"
        };
    }
}