using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public enum CategoriaProductoEnum {
        [Display(Name = "Mercancía", Description = "Artículos comprados a proveedores para ser vendidos directamente sin modificaciones. No requieren proceso de fabricación")]
        Mercancia,
        [Display(Name = "Producto Terminado", Description = "Artículos elaborados por la empresa a partir de materias primas y mano de obra. Su costo incluye materiales, actividades de producción y costos asociados")]
        ProductoTerminado,
        [Display(Name = "Materia Prima", Description = "Insumos o materiales utilizados para fabricar productos terminados. Pueden venderse directamente si están configurados para ello")]
        MateriaPrima
    }
}
