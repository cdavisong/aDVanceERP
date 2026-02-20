using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Venta;
using aDVanceERP.Core.Repositorios.BD;
using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Venta {
    public class RepoVenta : RepoEntidadBaseDatos<Modelos.Modulos.Venta.Venta, FiltroBusquedaVenta> {
        public RepoVenta() : base("adv__venta", "id_venta") {
        }

        protected override string GenerarComandoAdicionar(Modelos.Modulos.Venta.Venta entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__venta (
                    id_pedido,
                    id_cliente,
                    id_empleado_vendedor,
                    id_almacen,
                    numero_factura_ticket,
                    fecha_venta,
                    total_bruto,
                    descuento_total,
                    impuesto_total,
                    importe_total,
                    metodo_pago_principal,
                    estado_venta,
                    observaciones_venta,
                    activo
                ) VALUES (
                    @id_pedido,
                    @id_cliente,
                    @id_empleado_vendedor,
                    @id_almacen,
                    @numero_factura_ticket,
                    @fecha_venta,
                    @total_bruto,
                    @descuento_total,
                    @impuesto_total,
                    @importe_total,
                    @metodo_pago_principal,
                    @estado_venta,
                    @observaciones_venta,
                    @activo
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_pedido", entidad.IdPedido.HasValue ? entidad.IdPedido.Value : DBNull.Value },
                { "@id_cliente", entidad.IdCliente },
                { "@id_empleado_vendedor", entidad.IdEmpleadoVendedor.HasValue ? entidad.IdEmpleadoVendedor.Value : DBNull.Value },
                { "@id_almacen", entidad.IdAlmacen },
                { "@numero_factura_ticket", entidad.NumeroFacturaTicket },
                { "@fecha_venta", entidad.FechaVenta.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@total_bruto", entidad.TotalBruto },
                { "@descuento_total", entidad.DescuentoTotal },
                { "@impuesto_total", entidad.ImpuestoTotal },
                { "@importe_total", entidad.ImporteTotal },
                { "@metodo_pago_principal", entidad.MetodoPagoPrincipal },
                { "@estado_venta", entidad.EstadoVenta.ToString() },
                { "@observaciones_venta", entidad.ObservacionesVenta },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(Modelos.Modulos.Venta.Venta entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__venta 
                SET 
                    id_pedido = @id_pedido,
                    id_cliente = @id_cliente,
                    id_empleado_vendedor = @id_empleado_vendedor,
                    id_almacen = @id_almacen,
                    numero_factura_ticket = @numero_factura_ticket,
                    fecha_venta = @fecha_venta,
                    total_bruto = @total_bruto,
                    descuento_total = @descuento_total,
                    impuesto_total = @impuesto_total,
                    importe_total = @importe_total,
                    metodo_pago_principal = @metodo_pago_principal,
                    estado_venta = @estado_venta,
                    observaciones_venta = @observaciones_venta,
                    activo = @activo
                WHERE id_venta = @id_venta
                """;

            parametros = new Dictionary<string, object> {
                { "@id_venta", entidad.Id },
                { "@id_pedido", entidad.IdPedido.HasValue ? entidad.IdPedido.Value : DBNull.Value },
                { "@id_cliente", entidad.IdCliente },
                { "@id_empleado_vendedor", entidad.IdEmpleadoVendedor.HasValue ? entidad.IdEmpleadoVendedor.Value : DBNull.Value },
                { "@id_almacen", entidad.IdAlmacen },
                { "@numero_factura_ticket", entidad.NumeroFacturaTicket },
                { "@fecha_venta", entidad.FechaVenta.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@total_bruto", entidad.TotalBruto },
                { "@descuento_total", entidad.DescuentoTotal },
                { "@impuesto_total", entidad.ImpuestoTotal },
                { "@importe_total", entidad.ImporteTotal },
                { "@metodo_pago_principal", entidad.MetodoPagoPrincipal },
                { "@estado_venta", entidad.EstadoVenta.ToString() },
                { "@observaciones_venta", entidad.ObservacionesVenta },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__venta 
                WHERE id_venta = @id_venta
                """;

            parametros = new Dictionary<string, object> {
                { "@id_venta", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaVenta filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var fechaDesde = criteriosBusqueda.Length == 3 ? criteriosBusqueda[0] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var fechaHasta = criteriosBusqueda.Length == 3 ? criteriosBusqueda[1] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var criterio = criteriosBusqueda.Length == 3 ? criteriosBusqueda[2] : criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

            var consultaComun = $"""
                SELECT v.*, c.nombre_completo as nombre_cliente 
                FROM adv__venta v 
                LEFT JOIN adv__cliente cl ON v.id_cliente = cl.id_cliente 
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona 
                WHERE v.activo = @activo 
                { (criteriosBusqueda.Length == 3 ? "AND v.fecha_venta >= @fecha_desde AND v.fecha_venta <= @fecha_hasta" : string.Empty) } 
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaVenta.Id => $"""
                    {consultaComun}
                    AND v.id_venta = @id_venta
                    """,
                FiltroBusquedaVenta.IdCliente => $"""
                    {consultaComun}
                    AND v.id_cliente = @id_cliente
                    """,
                FiltroBusquedaVenta.NumeroFactura => $"""
                    {consultaComun}
                    AND v.numero_factura_ticket = @numero_factura
                    """,
                FiltroBusquedaVenta.Estado => $"""
                    {consultaComun}
                    AND v.estado_venta = @estado_venta
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaVenta.Id => new Dictionary<string, object> {
                    { "@id_venta", long.Parse(criterio) },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaVenta.IdCliente => new Dictionary<string, object> {
                    { "@id_cliente", long.Parse(criterio) },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaVenta.NumeroFactura => new Dictionary<string, object> {
                    { "@numero_factura", criterio },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaVenta.Estado => new Dictionary<string, object> {
                    { "@estado_venta", criterio },
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                _ => new Dictionary<string, object> {
                    { "@activo", !filtroBusqueda.ToString().Equals("Inactivos", StringComparison.OrdinalIgnoreCase) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                }
            };

            return consulta;
        }

        protected override (Modelos.Modulos.Venta.Venta, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var venta = new Modelos.Modulos.Venta.Venta {
                Id = Convert.ToInt64(lector["id_venta"]),
                IdPedido = lector["id_pedido"] != DBNull.Value ? Convert.ToInt64(lector["id_pedido"]) : null,
                IdCliente = Convert.ToInt64(lector["id_cliente"]),
                IdEmpleadoVendedor = lector["id_empleado_vendedor"] != DBNull.Value ? Convert.ToInt64(lector["id_empleado_vendedor"]) : null,
                IdAlmacen = Convert.ToInt64(lector["id_almacen"]),
                NumeroFacturaTicket = lector["numero_factura_ticket"] != DBNull.Value ? Convert.ToString(lector["numero_factura_ticket"]) : null,
                FechaVenta = Convert.ToDateTime(lector["fecha_venta"]),
                TotalBruto = Convert.ToDecimal(lector["total_bruto"], CultureInfo.InvariantCulture),
                DescuentoTotal = Convert.ToDecimal(lector["descuento_total"], CultureInfo.InvariantCulture),
                ImpuestoTotal = Convert.ToDecimal(lector["impuesto_total"], CultureInfo.InvariantCulture),
                ImporteTotal = Convert.ToDecimal(lector["importe_total"], CultureInfo.InvariantCulture),
                MetodoPagoPrincipal = lector["metodo_pago_principal"] != DBNull.Value ? Convert.ToString(lector["metodo_pago_principal"]) : null,
                EstadoVenta = Enum.Parse<EstadoVentaEnum>(Convert.ToString(lector["estado_venta"]) ?? "Pendiente"),
                ObservacionesVenta = lector["observaciones_venta"] != DBNull.Value ? Convert.ToString(lector["observaciones_venta"]) : null,
                Activo = Convert.ToBoolean(lector["activo"])
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            // Solo obtenemos el nombre del cliente de la BD
            if (lector.VisibleFieldCount > 15) {
                entidadesExtra.Add(new Persona {
                    NombreCompleto = Convert.ToString(lector["nombre_cliente"]) ?? "N/A"
                });
            }

            return (venta, entidadesExtra);
        }

        #region STATIC

        public static RepoVenta Instancia { get; } = new RepoVenta();

        #endregion

        #region UTILES

        public bool HabilitarDeshabilitarVenta(long id) {
            var consulta = $"""
                UPDATE adv__venta
                SET activo = NOT activo
                WHERE id_venta = @IdVenta;
                """;
            var parametros = new Dictionary<string, object> {
                { "@IdVenta", id }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);

            consulta = $"""
                SELECT activo
                FROM adv__venta
                WHERE id_venta = @IdVenta;
                """;

            return ContextoBaseDatos.EjecutarConsultaEscalar<bool>(consulta, parametros);
        }

        public bool CambiarEstadoVenta(long idVenta, EstadoVentaEnum nuevoEstado) {
            var consulta = $"""
                UPDATE adv__venta
                SET estado_venta = @nuevo_estado
                WHERE id_venta = @id_venta;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta },
                { "@nuevo_estado", nuevoEstado.ToString() }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public List<DetalleVentaProducto> ObtenerDetallesVenta(long idVenta) {
            var consulta = $"""
                SELECT * FROM adv__detalle_venta_producto
                WHERE id_venta = @id_venta
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var detalles = new List<DetalleVentaProducto>();
            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                MapearEntidadDetalleVentaProducto
            );

            foreach (var (detalle, _) in resultados) {
                detalles.Add(detalle);
            }

            return detalles;
        }

        private (DetalleVentaProducto, List<IEntidadBaseDatos>) MapearEntidadDetalleVentaProducto(MySqlDataReader lector) {
            var detalle = new DetalleVentaProducto {
                Id = Convert.ToInt64(lector["id_detalle_venta_producto"]),
                IdVenta = Convert.ToInt64(lector["id_venta"]),
                IdProducto = Convert.ToInt64(lector["id_producto"]),
                Cantidad = Convert.ToDecimal(lector["cantidad"], CultureInfo.InvariantCulture),
                PrecioCompraVigente = Convert.ToDecimal(lector["precio_compra_vigente"], CultureInfo.InvariantCulture),
                PrecioVentaUnitario = Convert.ToDecimal(lector["precio_venta_unitario"], CultureInfo.InvariantCulture),
                DescuentoItem = Convert.ToDecimal(lector["descuento_item"], CultureInfo.InvariantCulture),
                Subtotal = Convert.ToDecimal(lector["subtotal"], CultureInfo.InvariantCulture)
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            return (detalle, entidadesExtra);
        }

        public decimal ObtenerTotalVentasPorPeriodo(DateTime fechaInicio, DateTime fechaFin) {
            var consulta = $"""
                SELECT COALESCE(SUM(importe_total), 0) as total_ventas
                FROM adv__venta
                WHERE fecha_venta >= @fecha_inicio
                AND fecha_venta <= @fecha_fin
                AND estado_venta IN ('Completada', 'Entregada')
                AND activo = 1;
                """;

            var parametros = new Dictionary<string, object> {
                { "@fecha_inicio", fechaInicio.ToString("yyyy-MM-dd 00:00:00") },
                { "@fecha_fin", fechaFin.ToString("yyyy-MM-dd 23:59:59") }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
            return resultado;
        }

        public List<Modelos.Modulos.Venta.Venta> ObtenerVentasPorCliente(long idCliente, DateTime? fechaInicio = null, DateTime? fechaFin = null) {
            var consulta = $"""
                SELECT v.*, c.nombre_completo as nombre_cliente
                FROM adv__venta v
                LEFT JOIN adv__cliente cl ON v.id_cliente = cl.id_cliente
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona
                WHERE v.id_cliente = @id_cliente
                AND v.activo = 1
                """;

            var parametros = new Dictionary<string, object> {
                 { "@id_cliente", idCliente }
            };

            if (fechaInicio.HasValue) {
                consulta += " AND v.fecha_venta >= @fecha_inicio";
                parametros.Add("@fecha_inicio", fechaInicio.Value.ToString("yyyy-MM-dd 00:00:00"));
            }

            if (fechaFin.HasValue) {
                consulta += " AND v.fecha_venta <= @fecha_fin";
                parametros.Add("@fecha_fin", fechaFin.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            consulta += " ORDER BY v.fecha_venta DESC";

            var ventas = new List<Modelos.Modulos.Venta.Venta>();
            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                (reader) => {
                    var (venta, entidadesExtra) = MapearEntidad(reader);
                    return (venta, entidadesExtra);
                }
            );

            foreach (var (venta, _) in resultados) {
                ventas.Add(venta);
            }

            return ventas;
        }

        public bool VentaEstaPagadaCompletamente(long idVenta) {
            var consulta = $"""
                SELECT 
                    CASE 
                        WHEN v.importe_total <= COALESCE(SUM(p.monto_pagado), 0) 
                            AND COUNT(CASE WHEN p.estado_pago = 'Pendiente' THEN 1 END) = 0
                        THEN 1 
                        ELSE 0 
                    END as esta_pagada
                FROM adv__venta v
                LEFT JOIN adv__pago p ON v.id_venta = p.id_venta 
                    AND p.estado_pago IN ('Confirmado', 'Pendiente')
                WHERE v.id_venta = @id_venta
                GROUP BY v.id_venta, v.importe_total;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);

            return resultado == 1;
        }

        public EstadoPagoVenta VerificarEstadoPagoVenta(long idVenta) {
            var consulta = $"""
                SELECT 
                    v.importe_total,
                    COALESCE(SUM(p.monto_pagado), 0) as total_pagado,
                    v.importe_total - COALESCE(SUM(p.monto_pagado), 0) as saldo,
                    COUNT(CASE WHEN p.estado_pago = 'Pendiente' THEN 1 END) as pagos_pendientes
                FROM adv__venta v
                LEFT JOIN adv__pago p ON v.id_venta = p.id_venta 
                    AND p.estado_pago IN ('Confirmado', 'Pendiente')
                WHERE v.id_venta = @id_venta
                GROUP BY v.id_venta, v.importe_total;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var resultado = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidadEstadoPagoVenta).FirstOrDefault().entidadBase;

            if (resultado == null)
                throw new Exception("Venta no encontrada");

            return resultado;
        }

        private (EstadoPagoVenta, List<IEntidadBaseDatos>) MapearEntidadEstadoPagoVenta(MySqlDataReader reader) {
            var estadoPagoVenta = new EstadoPagoVenta {
                ImporteTotal = Convert.ToDecimal(reader["importe_total"], CultureInfo.InvariantCulture),
                TotalPagado = Convert.ToDecimal(reader["total_pagado"], CultureInfo.InvariantCulture),
                Saldo = Convert.ToDecimal(reader["saldo"], CultureInfo.InvariantCulture),
                EstaPagadaCompletamente = Convert.ToDecimal(reader["importe_total"], CultureInfo.InvariantCulture) <= Convert.ToDecimal(reader["total_pagado"], CultureInfo.InvariantCulture) && Convert.ToInt32(reader["pagos_pendientes"]) == 0,
                TienePagosPendientes = Convert.ToInt32(reader["pagos_pendientes"]) > 0,
                TieneSobrepago = Convert.ToDecimal(reader["total_pagado"], CultureInfo.InvariantCulture) > Convert.ToDecimal(reader["importe_total"], CultureInfo.InvariantCulture)
            };

            return (estadoPagoVenta, new List<IEntidadBaseDatos>());
        }

        public List<VentaPendientePago> ObtenerVentasPendientesDePago() {
            var consulta = $"""
                SELECT 
                    v.id_venta,
                    v.numero_factura_ticket,
                    v.fecha_venta,
                    v.importe_total,
                    COALESCE(SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END), 0) as total_pagado,
                    v.importe_total - COALESCE(SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END), 0) as saldo_pendiente,
                    COUNT(CASE WHEN p.estado_pago = 'Pendiente' THEN 1 END) as pagos_pendientes_confirmacion,
                    c.codigo_cliente,
                    per.nombre_completo as nombre_cliente
                FROM adv__venta v
                LEFT JOIN adv__cliente c ON v.id_cliente = c.id_cliente
                LEFT JOIN adv__persona per ON c.id_persona = per.id_persona
                LEFT JOIN adv__pago p ON v.id_venta = p.id_venta 
                    AND p.estado_pago IN ('Confirmado', 'Pendiente')
                WHERE v.estado_venta NOT IN ('Anulada')
                    AND v.activo = 1
                GROUP BY v.id_venta, v.numero_factura_ticket, v.fecha_venta, v.importe_total,
                         c.codigo_cliente, per.nombre_completo
                HAVING saldo_pendiente > 0
                ORDER BY v.fecha_venta ASC;
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearEntidadVentaPendientePago)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (VentaPendientePago, List<IEntidadBaseDatos>) MapearEntidadVentaPendientePago(MySqlDataReader reader) {
            var ventaPendiente = new VentaPendientePago {
                IdVenta = Convert.ToInt64(reader["id_venta"]),
                NumeroFacturaTicket = reader["numero_factura_ticket"]?.ToString() ?? string.Empty,
                FechaVenta = Convert.ToDateTime(reader["fecha_venta"]),
                ImporteTotal = Convert.ToDecimal(reader["importe_total"], CultureInfo.InvariantCulture),
                TotalPagado = Convert.ToDecimal(reader["total_pagado"], CultureInfo.InvariantCulture),
                SaldoPendiente = Convert.ToDecimal(reader["saldo_pendiente"], CultureInfo.InvariantCulture),
                PagosPendientesConfirmacion = Convert.ToInt32(reader["pagos_pendientes_confirmacion"]),
                CodigoCliente = reader["codigo_cliente"]?.ToString() ?? string.Empty,
                NombreCliente = reader["nombre_cliente"]?.ToString() ?? string.Empty
            };

            return (ventaPendiente, new List<IEntidadBaseDatos>());
        }

        public List<VentaSinPago> ObtenerVentasSinPagos() {
            var consulta = $"""
                SELECT 
                    v.id_venta,
                    v.numero_factura_ticket,
                    v.fecha_venta,
                    v.importe_total,
                    c.codigo_cliente,
                    per.nombre_completo as nombre_cliente as nombre_cliente,
                    DATEDIFF(NOW(), v.fecha_venta) as dias_sin_pago
                FROM adv__venta v
                LEFT JOIN adv__cliente c ON v.id_cliente = c.id_cliente
                LEFT JOIN adv__persona per ON c.id_persona = per.id_persona
                LEFT JOIN adv__pago p ON v.id_venta = p.id_venta
                WHERE v.estado_venta NOT IN ('Anulada')
                    AND v.activo = 1
                    AND p.id_pago IS NULL
                ORDER BY v.fecha_venta ASC;
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearEntidadVentaSinPago)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (VentaSinPago, List<IEntidadBaseDatos>) MapearEntidadVentaSinPago(MySqlDataReader reader) {
            var ventaSinPago = new VentaSinPago {
                IdVenta = Convert.ToInt64(reader["id_venta"]),
                NumeroFacturaTicket = reader["numero_factura_ticket"]?.ToString() ?? string.Empty,
                FechaVenta = Convert.ToDateTime(reader["fecha_venta"]),
                ImporteTotal = Convert.ToDecimal(reader["importe_total"], CultureInfo.InvariantCulture),
                CodigoCliente = reader["codigo_cliente"]?.ToString() ?? string.Empty,
                NombreCliente = reader["nombre_cliente"]?.ToString() ?? string.Empty,
                DiasSinPago = Convert.ToInt32(reader["dias_sin_pago"])
            };

            return (ventaSinPago, new List<IEntidadBaseDatos>());
        }

        public List<VentaPagoParcial> ObtenerVentasConPagosParciales() {
            var consulta = $"""
                SELECT 
                    v.id_venta,
                    v.numero_factura_ticket,
                    v.fecha_venta,
                    v.importe_total,
                    SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END) as total_pagado,
                    v.importe_total - SUM(CASE WHEN p.estado_pago = 'Confirmado' THEN p.monto_pagado ELSE 0 END) as saldo_pendiente,
                    COUNT(p.id_pago) as cantidad_pagos,
                    c.codigo_cliente,
                    per.nombre_completo as nombre_cliente
                FROM adv__venta v
                LEFT JOIN adv__cliente c ON v.id_cliente = c.id_cliente
                LEFT JOIN adv__persona per ON c.id_persona = per.id_persona
                INNER JOIN adv__pago p ON v.id_venta = p.id_venta
                WHERE v.estado_venta NOT IN ('Anulada')
                    AND v.activo = 1
                    AND p.estado_pago = 'Confirmado'
                GROUP BY v.id_venta, v.numero_factura_ticket, v.fecha_venta, v.importe_total,
                         c.codigo_cliente, per.nombre_completo
                HAVING total_pagado < v.importe_total AND total_pagado > 0
                ORDER BY v.fecha_venta ASC;
                """;

            return ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearEntidadVentaPagoParcial)
                .Select(r => r.entidadBase)
                .ToList();
        }

        private (VentaPagoParcial, List<IEntidadBaseDatos>) MapearEntidadVentaPagoParcial(MySqlDataReader reader) {
            var ventaPagoParcial = new VentaPagoParcial {
                IdVenta = Convert.ToInt64(reader["id_venta"]),
                NumeroFacturaTicket = reader["numero_factura_ticket"]?.ToString() ?? string.Empty,
                FechaVenta = Convert.ToDateTime(reader["fecha_venta"]),
                ImporteTotal = Convert.ToDecimal(reader["importe_total"], CultureInfo.InvariantCulture),
                TotalPagado = Convert.ToDecimal(reader["total_pagado"], CultureInfo.InvariantCulture),
                SaldoPendiente = Convert.ToDecimal(reader["saldo_pendiente"], CultureInfo.InvariantCulture),
                CantidadPagos = Convert.ToInt32(reader["cantidad_pagos"]),
                CodigoCliente = reader["codigo_cliente"]?.ToString() ?? string.Empty,
                NombreCliente = reader["nombre_cliente"]?.ToString() ?? string.Empty
            };

            return (ventaPagoParcial, new List<IEntidadBaseDatos>());
        }

        /// <summary>
        /// Determina el método de pago principal de una venta basándose en el monto total pagado con cada método.
        /// Si la venta tiene múltiples pagos, retorna el método con mayor monto acumulado.
        /// </summary>
        /// <param name="idVenta">ID de la venta</param>
        /// <returns>El método de pago principal, o null si no hay pagos confirmados</returns>
        public MetodoPagoEnum? DeterminarMetodoPagoPrincipal(long idVenta) {
            var consulta = $"""
                SELECT 
                    metodo_pago,
                    SUM(monto_pagado) as total_por_metodo
                FROM adv__pago
                WHERE id_venta = @id_venta
                AND estado_pago = 'Confirmado'
                GROUP BY metodo_pago
                ORDER BY total_por_metodo DESC
                LIMIT 1;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            try {
                var resultado = ContextoBaseDatos.EjecutarConsulta(
                    consulta,
                    parametros,
                    (reader) => {
                        var metodoPago = Convert.ToString(reader["metodo_pago"]);

                        return (metodoPago, new List<IEntidadBaseDatos>());
                    }
                );

                if (resultado.Any()) {
                    var metodoPagoStr = resultado.First().entidadBase as string;
                    
                    if (!string.IsNullOrEmpty(metodoPagoStr))
                        return Enum.Parse<MetodoPagoEnum>(metodoPagoStr);
                }
            } catch (Exception) {
                // Si hay error en la consulta, retornar null
                return null;
            }

            return null;
        }

        /// <summary>
        /// Actualiza el método de pago principal de una venta basándose en los pagos confirmados existentes.
        /// </summary>
        /// <param name="idVenta">ID de la venta</param>
        /// <returns>True si se actualizó correctamente, False si no se pudo actualizar o no hay pagos</returns>
        public bool ActualizarMetodoPagoPrincipal(long idVenta) {
            var metodoPagoPrincipal = DeterminarMetodoPagoPrincipal(idVenta);

            var consulta = $"""
                UPDATE adv__venta 
                SET metodo_pago_principal = @metodo_pago_principal
                WHERE id_venta = @id_venta
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta },
                { "@metodo_pago_principal", !metodoPagoPrincipal.HasValue ? string.Empty : metodoPagoPrincipal.Value.ToString() }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        #endregion
    }
}