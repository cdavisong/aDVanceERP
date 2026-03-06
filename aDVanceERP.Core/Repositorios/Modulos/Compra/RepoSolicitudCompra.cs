using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compra {
    public class RepoSolicitudCompra : RepoEntidadBaseDatos<SolicitudCompra, FiltroBusquedaSolicitudCompra> {
        public RepoSolicitudCompra() : base("adv__solicitud_compra", "id_solicitud_compra") {
        }

        protected override string GenerarComandoAdicionar(SolicitudCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__solicitud_compra (
                    codigo,
                    id_solicitante,
                    fecha_solicitud,
                    fecha_requerida,
                    observaciones,
                    estado,
                    activo
                ) VALUES (
                    @codigo,
                    @id_solicitante,
                    @fecha_solicitud,
                    @fecha_requerida,
                    @observaciones,
                    @estado,
                    @activo
                )
                """;

            parametros = new Dictionary<string, object>
            {
                { "@codigo", entidad.Codigo },
                { "@id_solicitante", entidad.IdSolicitante },
                { "@fecha_solicitud", entidad.FechaSolicitud.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_requerida", entidad.FechaRequerida?.ToString("yyyy-MM-dd") },
                { "@observaciones", entidad.Observaciones },
                { "@estado", entidad.Estado.ToString() },
                { "@activo", entidad.Activo }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(SolicitudCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__solicitud_compra 
                SET 
                    codigo = @codigo,
                    id_solicitante = @id_solicitante,
                    fecha_solicitud = @fecha_solicitud,
                    fecha_requerida = @fecha_requerida,
                    observaciones = @observaciones,
                    estado = @estado,
                    activo = @activo
                WHERE id_solicitud_compra = @id_solicitud_compra
                """;

            parametros = new Dictionary<string, object>
            {
                { "@codigo", entidad.Codigo },
                { "@id_solicitante", entidad.IdSolicitante },
                { "@fecha_solicitud", entidad.FechaSolicitud.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_requerida", entidad.FechaRequerida?.ToString("yyyy-MM-dd") },
                { "@observaciones", entidad.Observaciones },
                { "@estado", entidad.Estado.ToString() },
                { "@activo", entidad.Activo },
                { "@id_solicitud_compra", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                -- Eliminar primero los detalles (por la FK)
                DELETE FROM adv__detalle_solicitud_compra
                WHERE id_solicitud_compra = @id_solicitud_compra;

                -- Luego eliminar la solicitud
                DELETE FROM adv__solicitud_compra
                WHERE id_solicitud_compra = @id_solicitud_compra;
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_solicitud_compra", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaSolicitudCompra filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = """
                SELECT s.*, u.nombre as nombre_solicitante
                FROM adv__solicitud_compra s
                LEFT JOIN adv__cuenta_usuario u ON s.id_solicitante = u.id_cuenta_usuario
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaSolicitudCompra.Id => $"""
                    {consultaComun}
                    WHERE s.id_solicitud_compra = @id_solicitud_compra
                    """,
                FiltroBusquedaSolicitudCompra.Codigo => $"""
                    {consultaComun}
                    WHERE s.codigo = @codigo
                    """,
                FiltroBusquedaSolicitudCompra.IdSolicitante => $"""
                    {consultaComun}
                    WHERE s.id_solicitante = @id_solicitante
                    """,
                FiltroBusquedaSolicitudCompra.Estado => $"""
                    {consultaComun}
                    WHERE s.estado = @estado
                    """,
                FiltroBusquedaSolicitudCompra.FechaSolicitud => $"""
                    {consultaComun}
                    WHERE DATE(s.fecha_solicitud) = @fecha_solicitud
                    """,
                FiltroBusquedaSolicitudCompra.PendientesAprobacion => $"""
                    {consultaComun}
                    WHERE s.estado IN ('Pendiente_Aprobacion', 'Borrador')
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaSolicitudCompra.Id => new Dictionary<string, object> {
                    { "@id_solicitud_compra", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) } ,
                },
                FiltroBusquedaSolicitudCompra.Codigo => new Dictionary<string, object> {
                    { "@codigo", criterio }
                },
                FiltroBusquedaSolicitudCompra.IdSolicitante => new Dictionary<string, object> {
                    { "@id_solicitante", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) } ,
                },
                FiltroBusquedaSolicitudCompra.Estado => new Dictionary<string, object> {
                    { "@estado", criterio }
                },
                FiltroBusquedaSolicitudCompra.FechaSolicitud => new Dictionary<string, object> {
                    { "@fecha_solicitud", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (SolicitudCompra, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var solicitud = new SolicitudCompra {
                Id = Convert.ToInt64(lector["id_solicitud_compra"]),
                Codigo = Convert.ToString(lector["codigo"]) ?? "N/A",
                IdSolicitante = Convert.ToInt64(lector["id_solicitante"]),
                FechaSolicitud = Convert.ToDateTime(lector["fecha_solicitud"]),
                FechaRequerida = lector["fecha_requerida"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_requerida"]) : null,
                Observaciones = lector["observaciones"] != DBNull.Value ? Convert.ToString(lector["observaciones"]) ?? "N/A" : "N/A",
                Estado = Enum.Parse<EstadoSolicitudCompraEnum>(Convert.ToString(lector["estado"]) ?? "Borrador"),
                Activo = Convert.ToBoolean(lector["activo"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            // Cargar detalles si existen en el lector
            if (lector.VisibleFieldCount > 8 && lector["nombre_solicitante"] != DBNull.Value) {
                // Aquí podrías agregar información adicional si es necesario
            }

            return (solicitud, entidadesExtra);
        }

        #region STATIC

        public static RepoSolicitudCompra Instancia { get; } = new RepoSolicitudCompra();

        #endregion

        #region UTILES

        public long AdicionarConDetalles(SolicitudCompra solicitud, List<DetalleSolicitudCompra> detalles) {
            using var conexion = ContextoBaseDatos.ObtenerConexionOptimizada();
            conexion.Open();
            using var transaccion = conexion.BeginTransaction();

            try {
                // Insertar solicitud
                var idSolicitud = ContextoBaseDatos.EjecutarComandoInsert(
                    GenerarComandoAdicionar(solicitud, out var parametrosSolicitud),
                    parametrosSolicitud,
                    conexion,
                    transaccion);

                // Insertar detalles
                var repoDetalle = new RepoDetalleSolicitudCompra();
                foreach (var detalle in detalles) {
                    detalle.IdSolicitudCompra = idSolicitud;
                    repoDetalle.Adicionar(detalle);
                }

                transaccion.Commit();
                return idSolicitud;
            } catch {
                transaccion.Rollback();
                throw;
            }
        }

        public bool AprobarSolicitud(long idSolicitud, long idAprobador) {
            var consulta = """
                UPDATE adv__solicitud_compra
                SET estado = 'Aprobada'
                WHERE id_solicitud_compra = @id_solicitud_compra 
                  AND estado = 'Pendiente_Aprobacion'
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_solicitud_compra", idSolicitud }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool RechazarSolicitud(long idSolicitud, string motivo) {
            var consulta = """
                UPDATE adv__solicitud_compra
                SET estado = 'Rechazada',
                    observaciones = CONCAT(observaciones, ' | Rechazada: ', @motivo)
                WHERE id_solicitud_compra = @id_solicitud_compra
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_solicitud_compra", idSolicitud },
                { "@motivo", motivo }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        #endregion
    }
}