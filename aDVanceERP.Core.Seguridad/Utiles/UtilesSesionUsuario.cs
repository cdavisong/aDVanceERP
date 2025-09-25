using System.Security.Cryptography;

using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Seguridad.Utiles; 

public static class UtilesSesionUsuario {
    public static string GenerarToken() {
        using (var rng = new RNGCryptoServiceProvider()) {
            var tokenData = new byte[32];
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }

    public static void IniciarSesion(int idCuentaUsuario) {
        var token = GenerarToken();
        var fechaInicio = DateTime.Now;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            // Asegurarse de que no haya sesiones activas para este usuario
            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    "SELECT * FROM adv__sesion_usuario WHERE id_cuenta_usuario = @idCuentaUsuario AND fecha_fin IS NULL";
                comando.Parameters.AddWithValue("@idCuentaUsuario", idCuentaUsuario);

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos.HasRows)
                        throw new InvalidOperationException("El usuario ya está logueado en otra sesión.");
                }
            }

            // Insertar nueva sesión
            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    "INSERT INTO adv__sesion_usuario (id_cuenta_usuario, token, fecha_inicio) VALUES (@idCuentaUsuario, @token, @fechaInicio)";
                comando.Parameters.AddWithValue("@idCuentaUsuario", idCuentaUsuario);
                comando.Parameters.AddWithValue("@token", token);
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                comando.ExecuteNonQuery();
            }
        }
    }

    public static void CerrarSesion(string token) {
        var fechaFin = DateTime.Now;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    "UPDATE adv__sesion_usuario SET fecha_fin = @fechaFin WHERE token = @token AND fecha_fin IS NULL";
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);
                comando.Parameters.AddWithValue("@token", token);
                comando.ExecuteNonQuery();
            }
        }
    }

    public static bool ValidarToken(string token) {
        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "SELECT * FROM adv__sesion_usuario WHERE token = @token AND fecha_fin IS NULL";
                comando.Parameters.AddWithValue("@token", token);

                using (var lectorDatos = comando.ExecuteReader()) {
                    return lectorDatos.HasRows;
                }
            }
        }
    }

    public static TimeSpan ObtenerTiempoDeTrabajo(string token) {
        var tiempoDeTrabajo = TimeSpan.Zero;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "SELECT fecha_inicio, fecha_fin FROM adv__sesion_usuario WHERE token = @token";
                comando.Parameters.AddWithValue("@token", token);

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos.Read()) {
                        var fechaInicio = lectorDatos.GetDateTime("fecha_inicio");
                        var fechaFin = lectorDatos.IsDBNull(lectorDatos.GetOrdinal("fecha_fin"))
                            ? DateTime.Now
                            : lectorDatos.GetDateTime("fecha_fin");

                        tiempoDeTrabajo = fechaFin - fechaInicio;
                    }
                }
            }
        }

        return tiempoDeTrabajo;
    }
}