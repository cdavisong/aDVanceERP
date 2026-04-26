using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {

    public sealed class PresentacionProducto : IEquatable<PresentacionProducto>, IEntidadBaseDatos {
        public PresentacionProducto() {
            Cantidad = 1m;
            Activo = true;
        }

        public PresentacionProducto(
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
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Activo { get; set; }
        public decimal PrecioPorUnidad => Cantidad > 0 ? PrecioVenta / Cantidad : 0m;

        public override bool Equals(object? obj) {
            return Equals(obj as PresentacionProducto);
        }

        public bool Equals(PresentacionProducto? other) {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public override string ToString() {
            return $"";
        }
    }

    public enum FiltroBusquedaPresentacionProducto {
        Todos,
        [Display(Name = "ID")]
        Id,
        [Display(Name = "ID del Producto")]
        IdProducto,
        [Display(Name = "Presentaciones Activas")]
        PresentacionesActivas
    }
}
