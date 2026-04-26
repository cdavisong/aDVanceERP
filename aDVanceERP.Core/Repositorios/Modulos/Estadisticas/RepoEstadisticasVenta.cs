using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Venta;

namespace aDVanceERP.Core.Repositorios.Modulos.Estadisticas {
    public class RepoEstadisticasVenta {
        public MetricasVentas ObtenerMetricas() {
            var m = new MetricasVentas();

            m.VentasHoy = ObtenerVentasHoy();
            m.IngresosHoy = ObtenerIngresosHoy();
            m.IngresosMes = ObtenerIngresosMes(DateTime.Today.Year, DateTime.Today.Month);
            m.IngresosMesAnterior = ObtenerIngresosMes(
                                                 DateTime.Today.AddMonths(-1).Year,
                                                 DateTime.Today.AddMonths(-1).Month);
            m.VentasPendientes = ObtenerVentasPorEstado("Pendiente");
            m.PagosPendientesConfirmacion = ObtenerPagosPendientes();
            m.PedidosActivos = ObtenerPedidosActivos();
            m.TopProductos = ObtenerTopProductosMes();
            m.EvolucionIngresos = ObtenerEvolucionIngresos();
            m.DistribucionMetodosPago = ObtenerDistribucionMetodosPago();

            return m;
        }

        // ══════════════════════════════════════════════════════
        //  ESCALARES
        // ══════════════════════════════════════════════════════

        public int ObtenerVentasHoy()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(*)
                FROM adv__venta
                WHERE DATE(fecha_venta) = CURDATE()
                  AND estado_venta IN ('Completada', 'Entregada')
                  AND activo = 1
                """);

        public decimal ObtenerIngresosHoy()
            => ContextoBaseDatos.EjecutarConsultaEscalar<decimal>("""
                SELECT COALESCE(SUM(importe_total), 0)
                FROM adv__venta
                WHERE DATE(fecha_venta) = CURDATE()
                  AND estado_venta IN ('Completada', 'Entregada')
                  AND activo = 1
                """);

        public decimal ObtenerIngresosMes(int anio, int mes)
            => ContextoBaseDatos.EjecutarConsultaEscalar<decimal>("""
                SELECT COALESCE(SUM(importe_total), 0)
                FROM adv__venta
                WHERE YEAR(fecha_venta)  = @anio
                  AND MONTH(fecha_venta) = @mes
                  AND estado_venta IN ('Completada', 'Entregada')
                  AND activo = 1
                """,
                new Dictionary<string, object> {
                    { "@anio", anio },
                    { "@mes",  mes  }
                });

        public int ObtenerVentasPorEstado(string estado)
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(*)
                FROM adv__venta
                WHERE estado_venta = @estado AND activo = 1
                """,
                new Dictionary<string, object> { { "@estado", estado } });

        public int ObtenerPagosPendientes()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(*)
                FROM adv__pago
                WHERE estado_pago = 'Pendiente'
                """);

        public int ObtenerPedidosActivos()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(*)
                FROM adv__pedido
                WHERE estado_pedido NOT IN ('Cancelado', 'Retirado')
                  AND activo = 1
                """);

        // ══════════════════════════════════════════════════════
        //  SERIES / TABLAS
        // ══════════════════════════════════════════════════════

        /// <summary>Top 5 productos vendidos en el mes actual por cantidad.</summary>
        public List<ProductoTopVenta> ObtenerTopProductosMes(int top = 5) {
            var consulta = $"""
                SELECT
                    p.nombre                            AS nombre,
                    SUM(dvp.cantidad)                   AS cantidad_total,
                    SUM(dvp.subtotal)                   AS ingreso_total
                FROM adv__detalle_venta_producto dvp
                INNER JOIN adv__venta v   ON dvp.id_venta    = v.id_venta
                INNER JOIN adv__producto p ON dvp.id_producto = p.id_producto
                WHERE YEAR(v.fecha_venta)  = YEAR(CURDATE())
                  AND MONTH(v.fecha_venta) = MONTH(CURDATE())
                  AND v.estado_venta IN ('Completada', 'Entregada')
                  AND v.activo = 1
                GROUP BY p.id_producto, p.nombre
                ORDER BY cantidad_total DESC
                LIMIT {top}
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new ProductoTopVenta {
                        Nombre = lector["nombre"]?.ToString() ?? string.Empty,
                        CantidadTotal = Convert.ToDecimal(lector["cantidad_total"]),
                        IngresoTotal = Convert.ToDecimal(lector["ingreso_total"])
                    };
                    return (item, new List<Core.Modelos.Comun.Interfaces.IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }

        /// <summary>Ingresos y cantidad de ventas por día, últimos 30 días.</summary>
        public List<IngresoDiario> ObtenerEvolucionIngresos(int dias = 30) {
            var consulta = $"""
                SELECT
                    DATE(fecha_venta)               AS fecha,
                    COALESCE(SUM(importe_total), 0) AS ingresos,
                    COUNT(*)                        AS cantidad
                FROM adv__venta
                WHERE fecha_venta >= DATE_SUB(CURDATE(), INTERVAL {dias} DAY)
                  AND estado_venta IN ('Completada', 'Entregada')
                  AND activo = 1
                GROUP BY DATE(fecha_venta)
                ORDER BY fecha ASC
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new IngresoDiario {
                        Fecha = Convert.ToDateTime(lector["fecha"]),
                        Ingresos = Convert.ToDecimal(lector["ingresos"]),
                        Cantidad = Convert.ToInt32(lector["cantidad"])
                    };
                    return (item, new List<Core.Modelos.Comun.Interfaces.IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }

        /// <summary>Distribución de ventas por método de pago en el mes actual.</summary>
        public List<VentasPorMetodoPago> ObtenerDistribucionMetodosPago() {
            var consulta = """
                SELECT
                    p.metodo_pago,
                    SUM(p.monto_pagado)     AS monto,
                    COUNT(p.id_pago)        AS cantidad
                FROM adv__pago p
                INNER JOIN adv__venta v ON p.id_venta = v.id_venta
                WHERE YEAR(v.fecha_venta)  = YEAR(CURDATE())
                  AND MONTH(v.fecha_venta) = MONTH(CURDATE())
                  AND v.estado_venta IN ('Completada', 'Entregada')
                  AND v.activo = 1
                GROUP BY p.metodo_pago
                ORDER BY monto DESC
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new VentasPorMetodoPago {
                        MetodoPago = lector["metodo_pago"]?.ToString() ?? string.Empty,
                        Monto = Convert.ToDecimal(lector["monto"]),
                        Cantidad = Convert.ToInt32(lector["cantidad"])
                    };
                    return (item, new List<Core.Modelos.Comun.Interfaces.IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }

        #region SINGLETON

        public static RepoEstadisticasVenta Instancia => new();

        #endregion
    }
}
