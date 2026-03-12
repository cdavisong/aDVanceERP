using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;

namespace aDVanceERP.Core.Repositorios.Modulos.Estadisticas {
    public class RepoEstadisticasInventario {

        // ══════════════════════════════════════════════════════
        //  MÉTRICA PRINCIPAL — carga todo de una vez
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Ejecuta todas las consultas del dashboard de inventario y devuelve
        /// las métricas consolidadas. Abre una sola conexión para todas las consultas.
        /// </summary>
        public MetricasInventario ObtenerMetricas() {
            var m = new MetricasInventario();

            m.TotalProductos = ObtenerTotalProductos();
            m.ProductosBajoStockMinimo = ObtenerProductosBajoStockMinimo();
            m.ProductosSinStock = ObtenerProductosSinStock();
            m.ValorTotalInventario = ObtenerValorTotalInventario();
            m.TotalAlmacenes = ObtenerTotalAlmacenes();
            m.MovimientosHoy = ObtenerMovimientosHoy();
            m.TopProductosValor = ObtenerTopProductosValor();
            m.StockPorAlmacen = ObtenerStockPorAlmacen();
            m.EvolucionMovimientos = ObtenerEvolucionMovimientos();

            return m;
        }

        // ══════════════════════════════════════════════════════
        //  ESCALARES
        // ══════════════════════════════════════════════════════

        public int ObtenerTotalProductos()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>(
                "SELECT COUNT(*) FROM adv__producto WHERE activo = 1");

        public int ObtenerProductosBajoStockMinimo()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(DISTINCT i.id_producto)
                FROM adv__inventario i
                WHERE i.cantidad <= i.cantidad_minima
                  AND i.cantidad_minima > 0
                """);

        public int ObtenerProductosSinStock()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(DISTINCT p.id_producto)
                FROM adv__producto p
                LEFT JOIN adv__inventario i ON p.id_producto = i.id_producto
                WHERE p.activo = 1
                GROUP BY p.id_producto
                HAVING COALESCE(SUM(i.cantidad), 0) = 0
                """);

        public decimal ObtenerValorTotalInventario()
            => ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(
                "SELECT COALESCE(SUM(valor_total), 0) FROM adv__inventario");

        public int ObtenerTotalAlmacenes()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>(
                "SELECT COUNT(*) FROM adv__almacen WHERE estado = 1");

        public int ObtenerMovimientosHoy()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(*)
                FROM adv__movimiento
                WHERE DATE(fecha_creacion) = CURDATE()
                """);

        // ══════════════════════════════════════════════════════
        //  SERIES / TABLAS
        // ══════════════════════════════════════════════════════

        public List<ProductoTopInventario> ObtenerTopProductosValor(int top = 5) {
            var consulta = $"""
                SELECT p.nombre, i.cantidad, i.valor_total
                FROM adv__inventario i
                INNER JOIN adv__producto p ON i.id_producto = p.id_producto
                WHERE p.activo = 1
                ORDER BY i.valor_total DESC
                LIMIT {top}
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new ProductoTopInventario {
                        Nombre = lector["nombre"]?.ToString() ?? string.Empty,
                        Cantidad = Convert.ToDecimal(lector["cantidad"]),
                        ValorTotal = Convert.ToDecimal(lector["valor_total"])
                    };
                    return (item, new List<IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }

        public List<StockPorAlmacen> ObtenerStockPorAlmacen() {
            var consulta = """
                SELECT
                    a.nombre                            AS nombre_almacen,
                    COALESCE(SUM(i.valor_total), 0)     AS valor_total,
                    COUNT(DISTINCT i.id_producto)       AS cantidad_skus
                FROM adv__almacen a
                LEFT JOIN adv__inventario i ON a.id_almacen = i.id_almacen
                WHERE a.estado = 1
                GROUP BY a.id_almacen, a.nombre
                ORDER BY valor_total DESC
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new StockPorAlmacen {
                        NombreAlmacen = lector["nombre_almacen"]?.ToString() ?? string.Empty,
                        ValorTotal = Convert.ToDecimal(lector["valor_total"]),
                        CantidadSkus = Convert.ToInt32(lector["cantidad_skus"])
                    };
                    return (item, new List<IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }

        /// <summary>Entradas y salidas agrupadas por día para los últimos 30 días.</summary>
        public List<MovimientoDiario> ObtenerEvolucionMovimientos(int dias = 30) {
            var consulta = $"""
                SELECT
                    DATE(fecha_creacion)                        AS fecha,
                    COALESCE(SUM(CASE WHEN tm.nombre IN ('Compra','Ajuste Positivo','Carga_Inicial')
                                      THEN m.cantidad_movida ELSE 0 END), 0) AS entradas,
                    COALESCE(SUM(CASE WHEN tm.nombre IN ('Venta','Devolución a Proveedor','Ajuste Negativo')
                                      THEN m.cantidad_movida ELSE 0 END), 0) AS salidas
                FROM adv__movimiento m
                INNER JOIN adv__tipo_movimiento tm ON m.id_tipo_movimiento = tm.id_tipo_movimiento
                WHERE fecha_creacion >= DATE_SUB(CURDATE(), INTERVAL {dias} DAY)
                  AND m.estado = 'Completado'
                GROUP BY DATE(fecha_creacion)
                ORDER BY fecha ASC
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new MovimientoDiario {
                        Fecha = Convert.ToDateTime(lector["fecha"]),
                        Entradas = Convert.ToDecimal(lector["entradas"]),
                        Salidas = Convert.ToDecimal(lector["salidas"])
                    };
                    return (item, new List<IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }
    }
}
