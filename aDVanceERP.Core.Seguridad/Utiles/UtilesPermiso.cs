using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Seguridad.Utiles; 

public static class UtilesPermiso {
    public static long ObtenerTotalPermisos() {
        var totalPermisos = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "SELECT COUNT(id_permiso) AS total_permisos FROM adv__permiso;";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        totalPermisos = lectorDatos.GetInt32(lectorDatos.GetOrdinal("total_permisos"));
                }
            }
        }

        return totalPermisos;
    }

    public static long ObtenerIdPermiso(string nombrePermiso) {
        var idPermiso = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    $"SELECT id_permiso FROM adv__permiso WHERE LOWER(nombre) LIKE LOWER('%{nombrePermiso}%');";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        idPermiso = lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_permiso"));
                }
            }
        }

        return idPermiso;
    }

    public static string? ObtenerNombrePermiso(long idPermiso) {
        var nombrePermiso = string.Empty;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = $"SELECT nombre FROM adv__permiso WHERE id_permiso='{idPermiso}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        nombrePermiso = lectorDatos.GetString(lectorDatos.GetOrdinal("nombre"));
                }
            }
        }

        return nombrePermiso;
    }

    public static string[] ObtenerNombresPermisos(long idModulo = 0) {
        var nombresPermisos = new List<string>();

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = idModulo == 0
                    ? "SELECT nombre FROM adv__permiso;"
                    : $"SELECT nombre FROM adv__permiso WHERE id_modulo='{idModulo}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null)
                        while (lectorDatos.Read())
                            nombresPermisos.Add(lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")));
                }
            }
        }

        return nombresPermisos.ToArray();
    }

    public static bool ContienePermisoParcial(this string[] permisos, string value) {
        return permisos?.Any(p => p.Contains(value, StringComparison.OrdinalIgnoreCase)) ?? false;
    }

    public static bool ContienePermisoExacto(this string[] permisos, string value) {
        return permisos?.Any(p => p.Equals(value, StringComparison.OrdinalIgnoreCase)) ?? false;
    }
}