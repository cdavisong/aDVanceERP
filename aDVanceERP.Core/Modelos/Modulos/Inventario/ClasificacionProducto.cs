using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class ClasificacionProducto : IEntidadBaseDatos {
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