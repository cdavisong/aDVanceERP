using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos {
    public static class UtilesColorProducto {
        private static async Task<T?> EjecutarConsultaAsync<T>(string query, Func<MySqlDataReader, T> procesarResultado, params MySqlParameter[] parametros) {
            using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
            try {
                await conexion.OpenAsync().ConfigureAwait(false);
            } catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using var comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddRange(parametros);

            using var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false);
            return lectorDatos != null && await lectorDatos.ReadAsync().ConfigureAwait(false)
                ? procesarResultado((MySqlDataReader) lectorDatos)
                : default;
        }

        private static T? EjecutarConsulta<T>(string query, Func<MySqlDataReader, T> procesarResultado, params MySqlParameter[] parametros) {
            using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            } catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using var comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddRange(parametros);

            using var lectorDatos = comando.ExecuteReader();
            return lectorDatos != null && lectorDatos.Read()
                ? procesarResultado(lectorDatos)
                : default;
        }

        public static async Task<long> ObtenerIdColorProducto(string? nombreColorProducto) {
            const string query = "SELECT id_color_producto FROM adv__color_producto WHERE nombre=@NombreColorProducto;";
            var result = await EjecutarConsultaAsync(query, lector => lector.GetInt32("id_color_producto"),
                new MySqlParameter("@NombreColorProducto", $"{nombreColorProducto}"));
            return result != 0 ? result : 0;
        }

        public static string? ObtenerNombreColorProducto(long idColorProducto) {
            const string query = "SELECT nombre FROM adv__color_producto WHERE id_color_producto = @IdColorProducto;";
            return EjecutarConsulta(query, lector => lector.GetString("nombre"),
                new MySqlParameter("@IdColorProducto", idColorProducto));
        }

        public static string[] ObtenerNombresColoresProductos() {
            var query = "SELECT nombre FROM adv__color_producto;";
            return EjecutarConsulta(query, lector => {
                var nombres = new List<string>();
                do {
                    nombres.Add(lector.GetString("nombre"));
                } while (lector.Read());
                return nombres.ToArray();
            }) ?? Array.Empty<string>();
        }
    }
}
