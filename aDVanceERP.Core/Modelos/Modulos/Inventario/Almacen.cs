using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public sealed class Almacen : IEquatable<Almacen>, IEntidadBaseDatos {
        public Almacen() {
            Nombre = "Genérico";
            Descripcion = "No hay descripción disponible.";
            Tipo = TipoAlmacenEnum.Secundario;
        }

        public Almacen(long id, string nombre, string? descripcion, string? direccion, TipoAlmacenEnum tipo, bool estado) {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Direccion = direccion;
            Tipo = tipo;
            Estado = estado;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Direccion { get; set; } // Dirección física o ubicación geográfica para envíos y logística.
        public TipoAlmacenEnum Tipo { get; set; }
        public bool Estado { get; set; } //  Indicador de actividad (activo/inactivo) para control operativo

        public override bool Equals(object? obj) {
            return Equals(obj as Almacen);
        }

        public bool Equals(Almacen? other) {
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
            return $"{Id:000} : {Nombre}";
        }
    }

    public enum FiltroBusquedaAlmacen {
        Todos,
        [Display(Name = "ID")]
        Id,
        Nombre
    }
}