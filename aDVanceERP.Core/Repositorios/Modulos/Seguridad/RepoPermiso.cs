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
                {objeto.IdModuloAplicacion}, 
                '{objeto.Nombre}'
            );
            """;
    }

    protected override string GenerarComandoEditar(Permiso objeto) {
        return $"""
            UPDATE adv__permiso 
            SET 
                id_modulo = {objeto.IdModuloAplicacion}, 
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
            idModuloAplicacion: Convert.ToInt64(lector["id_modulo"]),
            nombre: lector["nombre"].ToString());
    }

    #region STATIC

    public static RepoPermiso Instancia { get; } = new RepoPermiso();

    #endregion
}
