using aDVanceERP.Core.Modelos.Modulos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos;

public class RepoModulo : RepoEntidadBaseDatos<Modulo, FiltroBusquedaModulo> {
    public RepoModulo() : base("adv__modulo", "id_modulo") {
    }

    protected override string GenerarComandoAdicionar(Modulo entidad) {
        return $"""
            INSERT INTO adv__modulo (
                nombre
            ) VALUES (
                '{entidad.Nombre}'
            );
            """;
    }

    protected override string GenerarComandoEditar(Modulo entidad) {
        return $"""
            UPDATE adv__modulo 
            SET 
                nombre = '{entidad.Nombre}' 
            WHERE id_modulo = {entidad.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__modulo 
            WHERE id_modulo = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaModulo filtroBusqueda, string criterio) {
        string? comando;

        switch (filtroBusqueda) {
            case FiltroBusquedaModulo.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__modulo 
                    WHERE id_modulo = {criterio};
                    """;
                break;
            case FiltroBusquedaModulo.Nombre:
                comando = $"""
                    SELECT * 
                    FROM adv__modulo 
                    WHERE nombre LIKE '%{criterio}%';
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__modulo;
                    """;
                break;
        }

        return comando;
    }

    protected override Modulo MapearEntidad(MySqlDataReader lector) {
        return new Modulo(
            id: Convert.ToInt64(lector["id_modulo"]),
            nombre: Convert.ToString(lector["nombre"]) ?? string.Empty);
    }

    #region STATIC

    public static RepoModulo Instancia { get; } = new RepoModulo();

    #endregion
}
