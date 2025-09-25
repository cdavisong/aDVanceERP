using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Taller.Modelos;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.Taller.Repositorios;

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
        var datoSplit = criterio.Split(';');
        return filtroBusqueda switch {
            FiltroBusquedaOrdenGastoDinamico.Todos =>
                "SELECT * FROM adv__orden_gasto_dinamico;",
            FiltroBusquedaOrdenGastoDinamico.Id =>
                $"SELECT * FROM adv__orden_gasto_dinamico WHERE id_orden_gasto_dinamico = {datoSplit[0]};",
            FiltroBusquedaOrdenGastoDinamico.IdOrdenGastoIndirecto =>
                $"SELECT * FROM adv__orden_gasto_dinamico WHERE id_orden_gasto_indirecto = {datoSplit[0]};",
            _ => throw new ArgumentOutOfRangeException(nameof(filtroBusqueda), filtroBusqueda, null)
        };

    }

    protected override OrdenGastoDinamico MapearEntidad(MySqlDataReader lectorDatos) {
        return new OrdenGastoDinamico(
            lectorDatos.GetInt64("id_orden_gasto_dinamico"),
            lectorDatos.GetInt64("id_orden_gasto_indirecto"),
            lectorDatos.GetString("formula")
        );
    }
}