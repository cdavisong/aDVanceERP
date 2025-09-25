using System.Globalization;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;

public class RepoCompra : RepoEntidadBaseDatos<Compra, FiltroBusquedaCompra>, IRepoCompra {
    public RepoCompra() : base("adv__compra", "id_compra") { }

    protected override string GenerarComandoAdicionar(Compra objeto) {
        return $"""
                INSERT INTO adv__compra (fecha, id_almacen, id_proveedor, total)
                VALUES (
                    '{objeto.Fecha:yyyy-MM-dd HH:mm:ss}',
                    '{objeto.IdAlmacen}',
                    '{objeto.IdProveedor}',
                    '{objeto.Total.ToString(CultureInfo.InvariantCulture)}'
                );
                """;
    }

    protected override string GenerarComandoEditar(Compra objeto) {
        return $"""
                UPDATE adv__compra
                SET
                    fecha='{objeto.Fecha:yyyy-MM-dd HH:mm:ss}',
                    id_almacen='{objeto.IdAlmacen}',
                    id_proveedor='{objeto.IdProveedor}',
                    total='{objeto.Total.ToString(CultureInfo.InvariantCulture)}'
                WHERE id_compra={objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
                START TRANSACTION;

                -- 1. Restaurar el cantidad restando las cantidades compradas
                UPDATE adv__inventario pa
                JOIN adv__detalle_compra_producto dcp ON pa.id_producto = dcp.id_producto
                JOIN adv__compra c ON dcp.id_compra = c.id_compra
                SET pa.cantidad = pa.cantidad - dcp.cantidad
                WHERE dcp.id_compra = {id} AND pa.id_almacen = c.id_almacen;

                -- 2. Eliminar los movimientos de inventario asociados a la compra
                DELETE m FROM adv__movimiento m
                JOIN adv__detalle_compra_producto dcp ON m.id_producto = dcp.id_producto
                JOIN adv__tipo_movimiento tm ON m.id_tipo_movimiento = tm.id_tipo_movimiento
                WHERE tm.nombre = 'Compra' AND tm.efecto = 'Carga' AND dcp.id_compra = {id};

                -- 3. Eliminar los detalles de productos comprados
                DELETE FROM adv__detalle_compra_producto WHERE id_compra = {id};

                -- 4. Finalmente eliminar el registro principal de la compra
                DELETE FROM adv__compra WHERE id_compra = {id};

                COMMIT;

                SELECT 1 AS Resultado;
                """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaCompra criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaCompra.Id:
                comando = $"""
                           SELECT *
                           FROM adv__compra
                           WHERE id_compra={dato};
                           """;
                break;
            case FiltroBusquedaCompra.NombreAlmacen:
                comando = $"""
                           SELECT c.*
                           FROM adv__compra c JOIN adv__almacen a ON c.id_almacen = a.id_almacen
                           WHERE LOWER(a.nombre) LIKE LOWER('%{dato}%');
                           """;
                break;
            case FiltroBusquedaCompra.RazonSocialProveedor:
                comando = $"""
                           SELECT c.*
                           FROM adv__compra c JOIN adv__proveedor p ON c.id_proveedor = p.id_proveedor
                           WHERE LOWER(p.razon_social) LIKE LOWER('%{dato}%');
                           """;
                break;
            case FiltroBusquedaCompra.Fecha:
                comando = $"""
                           SELECT *
                           FROM adv__compra
                           WHERE DATE(fecha) = '{dato}';
                           """;
                break;
            default:
                comando = """
                          SELECT *
                          FROM adv__compra;
                          """;
                break;
        }

        return comando;
    }

    protected override Compra MapearEntidad(MySqlDataReader lectorDatos) {
        return new Compra(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_compra")),
            lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_almacen")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_proveedor")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("total"))
        );
    }
}