using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public enum TipoAlmacen {
        Primario,
        Secundario,
        [Display(Name = "Tránsito")]
        Transito,
        Especial
    }
}