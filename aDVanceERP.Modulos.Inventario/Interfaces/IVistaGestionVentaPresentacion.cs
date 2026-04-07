using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaGestionVentaPresentacion : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaPrecioPresentacion>, INavegadorTuplasEntidades {
        long IdProducto { get; set; }
        UnidadMedida? UnidadMedida { get; set; }
        decimal Cantidad { get; set; }
        decimal PrecioVenta { get; set; }

        void CargarDatosProducto(Producto producto);
        void CargarUnidadesMedida(UnidadMedida[] unidadesMedida);
    }
}
