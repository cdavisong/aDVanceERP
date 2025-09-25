using System.Security;

using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Seguridad.MVP.Modelos;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Seguridad.Utiles; 

public static class UtilesCuentaUsuario {
    public static CuentaUsuario? UsuarioAutenticado { get; set; } = new();
    public static CuentaUsuario? UsuarioAutenticadoTelegram { get; set; } = new();
    public static string[]? PermisosUsuario { get; set; }
    public static string[]? PermisosUsuarioTelegram { get; set; }

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

    private static T? EjecutarConsulta<T>(string query, Func<MySqlDataReader, T> procesarResultado, params MySqlParameter[] parametros) {
        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
        try {
            if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
        }
        catch (MySqlException) {
            throw new ExcepcionConexionServidorMySQL();
        }

        using var comando = new MySqlCommand(query, conexion);
        comando.Parameters.AddRange(parametros);

        using var lectorDatos = comando.ExecuteReader();
        return lectorDatos != null && lectorDatos.Read()
            ? procesarResultado(lectorDatos)
            : default;
    }

    public static async Task<long> ObtenerIdCuentaUsuario(string? nombreCuentaUsuario) {
        const string query = "SELECT id_cuenta_usuario FROM adv__cuenta_usuario WHERE LOWER(nombre) LIKE LOWER(@NombreCuentaUsuario);";
        var result = await EjecutarConsultaAsync(query, lector => lector.GetInt32("id_cuenta_usuario"),
            new MySqlParameter("@NombreCuentaUsuario", $"%{nombreCuentaUsuario}%"));
        return result != 0 ? result : 0;
    }

    public static string? ObtenerNombreCuentaUsuario(long idCuentaUsuario) {
        const string query = "SELECT nombre FROM adv__cuenta_usuario WHERE id_cuenta_usuario = @IdCuentaUsuario;";
        return EjecutarConsulta(query, lector => lector.GetString("nombre"),
            new MySqlParameter("@IdCuentaUsuario", idCuentaUsuario));
    }

    public static async Task<bool> EsTablaCuentasUsuarioVacia() {
        var tablaVacia = false;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = new MySqlCommand("SELECT COUNT(*) AS user_count FROM adv__cuenta_usuario", conexion)) {
                using (var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false)) {
                    if (await lectorDatos.ReadAsync().ConfigureAwait(false)) {
                        var userCount = lectorDatos.GetInt32(lectorDatos.GetOrdinal("user_count"));

                        tablaVacia = userCount == 0;
                    }
                }
            }
        }

        return tablaVacia;
    }

    public static void CrearUsuarioAdministrador(string nombreUsuario, SecureString password) {
        var passwordSeguro = UtilesPassword.HashPassword(password);
        var passwordSalt = passwordSeguro.salt;
        var passwordHash = passwordSeguro.hash;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            var idRolAdministrador = UtilesRolUsuario.VerificarOCrearRolAdministrador();

            using (var comando = new MySqlCommand(
                       "INSERT INTO adv__cuenta_usuario (nombre, password_hash, password_salt, id_rol_usuario, administrador, aprobado) VALUES (@nombre, @passwordHash, @passwordSalt, @idRolUsuario, @administrador, @aprobado)",
                       conexion)) {
                comando.Parameters.AddWithValue("@nombre", nombreUsuario);
                comando.Parameters.AddWithValue("@passwordHash", passwordHash);
                comando.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                comando.Parameters.AddWithValue("@idRolUsuario", idRolAdministrador);
                comando.Parameters.AddWithValue("@administrador", true);
                comando.Parameters.AddWithValue("@aprobado", true);

                comando.ExecuteNonQuery();
            }
        }
    }
}