using aDVanceERP.Core.Modelos.Modulos.Taller;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Taller;

public class RepoOrdenGastoDinamico : RepoEntidadBaseDatos<OrdenGastoDinamico, FiltroBusquedaOrdenGastoDinamico> {
    public RepoOrdenGastoDinamico() : base("adv__orden_gasto_dinamico", "id_orden_gasto_dinamico") { }

    protected override string GenerarComandoAdicionar(OrdenGastoDinamico entidad) {
        return $"""
            INSERT INTO adv__orden_gasto_dinamico (
                id_orden_gasto_indirecto,
                formula
            )
            VALUES (
                {entidad.IdOrdenGastoIndirecto},
                '{entidad.Formula}'
            );
            """;
    }

    protected override string GenerarComandoEditar(OrdenGastoDinamico entidad) {
        return $"""
            UPDATE adv__orden_gasto_dinamico
            SET
                id_orden_gasto_indirecto = {entidad.IdOrdenGastoIndirecto},
                formula = '{entidad.Formula}'
            WHERE id_orden_gasto_dinamico = {entidad.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__orden_gasto_dinamico 
            WHERE id_orden_gasto_dinamico = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaOrdenGastoDinamico filtroBusqueda, string criterio) {
        string? comando;
        string[] criterioSplit = criterio.Split(';');

        switch (filtroBusqueda) {
            
            case FiltroBusquedaOrdenGastoDinamico.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_gasto_dinamico 
                    WHERE id_orden_gasto_dinamico = {criterioSplit[0]};
                    """;
                break;
            case FiltroBusquedaOrdenGastoDinamico.IdOrdenGastoIndirecto:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_gasto_dinamico 
                    WHERE id_orden_gasto_indirecto = {criterioSplit[0]};
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__orden_gasto_dinamico;
                    """;
                break;
        }

        return comando;
    }

    protected override OrdenGastoDinamico MapearEntidad(MySqlDataReader lectorDatos) {
        return new OrdenGastoDinamico(
            id: Convert.ToInt64(lectorDatos["id_orden_gasto_dinamico"]),
            idOrdenGastoIndirecto: Convert.ToInt64(lectorDatos["id_orden_gasto_indirecto"]),
            formula: Convert.ToString(lectorDatos["formula"]));
    }

    #region STATIC

    public static RepoOrdenGastoDinamico Instancia { get; } = new RepoOrdenGastoDinamico();

    #endregion
}