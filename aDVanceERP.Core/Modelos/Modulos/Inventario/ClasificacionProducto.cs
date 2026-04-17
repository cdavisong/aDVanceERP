using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class ClasificacionProducto : IEquatable<ClasificacionProducto>, IEntidadBaseDatos {
        public ClasificacionProducto() {
            Nombre = string.Empty;
            Descripcion = string.Empty;
        }

        public ClasificacionProducto(long id, string nombre, string descripcion) {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public override bool Equals(object? obj) {
            return Equals(obj as ClasificacionProducto);
        }

        public bool Equals(ClasificacionProducto? other) {
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
            return $"{Nombre}";
        }
    }

    public enum FiltroBusquedaClasificacionProducto {
        Todos,
        [Display(Name = "ID")]
        Id,
        Nombre
    }
}