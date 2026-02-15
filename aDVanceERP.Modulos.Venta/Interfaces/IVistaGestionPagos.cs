using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Venta.Interfaces {
    public interface IVistaGestionPagos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaPago>, INavegadorTuplasEntidades {
        
    }
}