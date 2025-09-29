using aDVanceERP.Core.Modelos.Modulos.Taller;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Taller;

public class RepoOrdenProduccion : RepoEntidadBaseDatos<OrdenProduccion, FiltroBusquedaOrdenProduccion> {
    public RepoOrdenProduccion() : base("adv__orden_produccion", "id_orden_produccion") { }

    protected override string GenerarComandoAdicionar(OrdenProduccion objeto) {
        return $"""
            INSERT INTO adv__orden_produccion (
                numero_orden,
                fecha_apertura,
                id_almacen,
                nombre_producto,
                cantidad,
                estado,
                observaciones,
                costo_total,
                precio_unitario,
                margen_ganancia
            )
            VALUES (
                '{objeto.NumeroOrden}',
                '{objeto.FechaApertura.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}',
                {objeto.IdAlmacen},
                '{objeto.NombreProducto}',
                {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                '{objeto.Estado}',
                '{objeto.Observaciones?.Replace("'", "''") ?? string.Empty}',
                {objeto.CostoTotal.ToString(CultureInfo.InvariantCulture)},
                {objeto.PrecioUnitario.ToString(CultureInfo.InvariantCulture)},
                {objeto.MargenGanancia.ToString(CultureInfo.InvariantCulture)}
            );
            """;
    }

    protected override string GenerarComandoEditar(OrdenProduccion objeto) {
        return $"""
            UPDATE adv__orden_produccion
            SET
                numero_orden = '{objeto.NumeroOrden}',
                fecha_apertura = '{objeto.FechaApertura.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}',
                id_almacen = {objeto.IdAlmacen},
                nombre_producto = '{objeto.NombreProducto}',
                cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                estado = '{objeto.Estado}',
                observaciones = '{objeto.Observaciones?.Replace("'", "''") ?? string.Empty}',
                fecha_cierre = {(objeto.FechaCierre.HasValue ? $"'{objeto.FechaCierre.Value:yyyy-MM-dd HH:mm:ss}'" : "NULL")},
                costo_total = {objeto.CostoTotal.ToString(CultureInfo.InvariantCulture)},
                precio_unitario = {objeto.PrecioUnitario.ToString(CultureInfo.InvariantCulture)},
                margen_ganancia = {objeto.MargenGanancia.ToString(CultureInfo.InvariantCulture)}
            WHERE id_orden_produccion = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            -- Primero eliminamos los registros relacionados
            DELETE FROM adv__orden_actividad WHERE id_orden_produccion = {id};
            DELETE FROM adv__orden_material WHERE id_orden_produccion = {id};
            DELETE FROM adv__orden_gasto_indirecto WHERE id_orden_produccion = {id};
                
            -- Finalmente eliminamos la orden
            DELETE FROM adv__orden_produccion WHERE id_orden_produccion = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaOrdenProduccion filtroBusqueda, string criterio) {
        string? comando;

        switch (filtroBusqueda) {
            case FiltroBusquedaOrdenProduccion.NumeroOrden:
                comando = $"""
                        SELECT * 
                        FROM adv__orden_produccion 
                        WHERE numero_orden LIKE '%{criterio}%';
                        """;
                break;
            case FiltroBusquedaOrdenProduccion.Producto:
                comando = $"""
                        SELECT * 
                        FROM adv__orden_produccion 
                        WHERE nombre_producto LIKE '%{criterio}%';
                        """;
                break;
            case FiltroBusquedaOrdenProduccion.Estado:
                comando = $"""
                        SELECT * 
                        FROM adv__orden_produccion 
                        WHERE estado = '{criterio}';
                        """;
                break;
            case FiltroBusquedaOrdenProduccion.FechaApertura:
                comando = $"""
                        SELECT * 
                        FROM adv__orden_produccion 
                        WHERE DATE(fecha_apertura) = '{criterio}';
                        """;
                break;
            case FiltroBusquedaOrdenProduccion.FechaCierre:
                comando = $"""
                        SELECT * 
                        FROM adv__orden_produccion 
                        WHERE DATE(fecha_cierre) = '{criterio}';
                        """;
                break;
            default:
                comando = "SELECT * FROM adv__orden_produccion;";
                break;
        }

        return comando;
    }

    protected override OrdenProduccion MapearEntidad(MySqlDataReader lector) {
        return new OrdenProduccion(
            id: Convert.ToInt64(lector["id_orden_produccion"]),
            numeroOrden: Convert.ToString(lector["numero_orden"]) ?? string.Empty,
            fechaApertura: Convert.ToDateTime(lector["fecha_apertura"]),
            fechaCierre: lector["fecha_cierre"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(lector["fecha_cierre"]),
            idAlmacen: Convert.ToInt64(lector["id_almacen"]),
            nombreProducto: Convert.ToString(lector["nombre_producto"]) ?? string.Empty,
            cantidad: Convert.ToDecimal(lector["cantidad"], CultureInfo.InvariantCulture),
            estado: Enum.TryParse<EstadoOrdenProduccion>(lector["estado"]?.ToString(), out var estado) ? estado : EstadoOrdenProduccion.Abierta,
            observaciones: Convert.ToString(lector["observaciones"]) ?? string.Empty,
            costoTotal: Convert.ToDecimal(lector["costo_total"], CultureInfo.InvariantCulture),
            precioUnitario: Convert.ToDecimal(lector["precio_unitario"], CultureInfo.InvariantCulture),
            margenGanancia: Convert.ToDecimal(lector["margen_ganancia"], CultureInfo.InvariantCulture)
            );
    }

    #region STATIC

    public static RepoOrdenProduccion Instancia { get; } = new RepoOrdenProduccion();

    #endregion
}