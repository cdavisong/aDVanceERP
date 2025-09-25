using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos {
    public static class UtilesDetalleProducto {
        private static async Task<T?> EjecutarConsultaAsync<T>(string query, Func<MySqlDataReader, T> procesarResultado, params MySqlParameter[] parametros) {
            using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
            try {
                await conexion.OpenAsync().ConfigureAwait(false);
            }
            catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using var comando = new MySqlCommand(query, conexion);
            comando.Parameters.AddRange(parametros);

            using var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false);
            return lectorDatos != null && await lectorDatos.ReadAsync().ConfigureAwait(false)
                ? procesarResultado((MySqlDataReader)lectorDatos)
                : default;
        }

        public static async Task<long> ObtenerIdDetalleProducto(long idProducto) {
            const string query = "SELECT p.id_detalle_producto FROM adv__producto p JOIN adv__detalle_producto dp ON p.id_detalle_producto=dp.id_detalle_producto WHERE p.id_producto=@IdProducto;";
            var result = await EjecutarConsultaAsync(query, lector => lector.GetInt64("id_detalle_producto"),
                new MySqlParameter("@IdProducto", idProducto));
            return result;
        }

        public static async Task<string?> ObtenerDescripcionProducto(long idProducto) {
            const string query = "SELECT dp.descripcion FROM adv__detalle_producto dp JOIN adv__producto p ON dp.id_detalle_producto=p.id_detalle_producto WHERE p.id_producto=@IdProducto;";
            var result = await EjecutarConsultaAsync(query, lector => lector.GetValue(lector.GetOrdinal("descripcion")),
                new MySqlParameter("@IdProducto", idProducto));
            return result?.ToString();
        }

        public static async Task<string?> ObtenerUnidadMedidaProducto(long idProducto, bool abreviatura) {
            string query = $"""
                SELECT 
                    {(abreviatura ? "um.abreviatura" : "um.nombre")} AS unidad
                FROM adv__producto p
                JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
                JOIN adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
                WHERE p.id_producto = @IdProducto;
                """;

            var parametros = new[] {
                new MySqlParameter("@IdProducto", idProducto)
            };

            return await EjecutarConsultaAsync<string>(query,
                lector => lector.GetString(lector.GetOrdinal("unidad")),
                parametros);
        }
    }
}
