using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Vistas.RolUsuario;
using aDVanceERP.Modulos.Seguridad.Vistas.RolUsuario.Plantillas;
using aDVanceERP.Core.Eventos;

namespace aDVanceERP.Modulos.Seguridad.Presentadores.RolUsuario;

public class PresentadorGestionRolesUsuarios : PresentadorVistaGestion<PresentadorTuplaRolUsuario,
    IVistaGestionRolesUsuarios, IVistaTuplaRolUsuario, Core.Modelos.Modulos.Seguridad.RolUsuario, RepoRolUsuario, FiltroBusquedaRolUsuario> {
    public PresentadorGestionRolesUsuarios(IVistaGestionRolesUsuarios vista) : base(vista) {
        AgregadorEventos.Suscribir("MostrarVistaGestionRolesUsuarios", OnMostrarVistaGestionRolesUsuarios);
    }

    private void OnMostrarVistaGestionRolesUsuarios(string obj) {
        Vista.CargarFiltrosBusqueda(UtilesBusquedaRolUsuario.FiltroBusquedaRolUsuario);
        Vista.Restaurar();
        Vista.Mostrar();

        ActualizarResultadosBusqueda();
    }

    protected override PresentadorTuplaRolUsuario ObtenerValoresTupla(Core.Modelos.Modulos.Seguridad.RolUsuario entidad) {
        var presentadorTupla = new PresentadorTuplaRolUsuario(new VistaTuplaRolUsuario(), entidad);
        var permisosRolUsuario = RepoPermisoRolUsuario.Instancia.Buscar(FiltroBusquedaPermisoRolUsuario.IdRolUsuario, entidad.Id.ToString()).entidades;
        var cuentasUsuario = RepoCuentaUsuario.Instancia.Buscar(FiltroBusquedaCuentaUsuario.IdRol, entidad.Id.ToString()).entidades;

        presentadorTupla.Vista.Id = entidad.Id.ToString();
        presentadorTupla.Vista.NombreRolUsuario = entidad.Nombre;
        presentadorTupla.Vista.CantidadPermisos = entidad.Nombre?.Equals("Administrador") ?? false
            ? "TODOS"
            : permisosRolUsuario.Count.ToString();
        presentadorTupla.Vista.CantidadUsuarios = cuentasUsuario.Count.ToString();

        return presentadorTupla;
    }
}