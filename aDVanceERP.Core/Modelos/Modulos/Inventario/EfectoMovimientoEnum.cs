using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public enum EfectoMovimientoEnum {
        Ninguno,
        [Display(Name = "Carga", Description = "Movimiento que incrementa el inventario, como compras o devoluciones de clientes.")]
        Carga,
        [Display(Name = "Descarga", Description = "Movimiento que decrementa el inventario, como ventas o devoluciones a proveedores.")]
        Descarga,
        [Display(Name = "Transferencia", Description = "Movimiento que transfiere inventario entre almacenes.")]
        Transferencia
    }
}