using aDVanceERP.Core.Modelos.Comun.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    public class PermisoRol : IEntidadBaseDatos {
        public long Id { get; set; }
        public long IdRol { get; set; }
        public string Modulo { get; set; }
        public bool PuedeVer { get; set; }
        public bool PuedeCrear { get; set; }
        public bool PuedeEditar { get; set; }
        public bool PuedeEliminar { get; set; }
    }

    public enum FiltroBusquedaPermisoRol {
        Todos,
        [Display(Name = "ID del Rol")]
        IdRol,
        [Display(Name = "Módulo")]
        Modulo
    }
}