using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Inventario.Interfaces {
    public interface IVistaGestionMovimientos : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaMovimiento>,
        INavegadorTuplasEntidades
    { }
}