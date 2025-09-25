using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;

namespace aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor.Plantillas;

public interface IVistaGestionProveedores : IVistaContenedor, IGestorEntidades, IBuscadorEntidades<FiltroBusquedaProveedor>,
    INavegadorTuplasEntidades { }