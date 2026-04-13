using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public class TipoMovimiento : IEntidadBaseDatos {
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
    }

    public enum FiltroBusquedaTipoMovimiento {
        Todos,
        [Display(Name = "ID")]
        Id,
        Nombre
    }
}