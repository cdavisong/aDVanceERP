using System.Globalization;

using aDVanceERP.Core.Modelos.Modulos.Compraventa;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compraventa;

public class RepoDetalleCompraProducto : RepoEntidadBaseDatos<DetalleCompra, CriterioDetalleCompraProducto> {
    public RepoDetalleCompraProducto() : base("adv__detalle_compra_producto", "id_detalle_compra_producto") { }

    protected override string GenerarComandoAdicionar(DetalleCompra objeto) {
        return $"""
                INSERT INTO adv__detalle_compra_producto (
                    id_compra,
                    id_producto,
                    cantidad,
                    precio_compra
                )
                VALUES (
                    {objeto.IdCompra},
                    {objeto.IdProducto},
                    {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    {objeto.PrecioCompra.ToString(CultureInfo.InvariantCulture)}
                );          
                """;
    }

    protected override string GenerarComandoEditar(DetalleCompra objeto) {
        return $"""
                UPDATE adv__detalle_compra_producto
                SET
                    id_compra = {objeto.IdCompra},
                    id_producto = {objeto.IdProducto},
                    cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                    precio_compra = {objeto.PrecioCompra.ToString(CultureInfo.InvariantCulture)}
                WHERE id_detalle_compra_producto = {objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
                DELETE
                FROM adv__detalle_compra_producto
                WHERE id_detalle_compra_producto = {id};
                """;
    }

    protected override string GenerarComandoObtener(CriterioDetalleCompraProducto criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case CriterioDetalleCompraProducto.Todos:
                comando = """
                          SELECT *
                          FROM adv__detalle_compra_producto;
                          """;
                break;
            case CriterioDetalleCompraProducto.Id:
                comando = $"""
                           SELECT *
                           FROM adv__detalle_compra_producto
                           WHERE id_detalle_compra_producto = {dato};
                           """;
                break;
            case CriterioDetalleCompraProducto.IdCompra:
                comando = $"""
                           SELECT *
                                FROM adv__detalle_compra_producto
                                WHERE id_compra = {dato};
                           """;
                break;
            case CriterioDetalleCompraProducto.IdProducto:
                comando = $"""
                           SELECT *
                           FROM adv__detalle_compra_producto
                           WHERE id_producto = {dato};
                           """;
                break;
            default:
                comando = """
                          SELECT *
                          FROM adv__detalle_compra_producto;
                          """;
                break;
        }

        return comando;
    }

    protected override DetalleCompra MapearEntidad(MySqlDataReader lector) {
        return new DetalleCompra(
            id: Convert.ToInt64(lector["id_detalle_compra_producto"]),
            idCompra: Convert.ToInt64(lector["id_compra"]),
            idProducto: Convert.ToInt64(lector["id_producto"]),
            cantidad: Convert.ToDecimal(lector["cantidad"], CultureInfo.InvariantCulture),
            precioCompra: Convert.ToDecimal(lector["precio_compra"], CultureInfo.InvariantCulture)
        );
    }

    #region STATIC

    public static RepoDetalleCompraProducto Instancia { get; } = new RepoDetalleCompraProducto();

    #endregion
}