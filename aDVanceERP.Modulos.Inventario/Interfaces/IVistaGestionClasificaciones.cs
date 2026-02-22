using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    internal interface IVistaGestionClasificaciones : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaClasificacionProducto>, INavegadorTuplasEntidades {
    }
}
