using aDVanceERP.Core.Modelos.Modulos.Taller;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Taller;

public class RepoOrdenActividadProduccion : RepoEntidadBaseDatos<OrdenActividadProduccion, FiltroBusquedaOrdenActividadProduccion> {
    public RepoOrdenActividadProduccion() : base("adv__orden_actividad", "id_orden_actividad") { }

    protected override string GenerarComandoAdicionar(OrdenActividadProduccion objeto) {
        return $"""
            INSERT INTO adv__orden_actividad (
                id_orden_produccion,
                nombre,
                cantidad,
                costo,
                total,
                fecha_registro
            )
            VALUES (
                {objeto.IdOrdenProduccion},
                '{objeto.Nombre}',
                {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                {objeto.Costo.ToString(CultureInfo.InvariantCulture)},
                {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
            );
            """;
    }

    protected override string GenerarComandoEditar(OrdenActividadProduccion objeto) {
        return $"""
            UPDATE adv__orden_actividad
            SET
                id_orden_produccion = {objeto.IdOrdenProduccion},
                nombre = '{objeto.Nombre}',
                cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                costo = {objeto.Costo.ToString(CultureInfo.InvariantCulture)},
                total = {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                fecha_registro = '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
            WHERE id_orden_actividad = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__orden_actividad 
            WHERE id_orden_actividad = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaOrdenActividadProduccion filtroBusqueda, string criterio) {
        string? comando;
        string[] criterioSplit = criterio.Split(';');

        switch (filtroBusqueda) {
            case FiltroBusquedaOrdenActividadProduccion.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_actividad 
                    WHERE id_orden_actividad = {criterio};
                    """;
                break;
            case FiltroBusquedaOrdenActividadProduccion.OrdenProduccion:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_actividad 
                    WHERE id_orden_produccion = {criterio};
                    """;
                break;
            case FiltroBusquedaOrdenActividadProduccion.Nombre:
                if (criterioSplit.Length > 1) {
                    comando = $"""
                        SELECT * 
                        FROM adv__orden_actividad 
                        WHERE id_orden_produccion = {criterioSplit[0]} 
                          AND nombre = '{criterioSplit[1]}';
                        """;
                } else {
                    comando = $"""
                        SELECT * 
                        FROM adv__orden_actividad 
                        WHERE nombre LIKE '%{criterio}%';
                        """;
                }
                break;
            case FiltroBusquedaOrdenActividadProduccion.FechaRegistro:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_actividad 
                    WHERE DATE(fecha_registro) = '{criterio}';
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__orden_actividad;
                    """;
                break;
        }

        return comando;
    }

    protected override OrdenActividadProduccion MapearEntidad(MySqlDataReader lectorDatos) {
        return new OrdenActividadProduccion(
            id: Convert.ToInt64(lectorDatos["id_orden_actividad"]),
            idOrdenProduccion: Convert.ToInt64(lectorDatos["id_orden_produccion"]),
            nombre: Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
            cantidad: Convert.ToDecimal(lectorDatos["cantidad"], CultureInfo.InvariantCulture),
            costo: Convert.ToDecimal(lectorDatos["costo"], CultureInfo.InvariantCulture),
            costoTotal: Convert.ToDecimal(lectorDatos["total"], CultureInfo.InvariantCulture)) {
            FechaRegistro = Convert.ToDateTime(lectorDatos["fecha_registro"])
        };
    }

    #region STATIC

    public static RepoOrdenActividadProduccion Instancia { get; } = new RepoOrdenActividadProduccion();

    #endregion
}