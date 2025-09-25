using System.Globalization;

using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos;

public class RepoEstadisticosVentas {
    public Dictionary<DateTime, decimal> VentasPorHora { get; set; } = new();
    public Dictionary<DateTime, decimal> VentasPorDia { get; set; } = new();
    public Dictionary<DateTime, decimal> VentasPorMes { get; set; } = new();
}

public static class UtilesVenta {
    private static RepoEstadisticosVentas _datos = new();

    // Método auxiliar para ejecutar consultas y devolver un valor decimal
    private static decimal EjecutarConsultaDecimal(string query, params MySqlParameter[] parameters) {
        decimal resultado = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                using (var comando = new MySqlCommand(query, conexion)) {
                    if (parameters != null) comando.Parameters.AddRange(parameters);

                    var result = comando.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        resultado = Convert.ToDecimal(result);
                }
            }
            catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }
            catch (Exception ex) {
                throw new Exception("Error inesperado al ejecutar la consulta.", ex);
            }
        }

        return resultado;
    }

    // Método auxiliar para ejecutar consultas y devolver un valor entero
    private static int EjecutarConsultaEntero(string query, params MySqlParameter[] parameters) {
        var resultado = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                using (var comando = new MySqlCommand(query, conexion)) {
                    if (parameters != null) comando.Parameters.AddRange(parameters);

                    var result = comando.ExecuteScalar();

                    if (result != null && result != DBNull.Value) resultado = Convert.ToInt32(result);
                }
            }
            catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }
            catch (Exception ex) {
                throw new Exception("Error inesperado al ejecutar la consulta.", ex);
            }
        }

        return resultado;
    }

    // Método auxiliar para ejecutar consultas y devolver un valor flotante
    private static float EjecutarConsultaFlotante(string query, params MySqlParameter[] parameters) {
        var resultado = 0F;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                using (var comando = new MySqlCommand(query, conexion)) {
                    if (parameters != null) comando.Parameters.AddRange(parameters);

                    var result = comando.ExecuteScalar();

                    if (result != null && result != DBNull.Value) resultado = Convert.ToSingle(result);
                }
            }
            catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }
            catch (Exception ex) {
                throw new Exception("Error inesperado al ejecutar la consulta.", ex);
            }
        }

        return resultado;
    }

    // Método auxiliar para ejecutar consultas y devolver una lista de strings
    private static List<string> EjecutarConsultaLista(string query, params MySqlParameter[] parameters) {
        var resultado = new List<string>();

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                using (var comando = new MySqlCommand(query, conexion)) {
                    if (parameters != null) comando.Parameters.AddRange(parameters);

                    using (var reader = comando.ExecuteReader()) {
                        while (reader.Read()) {
                            var fila = string.Empty;
                            for (var i = 0; i < reader.FieldCount; i++)
                                // Verificar si el valor es decimal y formatearlo correctamente
                                if (reader.GetFieldType(i) == typeof(decimal)) {
                                    var valorDecimal = reader.GetDecimal(i);
                                    fila += valorDecimal.ToString("N2", CultureInfo.InvariantCulture) + "|";
                                }
                                else if (reader.GetFieldType(i) == typeof(float)) {
                                    var valorDecimal = reader.GetFloat(i);
                                    fila += valorDecimal.ToString("N2", CultureInfo.InvariantCulture) + "|";
                                }
                                else {
                                    fila += reader[i] + "|";
                                }

                            resultado.Add(fila.TrimEnd('|'));
                        }
                    }
                }
            }
            catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }
            catch (Exception ex) {
                throw new Exception("Error inesperado al ejecutar la consulta.", ex);
            }
        }

        return resultado;
    }

    public static bool ExisteVenta(long idVenta) {
        var query = $"""
                     SELECT COUNT(1)
                     FROM adv__venta
                     WHERE id_venta = {idVenta};
                     """;

        return EjecutarConsultaEntero(query) > 0;
    }

    public static decimal ObtenerTotalProductosVendidosHoy() {
        const string query = """
                             SELECT SUM(dva.cantidad) AS total_vendido_hoy
                             FROM adv__detalle_venta_producto dva
                             INNER JOIN adv__venta v ON dva.id_venta = v.id_venta
                             WHERE DATE(v.fecha) = CURDATE();
                             """;

        return EjecutarConsultaDecimal(query);
    }

    public static decimal ObtenerCantidadProductosVenta(long idVenta) {
        const string query = """
                             SELECT SUM(cantidad) AS total_productos
                             FROM adv__detalle_venta_producto
                             WHERE id_venta = @IdVenta;
                             """;
        var parametros = new[] {
            new MySqlParameter("@IdVenta", idVenta)
        };

        return EjecutarConsultaDecimal(query, parametros);
    }

    public static IEnumerable<string> ObtenerProductosPorVenta(long idVenta) {
        const string query = """
            SELECT
                p.nombre,
                dva.cantidad,
                um.abreviatura AS unidad,
                dva.precio_venta_final,
                CASE 
                    WHEN p.categoria = 'ProductoTerminado' THEN 'Producto Terminado'
                    WHEN p.categoria = 'MateriaPrima' THEN 'Materia Prima'
                    ELSE 'Mercancía'
                END AS tipo_producto
            FROM adv__detalle_venta_producto dva
            JOIN adv__producto p ON dva.id_producto = p.id_producto
            LEFT JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
            LEFT JOIN adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
            WHERE dva.id_venta = @IdVenta;
            """;

        var parametros = new[] {
            new MySqlParameter("@IdVenta", idVenta)
        };

        return EjecutarConsultaLista(query, parametros);
    }

    public static string ObtenerEstadoPagoVenta(long idVenta) {
        const string queryTotalVenta = """
                                       SELECT total
                                       FROM adv__venta
                                       WHERE id_venta = @IdVenta;
                                       """;

        const string queryPagosConfirmados = """
                                             SELECT SUM(monto)
                                             FROM adv__pago
                                             WHERE id_venta = @IdVenta
                                             AND estado = 'Confirmado';
                                             """;

        var parametros = new[] {
            new MySqlParameter("@IdVenta", idVenta)
        };

        var totalVenta = EjecutarConsultaDecimal(queryTotalVenta, parametros);
        var pagosConfirmados = EjecutarConsultaDecimal(queryPagosConfirmados, parametros);

        if (pagosConfirmados == 0)
            return "Sin pagos";

        if (pagosConfirmados < totalVenta)
            return "Pago parcial";

        return pagosConfirmados == totalVenta ? "Completado" : "Sobrepago";
    }

    public static List<long> ObtenerVentasPendientes() {
        const string query = """
                             SELECT v.id_venta
                             FROM adv__venta v
                             WHERE NOT EXISTS (
                                 SELECT 1
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             ) OR (
                                 SELECT COALESCE(SUM(p.monto), 0)
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             ) < v.total;
                             """;

        var resultados = EjecutarConsultaLista(query);
        return resultados.Select(r => long.Parse(r.Split('|')[0])).ToList();
    }

    public static List<string> ObtenerPagosPorVenta(long idVenta) {
        const string query = """
                             SELECT *
                             FROM adv__pago
                             WHERE id_venta = @IdVenta;
                             """;
        var parametros = new[] {
            new MySqlParameter("@IdVenta", idVenta)
        };

        return EjecutarConsultaLista(query, parametros);
    }

    public static decimal ObtenerValorBrutoVentaDia(DateTime fecha) {
        const string query = """
                             SELECT SUM(v.total)
                             FROM adv__venta v
                             WHERE DATE(v.fecha) = @Fecha
                             AND EXISTS (
                                 SELECT 1
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             );
                             """;
        var parametros = new[] {
            new MySqlParameter("@Fecha", fecha.ToString("yyyy-MM-dd"))
        };

        return EjecutarConsultaDecimal(query, parametros);
    }

    public static decimal ObtenerValorGananciaTotalNegocio() {
        const string query = """
                             SELECT SUM((dva.precio_venta_final - dva.precio_compra_vigente) * dva.cantidad) AS ganancia_total
                             FROM adv__detalle_venta_producto dva
                             JOIN adv__venta v ON dva.id_venta = v.id_venta
                             WHERE (
                                 SELECT COALESCE(SUM(p.monto), 0)
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             ) >= v.total;
                             """;

        return EjecutarConsultaDecimal(query);
    }

    public static decimal ObtenerValorGananciaDia(DateTime fecha) {
        const string query = """
                             SELECT SUM((dva.precio_venta_final - dva.precio_compra_vigente) * dva.cantidad) AS ganancia_dia
                             FROM adv__detalle_venta_producto dva
                             JOIN adv__venta v ON dva.id_venta = v.id_venta
                             WHERE DATE(v.fecha) = @Fecha 
                             AND (
                                 SELECT COALESCE(SUM(p.monto), 0)
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             ) >= v.total;
                             """;
        var parametros = new[] {
            new MySqlParameter("@Fecha", fecha.ToString("yyyy-MM-dd"))
        };

        return EjecutarConsultaDecimal(query, parametros);
    }

    public static RepoEstadisticosVentas ObtenerDatosEstadisticosVentas(DateTime fecha) {
        _datos = new RepoEstadisticosVentas();

        ObtenerVentasPorHora(fecha);
        ObtenerVentasPorDia(fecha);
        ObtenerVentasPorMes(fecha);

        RellenarPeriodosVacios(_datos, fecha);

        _datos.VentasPorHora = _datos.VentasPorHora.OrderBy(v => v.Key).ToDictionary(v => v.Key, v => v.Value);
        _datos.VentasPorDia = _datos.VentasPorDia.OrderBy(v => v.Key).ToDictionary(v => v.Key, v => v.Value);
        _datos.VentasPorMes = _datos.VentasPorMes.OrderBy(v => v.Key).ToDictionary(v => v.Key, v => v.Value);

        return _datos;
    }

    private static void ObtenerVentasPorHora(DateTime fechaHora) {
        const string query = """
                             SELECT
                                 HOUR(v.fecha) AS Hora,
                                 SUM(dva.precio_venta_final * dva.cantidad) AS Total
                             FROM adv__venta v
                             JOIN adv__detalle_venta_producto dva ON v.id_venta = dva.id_venta
                             WHERE DATE(v.fecha) = @fecha
                             AND (
                                 SELECT COALESCE(SUM(p.monto), 0)
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             ) >= v.total
                             GROUP BY HOUR(v.fecha);
                             """;
        var parametros = new[] {
            new MySqlParameter("@fecha", fechaHora.Date.ToString("yyyy-MM-dd"))
        };

        EjecutarConsultaEstadistica(query, parametros, _datos.VentasPorHora, fechaHora);
    }

    private static void ObtenerVentasPorDia(DateTime fechaHora) {
        const string query = """
                             SELECT
                                 DAY(v.fecha) AS Dia,
                                 SUM(dva.precio_venta_final * dva.cantidad) AS Total
                             FROM adv__venta v
                             JOIN adv__detalle_venta_producto dva ON v.id_venta = dva.id_venta
                             WHERE MONTH(v.fecha) = @mes 
                             AND YEAR(v.fecha) = @anio
                             AND (
                                 SELECT COALESCE(SUM(p.monto), 0)
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             ) >= v.total
                             GROUP BY DAY(v.fecha);
                             """;
        var parametros = new[] {
            new MySqlParameter("@mes", fechaHora.Month),
            new MySqlParameter("@anio", fechaHora.Year)
        };

        EjecutarConsultaEstadistica(query, parametros, _datos.VentasPorDia, fechaHora);
    }

    private static void ObtenerVentasPorMes(DateTime fechaHora) {
        const string query = """
                             SELECT
                                 MONTH(v.fecha) AS Mes,
                                 SUM(dva.precio_venta_final * dva.cantidad) AS Total
                             FROM adv__venta v
                             JOIN adv__detalle_venta_producto dva ON v.id_venta = dva.id_venta
                             WHERE YEAR(v.fecha) = @anio
                             AND (
                                 SELECT COALESCE(SUM(p.monto), 0)
                                 FROM adv__pago p
                                 WHERE p.id_venta = v.id_venta
                                 AND p.estado = 'Confirmado'
                             ) >= v.total
                             GROUP BY MONTH(v.fecha);
                             """;
        var parametros = new[] {
            new MySqlParameter("@anio", fechaHora.Year)
        };

        EjecutarConsultaEstadistica(query, parametros, _datos.VentasPorMes, fechaHora);
    }

    private static void EjecutarConsultaEstadistica(string query, MySqlParameter[]? parameters,
        Dictionary<DateTime, decimal> datos, DateTime fecha) {
        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());

        try {
            if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

            using var comando = new MySqlCommand(query, conexion);

            if (parameters != null)
                comando.Parameters.AddRange(parameters);

            datos.Clear();

            using var reader = comando.ExecuteReader();

            while (reader.Read()) {
                var fechaResultado = query.Contains("HOUR")
                    ? fecha.Date.AddHours(reader.GetInt32(0))
                    : query.Contains("DAY")
                        ? new DateTime(fecha.Year, fecha.Month, reader.GetInt32(0))
                        : new DateTime(fecha.Year, reader.GetInt32(0), 1);

                datos.Add(fechaResultado, reader.GetDecimal(1));
            }
        }
        catch (MySqlException) {
            throw new ExcepcionConexionServidorMySQL();
        }
    }

    private static void RellenarPeriodosVacios(RepoEstadisticosVentas datos, DateTime fecha) {
        // Rellenar horas (0-23)
        for (var hora = 0; hora < 24; hora++) {
            var horaFecha = fecha.Date.AddHours(hora);

            datos.VentasPorHora.TryAdd(horaFecha, 0);
        }

        // Rellenar días del mes
        for (var dia = 1; dia <= DateTime.DaysInMonth(fecha.Year, fecha.Month); dia++) {
            var diaFecha = new DateTime(fecha.Year, fecha.Month, dia);

            datos.VentasPorDia.TryAdd(diaFecha, 0);
        }

        // Rellenar meses (1-12)
        for (var mes = 1; mes <= 12; mes++) {
            var mesFecha = new DateTime(fecha.Year, mes, 1);

            datos.VentasPorMes.TryAdd(mesFecha, 0);
        }
    }
}