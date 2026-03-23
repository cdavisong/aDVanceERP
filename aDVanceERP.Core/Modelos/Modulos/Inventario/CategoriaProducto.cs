using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Inventario {
    public enum CategoriaProducto {
        [Display(Name = "Mercancía")]
        Mercancia,
        [Display(Name = "Producto Terminado")]
        ProductoTerminado,
        [Display(Name = "Materia Prima")]
        MateriaPrima
    }
}
