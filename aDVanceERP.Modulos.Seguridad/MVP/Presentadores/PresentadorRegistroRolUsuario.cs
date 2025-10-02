using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.Permiso.Plantillas;
using aDVanceERP.Modulos.Seguridad.MVP.Vistas.RolUsuario.Plantillas;
using aDVanceERP.Modulos.Seguridad.Utiles;

namespace aDVanceERP.Modulos.Seguridad.MVP.Presentadores;

public class PresentadorRegistroRolUsuario : PresentadorVistaRegistro<IVistaRegistroRolUsuario, RolUsuario,
    RepoRolUsuario, FiltroBusquedaRolUsuario> {
    public PresentadorRegistroRolUsuario(IVistaRegistroRolUsuario vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(RolUsuario objeto) {
        Vista.ModoEdicion = true;
        Vista.NombreRolUsuario = objeto.Nombre;        

        var permisosRoles = UtilesRolUsuario.ObtenerPermisosDeRol(objeto.Id);

        foreach (var permisoRol in permisosRoles) 
            ((IVistaGestionPermisos)Vista).AdicionarPermisoRol(permisoRol);

        _entidad = objeto;
    }

    protected override RolUsuario? ObtenerEntidadDesdeVista() {
        return new RolUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreRolUsuario
        );
    }
}