using System.Globalization;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;

public class RepoVenta : RepoEntidadBaseDatos<Venta, FiltroBusquedaVenta>, IRepoVenta {
    public RepoVenta() : base("adv__venta", "id_venta") { }

    protected override string GenerarComandoAdicionar(Venta objeto) {
        return $"""
                INSERT INTO adv__venta (
                    fecha,
                    id_almacen,
                    id_cliente,
                    id_tipo_entrega,
                    direccion_entrega,
                    estado_entrega,
                    total
                )
                VALUES (
                    '{objeto.Fecha:yyyy-MM-dd HH:mm:ss}',
                    {objeto.IdAlmacen},
                    {objeto.IdCliente},
                    {objeto.IdTipoEntrega},
                    '{objeto.DireccionEntrega}',
                    '{objeto.EstadoEntrega}',
                    {objeto.Total.ToString(CultureInfo.InvariantCulture)});
                """;
    }

    protected override string GenerarComandoEditar(Venta objeto) {
        return $"""
                UPDATE adv__venta
                SET
                    fecha='{objeto.Fecha:yyyy-MM-dd HH:mm:ss}',
                    id_almacen = {objeto.IdAlmacen},
                    id_cliente = {objeto.IdCliente},
                    id_tipo_entrega = {objeto.IdTipoEntrega},
                    direccion_entrega = '{objeto.DireccionEntrega}',
                    estado_entrega = '{objeto.EstadoEntrega}',
                    total = {objeto.Total.ToString(CultureInfo.InvariantCulture)}
                WHERE id_venta = {objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
                START TRANSACTION;

                -- 1. Restaurar el cantidad sumando las cantidades vendidas
                UPDATE adv__inventario pa
                JOIN adv__detalle_venta_producto dvp ON pa.id_producto = dvp.id_producto
                JOIN adv__venta v ON dvp.id_venta = v.id_venta
                SET pa.cantidad = pa.cantidad + dvp.cantidad
                WHERE dvp.id_venta = {id} AND pa.id_almacen = v.id_almacen;

                -- 2. Eliminar los movimientos de inventario asociados a la venta
                DELETE m FROM adv__movimiento m
                JOIN adv__detalle_venta_producto dvp ON m.id_producto = dvp.id_producto
                JOIN adv__tipo_movimiento tm ON m.id_tipo_movimiento = tm.id_tipo_movimiento
                WHERE tm.nombre = 'Venta' AND tm.efecto = 'Descarga' AND dvp.id_venta = {id};

                -- 3. Eliminar registros relacionados en tablas dependientes
                DELETE FROM adv__seguimiento_entrega WHERE id_venta = {id};
                DELETE FROM adv__detalle_pago_transferencia WHERE id_venta = {id};
                DELETE FROM adv__pago WHERE id_venta = {id};

                -- 4. Eliminar los detalles de productos vendidos
                DELETE FROM adv__detalle_venta_producto WHERE id_venta = {id};

                -- 5. Finalmente eliminar el registro principal de la venta
                DELETE FROM adv__venta WHERE id_venta = {id};

                COMMIT;

                SELECT 1 AS Resultado;
                """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaVenta criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaVenta.Id:
                comando = $"SELECT * FROM adv__venta WHERE id_venta={dato};";
                break;
            case FiltroBusquedaVenta.NombreAlmacen:
                comando =
                    $"SELECT v.* FROM adv__venta v JOIN adv__almacen a ON v.id_almacen = a.id_almacen WHERE LOWER(a.nombre) LIKE LOWER('%{dato}%');";
                break;
            case FiltroBusquedaVenta.RazonSocialCliente:
                comando =
                    $"SELECT v.* FROM adv__venta v JOIN adv__cliente c ON v.id_cliente = c.id_cliente WHERE LOWER(c.razon_social) LIKE LOWER('%{dato}%');";
                break;
            case FiltroBusquedaVenta.Fecha:
                comando = $"SELECT * FROM adv__venta WHERE DATE(fecha) = '{dato}';";
                break;
            default:
                comando = "SELECT * FROM adv__venta;";
                break;
        }

        return comando;
    }

    protected override Venta MapearEntidad(MySqlDataReader lectorDatos) {
        return new Venta(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_venta")),
            lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_almacen")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_cliente")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_tipo_entrega")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("direccion_entrega")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("estado_entrega")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("total"))
        );
    }
}