using System.ComponentModel.DataAnnotations;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    public enum ModuloSistemaEnum {
        [Display(Name = "Seguridad", Description = "Proporciona funcionalidades de gestión de usuarios, roles y permisos.")]
        MOD_SEGURIDAD,
        [Display(Name = "Empresa", Description = "Proporciona funcionalidades de gestión de la empresa.")]
        MOD_EMPRESA,
        [Display(Name = "Inventario", Description = "Proporciona funcionalidades de gestión de inventarios y productos.")]
        MOD_INVENTARIO,
        [Display(Name = "Ventas", Description = "Proporciona funcionalidades de gestión de ventas y clientes.")]
        MOD_VENTA,
        [Display(Name = "Compras", Description = "Proporciona funcionalidades de gestión de compras y proveedores.")]
        MOD_COMPRA,
        [Display(Name = "Finanzas", Description = "Proporciona funcionalidades de gestión financiera y contable.")]
        MOD_FINANZAS,
        [Display(Name = "Recursos Humanos", Description = "Proporciona funcionalidades de gestión de empleados y nómina.")]
        MOD_RECURSOS_HUMANOS,
        [Display(Name = "Aplicaciones Móviles", Description = "Proporciona funcionalidades de detección de dispositivos móviles e identificación de aplicaciones asociadas a la suite aDVance ERP.")]
        MOD_MOVIL
    }
}
