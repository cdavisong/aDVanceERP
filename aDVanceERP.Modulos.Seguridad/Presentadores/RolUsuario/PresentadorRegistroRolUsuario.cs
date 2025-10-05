using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Vistas.Permiso.Plantillas;
using aDVanceERP.Modulos.Seguridad.Vistas.RolUsuario.Plantillas;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.RolUsuario;

public class PresentadorRegistroRolUsuario : PresentadorVistaRegistro<IVistaRegistroRolUsuario, Core.Modelos.Modulos.Seguridad.RolUsuario,
    RepoRolUsuario, FiltroBusquedaRolUsuario> {
    public PresentadorRegistroRolUsuario(IVistaRegistroRolUsuario vista) : base(vista) { 
        AgregadorEventos.Suscribir("MostrarVistaRegistroRolUsuario", OnMostrarVistaRegistroRolUsuario);
        AgregadorEventos.Suscribir("MostrarVistaEdicionRolUsuario", OnMostrarVistaEdicionRolUsuario);
    }

    private void OnMostrarVistaRegistroRolUsuario(string obj) {
        Vista.Restaurar();
        Vista.Mostrar();
    }

    private void OnMostrarVistaEdicionRolUsuario(string obj) {
        if (string.IsNullOrEmpty(obj))
            return;

        var rolUsuario = AgregadorEventos.DeserializarPayload<Core.Modelos.Modulos.Seguridad.RolUsuario>(obj);

        if (rolUsuario == null)
            return;

        Vista.Restaurar();

        PopularVistaDesdeEntidad(rolUsuario);

        Vista.Mostrar();
    }

    public override void PopularVistaDesdeEntidad(Core.Modelos.Modulos.Seguridad.RolUsuario entidad) {
        base.PopularVistaDesdeEntidad(entidad);
        
        var modulos = RepoModulo.Instancia.ObtenerTodos();
        var permisosRoles = RepoPermisoRolUsuario.Instancia.Buscar(FiltroBusquedaPermisoRolUsuario.IdRolUsuario, entidad.Id.ToString()).entidades;

        Vista.NombreRolUsuario = entidad.Nombre;
        Vista.CargarNombresModulos(modulos.Select(m => m.Nombre).ToArray());

        foreach (var permisoRol in permisosRoles)
            ((IVistaGestionPermisos) Vista).AdicionarPermisoRol(permisoRol.NombrePermiso);
    }

    protected override Core.Modelos.Modulos.Seguridad.RolUsuario? ObtenerEntidadDesdeVista() {
        return new Core.Modelos.Modulos.Seguridad.RolUsuario(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.NombreRolUsuario
        );
    }
}