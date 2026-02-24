using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compra {
    public class RepoDetalleRecepcionCompra : RepoEntidadBaseDatos<DetalleRecepcionCompra, FiltroBusquedaDetalleRecepcion> {
        public RepoDetalleRecepcionCompra() : base("adv__detalle_recepcion_compra", "id_detalle_recepcion_compra") {
        }

        protected override string GenerarComandoAdicionar(DetalleRecepcionCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__detalle_recepcion_compra (
                    id_recepcion_compra,
                    id_detalle_compra_producto,
                    cantidad_recibida
                ) VALUES (
                    @id_recepcion_compra,
                    @id_detalle_compra_producto,
                    @cantidad_recibida
                )
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_recepcion_compra", entidad.IdRecepcionCompra },
                { "@id_detalle_compra_producto", entidad.IdDetalleCompraProducto },
                { "@cantidad_recibida", entidad.CantidadRecibida }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(DetalleRecepcionCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__detalle_recepcion_compra 
                SET 
                    id_recepcion_compra = @id_recepcion_compra,
                    id_detalle_compra_producto = @id_detalle_compra_producto,
                    cantidad_recibida = @cantidad_recibida
                WHERE id_detalle_recepcion_compra = @id_detalle_recepcion
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_recepcion_compra", entidad.IdRecepcionCompra },
                { "@id_detalle_compra_producto", entidad.IdDetalleCompraProducto },
                { "@cantidad_recibida", entidad.CantidadRecibida },
                { "@id_detalle_recepcion", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                DELETE FROM adv__detalle_recepcion_compra
                WHERE id_detalle_recepcion_compra = @id_detalle
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_detalle", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaDetalleRecepcion filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = "SELECT * FROM adv__detalle_recepcion_compra";

            var consulta = filtroBusqueda switch {
                FiltroBusquedaDetalleRecepcion.Id => $"""
                    {consultaComun}
                    WHERE id_detalle_recepcion_compra = @id_detalle
                    """,
                FiltroBusquedaDetalleRecepcion.IdRecepcionCompra => $"""
                    {consultaComun}
                    WHERE id_recepcion_compra = @id_recepcion_compra
                    """,
                FiltroBusquedaDetalleRecepcion.IdDetalleCompraProducto => $"""
                    {consultaComun}
                    WHERE id_detalle_compra_producto = @id_detalle_compra
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaDetalleRecepcion.Id => new Dictionary<string, object>
                {
                    { "@id_detalle", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleRecepcion.IdRecepcionCompra => new Dictionary<string, object>
                {
                    { "@id_recepcion_compra", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleRecepcion.IdDetalleCompraProducto => new Dictionary<string, object>
                {
                    { "@id_detalle_compra", long.Parse(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (DetalleRecepcionCompra, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var detalle = new DetalleRecepcionCompra {
                Id = Convert.ToInt64(lector["id_detalle_recepcion_compra"]),
                IdRecepcionCompra = Convert.ToInt64(lector["id_recepcion_compra"]),
                IdDetalleCompraProducto = Convert.ToInt64(lector["id_detalle_compra_producto"]),
                CantidadRecibida = Convert.ToDecimal(lector["cantidad_recibida"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            return (detalle, entidadesExtra);
        }

        #region STATIC

        public static RepoDetalleRecepcionCompra Instancia { get; } = new RepoDetalleRecepcionCompra();

        #endregion

        #region UTILES

        public List<DetalleRecepcionCompra> ObtenerPorIdRecepcion(long idRecepcion) {
            var (_, resultados) = Buscar(FiltroBusquedaDetalleRecepcion.IdRecepcionCompra, idRecepcion.ToString());
            return resultados.Select(r => r.entidadBase).ToList();
        }

        #endregion
    }
}