using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.RecursosHumanos.Interfaces;

public interface IVistaGestionProveedores : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaProveedor>,
    INavegadorTuplasEntidades { }