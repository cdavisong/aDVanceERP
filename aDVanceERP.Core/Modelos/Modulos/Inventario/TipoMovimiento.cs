using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class TipoMovimiento : IEquatable<TipoMovimiento>, IEntidadBaseDatos {
        public TipoMovimiento() {
            Nombre = string.Empty;
            Efecto = EfectoMovimientoEnum.Ninguno;
        }

        public TipoMovimiento(long id, string nombre, EfectoMovimientoEnum efecto) {
            Id = id;
            Nombre = nombre;
            Efecto = efecto;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public EfectoMovimientoEnum Efecto { get; set; }

        public override bool Equals(object? obj) {
            return Equals(obj as TipoMovimiento);
        }

        public bool Equals(TipoMovimiento? other) {
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
            return Nombre;
        }
    }

    public enum FiltroBusquedaTipoMovimiento {
        Todos,
        [Display(Name = "ID")]
        Id,
        Nombre
    }
}