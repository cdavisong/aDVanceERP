
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos;

public static class UtilesMensajero {
    // Método auxiliar para ejecutar consultas y devolver un valor escalar
    private static async Task<T?> EjecutarConsultaEscalar<T>(string query, Func<MySqlDataReader, T> mapper,
        params MySqlParameter[]? parameters) {
        await using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                await conexion.OpenAsync().ConfigureAwait(false);

                await using var comando = new MySqlCommand(query, conexion);

                if (parameters != null) comando.Parameters.AddRange(parameters);

                await using var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false);

                if (await lectorDatos.ReadAsync().ConfigureAwait(false)) return mapper((MySqlDataReader)lectorDatos);
            }
            catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }
            catch (Exception ex) {
                throw new Exception("Error inesperado al ejecutar la consulta.", ex);
            }
        }

        return default;
    }

    // Método auxiliar para ejecutar consultas y devolver una lista
    private static async Task<List<T>> EjecutarConsultaLista<T>(string query, Func<MySqlDataReader, T> mapper,
        params MySqlParameter[]? parameters) {
        var resultados = new List<T>();

        await using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                await conexion.OpenAsync().ConfigureAwait(false);

                await using (var comando = new MySqlCommand(query, conexion)) {
                    if (parameters != null) comando.Parameters.AddRange(parameters);

                    await using (var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false)) {
                        while (await lectorDatos.ReadAsync().ConfigureAwait(false))
                            resultados.Add(mapper((MySqlDataReader)lectorDatos));
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

        return resultados;
    }

    public static async Task<long> ObtenerIdMensajero(string? nombreMensajero) {
        const string query = """
                             SELECT
                                id_mensajero
                             FROM adv__mensajero
                             WHERE nombre = @nombreMensajero;
                             """;
        var parametros = new[] {
            new MySqlParameter("@nombreMensajero", nombreMensajero)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetInt64("id_mensajero"), parametros);
    }

    public static async Task<string?> ObtenerNombreMensajero(long idMensajero) {
        const string query = """
                             SELECT nombre
                             FROM adv__mensajero
                             WHERE id_mensajero = @idMensajero;
                             """;
        var parametros = new MySqlParameter[] {
            new("@idMensajero", idMensajero)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetString(0), parametros);
    }

    public static async Task<object[]> ObtenerNombresMensajeros() {
        const string query = """
                             SELECT nombre
                             FROM adv__mensajero
                             WHERE activo = '1';
                             """;

        var nombres = await EjecutarConsultaLista(query, lector => lector.GetString(lector.GetOrdinal("nombre")));

        return nombres.ToArray();
    }
}