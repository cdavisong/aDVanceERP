using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {

    public sealed class PrecioPresentacion : IEntidadBaseDatos {
        public PrecioPresentacion() {
            Cantidad = 1m;
            Activo = true;
        }

        public PrecioPresentacion(
            long id,
            long idProducto,
            long idUnidadMedida,
            decimal cantidad,
            decimal precioVenta,
            bool activo) {
            Id = id;
            IdProducto = idProducto;
            IdUnidadMedida = idUnidadMedida;
            Cantidad = cantidad;
            PrecioVenta = precioVenta;
            Activo = activo;
        }

        public long Id { get; set; }

        public long IdProducto { get; set; }

        public long IdUnidadMedida { get; set; }

        /// <summary>Cantidad de unidades base que agrupa esta presentación.</summary>
        public decimal Cantidad { get; set; }

        /// <summary>Precio total para la cantidad indicada.</summary>
        public decimal PrecioVenta { get; set; }

        public bool Activo { get; set; }

        // ── Propiedades calculadas (no mapeadas a BD) ──────────

        /// <summary>Precio efectivo por unidad base.</summary>
        public decimal PrecioPorUnidad => Cantidad > 0 ? PrecioVenta / Cantidad : 0m;
    }

    // ── Enumeraciones de filtro ──────────────────────────────

    public enum FiltroBusquedaPrecioPresentacion {
        Todos,
        [Display(Name = "ID")]
        Id,
        [Display(Name = "ID del Producto")]
        IdProducto,
        [Display(Name = "Presentaciones Activas")]
        PresentacionesActivas
    }
}
