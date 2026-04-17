using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class Inventario : IEquatable<Inventario>, IEntidadBaseDatos {
        public Inventario() { }

        public Inventario(long id, long idProducto, long idAlmacen, decimal cantidad, decimal costoPromedio, decimal valorTotal, DateTime ultimaActualizacion) {
            Id = id;
            IdProducto = idProducto;
            IdAlmacen = idAlmacen;
            Cantidad = cantidad;
            CostoPromedio = costoPromedio;
            ValorTotal = valorTotal;
            UltimaActualizacion = ultimaActualizacion;
        }

        public long Id { get; set; }
        public long IdProducto { get; set; }
        public long IdAlmacen { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime UltimaActualizacion { get; set; }

        public override bool Equals(object? obj) {
            return Equals(obj as Inventario);
        }

        public bool Equals(Inventario? other) {
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
            return $"{Id:000}, ID_ALM: {IdAlmacen}, ID_PROD: {IdProducto}, CANT: {Cantidad}, COST: {CostoPromedio}";
        }
    }

    public enum FiltroBusquedaInventario {
        Todos,
        [Display(Name = "ID")]
        Id,
        [Display(Name = "ID del Producto")]
        IdProducto,
        [Display(Name = "ID del Almacén")]
        IdAlmacen
    }
}