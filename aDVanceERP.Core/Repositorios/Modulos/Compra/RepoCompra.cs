using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

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
                { "@estado_compra", entidad.EstadoCompra.ToString() },
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
                { "@estado_compra", entidad.EstadoCompra.ToString() },
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
                FiltroBusquedaCompra.Id => new Dictionary<string, object> {
                    { "@id_compra", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) } ,
                },
                FiltroBusquedaCompra.Codigo => new Dictionary<string, object> {
                    { "@codigo", criterio }
                },
                FiltroBusquedaCompra.IdProveedor => new Dictionary<string, object> {
                    { "@id_proveedor", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) } ,
                },
                FiltroBusquedaCompra.IdSolicitudCompra => new Dictionary<string, object> {
                    { "@id_solicitud_compra", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) } ,
                },
                FiltroBusquedaCompra.Estado => new Dictionary<string, object> {
                    { "@estado", criterio }
                },
                FiltroBusquedaCompra.FechaOrden => new Dictionary<string, object> {
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
                EstadoCompra = Enum.Parse<EstadoCompraEnum>(Convert.ToString(lector["estado_compra"]) ?? "Borrador"),
                FechaAprobacion = lector["fecha_aprobacion"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_aprobacion"]) : null,
                AprobadoPor = lector["aprobado_por"] != DBNull.Value ? Convert.ToInt64(lector["aprobado_por"]) : null,
                Observaciones = lector["observaciones"] != DBNull.Value ? Convert.ToString(lector["observaciones"]) ?? "N/A" : "N/A",
                Activo = Convert.ToBoolean(lector["activo"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            return (compra, entidadesExtra);
        }

        #region SINGLETON

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

        public bool CambiarEstadoCompra(long idCompra, EstadoCompraEnum nuevoEstado) {
            var setAprobacion = nuevoEstado == EstadoCompraEnum.Aprobada
                ? ", fecha_aprobacion = NOW()"
                : string.Empty;

            var consulta = $"""
                UPDATE adv__compra
                SET estado_compra = @nuevo_estado{setAprobacion}
                WHERE id_compra = @id_compra
                """;

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta,
                new Dictionary<string, object> {
                    { "@id_compra", idCompra },
                    { "@nuevo_estado", nuevoEstado.ToString() }
                }) > 0;
        }

        public bool CancelarCompra(long idCompra, string motivo) {
            var consulta = """
                UPDATE adv__compra
                SET estado_compra = 'Cancelada',
                    observaciones = CONCAT(observaciones, ' | Cancelada: ', @motivo)
                WHERE id_compra = @id_compra 
                  AND estado_compra NOT IN ('Recibida_Completa', 'Facturada')
                """;
            var parametros = new Dictionary<string, object> {
                { "@id_compra", idCompra },
                { "@motivo", motivo }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        /// <summary>
        /// Devuelve el monto total de pagos CONFIRMADOS de la compra.
        /// Análogo a RepoPago.ObtenerTotalPagadoPorVenta().
        /// </summary>
        public decimal ObtenerTotalPagado(long idCompra) {
            const string consulta = """
                SELECT COALESCE(SUM(monto_pagado), 0)
                FROM adv__pago
                WHERE id_compra = @id_compra
                  AND estado_pago = 'Confirmado'
                """;

            return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta,
                new Dictionary<string, object> { { "@id_compra", idCompra } });
        }

        /// <summary>Devuelve el saldo pendiente (total_compra − total pagado confirmado).</summary>
        public decimal ObtenerSaldoPendiente(long idCompra) {
            var compra = ObtenerPorId(idCompra);
            return compra == null ? 0 : compra.TotalCompra - ObtenerTotalPagado(idCompra);
        }

        /// <summary>
        /// Análogo a RepoVenta.CompraEstaPagadaCompletamente().
        /// Retorna true cuando total_pagado >= total_compra Y no quedan pagos en estado Pendiente.
        /// </summary>
        public bool CompraEstaPagadaCompletamente(long idCompra) {
            const string consulta = """
                SELECT 
                    CASE 
                        WHEN c.total_compra <= COALESCE(SUM(p.monto_pagado), 0) 
                             AND COUNT(CASE WHEN p.estado_pago = 'Pendiente' THEN 1 END) = 0
                        THEN 1 ELSE 0 
                    END AS esta_pagada
                FROM adv__compra c
                LEFT JOIN adv__pago p ON c.id_compra = p.id_compra 
                    AND p.estado_pago IN ('Confirmado', 'Pendiente')
                WHERE c.id_compra = @id_compra
                GROUP BY c.id_compra, c.total_compra
                """;

            return ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta,
                new Dictionary<string, object> { { "@id_compra", idCompra } }) == 1;
        }

        /// <summary>
        /// Instantánea completa del estado de pago de una compra.
        /// Análogo a RepoVenta.VerificarEstadoPagoVenta().
        /// </summary>
        public EstadoPagoCompra VerificarEstadoPagoCompra(long idCompra) {
            const string consulta = """
                SELECT 
                    c.total_compra,
                    COALESCE(SUM(p.monto_pagado), 0) AS total_pagado,
                    c.total_compra - COALESCE(SUM(p.monto_pagado), 0) AS saldo,
                    COUNT(CASE WHEN p.estado_pago = 'Pendiente' THEN 1 END) AS pagos_pendientes
                FROM adv__compra c
                LEFT JOIN adv__pago p ON c.id_compra = p.id_compra 
                    AND p.estado_pago IN ('Confirmado', 'Pendiente')
                WHERE c.id_compra = @id_compra
                GROUP BY c.id_compra, c.total_compra
                """;

            var resultado = ContextoBaseDatos.EjecutarConsulta(consulta,
                new Dictionary<string, object> { { "@id_compra", idCompra } },
                MapearEstadoPagoCompra).FirstOrDefault().entidadBase;

            if (resultado == null)
                throw new Exception($"Compra {idCompra} no encontrada.");

            return resultado;
        }

        private (EstadoPagoCompra, List<IEntidadBaseDatos>) MapearEstadoPagoCompra(MySqlDataReader r) {
            var total = Convert.ToDecimal(r["total_compra"], CultureInfo.InvariantCulture);
            var pagado = Convert.ToDecimal(r["total_pagado"], CultureInfo.InvariantCulture);
            var saldo = Convert.ToDecimal(r["saldo"], CultureInfo.InvariantCulture);
            var pending = Convert.ToInt32(r["pagos_pendientes"]);

            return (new EstadoPagoCompra {
                TotalCompra = total,
                TotalPagado = pagado,
                Saldo = saldo,
                EstaPagadaCompletamente = total <= pagado && pending == 0,
                TienePagosPendientes = pending > 0,
                TieneSobrepago = pagado > total
            }, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Compras recibidas/facturadas con saldo > 0, ordenadas por antigüedad.
        /// Análogo a RepoVenta.ObtenerVentasPendientesDePago().
        /// </summary>
        public List<CompraPendientePago> ObtenerComprasPendientesDePago() {
            const string consulta = """
                SELECT 
                    c.id_compra,
                    c.codigo AS codigo_compra,
                    c.fecha_orden,
                    c.fecha_entrega_esperada,
                    c.total_compra,
                    c.estado_compra,
                    COALESCE(SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END), 0) AS total_pagado,
                    c.total_compra - COALESCE(SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END), 0) AS saldo_pendiente,
                    COUNT(CASE WHEN p.estado_pago = 'Pendiente' THEN 1 END) AS pagos_pendientes_confirmacion,
                    prov.codigo_proveedor,
                    prov.razon_social AS nombre_proveedor
                FROM adv__compra c
                LEFT JOIN adv__proveedor prov ON c.id_proveedor = prov.id_proveedor
                LEFT JOIN adv__pago p ON c.id_compra = p.id_compra
                    AND p.estado_pago IN ('Confirmado', 'Pendiente')
                WHERE c.estado_compra IN ('Recibida_Completa', 'Facturada')
                  AND c.activo = 1
                GROUP BY c.id_compra, c.codigo, c.fecha_orden, c.fecha_entrega_esperada,
                         c.total_compra, c.estado_compra, prov.codigo_proveedor, prov.razon_social
                HAVING saldo_pendiente > 0
                ORDER BY c.fecha_orden ASC
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearCompraPendientePago)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (CompraPendientePago, List<IEntidadBaseDatos>) MapearCompraPendientePago(MySqlDataReader r) {
            return (new CompraPendientePago {
                IdCompra = Convert.ToInt64(r["id_compra"]),
                CodigoCompra = r["codigo_compra"]?.ToString() ?? string.Empty,
                FechaOrden = Convert.ToDateTime(r["fecha_orden"]),
                FechaEntregaEsperada = r["fecha_entrega_esperada"] != DBNull.Value ? Convert.ToDateTime(r["fecha_entrega_esperada"]) : null,
                TotalCompra = Convert.ToDecimal(r["total_compra"], CultureInfo.InvariantCulture),
                TotalPagado = Convert.ToDecimal(r["total_pagado"], CultureInfo.InvariantCulture),
                SaldoPendiente = Convert.ToDecimal(r["saldo_pendiente"], CultureInfo.InvariantCulture),
                PagosPendientesConfirmacion = Convert.ToInt32(r["pagos_pendientes_confirmacion"]),
                CodigoProveedor = r["codigo_proveedor"]?.ToString() ?? string.Empty,
                NombreProveedor = r["nombre_proveedor"]?.ToString() ?? string.Empty,
                EstadoCompra = r["estado_compra"]?.ToString() ?? string.Empty
            }, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Compras recibidas/facturadas sin ningún pago registrado.
        /// Análogo a RepoVenta.ObtenerVentasSinPagos().
        /// </summary>
        public List<CompraSinPago> ObtenerComprasSinPagos() {
            const string consulta = """
                SELECT 
                    c.id_compra,
                    c.codigo AS codigo_compra,
                    c.fecha_orden,
                    c.total_compra,
                    c.estado_compra,
                    prov.codigo_proveedor,
                    prov.razon_social AS nombre_proveedor,
                    DATEDIFF(NOW(), c.fecha_orden) AS dias_sin_pago
                FROM adv__compra c
                LEFT JOIN adv__proveedor prov ON c.id_proveedor = prov.id_proveedor
                LEFT JOIN adv__pago p ON c.id_compra = p.id_compra
                    AND p.estado_pago IN ('Confirmado', 'Pendiente')
                WHERE c.estado_compra IN ('Recibida_Completa', 'Facturada')
                  AND c.activo = 1
                  AND p.id_pago IS NULL
                ORDER BY c.fecha_orden ASC
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearCompraSinPago)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (CompraSinPago, List<IEntidadBaseDatos>) MapearCompraSinPago(MySqlDataReader r) {
            return (new CompraSinPago {
                IdCompra = Convert.ToInt64(r["id_compra"]),
                CodigoCompra = r["codigo_compra"]?.ToString() ?? string.Empty,
                FechaOrden = Convert.ToDateTime(r["fecha_orden"]),
                TotalCompra = Convert.ToDecimal(r["total_compra"], CultureInfo.InvariantCulture),
                CodigoProveedor = r["codigo_proveedor"]?.ToString() ?? string.Empty,
                NombreProveedor = r["nombre_proveedor"]?.ToString() ?? string.Empty,
                DiasSinPago = Convert.ToInt32(r["dias_sin_pago"]),
                EstadoCompra = r["estado_compra"]?.ToString() ?? string.Empty
            }, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Compras con al menos 1 pago confirmado pero con saldo pendiente > 0.
        /// Análogo a RepoVenta.ObtenerVentasConPagosParciales().
        /// </summary>
        public List<CompraPagoParcial> ObtenerComprasConPagosParciales() {
            const string consulta = """
                SELECT 
                    c.id_compra,
                    c.codigo AS codigo_compra,
                    c.fecha_orden,
                    c.total_compra,
                    SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END) AS total_pagado,
                    c.total_compra - SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END) AS saldo_pendiente,
                    COUNT(p.id_pago) AS cantidad_pagos,
                    prov.codigo_proveedor,
                    prov.razon_social AS nombre_proveedor
                FROM adv__compra c
                LEFT JOIN adv__proveedor prov ON c.id_proveedor = prov.id_proveedor
                INNER JOIN adv__pago p ON c.id_compra = p.id_compra
                WHERE c.estado_compra IN ('Recibida_Completa', 'Facturada')
                  AND c.activo = 1
                  AND p.estado_pago = 'Confirmado'
                GROUP BY c.id_compra, c.codigo, c.fecha_orden, c.total_compra,
                         prov.codigo_proveedor, prov.razon_social
                HAVING total_pagado < c.total_compra AND total_pagado > 0
                ORDER BY c.fecha_orden ASC
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearCompraPagoParcial)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (CompraPagoParcial, List<IEntidadBaseDatos>) MapearCompraPagoParcial(MySqlDataReader r) {
            return (new CompraPagoParcial {
                IdCompra = Convert.ToInt64(r["id_compra"]),
                CodigoCompra = r["codigo_compra"]?.ToString() ?? string.Empty,
                FechaOrden = Convert.ToDateTime(r["fecha_orden"]),
                TotalCompra = Convert.ToDecimal(r["total_compra"], CultureInfo.InvariantCulture),
                TotalPagado = Convert.ToDecimal(r["total_pagado"], CultureInfo.InvariantCulture),
                SaldoPendiente = Convert.ToDecimal(r["saldo_pendiente"], CultureInfo.InvariantCulture),
                CantidadPagos = Convert.ToInt32(r["cantidad_pagos"]),
                CodigoProveedor = r["codigo_proveedor"]?.ToString() ?? string.Empty,
                NombreProveedor = r["nombre_proveedor"]?.ToString() ?? string.Empty
            }, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Resumen de cuenta corriente por proveedor:
        /// total comprado, total pagado y saldo pendiente consolidados.
        /// Útil para la vista de "Cuentas por pagar".
        /// </summary>
        public List<ResumenPagosProveedor> ObtenerResumenPagosPorProveedor() {
            const string consulta = """
                SELECT 
                    prov.id_proveedor,
                    prov.codigo_proveedor,
                    prov.razon_social AS nombre_proveedor,
                    COUNT(c.id_compra) AS total_compras,
                    COUNT(CASE WHEN c.total_compra > COALESCE(pagos.total_pagado, 0) THEN 1 END) AS compras_pendientes_pago,
                    COALESCE(SUM(c.total_compra), 0) AS monto_total_comprado,
                    COALESCE(SUM(pagos.total_pagado), 0) AS monto_total_pagado,
                    COALESCE(SUM(c.total_compra), 0) - COALESCE(SUM(pagos.total_pagado), 0) AS saldo_total_pendiente
                FROM adv__proveedor prov
                LEFT JOIN adv__compra c ON prov.id_proveedor = c.id_proveedor
                    AND c.estado_compra IN ('Recibida_Completa', 'Facturada')
                    AND c.activo = 1
                LEFT JOIN (
                    SELECT id_compra, SUM(monto_pagado) AS total_pagado
                    FROM adv__pago
                    WHERE estado_pago = 'Confirmado'
                    GROUP BY id_compra
                ) pagos ON c.id_compra = pagos.id_compra
                WHERE prov.activo = 1
                GROUP BY prov.id_proveedor, prov.codigo_proveedor, prov.razon_social
                HAVING saldo_total_pendiente > 0
                ORDER BY saldo_total_pendiente DESC
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearResumenPagosProveedor)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (ResumenPagosProveedor, List<IEntidadBaseDatos>) MapearResumenPagosProveedor(MySqlDataReader r) {
            return (new ResumenPagosProveedor {
                IdProveedor = Convert.ToInt64(r["id_proveedor"]),
                CodigoProveedor = r["codigo_proveedor"]?.ToString() ?? string.Empty,
                NombreProveedor = r["nombre_proveedor"]?.ToString() ?? string.Empty,
                TotalCompras = Convert.ToInt32(r["total_compras"]),
                ComprasPendientesPago = Convert.ToInt32(r["compras_pendientes_pago"]),
                MontoTotalComprado = Convert.ToDecimal(r["monto_total_comprado"], CultureInfo.InvariantCulture),
                MontoTotalPagado = Convert.ToDecimal(r["monto_total_pagado"], CultureInfo.InvariantCulture),
                SaldoTotalPendiente = Convert.ToDecimal(r["saldo_total_pendiente"], CultureInfo.InvariantCulture)
            }, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Pagos pendientes de un proveedor específico.
        /// Análogo a RepoPago.ObtenerPagosPendientesPorCliente().
        /// </summary>
        public List<Pago> ObtenerPagosPendientesPorProveedor(long idProveedor) {
            const string consulta = """
                SELECT p.*
                FROM adv__pago p
                INNER JOIN adv__compra c ON p.id_compra = c.id_compra
                WHERE c.id_proveedor = @id_proveedor
                  AND p.estado_pago  = 'Pendiente'
                ORDER BY p.fecha_pago DESC
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta,
                    new Dictionary<string, object> { { "@id_proveedor", idProveedor } },
                    MapearPago)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (Pago, List<IEntidadBaseDatos>) MapearPago(MySqlDataReader r) {
            return (new Pago {
                Id = Convert.ToInt64(r["id_pago"]),
                IdCompra = r["id_compra"] != DBNull.Value ? Convert.ToInt64(r["id_compra"]) : 0,
                IdVenta = r["id_venta"] != DBNull.Value ? Convert.ToInt64(r["id_venta"]) : 0,
                MetodoPago = Enum.Parse<MetodoPagoEnum>(Convert.ToString(r["metodo_pago"]) ?? "Efectivo"),
                MontoPagado = Convert.ToDecimal(r["monto_pagado"], CultureInfo.InvariantCulture),
                FechaPago = r["fecha_pago"] != DBNull.Value ? Convert.ToDateTime(r["fecha_pago"]) : null,
                FechaConfirmacionPago = r["fecha_confirmacion_pago"] != DBNull.Value ? Convert.ToDateTime(r["fecha_confirmacion_pago"]) : null,
                EstadoPago = Enum.Parse<EstadoPagoEnum>(Convert.ToString(r["estado_pago"]) ?? "Pendiente")
            }, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Resumen de recepciones de una compra: cuánto se pidió vs. cuánto llegó.
        /// Útil para el panel de seguimiento de una OC.
        /// </summary>
        public ResumenRecepcionCompra ObtenerResumenRecepcion(long idCompra) {
            const string consulta = """
                SELECT 
                    c.id_compra,
                    c.codigo AS codigo_compra,
                    COUNT(rc.id_recepcion_compra) AS total_recepciones,
                    MAX(rc.fecha_recepcion) AS ultima_recepcion,
                    COALESCE(SUM(dc.cantidad), 0) AS cantidad_total_pedida,
                    COALESCE(SUM(drc.cantidad_recibida), 0) AS cantidad_total_recibida,
                    COALESCE(SUM(dc.cantidad), 0) - COALESCE(SUM(drc.cantidad_recibida), 0) AS cantidad_pendiente
                FROM adv__compra c
                LEFT JOIN adv__detalle_compra_producto dc
                       ON c.id_compra = dc.id_compra
                LEFT JOIN adv__recepcion_compra rc
                       ON c.id_compra = rc.id_compra
                LEFT JOIN adv__detalle_recepcion_compra drc
                       ON rc.id_recepcion_compra = drc.id_recepcion_compra
                WHERE c.id_compra = @id_compra
                GROUP BY c.id_compra, c.codigo
                """;

            var resultado = ContextoBaseDatos.EjecutarConsulta(consulta,
                    new Dictionary<string, object> { { "@id_compra", idCompra } },
                    MapearResumenRecepcion)
                .FirstOrDefault().entidadBase;

            if (resultado == null)
                throw new Exception($"Compra {idCompra} no encontrada.");

            return resultado;
        }

        private (ResumenRecepcionCompra, List<IEntidadBaseDatos>) MapearResumenRecepcion(MySqlDataReader r) {
            return (new ResumenRecepcionCompra {
                IdCompra = Convert.ToInt64(r["id_compra"]),
                CodigoCompra = r["codigo_compra"]?.ToString() ?? string.Empty,
                TotalRecepciones = Convert.ToInt32(r["total_recepciones"]),
                UltimaRecepcion = r["ultima_recepcion"] != DBNull.Value ? Convert.ToDateTime(r["ultima_recepcion"]) : null,
                CantidadTotalPedida = Convert.ToDecimal(r["cantidad_total_pedida"], CultureInfo.InvariantCulture),
                CantidadTotalRecibida = Convert.ToDecimal(r["cantidad_total_recibida"], CultureInfo.InvariantCulture),
                CantidadPendiente = Convert.ToDecimal(r["cantidad_pendiente"], CultureInfo.InvariantCulture)
            }, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Todas las compras que aún esperan recepción total o parcial.
        /// Incluye cuántos días lleva abierta y si ya pasó la fecha esperada.
        /// </summary>
        public List<Modelos.Modulos.Compra.Compra> ObtenerComprasPendientesRecepcion() {
            return Buscar(FiltroBusquedaCompra.PendientesRecepcion)
                .resultadosBusqueda
                .Select(r => r.entidadBase)
                .ToList();
        }

        /// <summary>
        /// Total monetario de compras COMPLETADAS en un período.
        /// Análogo a RepoVenta.ObtenerTotalVentasPorPeriodo().
        /// </summary>
        public decimal ObtenerTotalComprasPorPeriodo(DateTime fechaInicio, DateTime fechaFin) {
            const string consulta = """
                SELECT COALESCE(SUM(total_compra), 0)
                FROM adv__compra
                WHERE fecha_orden   >= @fecha_inicio
                  AND fecha_orden   <= @fecha_fin
                  AND estado_compra IN ('Recibida_Completa', 'Facturada')
                  AND activo = 1
                """;

            return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta,
                new Dictionary<string, object> {
                    { "@fecha_inicio", fechaInicio.ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_fin",    fechaFin.ToString("yyyy-MM-dd 23:59:59") }
                });
        }

        /// <summary>
        /// Historial completo de compras a un proveedor, con filtro de fechas opcional.
        /// Análogo a RepoVenta.ObtenerVentasPorCliente().
        /// </summary>
        public List<Modelos.Modulos.Compra.Compra> ObtenerComprasPorProveedor(
            long idProveedor,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null) {

            var sql = """
                SELECT c.*, 
                       prov.razon_social AS nombre_proveedor,
                       prov.codigo_proveedor,
                       al.nombre AS nombre_almacen,
                       tc.nombre AS nombre_tipo_compra
                FROM adv__compra c
                LEFT JOIN adv__proveedor prov ON c.id_proveedor = prov.id_proveedor
                LEFT JOIN adv__almacen   al   ON c.id_almacen_destino = al.id_almacen
                LEFT JOIN adv__tipo_compra tc  ON c.id_tipo_compra = tc.id_tipo_compra
                WHERE c.id_proveedor = @id_proveedor
                  AND c.activo = 1
                """;

            var parametros = new Dictionary<string, object> { { "@id_proveedor", idProveedor } };

            if (fechaInicio.HasValue) {
                sql += " AND c.fecha_orden >= @fecha_inicio";
                parametros.Add("@fecha_inicio", fechaInicio.Value.ToString("yyyy-MM-dd 00:00:00"));
            }

            if (fechaFin.HasValue) {
                sql += " AND c.fecha_orden <= @fecha_fin";
                parametros.Add("@fecha_fin", fechaFin.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            sql += " ORDER BY c.fecha_orden DESC";

            return ContextoBaseDatos.EjecutarConsulta(sql, parametros, MapearEntidad)
                .Select(r => r.entidadBase)
                .ToList();
        }

        #endregion
    }
}