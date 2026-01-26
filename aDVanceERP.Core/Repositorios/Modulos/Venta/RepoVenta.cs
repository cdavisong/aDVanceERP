using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Ventas;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Ventas {
    public class RepoVenta : RepoEntidadBaseDatos<Venta, FiltroBusquedaVenta> {
        public RepoVenta() : base("adv__venta", "id_venta") {
        }

        protected override string GenerarComandoAdicionar(Venta entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
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
                { "@total_bruto", entidad.TotalBruto.ToString(CultureInfo.InvariantCulture) },
                { "@descuento_total", entidad.DescuentoTotal.ToString(CultureInfo.InvariantCulture) },
                { "@impuesto_total", entidad.ImpuestoTotal.ToString(CultureInfo.InvariantCulture) },
                { "@importe_total", entidad.ImporteTotal.ToString(CultureInfo.InvariantCulture) },
                { "@metodo_pago_principal", entidad.MetodoPagoPrincipal },
                { "@estado_venta", entidad.EstadoVenta.ToString() },
                { "@observaciones_venta", entidad.ObservacionesVenta },
                { "@activo", entidad.Activo }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(Venta entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
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
                { "@total_bruto", entidad.TotalBruto.ToString(CultureInfo.InvariantCulture) },
                { "@descuento_total", entidad.DescuentoTotal.ToString(CultureInfo.InvariantCulture) },
                { "@impuesto_total", entidad.ImpuestoTotal.ToString(CultureInfo.InvariantCulture) },
                { "@importe_total", entidad.ImporteTotal.ToString(CultureInfo.InvariantCulture) },
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
            var fechaDesde = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var fechaHasta = criteriosBusqueda.Length > 0 ? criteriosBusqueda[1] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[2] : string.Empty;

            var consultaComun = $"""
                SELECT v.*, c.nombre_completo as nombre_cliente 
                FROM adv__venta v 
                LEFT JOIN adv__cliente cl ON v.id_cliente = cl.id_cliente 
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona 
                WHERE v.fecha_venta >= @fecha_desde AND v.fecha_venta <= @fecha_hasta 
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
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaVenta.IdCliente => new Dictionary<string, object> {
                    { "@id_cliente", long.Parse(criterio) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaVenta.NumeroFactura => new Dictionary<string, object> {
                    { "@numero_factura", criterio },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaVenta.Estado => new Dictionary<string, object> {
                    { "@estado_venta", criterio },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                _ => new Dictionary<string, object> {
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                }
            };

            return consulta;
        }

        protected override (Venta, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var venta = new Venta {
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
                EstadoVenta = Enum.Parse<EstadoVenta>(Convert.ToString(lector["estado_venta"]) ?? "Pendiente"),
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

        public bool CambiarEstadoVenta(long idVenta, EstadoVenta nuevoEstado) {
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

        public bool AnularVenta(long idVenta, string motivo) {
            var consulta = $"""
                UPDATE adv__venta
                SET estado_venta = 'Anulada',
                    observaciones_venta = CONCAT(COALESCE(observaciones_venta, ''), '\\nAnulada: ', @motivo)
                WHERE id_venta = @id_venta
                AND estado_venta IN ('Pendiente', 'Completada');
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta },
                { "@motivo", motivo }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
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

        public List<Venta> ObtenerVentasPorCliente(long idCliente, DateTime? fechaInicio = null, DateTime? fechaFin = null) {
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

            var ventas = new List<Venta>();
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

        public string GenerarNumeroFactura() {
            var consulta = $"""
                SELECT CONCAT('FAC-', YEAR(NOW()), '-', LPAD(COALESCE(MAX(SUBSTRING_INDEX(SUBSTRING_INDEX(numero_factura_ticket, '-', -1), '-', 1)), 0) + 1, 6, '0')) as nuevo_numero
                FROM adv__venta
                WHERE numero_factura_ticket LIKE CONCAT('FAC-', YEAR(NOW()), '-%')
                AND numero_factura_ticket REGEXP '^FAC-[0-9]{4}-[0-9]{6}$';
                """;

            var parametros = new Dictionary<string, object>();

            var numero = ContextoBaseDatos.EjecutarConsultaEscalar<string>(consulta, parametros);

            if (string.IsNullOrEmpty(numero)) {
                numero = $"FAC-{DateTime.Now.Year}-000001";
            }

            return numero;
        }

        public List<Venta> ObtenerVentasPendientesPago() {
            var consulta = $"""
                SELECT v.*, c.nombre_completo as nombre_cliente,
                       (SELECT COALESCE(SUM(monto_pagado), 0) 
                        FROM adv__pago p 
                        WHERE p.id_venta = v.id_venta 
                        AND p.estado_pago = 'Confirmado') as total_pagado
                FROM adv__venta v
                LEFT JOIN adv__cliente cl ON v.id_cliente = cl.id_cliente
                LEFT JOIN adv__persona c ON cl.id_persona = c.id_persona
                WHERE v.estado_venta IN ('Completada', 'Entregada')
                AND v.activo = 1
                AND v.importe_total > (
                    SELECT COALESCE(SUM(monto_pagado), 0) 
                    FROM adv__pago p 
                    WHERE p.id_venta = v.id_venta 
                    AND p.estado_pago = 'Confirmado'
                )
                ORDER BY v.fecha_venta DESC;
                """;

            var parametros = new Dictionary<string, object>();

            var ventas = new List<Venta>();
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

        #endregion
    }
}