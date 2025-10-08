using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad;

public class RepoPermiso : RepoEntidadBaseDatos<Permiso, FiltroBusquedaPermiso> {
    public RepoPermiso() : base("adv__permiso", "id_permiso") {
    }

    protected override string GenerarComandoAdicionar(Permiso objeto) {
        return $"""
            INSERT INTO adv__permiso (
                id_modulo, 
                nombre
            ) VALUES (
                {objeto.IdModulo}, 
                '{objeto.Nombre}'
            );
            """;
    }

    protected override string GenerarComandoEditar(Permiso objeto) {
        return $"""
            UPDATE adv__permiso 
            SET 
                id_modulo = {objeto.IdModulo}, 
                nombre = '{objeto.Nombre}' 
            WHERE id_permiso = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__permiso 
            WHERE id_permiso = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaPermiso filtroBusqueda, string criterio) {
        string? comando;

        switch (filtroBusqueda) {
            case FiltroBusquedaPermiso.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__permiso 
                    WHERE id_permiso = {criterio};
                    """;
                break;
            case FiltroBusquedaPermiso.IdModulo:
                comando = $"""
                    SELECT * 
                    FROM adv__permiso 
                    WHERE id_modulo = {criterio};
                    """;
                break;
            case FiltroBusquedaPermiso.Nombre:
                comando = $"""
                    SELECT * 
                    FROM adv__permiso 
                    WHERE nombre LIKE '%{criterio}%';
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__permiso;
                    """;
                break;
        }

        return comando;
    }

    protected override Permiso MapearEntidad(MySqlDataReader lector) {
        return new Permiso(
            id: Convert.ToInt64(lector["id_permiso"]),
            idModulo: Convert.ToInt64(lector["id_modulo"]),
            nombre: lector["nombre"].ToString());
    }

    #region STATIC

    public static RepoPermiso Instancia { get; } = new RepoPermiso();

    #endregion

    #region UTILES

    public List<Permiso> ObtenerPorNombreModulo(string nombreModulo) {
        var comando = $"""
            SELECT p.* 
            FROM adv__permiso p
            JOIN adv__modulo m ON p.id_modulo = m.id_modulo
            WHERE m.nombre = @nombreModulo;
            """;
        var parametros = new Dictionary<string, object> {
            { "@nombreModulo", nombreModulo }
        };

        return ContextoBaseDatos.EjecutarConsulta(comando, parametros, MapearEntidad).ToList();
    }

    public List<Permiso> ObtenerPorIdRolUsuario(long idRolUsuario) {
        var comando = $"""
            SELECT p.* 
            FROM adv__permiso p
            JOIN adv__rol_permiso rp ON p.id_permiso = rp.id_permiso
            WHERE rp.id_rol_usuario = @idRolUsuario;
            """;
        var parametros = new Dictionary<string, object> {
            { "@idRolUsuario", idRolUsuario }
        };

        return ContextoBaseDatos.EjecutarConsulta(comando, parametros, MapearEntidad).ToList();
    }

    #endregion
}
