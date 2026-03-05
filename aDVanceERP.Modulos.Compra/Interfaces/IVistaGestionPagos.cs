using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaGestionPagos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaPago>, INavegadorTuplasEntidades {
        
    }
}