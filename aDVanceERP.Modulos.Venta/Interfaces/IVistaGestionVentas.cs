using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Ventas;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaGestionVentas : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaVenta>, INavegadorTuplasEntidades {
        
    }
}