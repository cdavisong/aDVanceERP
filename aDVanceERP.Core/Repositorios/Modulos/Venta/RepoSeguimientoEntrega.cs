using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;
using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Venta {
    public class RepoSeguimientoEntrega : RepoEntidadBaseDatos<SeguimientoEntrega, FiltroBusquedaSeguimientoEntrega> {
        public RepoSeguimientoEntrega() : base("adv__seguimiento_entrega", "id_seguimiento_entrega") {
        }

        protected override string GenerarComandoAdicionar(SeguimientoEntrega entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__seguimiento_entrega (
                    id_venta,
                    id_mensajero,
                    tipo_envio,
                    fecha_asignacion,
                    fecha_entrega_realizada,
                    fecha_pago_negocio,
                    estado_entrega,
                    monto_cobrado_al_cliente,
                    observaciones_entrega
                ) VALUES (
                    @id_venta,
                    @id_mensajero,
                    @tipo_envio,
                    @fecha_asignacion,
                    @fecha_entrega_realizada,
                    @fecha_pago_negocio,
                    @estado_entrega,
                    @monto_cobrado_al_cliente,
                    @observaciones_entrega
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_venta", entidad.IdVenta },
                { "@id_mensajero", entidad.IdMensajero.HasValue ? entidad.IdMensajero.Value : DBNull.Value },
                { "@tipo_envio", entidad.TipoEnvio.ToString() },
                { "@fecha_asignacion", entidad.FechaAsignacion.HasValue ? entidad.FechaAsignacion.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@fecha_entrega_realizada", entidad.FechaEntregaRealizada.HasValue ? entidad.FechaEntregaRealizada.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@fecha_pago_negocio", entidad.FechaPagoNegocio.HasValue ? entidad.FechaPagoNegocio.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@estado_entrega", entidad.EstadoEntrega.ToString() },
                { "@monto_cobrado_al_cliente", entidad.MontoCobradoAlCliente },
                { "@observaciones_entrega", entidad.ObservacionesEntrega }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(SeguimientoEntrega entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__seguimiento_entrega 
                SET 
                    id_venta = @id_venta,
                    id_mensajero = @id_mensajero,
                    tipo_envio = @tipo_envio,
                    fecha_asignacion = @fecha_asignacion,
                    fecha_entrega_realizada = @fecha_entrega_realizada,
                    fecha_pago_negocio = @fecha_pago_negocio,
                    estado_entrega = @estado_entrega,
                    monto_cobrado_al_cliente = @monto_cobrado_al_cliente,
                    observaciones_entrega = @observaciones_entrega
                WHERE id_seguimiento_entrega = @id_seguimiento
                """;

            parametros = new Dictionary<string, object> {
                { "@id_seguimiento", entidad.Id },
                { "@id_venta", entidad.IdVenta },
                { "@id_mensajero", entidad.IdMensajero.HasValue ? entidad.IdMensajero.Value : DBNull.Value },
                { "@tipo_envio", entidad.TipoEnvio.ToString() },
                { "@fecha_asignacion", entidad.FechaAsignacion.HasValue ? entidad.FechaAsignacion.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@fecha_entrega_realizada", entidad.FechaEntregaRealizada.HasValue ? entidad.FechaEntregaRealizada.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@fecha_pago_negocio", entidad.FechaPagoNegocio.HasValue ? entidad.FechaPagoNegocio.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@estado_entrega", entidad.EstadoEntrega.ToString() },
                { "@monto_cobrado_al_cliente", entidad.MontoCobradoAlCliente },
                { "@observaciones_entrega", entidad.ObservacionesEntrega }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__seguimiento_entrega 
                WHERE id_seguimiento_entrega = @id_seguimiento
                """;

            parametros = new Dictionary<string, object> {
                { "@id_seguimiento", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaSeguimientoEntrega filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT se.*, 
                       v.numero_factura_ticket,
                       v.importe_total as total_venta,
                       c.nombre_completo as nombre_cliente
                FROM adv__seguimiento_entrega se
                LEFT JOIN adv__venta v ON se.id_venta = v.id_venta
                LEFT JOIN adv__cliente cl ON v.id_cliente = cl.id_cliente
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaSeguimientoEntrega.Id => $"""
                    {consultaComun}
                    WHERE se.id_seguimiento_entrega = @id_seguimiento
                    """,
                FiltroBusquedaSeguimientoEntrega.IdVenta => $"""
                    {consultaComun}
                    WHERE se.id_venta = @id_venta
                    """,
                FiltroBusquedaSeguimientoEntrega.IdMensajero => $"""
                    {consultaComun}
                    WHERE se.id_mensajero = @id_mensajero
                    """,
                FiltroBusquedaSeguimientoEntrega.Estado => $"""
                    {consultaComun}
                    WHERE se.estado_entrega = @estado_entrega
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaSeguimientoEntrega.Id => new Dictionary<string, object> {
                    { "@id_seguimiento", long.Parse(criterio) }
                },
                FiltroBusquedaSeguimientoEntrega.IdVenta => new Dictionary<string, object> {
                    { "@id_venta", long.Parse(criterio) }
                },
                FiltroBusquedaSeguimientoEntrega.IdMensajero => new Dictionary<string, object> {
                    { "@id_mensajero", long.Parse(criterio) }
                },
                FiltroBusquedaSeguimientoEntrega.Estado => new Dictionary<string, object> {
                    { "@estado_entrega", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (SeguimientoEntrega, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var seguimiento = new SeguimientoEntrega {
                Id = Convert.ToInt64(lector["id_seguimiento_entrega"]),
                IdVenta = Convert.ToInt64(lector["id_venta"]),
                IdMensajero = lector["id_mensajero"] != DBNull.Value ? Convert.ToInt64(lector["id_mensajero"]) : null,
                TipoEnvio = Enum.Parse<TipoEnvioEnum>(Convert.ToString(lector["tipo_envio"]) ?? "RetiroEnLocal"),
                FechaAsignacion = lector["fecha_asignacion"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_asignacion"]) : null,
                FechaEntregaRealizada = lector["fecha_entrega_realizada"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_entrega_realizada"]) : null,
                FechaPagoNegocio = lector["fecha_pago_negocio"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_pago_negocio"]) : null,
                EstadoEntrega = Enum.Parse<EstadoEntregaEnum>(Convert.ToString(lector["estado_entrega"]) ?? "PendienteAsignacion"),
                MontoCobradoAlCliente = Convert.ToDecimal(lector["monto_cobrado_al_cliente"], CultureInfo.InvariantCulture),
                ObservacionesEntrega = lector["observaciones_entrega"] != DBNull.Value ? Convert.ToString(lector["observaciones_entrega"]) : null
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 9) {
                entidadesExtra.Add(new Modelos.Modulos.Venta.Venta {
                    NumeroFacturaTicket = lector["numero_factura_ticket"] != DBNull.Value ? Convert.ToString(lector["numero_factura_ticket"]) : null,
                    ImporteTotal = Convert.ToDecimal(lector["total_venta"], CultureInfo.InvariantCulture)
                });

                entidadesExtra.Add(new Persona {
                    NombreCompleto = Convert.ToString(lector["nombre_cliente"]) ?? "N/A"
                });
            }

            return (seguimiento, entidadesExtra);
        }

        #region STATIC

        public static RepoSeguimientoEntrega Instancia { get; } = new RepoSeguimientoEntrega();

        #endregion

        #region UTILES

        public bool AsignarMensajero(long idSeguimiento, long idMensajero) {
            var consulta = $"""
                UPDATE adv__seguimiento_entrega
                SET id_mensajero = @id_mensajero,
                    estado_entrega = 'Asignado',
                    fecha_asignacion = NOW()
                WHERE id_seguimiento_entrega = @id_seguimiento
                AND estado_entrega = 'Pendiente Asignación';
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_seguimiento", idSeguimiento },
                { "@id_mensajero", idMensajero }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool MarcarEnRuta(long idSeguimiento) {
            var consulta = $"""
                UPDATE adv__seguimiento_entrega
                SET estado_entrega = 'En Ruta'
                WHERE id_seguimiento_entrega = @id_seguimiento
                AND estado_entrega = 'Asignado';
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_seguimiento", idSeguimiento }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool MarcarEntregado(long idSeguimiento, decimal montoCobrado, string observaciones = null) {
            var consulta = $"""
                UPDATE adv__seguimiento_entrega
                SET estado_entrega = 'Entregado',
                    fecha_entrega_realizada = NOW(),
                    monto_cobrado_al_cliente = @monto_cobrado,
                    observaciones_entrega = @observaciones
                WHERE id_seguimiento_entrega = @id_seguimiento
                AND estado_entrega IN ('Asignado', 'En Ruta');
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_seguimiento", idSeguimiento },
                { "@monto_cobrado", montoCobrado },
                { "@observaciones", observaciones }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool RegistrarPagoRecibido(long idSeguimiento) {
            var consulta = $"""
                UPDATE adv__seguimiento_entrega
                SET estado_entrega = 'Pago Recibido',
                    fecha_pago_negocio = NOW()
                WHERE id_seguimiento_entrega = @id_seguimiento
                AND estado_entrega = 'Entregado';
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_seguimiento", idSeguimiento }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool CancelarEntrega(long idSeguimiento, string motivo) {
            var consulta = $"""
                UPDATE adv__seguimiento_entrega
                SET estado_entrega = 'Cancelado',
                    observaciones_entrega = CONCAT(COALESCE(observaciones_entrega, ''), '\\nCancelado: ', @motivo)
                WHERE id_seguimiento_entrega = @id_seguimiento
                AND estado_entrega NOT IN ('Entregado', 'Pago Recibido');
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_seguimiento", idSeguimiento },
                { "@motivo", motivo }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public List<SeguimientoEntrega> ObtenerEntregasPendientesPorMensajero(long idMensajero) {
            var consulta = $"""
                SELECT se.*, v.numero_factura_ticket, c.nombre_completo as nombre_cliente
                FROM adv__seguimiento_entrega se
                LEFT JOIN adv__venta v ON se.id_venta = v.id_venta
                LEFT JOIN adv__cliente cl ON v.id_cliente = cl.id_cliente
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona
                WHERE se.id_mensajero = @id_mensajero
                AND se.estado_entrega IN ('Asignado', 'En Ruta')
                ORDER BY se.fecha_asignacion ASC;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_mensajero", idMensajero }
            };

            var seguimientos = new List<SeguimientoEntrega>();
            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                (reader) => {
                    var (seguimiento, entidadesExtra) = MapearEntidad(reader);
                    return (seguimiento, entidadesExtra);
                }
            );

            foreach (var (seguimiento, _) in resultados) {
                seguimientos.Add(seguimiento);
            }

            return seguimientos;
        }

        public SeguimientoEntrega? ObtenerPorVenta(long idVenta) {
            var consulta = $"""
                SELECT * FROM adv__seguimiento_entrega
                WHERE id_venta = @id_venta
                LIMIT 1;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                (reader) => {
                    var (seguimiento, entidadesExtra) = MapearEntidad(reader);
                    return (seguimiento, entidadesExtra);
                }
            ).FirstOrDefault();

            return resultados.entidadBase;
        }

        public decimal ObtenerTotalCobradoPorMensajero(long idMensajero, DateTime? fechaInicio = null, DateTime? fechaFin = null) {
            var consulta = $"""
                SELECT COALESCE(SUM(monto_cobrado_al_cliente), 0) as total_cobrado
                FROM adv__seguimiento_entrega
                WHERE id_mensajero = @id_mensajero
                AND estado_entrega IN ('Entregado', 'Pago Recibido')
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_mensajero", idMensajero }
            };

            if (fechaInicio.HasValue) {
                consulta += " AND fecha_entrega_realizada >= @fecha_inicio";
                parametros.Add("@fecha_inicio", fechaInicio.Value.ToString("yyyy-MM-dd 00:00:00"));
            }

            if (fechaFin.HasValue) {
                consulta += " AND fecha_entrega_realizada <= @fecha_fin";
                parametros.Add("@fecha_fin", fechaFin.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
        }
        #endregion
    }
}