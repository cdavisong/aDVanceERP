using aDVanceERP.Core.Modelos.Comun.Interfaces;

namespace aDVanceERP.Core.Modelos.Modulos.Compra {
    public sealed class DetalleCompraProducto : IEntidadBaseDatos {
        public DetalleCompraProducto() {
            CantidadOrdenada = 0;
            CantidadRecibida = 0;
            CostoUnitario = 0;
            Descuento = 0;
            ImpuestoPorcentaje = 0;
        }

        public DetalleCompraProducto(
            long id,
            long idCompra,
            long idProducto,
            decimal cantidadOrdenada,
            decimal cantidadRecibida,
            decimal costoUnitario,
            decimal descuento,
            decimal impuestoPorcentaje) {
            Id = id;
            IdCompra = idCompra;
            IdProducto = idProducto;
            CantidadOrdenada = cantidadOrdenada;
            CantidadRecibida = cantidadRecibida;
            CostoUnitario = costoUnitario;
            Descuento = descuento;
            ImpuestoPorcentaje = impuestoPorcentaje;
        }

        public long Id { get; set; }
        public long IdCompra { get; set; }
        public long IdProducto { get; set; }
        public decimal CantidadOrdenada { get; set; }
        public decimal CantidadRecibida { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal ImpuestoPorcentaje { get; set; }

        // Propiedades calculadas
        public decimal SubtotalLinea => CantidadOrdenada * CostoUnitario;
        public decimal TotalLineaConImpuesto => SubtotalLinea * (1 + (ImpuestoPorcentaje / 100));
        public decimal PendientePorRecibir => CantidadOrdenada - CantidadRecibida;
        public bool EstaCompletamenteRecibido => PendientePorRecibir <= 0;
    }

    public enum FiltroBusquedaDetalleCompra {
        Todos,
        Id,
        IdCompra,
        IdProducto
    }

    public static class UtilesBusquedaDetalleCompra {
        public static object[] Filtros = {
            "Todos los detalles",
            "Identificador de BD",
            "ID de compra",
            "ID del producto"
        };
    }
}