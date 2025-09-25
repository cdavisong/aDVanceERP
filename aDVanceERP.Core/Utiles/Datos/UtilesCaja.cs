using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos;

public static class UtilesCaja {
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

    public static bool ExisteCajaActiva() {
        var query = $"""
                     SELECT COUNT(1)
                     FROM adv__caja
                     WHERE estado='Abierta';
                     """;

        return EjecutarConsulta(query, lector => lector.GetInt32(0)) > 0;
    }

    public static long ObtenerIdCajaActiva() {
        var query = $"""
                     SELECT id_caja
                     FROM adv__caja
                     WHERE estado='Abierta';
                     """;

        return EjecutarConsulta(query, lector => lector.GetInt64("id_caja"));
    }

    public static int ObtenerCantidadMovimientos(long idCaja) {
        var query = $"""
                     SELECT COUNT(1)
                     FROM adv__movimiento_caja
                     WHERE id_caja = @idCaja;
                     """;
        var parametro = new MySqlParameter("@idCaja", idCaja);

        return EjecutarConsulta(query, lector => lector.GetInt32(0), parametro);
    }

    public static void ActualizarMontoCaja(long idCaja, decimal monto) {
        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
        try {
            if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
        } catch (MySqlException) {
            throw new ExcepcionConexionServidorMySQL();
        }

        var query = $"""
                    UPDATE adv__caja
                    SET saldo_actual = saldo_inicial + @monto
                    WHERE id_caja = @idCaja;
                    """;

        var parametros = new[] {
            new MySqlParameter("@monto", monto),
            new MySqlParameter("@idCaja", idCaja)
        };

        using var comando = new MySqlCommand(query, conexion);
        comando.Parameters.AddRange(parametros);
        comando.ExecuteNonQuery();
    }
}
