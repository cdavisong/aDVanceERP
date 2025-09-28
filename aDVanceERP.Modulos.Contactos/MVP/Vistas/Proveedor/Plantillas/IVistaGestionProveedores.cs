using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Contactos;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor.Plantillas;

public interface IVistaGestionProveedores : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaProveedor>,
    INavegadorTuplasEntidades { }