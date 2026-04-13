using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class UnidadMedida : IEntidadBaseDatos {
        public UnidadMedida() {
            Nombre = string.Empty;
            Abreviatura = string.Empty;
            Descripcion = string.Empty;
        }

        public UnidadMedida(long id, string nombre, string abreviatura, string descripcion) {
            Id = id;
            Nombre = nombre;
            Abreviatura = abreviatura;
            Descripcion = descripcion;
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }

        public override string ToString() {
            return $"{Nombre} ({Abreviatura})";
        }
    }

    public enum FiltroBusquedaUnidadMedida {
        Todos,
        [Display(Name = "ID")]
        Id,
        Nombre,
        Abreviatura
    }
}
