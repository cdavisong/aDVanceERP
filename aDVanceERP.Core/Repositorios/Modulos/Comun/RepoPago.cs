using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Comun;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Comun {
    public class RepoPago : RepoEntidadBaseDatos<Pago, FiltroBusquedaPago> {
        public RepoPago() : base("adv__pago", "id_pago") {
        }

        protected override string GenerarComandoAdicionar(Pago entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                INSERT INTO adv__pago (
                    id_compra,
                    id_venta,
                    metodo_pago,
                    monto_pagado,
                    fecha_pago,
                    fecha_confirmacion_pago,
                    estado_pago
                ) VALUES (
                    @id_compra,
                    @id_venta,
                    @metodo_pago,
                    @monto_pagado,
                    @fecha_pago,
                    @fecha_confirmacion_pago,
                    @estado_pago
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@id_compra", entidad.IdCompra },
                { "@id_venta", entidad.IdVenta },
                { "@metodo_pago", entidad.MetodoPago.ToString() },
                { "@monto_pagado", entidad.MontoPagado },
                { "@fecha_pago", entidad.FechaPago.HasValue ? entidad.FechaPago.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@fecha_confirmacion_pago", entidad.FechaConfirmacionPago.HasValue ? entidad.FechaConfirmacionPago.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@estado_pago", entidad.EstadoPago.ToString() }
            };

            return comando;
        }

        protected override string GenerarComandoEditar(Pago entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var comando = $"""
                UPDATE adv__pago 
                SET 
                    id_compra = @id_compra,
                    id_venta = @id_venta,
                    metodo_pago = @metodo_pago,
                    monto_pagado = @monto_pagado,
                    fecha_pago = @fecha_pago,
                    fecha_confirmacion_pago = @fecha_confirmacion_pago,
                    estado_pago = @estado_pago
                WHERE id_pago = @id_pago
                """;

            parametros = new Dictionary<string, object> {
                { "@id_pago", entidad.Id },
                { "@id_compra", entidad.IdCompra },
                { "@id_venta", entidad.IdVenta },
                { "@metodo_pago", entidad.MetodoPago.ToString() },
                { "@monto_pagado", entidad.MontoPagado },
                { "@fecha_pago", entidad.FechaPago.HasValue ? entidad.FechaPago.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@fecha_confirmacion_pago", entidad.FechaConfirmacionPago.HasValue ? entidad.FechaConfirmacionPago.Value.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value },
                { "@estado_pago", entidad.EstadoPago.ToString() }
            };

            return comando;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var comando = $"""
                DELETE FROM adv__pago 
                WHERE id_pago = @id_pago
                """;

            parametros = new Dictionary<string, object> {
                { "@id_pago", id }
            };

            return comando;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaPago filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var fechaDesde = criteriosBusqueda.Length == 4 ? criteriosBusqueda[0] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var fechaHasta = criteriosBusqueda.Length == 4 ? criteriosBusqueda[1] : DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var criterio = criteriosBusqueda.Length == 4 ? criteriosBusqueda[2] : criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var pagoVenta = criteriosBusqueda.Length == 4 ? criteriosBusqueda[3].Equals("Venta") : criteriosBusqueda.Length > 0 ? criteriosBusqueda[1].Equals("Venta") : criteriosBusqueda[0].Equals("Venta");

            var consultaComun = $"""
                SELECT p.*, {(pagoVenta ? "v.numero_factura_ticket, v.importe_total" : "c.codigo, c.total_compra")} as total_compraventa
                FROM adv__pago p
                {(pagoVenta ? string.Empty : "LEFT JOIN adv__compra c ON p.id_compra = c.id_compra")}
                {(pagoVenta ? "LEFT JOIN adv__venta v ON p.id_venta = v.id_venta" : string.Empty)}
                {(criteriosBusqueda.Length == 4 && filtroBusqueda == FiltroBusquedaPago.Todos ? "WHERE p.fecha_pago >= @fecha_desde AND p.fecha_pago <= @fecha_hasta" : string.Empty)}
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaPago.Id => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 4 && filtroBusqueda == FiltroBusquedaPago.Todos ? "AND" : "WHERE")} p.id_pago = @id_pago
                    """,
                FiltroBusquedaPago.IdCompraVenta => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 4 && filtroBusqueda == FiltroBusquedaPago.Todos ? "AND" : "WHERE")} {(pagoVenta ? "p.id_venta = @id_compraventa" : "p.id_compra = @id_compraventa")}
                    """,
                FiltroBusquedaPago.Estado => $"""
                    {consultaComun}
                    {(criteriosBusqueda.Length == 4 && filtroBusqueda == FiltroBusquedaPago.Todos ? "AND" : "WHERE")} p.estado_pago = @estado_pago
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPago.Id => new Dictionary<string, object> {
                    { "@id_pago", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaPago.IdCompraVenta => new Dictionary<string, object> {
                    { "@id_compraventa", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                FiltroBusquedaPago.Estado => new Dictionary<string, object> {
                    { "@estado_pago", criterio },
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                },
                _ => new Dictionary<string, object>() {
                    { "@fecha_desde", DateTime.Parse(fechaDesde).ToString("yyyy-MM-dd 00:00:00") },
                    { "@fecha_hasta", DateTime.Parse(fechaHasta).ToString("yyyy-MM-dd 00:00:00") }
                }
            };

            return consulta;
        }

        protected override (Pago, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            var pago = new Pago {
                Id = Convert.ToInt64(lector["id_pago"]),
                IdCompra = lector["id_compra"] != DBNull.Value ? Convert.ToInt64(lector["id_compra"]) : 0,
                IdVenta = lector["id_venta"] != DBNull.Value ? Convert.ToInt64(lector["id_venta"]) : 0,
                MetodoPago = Enum.Parse<MetodoPagoEnum>(Convert.ToString(lector["metodo_pago"]) ?? "Efectivo"),
                MontoPagado = Convert.ToDecimal(lector["monto_pagado"], CultureInfo.InvariantCulture),
                FechaPago = lector["fecha_pago"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_pago"]) : null,
                FechaConfirmacionPago = lector["fecha_confirmacion_pago"] != DBNull.Value ? Convert.ToDateTime(lector["fecha_confirmacion_pago"]) : null,
                EstadoPago = Enum.Parse<EstadoPagoEnum>(Convert.ToString(lector["estado_pago"]) ?? "Pendiente")
            };

            var entidadesExtra = new List<IEntidadBaseDatos>();

            if (lector.VisibleFieldCount > 8) {
                if (pago.IdVenta != 0)
                    entidadesExtra.Add(new Modelos.Modulos.Venta.Venta {
                        NumeroFacturaTicket = lector["numero_factura_ticket"] != DBNull.Value ? Convert.ToString(lector["numero_factura_ticket"]) : null,
                        ImporteTotal = Convert.ToDecimal(lector["total_compraventa"], CultureInfo.InvariantCulture)
                    });
                else if (pago.IdCompra != 0)
                    entidadesExtra.Add(new Modelos.Modulos.Compra.Compra {
                        Codigo = lector["codigo"] != DBNull.Value ? Convert.ToString(lector["codigo"]) : null,
                        TotalCompra = Convert.ToDecimal(lector["total_compraventa"], CultureInfo.InvariantCulture)
                    });
            }

            return (pago, entidadesExtra);
        }

        #region STATIC

        public static RepoPago Instancia { get; } = new RepoPago();

        #endregion

        #region UTILES

        public bool CambiarEstadoPago(long id, EstadoPagoEnum estado) {
            var consulta = $"""
                UPDATE adv__pago 
                SET estado_pago = @estado_pago,
                    fecha_confirmacion_pago = @fecha_confirmacion_pago
                WHERE id_pago = @id_pago
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_pago", id },
                { "@estado_pago", estado.ToString() },
                { "@fecha_confirmacion_pago", estado != EstadoPagoEnum.Confirmado
                    ? DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")
                    : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }
            };

            return ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros) > 0;
        }

        public decimal ObtenerTotalPagadoPorVenta(long idVenta) {
            var consulta = $"""
                SELECT COALESCE(SUM(monto_pagado), 0) as total_pagado
                FROM adv__pago
                WHERE id_venta = @id_venta
                AND estado_pago = 'Confirmado';
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);

            return resultado;
        }

        public bool ExistePagoParaVenta(long idVenta) {
            var consulta = $"""
                SELECT COUNT(*) 
                FROM adv__pago
                WHERE id_venta = @id_venta
                AND estado_pago IN ('Pendiente', 'Confirmado');
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_venta", idVenta }
            };

            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
            return resultado > 0;
        }

        public List<Pago> ObtenerPagosPendientesPorCliente(long idCliente) {
            var consulta = $"""
                SELECT p.*, v.numero_factura_ticket, v.importe_total
                FROM adv__pago p
                INNER JOIN adv__venta v ON p.id_venta = v.id_venta
                WHERE v.id_cliente = @id_cliente
                AND p.estado_pago = 'Pendiente'
                ORDER BY p.fecha_pago DESC;
                """;

            var parametros = new Dictionary<string, object> {
                { "@id_cliente", idCliente }
            };

            var pagos = new List<Pago>();
            var resultados = ContextoBaseDatos.EjecutarConsulta(
                consulta,
                parametros,
                (reader) => {
                    var (pago, entidadesExtra) = MapearEntidad(reader);
                    return (pago, entidadesExtra);
                }
            );

            foreach (var (pago, _) in resultados) {
                pagos.Add(pago);
            }

            return pagos;
        }

        #endregion
    }
}