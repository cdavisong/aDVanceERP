using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos {
    public static class UtilesTipoMateriaPrima {
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

        public static async Task<long> ObtenerIdTipoMateriaPrima(string? nombreTipoMateriaPrima) {
            const string query = "SELECT id_tipo_materia_prima FROM adv__tipo_materia_prima WHERE nombre = @NombreTipoMateriaPrima;";
            var result = await EjecutarConsultaAsync(query, lector => lector.GetInt32("id_tipo_materia_prima"),
                new MySqlParameter("@NombreTipoMateriaPrima", nombreTipoMateriaPrima)); // Sin comillas adicionales
            return result > 0 ? result : 0; // Manejo de null
        }

        public static string? ObtenerNombreTipoMateriaPrima(long idTipoMateriaPrima) {
            const string query = "SELECT nombre FROM adv__tipo_materia_prima WHERE id_tipo_materia_prima = @IdTipoMateriaPrima;";
            return EjecutarConsulta(query, lector => lector.GetString("nombre"),
                new MySqlParameter("@IdTipoMateriaPrima", idTipoMateriaPrima));
        }

        public static object[] ObtenerNombresTiposMateriasPrimas() {
            var query = "SELECT nombre FROM adv__tipo_materia_prima;";
            return EjecutarConsulta(query, lector => {
                var nombres = new List<string>();
                do {
                    nombres.Add(lector.GetString("nombre"));
                } while (lector.Read());
                return nombres.ToArray();
            }) ?? Array.Empty<object>();
        }

        public static string[] ObtenerDescripcionesTiposMateriaPrima() {
            var query = "SELECT descripcion FROM adv__tipo_materia_prima;";
            return EjecutarConsulta(query, lector => {
                var descripciones = new List<string>();
                do {
                    descripciones.Add(lector.GetString("descripcion"));
                } while (lector.Read());
                return descripciones.ToArray();
            }) ?? Array.Empty<string>();
        }
    }
}
