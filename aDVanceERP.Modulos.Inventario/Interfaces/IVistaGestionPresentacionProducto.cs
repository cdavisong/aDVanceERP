using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Monedas;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaGestionPresentacionProducto : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaPresentacionProducto>, INavegadorTuplasEntidades {
        bool ModoEdicion { get; set; }
        long IdProducto { get; set; }
        UnidadMedida? UnidadMedida { get; set; }
        decimal Cantidad { get; set; }
        decimal PrecioVenta { get; set; }
        Moneda? MonedaPrecioVenta { get; set; }

        void CargarUnidadesMedida(UnidadMedida[] unidadesMedida);
        void CargarMonedas(Moneda[] monedas);
        void CargarDatosProducto(Producto producto);
    }
}
