using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;
using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Venta {
    public class RepoDetallePedidoProducto : RepoEntidadBaseDatos<DetallePedidoProducto, FiltroBusquedaDetallePedido> {
        public RepoDetallePedidoProducto() : base("adv__detalle_pedido_producto", "id_detalle_pedido_producto") {
        }

        protected override string GenerarComandoAdicionar(DetallePedidoProducto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__detalle_pedido_producto (
                    id_pedido,
                    id_producto,
                    cantidad_solicitada,
                    precio_venta_referencia
                ) VALUES (
                    @id_pedido,
                    @id_producto,
                    @cantidad_solicitada,
                    @precio_venta_referencia
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_pedido", entidad.IdPedido },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad_solicitada", entidad.CantidadSolicitada },
                { "@precio_venta_referencia", entidad.PrecioVentaReferencia }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(DetallePedidoProducto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__detalle_pedido_producto 
                SET 
                    id_pedido = @id_pedido,
                    id_producto = @id_producto,
                    cantidad_solicitada = @cantidad_solicitada,
                    precio_venta_referencia = @precio_venta_referencia
                WHERE id_detalle_pedido_producto = @id_detalle
                """;

            parametros = new Dictionary<string, object> {
                { "@id_detalle", entidad.Id },
                { "@id_pedido", entidad.IdPedido },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad_solicitada", entidad.CantidadSolicitada },
                { "@precio_venta_referencia", entidad.PrecioVentaReferencia }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__detalle_pedido_producto 
                WHERE id_detalle_pedido_producto = @id_detalle
                """;

            parametros = new Dictionary<string, object> {
                { "@id_detalle", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaDetallePedido filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT d.*, p.nombre as nombre_producto, p.codigo as codigo_producto
                FROM adv__detalle_pedido_producto d
                LEFT JOIN adv__producto p ON d.id_producto = p.id_producto
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaDetallePedido.PorPedido => $"""
                    {consultaComun}
                    WHERE d.id_pedido = @id_pedido
                    """,
                FiltroBusquedaDetallePedido.PorProducto => $"""
                    {consultaComun}
                    WHERE d.id_producto = @id_producto
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaDetallePedido.PorPedido => new Dictionary<string, object> {
                    { "@id_pedido", long.Parse(criterio) }
                },
                FiltroBusquedaDetallePedido.PorProducto => new Dictionary<string, object> {
                    { "@id_producto", long.Parse(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (DetallePedidoProducto, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var detalle = new DetallePedidoProducto {
                Id = Convert.ToInt64(lector["id_detalle_pedido_producto"]),
                IdPedido = Convert.ToInt64(lector["id_pedido"]),
                IdProducto = Convert.ToInt64(lector["id_producto"]),
                CantidadSolicitada = Convert.ToDecimal(lector["cantidad_solicitada"], CultureInfo.InvariantCulture),
                PrecioVentaReferencia = Convert.ToDecimal(lector["precio_venta_referencia"], CultureInfo.InvariantCulture),
                Subtotal = Convert.ToDecimal(lector["subtotal"], CultureInfo.InvariantCulture)
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 6) {
                entidadesExtra.Add(new Producto {
                    Nombre = Convert.ToString(lector["nombre_producto"]) ?? "N/A",
                    Codigo = Convert.ToString(lector["codigo_producto"]) ?? "N/A"
                });
            }

            return (detalle, entidadesExtra);
        }

        #region STATIC
        public static RepoDetallePedidoProducto Instancia { get; } = new RepoDetallePedidoProducto();

        #endregion

        #region UTILES

        public bool EliminarDetallesPorPedido(long idPedido) {
            var consulta = $"""
                DELETE FROM adv__detalle_pedido_producto
                WHERE id_pedido = @id_pedido;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pedido", idPedido }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool ExisteProductoEnPedido(long idPedido, long idProducto) {
            var consulta = $"""
                SELECT COUNT(*) 
                FROM adv__detalle_pedido_producto
                WHERE id_pedido = @id_pedido 
                AND id_producto = @id_producto
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pedido", idPedido },
                { "@id_producto", idProducto }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
            return resultado > 0;
        }

        public void ActualizarCantidadProducto(long idPedido, long idProducto, decimal nuevaCantidad) {
            var consulta = $"""
                UPDATE adv__detalle_pedido_producto
                SET cantidad_solicitada = @nueva_cantidad
                WHERE id_pedido = @id_pedido 
                AND id_producto = @id_producto
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pedido", idPedido },
                { "@id_producto", idProducto },
                { "@nueva_cantidad", nuevaCantidad }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
        }

        #endregion
    }
}