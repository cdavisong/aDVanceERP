using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compra {
    public class RepoRecepcionCompra : RepoEntidadBaseDatos<RecepcionCompra, FiltroBusquedaRecepcionCompra> {
        public RepoRecepcionCompra() : base("adv__recepcion_compra", "id_recepcion_compra") {
        }

        protected override string GenerarComandoAdicionar(RecepcionCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__recepcion_compra (
                    id_compra,
                    fecha_recepcion,
                    recibido_por,
                    observaciones,
                    id_movimiento_generado
                ) VALUES (
                    @id_compra,
                    @fecha_recepcion,
                    @recibido_por,
                    @observaciones,
                    @id_movimiento_generado
                )
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_compra", entidad.IdCompra },
                { "@fecha_recepcion", entidad.FechaRecepcion.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@recibido_por", entidad.RecibidoPor },
                { "@observaciones", entidad.Observaciones },
                { "@id_movimiento_generado", entidad.IdMovimientoGenerado }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(RecepcionCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__recepcion_compra 
                SET 
                    id_compra = @id_compra,
                    fecha_recepcion = @fecha_recepcion,
                    recibido_por = @recibido_por,
                    observaciones = @observaciones,
                    id_movimiento_generado = @id_movimiento_generado
                WHERE id_recepcion_compra = @id_recepcion
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_compra", entidad.IdCompra },
                { "@fecha_recepcion", entidad.FechaRecepcion.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@recibido_por", entidad.RecibidoPor },
                { "@observaciones", entidad.Observaciones },
                { "@id_movimiento_generado", entidad.IdMovimientoGenerado },
                { "@id_recepcion", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                -- Eliminar detalles primero
                DELETE FROM adv__detalle_recepcion_compra
                WHERE id_recepcion_compra = @id_recepcion;

                -- Eliminar recepción
                DELETE FROM adv__recepcion_compra
                WHERE id_recepcion_compra = @id_recepcion;
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_recepcion", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaRecepcionCompra filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = """
                SELECT r.*, u.nombre as nombre_recibido_por
                FROM adv__recepcion_compra r
                LEFT JOIN adv__cuenta_usuario u ON r.recibido_por = u.id_cuenta_usuario
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaRecepcionCompra.Id => $"""
                    {consultaComun}
                    WHERE r.id_recepcion_compra = @id_recepcion
                    """,
                FiltroBusquedaRecepcionCompra.IdCompra => $"""
                    {consultaComun}
                    WHERE r.id_compra = @id_compra
                    """,
                FiltroBusquedaRecepcionCompra.FechaRecepcion => $"""
                    {consultaComun}
                    WHERE DATE(r.fecha_recepcion) = @fecha_recepcion
                    """,
                FiltroBusquedaRecepcionCompra.RecibidoPor => $"""
                    {consultaComun}
                    WHERE r.recibido_por = @recibido_por
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaRecepcionCompra.Id => new Dictionary<string, object>
                {
                    { "@id_recepcion", long.Parse(criterio) }
                },
                FiltroBusquedaRecepcionCompra.IdCompra => new Dictionary<string, object>
                {
                    { "@id_compra", long.Parse(criterio) }
                },
                FiltroBusquedaRecepcionCompra.FechaRecepcion => new Dictionary<string, object>
                {
                    { "@fecha_recepcion", criterio }
                },
                FiltroBusquedaRecepcionCompra.RecibidoPor => new Dictionary<string, object>
                {
                    { "@recibido_por", long.Parse(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (RecepcionCompra, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var recepcion = new RecepcionCompra {
                Id = Convert.ToInt64(lector["id_recepcion_compra"]),
                IdCompra = Convert.ToInt64(lector["id_compra"]),
                FechaRecepcion = Convert.ToDateTime(lector["fecha_recepcion"]),
                RecibidoPor = Convert.ToInt64(lector["recibido_por"]),
                Observaciones = lector["observaciones"] != DBNull.Value ? Convert.ToString(lector["observaciones"]) ?? "N/A" : "N/A",
                IdMovimientoGenerado = lector["id_movimiento_generado"] != DBNull.Value ? Convert.ToInt64(lector["id_movimiento_generado"]) : null
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            return (recepcion, entidadesExtra);
        }

        #region SINGLETON

        public static RepoRecepcionCompra Instancia { get; } = new RepoRecepcionCompra();

        #endregion

        #region UTILES

        public long RegistrarRecepcionConDetalles(RecepcionCompra recepcion, List<DetalleRecepcionCompra> detalles, long idMovimientoInventario) {
            using var conexion = ContextoBaseDatos.ObtenerConexionOptimizada();
            conexion.Open();
            using var transaccion = conexion.BeginTransaction();

            try {
                // Asignar el movimiento de inventario
                recepcion.IdMovimientoGenerado = idMovimientoInventario;

                // Insertar recepción
                var idRecepcion = ContextoBaseDatos.EjecutarComandoInsert(
                    GenerarComandoAdicionar(recepcion, out var parametrosRecepcion),
                    parametrosRecepcion,
                    conexion,
                    transaccion);

                // Insertar detalles
                var repoDetalle = new RepoDetalleRecepcionCompra();
                foreach (var detalle in detalles) {
                    detalle.IdRecepcionCompra = idRecepcion;
                    repoDetalle.Adicionar(detalle);
                }

                // Actualizar cantidades recibidas en los detalles de compra
                var repoDetalleCompra = new RepoDetalleCompraProducto();
                foreach (var detalle in detalles) {
                    var detalleCompra = repoDetalleCompra.ObtenerPorId(detalle.IdDetalleCompraProducto);
                    if (detalleCompra != null) {
                        var nuevaCantidadRecibida = detalleCompra.CantidadRecibida + detalle.CantidadRecibida;
                        repoDetalleCompra.ActualizarCantidadRecibida(detalle.IdDetalleCompraProducto, nuevaCantidadRecibida);
                    }
                }

                // Verificar si la compra está completamente recibida
                var repoCompra = new RepoCompra();
                var detallesCompra = repoDetalleCompra.ObtenerPorIdCompra(recepcion.IdCompra);
                var completamenteRecibida = detallesCompra.All(d => d.PendientePorRecibir <= 0);

                if (completamenteRecibida) {
                    var consulta = """
                        UPDATE adv__compra
                        SET estado_compra = 'Recibida_Completa'
                        WHERE id_compra = @id_compra
                        """;
                    var parametros = new Dictionary<string, object>
                    {
                        { "@id_compra", recepcion.IdCompra }
                    };
                    ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros, conexion, transaccion);
                } else {
                    var consulta = """
                        UPDATE adv__compra
                        SET estado_compra = 'Recibida_Parcial'
                        WHERE id_compra = @id_compra
                          AND estado_compra NOT IN ('Recibida_Completa', 'Cancelada')
                        """;
                    var parametros = new Dictionary<string, object>
                    {
                        { "@id_compra", recepcion.IdCompra }
                    };
                    ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros, conexion, transaccion);
                }

                transaccion.Commit();
                return idRecepcion;
            } catch {
                transaccion.Rollback();
                throw;
            }
        }

        public List<RecepcionCompra> ObtenerPorIdCompra(long idCompra) {
            var (_, resultados) = Buscar(FiltroBusquedaRecepcionCompra.IdCompra, idCompra.ToString());
            return resultados.Select(r => r.entidadBase).ToList();
        }

        #endregion
    }
}