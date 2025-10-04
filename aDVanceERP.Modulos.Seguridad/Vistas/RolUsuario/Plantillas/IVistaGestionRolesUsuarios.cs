using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas.RolUsuario.Plantillas;

public interface IVistaGestionRolesUsuarios : IVistaContenedor, IGestorEntidades,
    IBuscadorEntidades<FiltroBusquedaRolUsuario>, INavegadorTuplasEntidades
{ }