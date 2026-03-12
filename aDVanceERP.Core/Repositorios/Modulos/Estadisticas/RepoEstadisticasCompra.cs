using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Compra;

namespace aDVanceERP.Core.Repositorios.Modulos.Estadisticas {
    public class RepoEstadisticasCompra {
        public MetricasCompras ObtenerMetricas() {
            var m = new MetricasCompras();

            m.OrdenesPendientesAprobacion = ObtenerOrdenesPorEstado("Pendiente_Aprobacion");
            m.OrdenesAprobadas = ObtenerOrdenesPorEstado("Aprobada");
            m.OrdenesRecibidasParcial = ObtenerOrdenesPorEstado("Recibida_Parcial");
            m.SolicitudesPendientes = ObtenerSolicitudesPendientes();
            m.GastoMesActual = ObtenerGastoMes(DateTime.Today.Year, DateTime.Today.Month);
            m.GastoMesAnterior = ObtenerGastoMes(
                                                DateTime.Today.AddMonths(-1).Year,
                                                DateTime.Today.AddMonths(-1).Month);
            m.TopProveedores = ObtenerTopProveedores();
            m.EvolucionGasto = ObtenerEvolucionGasto();
            m.DistribucionEstados = ObtenerDistribucionEstados();

            return m;
        }

        // ══════════════════════════════════════════════════════
        //  ESCALARES
        // ══════════════════════════════════════════════════════

        public int ObtenerOrdenesPorEstado(string estado)
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(*)
                FROM adv__compra
                WHERE estado_compra = @estado AND activo = 1
                """,
                new Dictionary<string, object> { { "@estado", estado } });

        public int ObtenerSolicitudesPendientes()
            => ContextoBaseDatos.EjecutarConsultaEscalar<int>("""
                SELECT COUNT(*)
                FROM adv__solicitud_compra
                WHERE estado = 'Pendiente_Aprobacion' AND activo = 1
                """);

        public decimal ObtenerGastoMes(int anio, int mes)
            => ContextoBaseDatos.EjecutarConsultaEscalar<decimal>("""
                SELECT COALESCE(SUM(total_compra), 0)
                FROM adv__compra
                WHERE YEAR(fecha_orden)  = @anio
                  AND MONTH(fecha_orden) = @mes
                  AND estado_compra NOT IN ('Cancelada', 'Borrador')
                  AND activo = 1
                """,
                new Dictionary<string, object> {
                    { "@anio", anio },
                    { "@mes",  mes  }
                });

        // ══════════════════════════════════════════════════════
        //  SERIES / TABLAS
        // ══════════════════════════════════════════════════════

        public List<ProveedorTopCompras> ObtenerTopProveedores(int top = 5) {
            var consulta = $"""
                SELECT
                    p.razon_social          AS nombre_proveedor,
                    SUM(c.total_compra)     AS monto_total,
                    COUNT(c.id_compra)      AS cantidad_ordenes
                FROM adv__compra c
                INNER JOIN adv__proveedor p ON c.id_proveedor = p.id_proveedor
                WHERE c.activo = 1
                  AND c.estado_compra NOT IN ('Cancelada', 'Borrador')
                  AND c.fecha_orden >= DATE_SUB(CURDATE(), INTERVAL 12 MONTH)
                GROUP BY p.id_proveedor, p.razon_social
                ORDER BY monto_total DESC
                LIMIT {top}
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new ProveedorTopCompras {
                        NombreProveedor = lector["nombre_proveedor"]?.ToString() ?? string.Empty,
                        MontoTotal = Convert.ToDecimal(lector["monto_total"]),
                        CantidadOrdenes = Convert.ToInt32(lector["cantidad_ordenes"])
                    };
                    return (item, new List<Core.Modelos.Comun.Interfaces.IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }

        /// <summary>Gasto mensual de los últimos 6 meses.</summary>
        public List<GastoMensual> ObtenerEvolucionGasto(int meses = 6) {
            var consulta = $"""
                SELECT
                    YEAR(fecha_orden)   AS anio,
                    MONTH(fecha_orden)  AS mes,
                    SUM(total_compra)   AS gasto
                FROM adv__compra
                WHERE fecha_orden >= DATE_SUB(CURDATE(), INTERVAL {meses} MONTH)
                  AND estado_compra NOT IN ('Cancelada', 'Borrador')
                  AND activo = 1
                GROUP BY YEAR(fecha_orden), MONTH(fecha_orden)
                ORDER BY anio ASC, mes ASC
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new GastoMensual {
                        Anio = Convert.ToInt32(lector["anio"]),
                        Mes = Convert.ToInt32(lector["mes"]),
                        Gasto = Convert.ToDecimal(lector["gasto"])
                    };
                    return (item, new List<Core.Modelos.Comun.Interfaces.IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }

        /// <summary>Distribución de órdenes activas por estado.</summary>
        public List<ComprasPorEstado> ObtenerDistribucionEstados() {
            var consulta = """
                SELECT estado_compra AS estado, COUNT(*) AS cantidad
                FROM adv__compra
                WHERE activo = 1
                  AND estado_compra NOT IN ('Cancelada')
                GROUP BY estado_compra
                ORDER BY cantidad DESC
                """;

            return ContextoBaseDatos
                .EjecutarConsulta(consulta, null, lector => {
                    var item = new ComprasPorEstado {
                        Estado = lector["estado"]?.ToString() ?? string.Empty,
                        Cantidad = Convert.ToInt32(lector["cantidad"])
                    };
                    return (item, new List<Core.Modelos.Comun.Interfaces.IEntidadBaseDatos>());
                })
                .Select(r => r.entidadBase)
                .ToList();
        }
    }
}
