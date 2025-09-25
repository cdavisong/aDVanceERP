using System.Globalization;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;

public class RepoDetalleVentaProducto : RepoEntidadBaseDatos<DetalleVentaProducto, CriterioDetalleVentaProducto>, IRepoDetalleVentaProducto {
    public RepoDetalleVentaProducto() : base("adv__detalle_venta_producto", "id_detalle_venta_producto") { }

    protected override string GenerarComandoAdicionar(DetalleVentaProducto objeto) {
        return $"""
                INSERT INTO adv__detalle_venta_producto (
                    id_venta,
                    id_producto,
                    precio_compra_vigente,
                    precio_venta_final,
                    cantidad
                )
                VALUES (
                    {objeto.IdVenta},
                    {objeto.IdProducto},
                    {objeto.PrecioCompraVigente.ToString(CultureInfo.InvariantCulture)},
                    {objeto.PrecioVentaFinal.ToString(CultureInfo.InvariantCulture)},
                    '{objeto.Cantidad.ToString(CultureInfo.InvariantCulture)}'
                );
                """;
    }

    protected override string GenerarComandoEditar(DetalleVentaProducto objeto) {
        return $"""
                UPDATE adv__detalle_venta_producto
                SET
                    id_venta={objeto.IdVenta},
                    id_producto={objeto.IdProducto},
                    precio_compra_vigente={objeto.PrecioCompraVigente.ToString(CultureInfo.InvariantCulture)},
                    precio_venta_final={objeto.PrecioVentaFinal.ToString(CultureInfo.InvariantCulture)},
                    cantidad='{objeto.Cantidad.ToString(CultureInfo.InvariantCulture)}'
                WHERE id_detalle_venta_producto={objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
                DELETE
                FROM adv__detalle_venta_producto
                WHERE id_detalle_venta_producto={id};
                """;
    }

    protected override string GenerarComandoObtener(CriterioDetalleVentaProducto criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case CriterioDetalleVentaProducto.Id:
                comando = $"""
                           SELECT *
                           FROM adv__detalle_venta_producto
                           WHERE id_detalle_venta_producto={dato};
                           """;
                break;
            case CriterioDetalleVentaProducto.IdVenta:
                comando = $"""
                           SELECT *
                           FROM adv__detalle_venta_producto
                           WHERE id_venta={dato};
                           """;
                break;
            case CriterioDetalleVentaProducto.IdProducto:
                comando = $"""
                           SELECT *
                           FROM adv__detalle_venta_producto
                           WHERE id_producto={dato};
                           """;
                break;
            default:
                comando = """
                          SELECT *
                          FROM adv__detalle_venta_producto;
                          """;
                break;
        }

        return comando;
    }

    protected override DetalleVentaProducto MapearEntidad(MySqlDataReader lectorDatos) {
        return new DetalleVentaProducto(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_detalle_venta_producto")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_venta")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_producto")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("precio_compra_vigente")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("precio_venta_final")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("cantidad"))
        );
    }
}