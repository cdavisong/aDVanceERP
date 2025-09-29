using aDVanceERP.Core.Modelos.Modulos.Taller;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Taller;

public class RepoOrdenMateriaPrima : RepoEntidadBaseDatos<OrdenMateriaPrima, FiltroBusquedaOrdenMateriaPrima> {
    public RepoOrdenMateriaPrima() : base("adv__orden_material", "id_orden_material") { }

    protected override string GenerarComandoAdicionar(OrdenMateriaPrima objeto) {
        return $"""
            INSERT INTO adv__orden_material (
                id_orden_produccion,
                id_almacen,
                id_producto,
                cantidad,
                costo_unitario,
                total,
                fecha_registro
            )
            VALUES (
                {objeto.IdOrdenProduccion},
                {objeto.IdAlmacen},
                {objeto.IdProducto},
                {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                {objeto.CostoUnitario.ToString(CultureInfo.InvariantCulture)},
                {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
            );
            """;
    }

    protected override string GenerarComandoEditar(OrdenMateriaPrima objeto) {
        return $"""
            UPDATE adv__orden_material
            SET
                id_orden_produccion = {objeto.IdOrdenProduccion},
                id_almacen = {objeto.IdAlmacen},
                id_producto = {objeto.IdProducto},
                cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                costo_unitario = {objeto.CostoUnitario.ToString(CultureInfo.InvariantCulture)},
                total = {objeto.Total.ToString(CultureInfo.InvariantCulture)},
                fecha_registro = '{objeto.FechaRegistro.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'
            WHERE id_orden_material = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__orden_material 
            WHERE id_orden_material = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaOrdenMateriaPrima filtroBusqueda, string criterio) {
        string? comando;
        string[] datoSplit = criterio.Split(';');

        switch (filtroBusqueda) {
            case FiltroBusquedaOrdenMateriaPrima.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_material 
                    WHERE id_orden_material = {criterio};
                    """;
                break;
            case FiltroBusquedaOrdenMateriaPrima.OrdenProduccion:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_material 
                    WHERE id_orden_produccion = {criterio};
                    """;
                break;
            case FiltroBusquedaOrdenMateriaPrima.Producto:
                comando = datoSplit.Length > 1
                    ? $"""
                        SELECT * 
                        FROM adv__orden_material 
                        WHERE id_orden_produccion = {datoSplit[0]} 
                            AND id_producto = {datoSplit[1]};
                        """
                    : $"""
                        SELECT * 
                        FROM adv__orden_material 
                        WHERE id_producto = {criterio};
                        """;
                break;
            case FiltroBusquedaOrdenMateriaPrima.FechaRegistro:
                comando = $"""
                    SELECT * 
                    FROM adv__orden_material 
                    WHERE DATE(fecha_registro) = '{criterio}';
                    """;
                break;
            default:
                comando = "SELECT * FROM adv__orden_material;";
                break;
        }

        return comando;
    }

    protected override OrdenMateriaPrima MapearEntidad(MySqlDataReader lectorDatos) {
        return new OrdenMateriaPrima(
            id: Convert.ToInt64(lectorDatos["id_orden_material"]),
            idOrdenProduccion: Convert.ToInt64(lectorDatos["id_orden_produccion"]),
            idAlmacen: Convert.ToInt64(lectorDatos["id_almacen"]),
            idProducto: Convert.ToInt64(lectorDatos["id_producto"]),
            cantidad: Convert.ToDecimal(lectorDatos["cantidad"], CultureInfo.InvariantCulture),
            costoUnitario: Convert.ToDecimal(lectorDatos["costo_unitario"], CultureInfo.InvariantCulture),
            costoTotal: Convert.ToDecimal(lectorDatos["total"], CultureInfo.InvariantCulture)) {
            FechaRegistro = Convert.ToDateTime(lectorDatos["fecha_registro"])
        };
    }

    #region STATIC

    public static RepoOrdenMateriaPrima Instancia { get; } = new RepoOrdenMateriaPrima();

    #endregion
}