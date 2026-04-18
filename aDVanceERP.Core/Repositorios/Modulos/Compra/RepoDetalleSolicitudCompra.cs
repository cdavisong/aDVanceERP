using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compra {
    public class RepoDetalleSolicitudCompra : RepoEntidadBaseDatos<DetalleSolicitudCompra, FiltroBusquedaDetalleSolicitudCompra> {
        public RepoDetalleSolicitudCompra() : base("adv__detalle_solicitud_compra", "id_detalle_solicitud_compra") {
        }

        protected override string GenerarComandoAdicionar(DetalleSolicitudCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__detalle_solicitud_compra (
                    id_solicitud_compra,
                    id_producto,
                    cantidad_solicitada,
                    precio_adquisicion_referencia,
                    id_presentacion
                ) VALUES (
                    @id_solicitud_compra,
                    @id_producto,
                    @cantidad_solicitada,
                    @precio_adquisicion_referencia,
                    @id_presentacion
                )
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_solicitud_compra", entidad.IdSolicitudCompra },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad_solicitada", entidad.CantidadSolicitada },
                { "@precio_adquisicion_referencia", entidad.PrecioAdquisicionReferencia },
                { "@id_presentacion", entidad.IdPresentacion }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(DetalleSolicitudCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__detalle_solicitud_compra 
                SET 
                    id_solicitud_compra = @id_solicitud_compra,
                    id_producto = @id_producto,
                    cantidad_solicitada = @cantidad_solicitada,
                    precio_adquisicion_referencia = @precio_adquisicion_referencia,
                    id_presentacion = @id_presentacion
                WHERE id_detalle_solicitud_compra = @id_detalle_solicitud_compra
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_solicitud_compra", entidad.IdSolicitudCompra },
                { "@id_producto", entidad.IdProducto },
                { "@cantidad_solicitada", entidad.CantidadSolicitada },
                { "@precio_adquisicion_referencia", entidad.PrecioAdquisicionReferencia },
                { "@id_presentacion", entidad.IdPresentacion },
                { "@id_detalle_solicitud_compra", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                DELETE FROM adv__detalle_solicitud_compra
                WHERE id_detalle_solicitud_compra = @id_detalle_solicitud_compra
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_detalle_solicitud_compra", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaDetalleSolicitudCompra filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = """
                SELECT d.*, p.nombre as nombre_producto, p.codigo as codigo_producto
                FROM adv__detalle_solicitud_compra d
                LEFT JOIN adv__producto p ON d.id_producto = p.id_producto
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaDetalleSolicitudCompra.Id => $"""
                    {consultaComun}
                    WHERE d.id_detalle_solicitud_compra = @id_detalle
                    """,
                FiltroBusquedaDetalleSolicitudCompra.IdSolicitudCompra => $"""
                    {consultaComun}
                    WHERE d.id_solicitud_compra = @id_solicitud_compra
                    """,
                FiltroBusquedaDetalleSolicitudCompra.IdProducto => $"""
                    {consultaComun}
                    WHERE d.id_producto = @id_producto
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaDetalleSolicitudCompra.Id => new Dictionary<string, object>
                {
                    { "@id_detalle", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleSolicitudCompra.IdSolicitudCompra => new Dictionary<string, object>
                {
                    { "@id_solicitud_compra", long.Parse(criterio) }
                },
                FiltroBusquedaDetalleSolicitudCompra.IdProducto => new Dictionary<string, object>
                {
                    { "@id_producto", long.Parse(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (DetalleSolicitudCompra, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var detalle = new DetalleSolicitudCompra {
                Id = Convert.ToInt64(lector["id_detalle_solicitud_compra"]),
                IdSolicitudCompra = Convert.ToInt64(lector["id_solicitud_compra"]),
                IdProducto = Convert.ToInt64(lector["id_producto"]),
                CantidadSolicitada = Convert.ToDecimal(lector["cantidad_solicitada"]),
                PrecioAdquisicionReferencia = Convert.ToDecimal(lector["precio_adquisicion_referencia"]),
                IdPresentacion = Convert.ToInt64(lector["id_presentacion"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            return (detalle, entidadesExtra);
        }

        #region SINGLETON

        public static RepoDetalleSolicitudCompra Instancia { get; } = new RepoDetalleSolicitudCompra();

        #endregion

        #region UTILES

        public List<DetalleSolicitudCompra> ObtenerPorIdSolicitud(long idSolicitud) {
            var (_, resultados) = Buscar(FiltroBusquedaDetalleSolicitudCompra.IdSolicitudCompra, idSolicitud.ToString());
            return resultados.Select(r => r.entidadBase).ToList();
        }

        public bool EliminarPorIdSolicitud(long idSolicitud) {
            var consulta = """
                DELETE FROM adv__detalle_solicitud_compra
                WHERE id_solicitud_compra = @id_solicitud_compra
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_solicitud_compra", idSolicitud }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        #endregion
    }
}