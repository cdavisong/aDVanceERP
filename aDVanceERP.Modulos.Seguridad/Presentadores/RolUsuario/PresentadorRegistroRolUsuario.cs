using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Vistas.Permiso.Plantillas;
using aDVanceERP.Modulos.Seguridad.Vistas.RolUsuario.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.RolUsuario;

public class PresentadorRegistroRolUsuario : PresentadorVistaRegistro<IVistaRegistroRolUsuario, Core.Modelos.Modulos.Seguridad.RolUsuario,
    RepoRolUsuario, FiltroBusquedaRolUsuario> {
    public PresentadorRegistroRolUsuario(IVistaRegistroRolUsuario vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Seguridad.RolUsuario entidad) {
        Vista.ModoEdicion = true;
        Vista.NombreRolUsuario = entidad.Nombre;

        var permisosRoles = RepoPermisoRolUsuario.Instancia.Buscar(FiltroBusquedaPermisoRolUsuario.Id, entidad.Id.ToString()).entidades;

        foreach (var permisoRol in permisosRoles)
            ((IVistaGestionPermisos) Vista).AdicionarPermisoRol(permisoRol.NombrePermiso);

        _entidad = entidad;
    }

    protected override Core.Modelos.Modulos.Seguridad.RolUsuario? ObtenerEntidadDesdeVista() {
        return new Core.Modelos.Modulos.Seguridad.RolUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreRolUsuario
        );
    }
}