using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Vistas.Permiso.Plantillas;

public interface IVistaGestionPermisos : IVistaContenedor, IGestorEntidades
{
    void AdicionarPermisoRol(string nombrePermiso = "");
}