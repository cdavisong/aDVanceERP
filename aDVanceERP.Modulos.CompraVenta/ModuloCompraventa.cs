using aDVanceERP.Core.Extension.Interfaces;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.CompraVenta; 

public class ModuloCompraventa : IModuloExtension {
    public string Nombre => "MOD_COMPRAVENTA";

    public string Descripcion => """
        Módulo para la gestión de compras y ventas en aDVanceERP. El módulo permite:
        - Registrar y gestionar órdenes de compra y venta.
        - Vincular proveedores y clientes a las transacciones.
        - Generar facturas y recibos asociados a las compras y ventas.
        - Realizar seguimiento del estado de las órdenes y pagos.
        - Integrar con el inventario para actualizar automáticamente las existencias.
        - Generar reportes de compras y ventas para análisis financiero.
        - Configurar permisos específicos para usuarios relacionados con las operaciones de compra y venta.
        """;

    public Version Version => new Version(0, 1, 0, 0);

    public static readonly string[] Permisos = {
        "MOD_COMPRAVENTA_TODOS",
        "MOD_COMPRAVENTA_COMPRA_TODOS",
        "MOD_COMPRAVENTA_COMPRA_ADICIONAR",
        "MOD_COMPRAVENTA_COMPRA_EDITAR",
        "MOD_COMPRAVENTA_COMPRA_ELIMINAR",
        "MOD_COMPRAVENTA_VENTA_TODOS",
        "MOD_COMPRAVENTA_VENTA_ADICIONAR",
        "MOD_COMPRAVENTA_VENTA_EDITAR",
        "MOD_COMPRAVENTA_VENTA_ELIMINAR"
    };

    public void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
        //throw new NotImplementedException();
    }

    public void Apagar() {
        throw new NotImplementedException();
    }
}