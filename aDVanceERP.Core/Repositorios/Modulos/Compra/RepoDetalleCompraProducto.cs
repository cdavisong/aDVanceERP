using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compra {
    public class RepoDetalleCompraProducto : RepoEntidadBaseDatos<DetalleCompraProducto, FiltroBusquedaDetalleCompra> {
        public RepoDetalleCompraProducto() : base("adv__detalle_compra_producto", "id_detalle_compra_producto") {
        }

        protected override string GenerarComandoAdicionar(DetalleCompraProducto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__detalle_compra_producto (
                    id_compra,
                    id_producto,
                    cantidad_ordenada,
                    cantidad_recibida,
                    costo_unitario,
                    descuento,
                    impuesto_porcentaje,
                    id_presentacion
                ) VALUES (
                    @id_compra,
                    @id_producto,
                    @cantidad_ordenada,
                    @cantidad_recibida,
                    @costo_unitario,
                    @descuento,
                    @impuesto_porcentaje,
                    @id_presentacion
                )
                """;

            parametros = new Dictionary<string, object> {
                { "@id_compra", entidad.IdCompra },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad_ordenada", entidad.CantidadOrdenada },
                { "@cantidad_recibida", entidad.CantidadRecibida },
                { "@costo_unitario", entidad.CostoUnitario },
                { "@descuento", entidad.Descuento },
                { "@impuesto_porcentaje", entidad.ImpuestoPorcentaje },
                { "@id_presentacion", entidad.IdPresentacion }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(DetalleCompraProducto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__detalle_compra_producto 
                SET 
                    id_compra = @id_compra,
                    id_producto = @id_producto,
                    cantidad_ordenada = @cantidad_ordenada,
                    cantidad_recibida = @cantidad_recibida,
                    costo_unitario = @costo_unitario,
                    descuento = @descuento,
                    impuesto_porcentaje = @impuesto_porcentaje,
                    id_presentacion = @id_presentacion
                WHERE id_detalle_compra_producto = @id_detalle
                """;

            parametros = new Dictionary<string, object> {
                { "@id_compra", entidad.IdCompra },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad_ordenada", entidad.CantidadOrdenada },
                { "@cantidad_recibida", entidad.CantidadRecibida },
                { "@costo_unitario", entidad.CostoUnitario },
                { "@descuento", entidad.Descuento },
                { "@impuesto_porcentaje", entidad.ImpuestoPorcentaje },
                { "@id_presentacion", entidad.IdPresentacion },
                { "@id_detalle", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                DELETE FROM adv__detalle_compra_producto
                WHERE id_detalle_compra_producto = @id_detalle
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_detalle", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaDetalleCompra filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = """
                SELECT d.*, p.nombre as nombre_producto, p.codigo as codigo_producto
                FROM adv__detalle_compra_producto d
                LEFT JOIN adv__producto p ON d.id_producto = p.id_producto
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaDetalleCompra.Id => $"""
                    {consultaComun}
                    WHERE d.id_detalle_compra_producto = @id_detalle
                    """,
                FiltroBusquedaDetalleCompra.IdCompra => $"""
                    {consultaComun}
                    WHERE d.id_compra = @id_compra
                    """,
                FiltroBusquedaDetalleCompra.IdProducto => $"""
                    {consultaComun}
                    WHERE d.id_producto = @id_producto
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaDetalleCompra.Id => new Dictionary<string, object>
                {
                    { "@id_detalle", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleCompra.IdCompra => new Dictionary<string, object>
                {
                    { "@id_compra", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleCompra.IdProducto => new Dictionary<string, object>
                {
                    { "@id_producto", long.Parse(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (DetalleCompraProducto, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var detalle = new DetalleCompraProducto {
                Id = Convert.ToInt64(lector["id_detalle_compra_producto"]),
                IdCompra = Convert.ToInt64(lector["id_compra"]),
                IdProducto = Convert.ToInt64(lector["id_producto"]),
                CantidadOrdenada = Convert.ToDecimal(lector["cantidad_ordenada"]),
                CantidadRecibida = Convert.ToDecimal(lector["cantidad_recibida"]),
                CostoUnitario = Convert.ToDecimal(lector["costo_unitario"]),
                Descuento = Convert.ToDecimal(lector["descuento"]),
                ImpuestoPorcentaje = Convert.ToDecimal(lector["impuesto_porcentaje"]),
                IdPresentacion = Convert.ToInt64(lector["id_presentacion"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            return (detalle, entidadesExtra);
        }

        #region STATIC

        public static RepoDetalleCompraProducto Instancia { get; } = new RepoDetalleCompraProducto();

        #endregion

        #region UTILES

        public List<DetalleCompraProducto> ObtenerPorIdCompra(long idCompra) {
            var (_, resultados) = Buscar(FiltroBusquedaDetalleCompra.IdCompra, idCompra.ToString());
            return resultados.Select(r => r.entidadBase).ToList();
        }

        public bool ActualizarCantidadRecibida(long idDetalle, decimal cantidadRecibida) {
            var consulta = """
                UPDATE adv__detalle_compra_producto
                SET cantidad_recibida = @cantidad_recibida
                WHERE id_detalle_compra_producto = @id_detalle
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_detalle", idDetalle },
                { "@cantidad_recibida", cantidadRecibida }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        #endregion
    }
}