using System.Globalization;

using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos;

public static class UtilesCompra {
    // Método auxiliar para ejecutar consultas y devolver un valor decimal
    private static decimal EjecutarConsultaDecimal(string query, params MySqlParameter[] parameters) {
        decimal resultado = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                using (var comando = new MySqlCommand(query, conexion)) {
                    if (parameters != null) comando.Parameters.AddRange(parameters);

                    var result = comando.ExecuteScalar();

                    if (result != null && result != DBNull.Value) resultado = Convert.ToDecimal(result);
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

    public static decimal ObtenerCantidadProductosCompra(long idCompra) {
        const string query = """
                             SELECT
                                SUM(cantidad) AS total_productos
                             FROM adv__detalle_compra_producto
                             WHERE id_compra = @IdCompra;
                             """;
        var parametros = new[] {
            new MySqlParameter("@IdCompra", idCompra)
        };

        return EjecutarConsultaDecimal(query, parametros);
    }

    public static IEnumerable<string> ObtenerProductosPorCompra(long idCompra) {
        const string query = """
            SELECT
                p.nombre,
                dc.cantidad,
                um.abreviatura AS unidad,
                dc.precio_compra,
                CASE 
                    WHEN p.categoria = 'ProductoTerminado' THEN 'Producto Terminado'
                    WHEN p.categoria = 'MateriaPrima' THEN 'Materia Prima'
                    ELSE 'Mercancía'
                END AS tipo_producto
            FROM adv__detalle_compra_producto dc
            JOIN adv__producto p ON dc.id_producto = p.id_producto
            LEFT JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
            LEFT JOIN adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
            WHERE dc.id_compra = @IdCompra;
            """;

        var parametros = new[] {
            new MySqlParameter("@IdCompra", idCompra)
        };

        return EjecutarConsultaLista(query, parametros);
    }

    public static decimal ObtenerValorBrutoCompraDia(DateTime fecha) {
        const string query = """
                             SELECT
                                SUM(total) AS total_dinero
                                FROM adv__compra
                                WHERE DATE(fecha) = @Fecha;
                             """;
        var parametros = new[] {
            new MySqlParameter("@Fecha", fecha.ToString("yyyy-MM-dd"))
        };

        return EjecutarConsultaDecimal(query, parametros);
    }
}