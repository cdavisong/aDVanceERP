using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos; 

public static class UtilesModulo {
    public static long ObtenerIdModulo(string nombreModulo) {
        var idModulo = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    $"SELECT id_modulo FROM adv__modulo WHERE LOWER(nombre) LIKE LOWER('%{nombreModulo}%');";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        idModulo = lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_modulo"));
                }
            }
        }

        return idModulo;
    }

    public static string? ObtenerNombreModulo(long idModulo) {
        var nombreModulo = string.Empty;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = $"SELECT nombre FROM adv__modulo WHERE id_modulo='{idModulo}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        nombreModulo = lectorDatos.GetString(lectorDatos.GetOrdinal("nombre"));
                }
            }
        }

        return nombreModulo;
    }

    public static string[] ObtenerNombresModulos() {
        var nombresModuloes = new List<string>();

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "SELECT nombre FROM adv__modulo;";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null)
                        while (lectorDatos.Read())
                            nombresModuloes.Add(lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")));
                }
            }
        }

        return nombresModuloes.ToArray();
    }
}