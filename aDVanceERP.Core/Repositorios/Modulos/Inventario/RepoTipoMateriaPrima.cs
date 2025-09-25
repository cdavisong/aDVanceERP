using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoTipoMateriaPrima : RepoEntidadBaseDatos<TipoMateriaPrima, FiltroBusquedaTipoMateriaPrima> {
    public RepoTipoMateriaPrima() : base("adv__tipo_materia_prima", "id_tipo_materia_prima") { }

    protected override string GenerarComandoAdicionar(TipoMateriaPrima objeto) {
        return $"""
            INSERT INTO adv__tipo_materia_prima (
                nombre,
                descripcion
            )
            VALUES (
                '{objeto.Nombre}',
                '{objeto.Descripcion}'
            );
            """;
    }

    protected override string GenerarComandoEditar(TipoMateriaPrima objeto) {
        return $"""
            UPDATE adv__tipo_materia_prima
            SET
                nombre = '{objeto.Nombre}',
                descripcion = '{objeto.Descripcion}'
            WHERE id_tipo_materia_prima = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            UPDATE adv__producto
            SET id_tipo_materia_prima = 0
            WHERE id_tipo_materia_prima = {id};

            DELETE FROM adv__tipo_materia_prima 
            WHERE id_tipo_materia_prima = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaTipoMateriaPrima criterio, string dato) {
        return criterio switch {
            FiltroBusquedaTipoMateriaPrima.Todos => """
                SELECT * 
                FROM adv__tipo_materia_prima;
                """,
            FiltroBusquedaTipoMateriaPrima.Id => $"""
                SELECT * 
                FROM adv__tipo_materia_prima 
                WHERE id_tipo_materia_prima = {dato};
                """,
            FiltroBusquedaTipoMateriaPrima.Nombre => $"""
                SELECT * 
                FROM adv__tipo_materia_prima 
                WHERE LOWER(nombre) LIKE LOWER('%{dato}%');
                """,
            _ => throw new ArgumentOutOfRangeException(nameof(criterio), criterio, null)
        };
    }

    protected override TipoMateriaPrima MapearEntidad(MySqlDataReader lectorDatos) {
        return new TipoMateriaPrima(
            lectorDatos.GetInt64("id_tipo_materia_prima"),
            lectorDatos.GetString("nombre"),
            lectorDatos.GetString("descripcion")
        );
    }

    #region STATIC

    public static RepoTipoMateriaPrima Instancia = new RepoTipoMateriaPrima();

    #endregion
}
