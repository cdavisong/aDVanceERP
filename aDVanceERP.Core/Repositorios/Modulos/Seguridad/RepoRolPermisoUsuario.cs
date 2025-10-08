using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad;

public class RepoRolPermisoUsuario : RepoEntidadBaseDatos<RolPermisoUsuario, FiltroBusquedaPermisoRolUsuario> {
    public RepoRolPermisoUsuario() : base("adv__rol_permiso", "id_rol_permiso") { }

    protected override string GenerarComandoAdicionar(RolPermisoUsuario objeto) {
        return $"""
            INSERT INTO adv__rol_permiso (
                id_rol_usuario, 
                id_permiso
            ) VALUES (
                {objeto.IdRolUsuario}, 
                {objeto.IdPermiso});
            """;
    }

    protected override string GenerarComandoEditar(RolPermisoUsuario objeto) {
        return $"""
            UPDATE adv__rol_permiso 
            SET 
                id_rol_usuario = {objeto.IdRolUsuario}, 
                id_permiso = {objeto.IdPermiso} 
            WHERE id_rol_permiso = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__rol_permiso 
            WHERE id_rol_permiso = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaPermisoRolUsuario filtroBusqueda, string criterio) {
        string comando;

        switch (filtroBusqueda) {
            case FiltroBusquedaPermisoRolUsuario.Id:
                comando = $"""
                    SELECT rp.*, p.id_modulo, p.nombre
                    FROM adv__rol_permiso rp
                    LEFT JOIN adv__permiso p ON rp.id_permiso = p.id_permiso
                    WHERE rp.id_rol_permiso = {criterio};
                    """;
                break;
            case FiltroBusquedaPermisoRolUsuario.IdRolUsuario:
                comando = $"""
                    SELECT rp.*, p.id_modulo, p.nombre
                    FROM adv__rol_permiso rp
                    LEFT JOIN adv__permiso p ON rp.id_permiso = p.id_permiso
                    WHERE rp.id_rol_usuario = {criterio};
                    """;
                break;
            default:
                comando = """
                    SELECT rp.*, p.id_modulo, p.nombre
                    FROM adv__rol_permiso rp
                    LEFT JOIN adv__permiso p ON rp.id_permiso = p.id_permiso;
                    """;
                break;
        }

        return comando;
    }

    protected override RolPermisoUsuario MapearEntidad(MySqlDataReader lector) {
        return new RolPermisoUsuario(
            id: Convert.ToInt64(lector["id_rol_permiso"]),
            idRolUsuario: Convert.ToInt64(lector["id_rol_usuario"]),
            idPermiso: Convert.ToInt64(lector["id_permiso"])) {
            IdModulo = Convert.ToInt64(lector["id_modulo"]),
            NombrePermiso = Convert.ToString(lector["nombre"]) ?? string.Empty
        };
    }

    #region STATIC

    public static RepoRolPermisoUsuario Instancia { get; } = new RepoRolPermisoUsuario();

    #endregion

    #region UTILES

    public void EliminarPorRolUsuario(long idRolUsuario) {
        var consulta = """
            DELETE FROM adv__rol_permiso 
            WHERE id_rol_usuario = @idRolUsuario
            """;
        var parametros = new Dictionary<string, object> {
            { "@idRolUsuario", idRolUsuario }
        };

        ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
    }

    #endregion
}