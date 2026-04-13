using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public enum EstadoMovimiento {

        [Display(Name = "Pendiente", Description = "Movimiento pendiente de completar")]
        Pendiente,
        [Display(Name = "Completado", Description = "Movimiento completado")]   
        Completado,
        [Display(Name = "Cancelado", Description = "Movimiento cancelado, no se realizará ningún cambio en el inventario")]
        Cancelado
    }
}
