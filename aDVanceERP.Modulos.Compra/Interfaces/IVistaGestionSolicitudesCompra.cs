using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Compra.Interfaces {
    internal interface IVistaGestionSolicitudesCompra : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaSolicitudCompra>, INavegadorTuplasEntidades {        
    }
}