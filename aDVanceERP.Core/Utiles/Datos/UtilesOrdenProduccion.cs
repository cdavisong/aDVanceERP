using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos {
    public class UtilesOrdenProduccion {
        /// <summary>
        /// Obtiene los nombres de todas las materias primas utilizadas en órdenes de producción
        /// </summary>
        public static List<string> ObtenerNombresMateriasPrimasUtilizadas() {
            var nombres = new List<string>();
            var query = """
            SELECT DISTINCT p.nombre
            FROM adv__orden_material om
            JOIN adv__producto p ON om.id_producto = p.id_producto
            WHERE p.categoria = 'MateriaPrima'
            ORDER BY p.nombre;
            """;

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        nombres.Add(reader.GetString(0));
                    }
                }
            }

            return nombres;
        }

        /// <summary>
        /// Obtiene los nombres de todas las actividades de producción utilizadas en órdenes (sin repetir)
        /// </summary>
        public static List<string> ObtenerNombresActividadesUtilizadas() {
            var nombres = new List<string>();
            var query = """
            SELECT DISTINCT nombre
            FROM adv__orden_actividad
            WHERE nombre IS NOT NULL AND nombre != ''
            ORDER BY nombre;
            """;

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        nombres.Add(reader.GetString(0));
                    }
                }
            }

            return nombres;
        }

        /// <summary>
        /// Obtiene los conceptos de gastos indirectos utilizados en órdenes de producción (sin repetir)
        /// </summary>
        public static List<string> ObtenerConceptosGastosIndirectosUtilizados() {
            var conceptos = new List<string>();
            var query = """
            SELECT DISTINCT concepto
            FROM adv__orden_gasto_indirecto
            WHERE concepto IS NOT NULL AND concepto != ''
            ORDER BY concepto;
            """;

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
                using (var cmd = new MySqlCommand(query, conexion))
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        conceptos.Add(reader.GetString(0));
                    }
                }
            }

            return conceptos;
        }

        /// <summary>
        /// Obtiene el próximo número de orden de producción disponible en formato OP#####
        /// </summary>
        public static string ObtenerProximoNumeroOrden() {
            var query = """
                SELECT CONCAT('OP', LPAD(
                    IFNULL(
                        MAX(CAST(SUBSTRING(numero_orden, 3) AS UNSIGNED)), 
                        0
                    ) + 1, 
                    5, '0'
                )) AS proximo_numero_orden
                FROM adv__orden_produccion
                WHERE numero_orden REGEXP '^OP[0-9]{5}$';
            """;

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
                using (var cmd = new MySqlCommand(query, conexion)) {
                    var result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "OP00001"; // Valor por defecto si no hay resultados
                }
            }
        }

        public static decimal ObtenerCostoOrdenActividadProduccion(long idOrdenProduccion, string nombre) {
            var query = """
                SELECT costo 
                FROM adv__orden_actividad 
                WHERE 
                    (id_orden_produccion = @idOrdenProduccion OR @idOrdenProduccion = 0)
                    AND nombre = @nombre
                ORDER BY 
                    CASE WHEN @idOrdenProduccion = 0 THEN fecha_registro END DESC,
                    id_orden_produccion = @idOrdenProduccion DESC
                LIMIT 1;
                """;

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
                using (var cmd = new MySqlCommand(query, conexion)) {
                    cmd.Parameters.AddWithValue("@idOrdenProduccion", idOrdenProduccion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    var result = cmd.ExecuteScalar();
                    return result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0m;
                }
            }
        }

        public static decimal ObtenerCostoOrdenGastoIndirecto(long idOrdenProduccion, string concepto) {
            var query = """
                SELECT monto 
                FROM adv__orden_gasto_indirecto 
                WHERE 
                    (id_orden_produccion = @idOrdenProduccion OR @idOrdenProduccion = 0)
                    AND concepto = @concepto
                ORDER BY 
                    CASE WHEN @idOrdenProduccion = 0 THEN fecha_registro END DESC,
                    id_orden_produccion = @idOrdenProduccion DESC
                LIMIT 1;
                """;

            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
                using (var cmd = new MySqlCommand(query, conexion)) {
                    cmd.Parameters.AddWithValue("@idOrdenProduccion", idOrdenProduccion);
                    cmd.Parameters.AddWithValue("@concepto", concepto);

                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0m;
                }
            }
        }
    }
}