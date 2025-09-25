using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos; 

public static class UtilesContacto {
    public static async Task<long> ObtenerIdContacto(string nombreContacto) {
        var idContacto = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                await conexion.OpenAsync().ConfigureAwait(false);
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = $"SELECT id_contacto FROM adv__contacto WHERE nombre='{nombreContacto}';";

                using (var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false)) {
                    if (lectorDatos != null && await lectorDatos.ReadAsync().ConfigureAwait(false))
                        idContacto = lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_contacto"));
                }
            }
        }

        return idContacto;
    }

    public static string? ObtenerNombreContacto(long idContacto) {
        var nombreContacto = string.Empty;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = $"SELECT nombre FROM adv__contacto WHERE id_contacto='{idContacto}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        nombreContacto = lectorDatos.GetString(lectorDatos.GetOrdinal("nombre"));
                }
            }
        }

        return nombreContacto;
    }

    public static string? ObtenerCorreoElectronicoContacto(long idContacto) {
        var correoElectronicoContacto = string.Empty;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            } catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = $"SELECT direccion_correo_electronico FROM adv__contacto WHERE id_contacto = {idContacto};";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        correoElectronicoContacto = lectorDatos.GetString(lectorDatos.GetOrdinal("direccion_correo_electronico"));
                }
            }
        }

        return correoElectronicoContacto;
    }

    public static object[] ObtenerNombresContactos() {
        var nombresContactos = new List<string>();

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "SELECT nombre FROM adv__contacto;";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null)
                        while (lectorDatos.Read())
                            nombresContactos.Add(lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")));
                }
            }
        }

        return nombresContactos.ToArray();
    }
}