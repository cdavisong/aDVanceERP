using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compra {
    public class RepoCompra : RepoEntidadBaseDatos<Modelos.Modulos.Compra.Compra, FiltroBusquedaCompra> {
        public RepoCompra() : base("adv__compra", "id_compra") {
        }

        protected override string GenerarComandoAdicionar(Modelos.Modulos.Compra.Compra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__compra (
                    codigo,
                    id_proveedor,
                    id_solicitud_compra,
                    id_empleado_comprador,
                    id_almacen_destino,
                    id_tipo_compra,
                    fecha_orden,
                    fecha_entrega_esperada,
                    condiciones_pago,
                    subtotal,
                    impuesto_total,
                    total_compra,
                    estado_compra,
                    fecha_aprobacion,
                    aprobado_por,
                    observaciones,
                    activo
                ) VALUES (
                    @codigo,
                    @id_proveedor,
                    @id_solicitud_compra,
                    @id_empleado_comprador,
                    @id_almacen_destino,
                    @id_tipo_compra,
                    @fecha_orden,
                    @fecha_entrega_esperada,
                    @condiciones_pago,
                    @subtotal,
                    @impuesto_total,
                    @total_compra,
                    @estado_compra,
                    @fecha_aprobacion,
                    @aprobado_por,
                    @observaciones,
                    @activo
                )
                """;

            parametros = new Dictionary<string, object>
            {
                { "@codigo", entidad.Codigo },
                { "@id_proveedor", entidad.IdProveedor },
                { "@id_solicitud_compra", entidad.IdSolicitudCompra },
                { "@id_empleado_comprador", entidad.IdEmpleadoComprador },
                { "@id_almacen_destino", entidad.IdAlmacenDestino },
                { "@id_tipo_compra", entidad.IdTipoCompra },
                { "@fecha_orden", entidad.FechaOrden.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_entrega_esperada", entidad.FechaEntregaEsperada?.ToString("yyyy-MM-dd") },
                { "@condiciones_pago", entidad.CondicionesPago },
                { "@subtotal", entidad.Subtotal },
                { "@impuesto_total", entidad.ImpuestoTotal },
                { "@total_compra", entidad.TotalCompra },
                { "@estado_compra", entidad.EstadoCompra },
                { "@fecha_aprobacion", entidad.FechaAprobacion?.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@aprobado_por", entidad.AprobadoPor },
                { "@observaciones", entidad.Observaciones },
                { "@activo", entidad.Activo }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Modelos.Modulos.Compra.Compra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__compra 
                SET 
                    codigo = @codigo,
                    id_proveedor = @id_proveedor,
                    id_solicitud_compra = @id_solicitud_compra,
                    id_empleado_comprador = @id_empleado_comprador,
                    id_almacen_destino = @id_almacen_destino,
                    id_tipo_compra = @id_tipo_compra,
                    fecha_orden = @fecha_orden,
                    fecha_entrega_esperada = @fecha_entrega_esperada,
                    condiciones_pago = @condiciones_pago,
                    subtotal = @subtotal,
                    impuesto_total = @impuesto_total,
                    total_compra = @total_compra,
                    estado_compra = @estado_compra,
                    fecha_aprobacion = @fecha_aprobacion,
                    aprobado_por = @aprobado_por,
                    observaciones = @observaciones,
                    activo = @activo
                WHERE id_compra = @id_compra
                """;

            parametros = new Dictionary<string, object>
            {
                { "@codigo", entidad.Codigo },
                { "@id_proveedor", entidad.IdProveedor },
                { "@id_solicitud_compra", entidad.IdSolicitudCompra },
                { "@id_empleado_comprador", entidad.IdEmpleadoComprador },
                { "@id_almacen_destino", entidad.IdAlmacenDestino },
                { "@id_tipo_compra", entidad.IdTipoCompra },
                { "@fecha_orden", entidad.FechaOrden.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@fecha_entrega_esperada", entidad.FechaEntregaEsperada?.ToString("yyyy-MM-dd") },
                { "@condiciones_pago", entidad.CondicionesPago },
                { "@subtotal", entidad.Subtotal },
                { "@impuesto_total", entidad.ImpuestoTotal },
                { "@total_compra", entidad.TotalCompra },
                { "@estado_compra", entidad.EstadoCompra },
                { "@fecha_aprobacion", entidad.FechaAprobacion?.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@aprobado_por", entidad.AprobadoPor },
                { "@observaciones", entidad.Observaciones },
                { "@activo", entidad.Activo },
                { "@id_compra", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                -- Eliminar recepciones y sus detalles primero
                DELETE drc FROM adv__detalle_recepcion_compra drc
                INNER JOIN adv__recepcion_compra rc ON drc.id_recepcion_compra = rc.id_recepcion_compra
                WHERE rc.id_compra = @id_compra;

                DELETE FROM adv__recepcion_compra
                WHERE id_compra = @id_compra;

                -- Eliminar detalles de compra
                DELETE FROM adv__detalle_compra_producto
                WHERE id_compra = @id_compra;

                -- Finalmente eliminar la compra
                DELETE FROM adv__compra
                WHERE id_compra = @id_compra;
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_compra", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaCompra filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = """
                SELECT c.*, 
                       prov.razon_social as nombre_proveedor,
                       al.nombre as nombre_almacen,
                       tc.nombre as nombre_tipo_compra
                FROM adv__compra c
                LEFT JOIN adv__proveedor prov ON c.id_proveedor = prov.id_proveedor
                LEFT JOIN adv__almacen al ON c.id_almacen_destino = al.id_almacen
                LEFT JOIN adv__tipo_compra tc ON c.id_tipo_compra = tc.id_tipo_compra
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaCompra.Id => $"""
                    {consultaComun}
                    WHERE c.id_compra = @id_compra
                    """,
                FiltroBusquedaCompra.Codigo => $"""
                    {consultaComun}
                    WHERE c.codigo = @codigo
                    """,
                FiltroBusquedaCompra.IdProveedor => $"""
                    {consultaComun}
                    WHERE c.id_proveedor = @id_proveedor
                    """,
                FiltroBusquedaCompra.IdSolicitudCompra => $"""
                    {consultaComun}
                    WHERE c.id_solicitud_compra = @id_solicitud_compra
                    """,
                FiltroBusquedaCompra.Estado => $"""
                    {consultaComun}
                    WHERE c.estado_compra = @estado
                    """,
                FiltroBusquedaCompra.FechaOrden => $"""
                    {consultaComun}
                    WHERE DATE(c.fecha_orden) = @fecha_orden
                    """,
                FiltroBusquedaCompra.PendientesAprobacion => $"""
                    {consultaComun}
                    WHERE c.estado_compra IN ('Borrador', 'Pendiente_Aprobacion')
                    """,
                FiltroBusquedaCompra.PendientesRecepcion => $"""
                    {consultaComun}
                    WHERE c.estado_compra IN ('Aprobada', 'Enviada', 'Recibida_Parcial')
                    """,
                FiltroBusquedaCompra.PendientesPago => $"""
                    {consultaComun}
                    WHERE c.estado_compra IN ('Recibida_Completa', 'Facturada')
                      AND c.total_compra > COALESCE((
                          SELECT SUM(monto_pagado) 
                          FROM adv__pago 
                          WHERE id_compra = c.id_compra 
                            AND estado_pago = 'Confirmado'
                      ), 0)
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaCompra.Id => new Dictionary<string, object>
                {
                    { "@id_compra", long.Parse(criterio) }
                },
                FiltroBusquedaCompra.Codigo => new Dictionary<string, object>
                {
                    { "@codigo", criterio }
                },
                FiltroBusquedaCompra.IdProveedor => new Dictionary<string, object>
                {
                    { "@id_proveedor", long.Parse(criterio) }
                },
                FiltroBusquedaCompra.IdSolicitudCompra => new Dictionary<string, object>
                {
                    { "@id_solicitud_compra", long.Parse(criterio) }
                },
                FiltroBusquedaCompra.Estado => new Dictionary<string, object>
                {
                    { "@estado", criterio }
                },
                FiltroBusquedaCompra.FechaOrden => new Dictionary<string, object>
                {
                    { "@fecha_orden", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Modelos.Modulos.Compra.Compra, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var compra = new Modelos.Modulos.Compra.Compra {
                Id = Convert.ToInt64(lector["id_compra"]),
                Codigo = Convert.ToString(lector["codigo"]) ?? "N/A",
                IdProveedor = Convert.ToInt64(lector["id_proveedor"]),
                IdSolicitudCompra = lector["id_solicitud_compra"] != DBNull.Value ? Convert.ToInt64(lector["id_solicitud_compra"]) : null,
                IdEmpleadoComprador = lector["id_empleado_comprador"] != DBNull.Value ? Convert.ToInt64(lector["id_empleado_comprador"]) : null,
                IdAlmacenDestino = Convert.ToInt64(lector["id_almacen_destino"]),
                IdTipoCompra = lector["id_tipo_compra"] != DBNull.Value ? Convert.ToInt64(lector["id_tipo_compra"]) : null,
                FechaOrden = Convert.ToDateTime(lector["fecha_orden"]),
                FechaEntregaEsperada = lector["fecha_entrega_esperada"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_entrega_esperada"]) : null,
                CondicionesPago = lector["condiciones_pago"] != DBNull.Value ? Convert.ToString(lector["condiciones_pago"]) ?? "N/A" : "N/A",
                Subtotal = Convert.ToDecimal(lector["subtotal"]),
                ImpuestoTotal = Convert.ToDecimal(lector["impuesto_total"]),
                TotalCompra = Convert.ToDecimal(lector["total_compra"]),
                EstadoCompra = Convert.ToString(lector["estado_compra"]) ?? "Borrador",
                FechaAprobacion = lector["fecha_aprobacion"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_aprobacion"]) : null,
                AprobadoPor = lector["aprobado_por"] != DBNull.Value ? Convert.ToInt64(lector["aprobado_por"]) : null,
                Observaciones = lector["observaciones"] != DBNull.Value ? Convert.ToString(lector["observaciones"]) ?? "N/A" : "N/A",
                Activo = Convert.ToBoolean(lector["activo"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            return (compra, entidadesExtra);
        }

        #region STATIC

        public static RepoCompra Instancia { get; } = new RepoCompra();

        #endregion

        #region UTILES

        public long AdicionarConDetalles(Modelos.Modulos.Compra.Compra compra, List<DetalleCompraProducto> detalles) {
            using var conexion = ContextoBaseDatos.ObtenerConexionOptimizada();
            conexion.Open();
            using var transaccion = conexion.BeginTransaction();

            try {
                // Insertar compra
                var idCompra = ContextoBaseDatos.EjecutarComandoInsert(
                    GenerarComandoAdicionar(compra, out var parametrosCompra),
                    parametrosCompra,
                    conexion,
                    transaccion);

                // Insertar detalles
                var repoDetalle = new RepoDetalleCompraProducto();
                foreach (var detalle in detalles) {
                    detalle.IdCompra = idCompra;
                    repoDetalle.Adicionar(detalle);
                }

                // Si viene de una solicitud, actualizar estado de la solicitud
                if (compra.IdSolicitudCompra.HasValue) {
                    var consulta = """
                        UPDATE adv__solicitud_compra
                        SET estado = 'Convertida'
                        WHERE id_solicitud_compra = @id_solicitud
                        """;
                    var parametros = new Dictionary<string, object>
                    {
                        { "@id_solicitud", compra.IdSolicitudCompra.Value }
                    };
                    ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros, conexion, transaccion);
                }

                transaccion.Commit();
                return idCompra;
            } catch {
                transaccion.Rollback();
                throw;
            }
        }

        public bool AprobarCompra(long idCompra, long idAprobador) {
            var consulta = """
                UPDATE adv__compra
                SET estado_compra = 'Aprobada',
                    fecha_aprobacion = NOW(),
                    aprobado_por = @aprobado_por
                WHERE id_compra = @id_compra 
                  AND estado_compra IN ('Borrador', 'Pendiente_Aprobacion')
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_compra", idCompra },
                { "@aprobado_por", idAprobador }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool EnviarCompra(long idCompra) {
            var consulta = """
                UPDATE adv__compra
                SET estado_compra = 'Enviada'
                WHERE id_compra = @id_compra 
                  AND estado_compra = 'Aprobada'
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_compra", idCompra }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public bool CancelarCompra(long idCompra, string motivo) {
            var consulta = """
                UPDATE adv__compra
                SET estado_compra = 'Cancelada',
                    observaciones = CONCAT(observaciones, ' | Cancelada: ', @motivo)
                WHERE id_compra = @id_compra 
                  AND estado_compra NOT IN ('Recibida_Completa', 'Facturada')
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_compra", idCompra },
                { "@motivo", motivo }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public decimal ObtenerTotalPagado(long idCompra) {
            var consulta = """
                SELECT COALESCE(SUM(monto_pagado), 0)
                FROM adv__pago
                WHERE id_compra = @id_compra
                  AND estado_pago = 'Confirmado'
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@id_compra", idCompra }
            };

            return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
        }

        public decimal ObtenerSaldoPendiente(long idCompra) {
            var compra = ObtenerPorId(idCompra);
            if (compra == null) return 0;

            var totalPagado = ObtenerTotalPagado(idCompra);
            return compra.TotalCompra - totalPagado;
        }

        #endregion
    }
}