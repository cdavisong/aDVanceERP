using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Seguridad.Utiles; 

public static class UtilesSeguridadModulosAplicacion {
    public static void InicializarPermisosModulo(string nombreModulo, string[] nombresPermisos) {
        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var transaccion = conexion.BeginTransaction()) {
                try {
                    // Paso 1: Verificar si el módulo ya existe
                    var idModulo = ObtenerIdModulo(conexion, transaccion, nombreModulo);

                    if (idModulo == 0) {
                        idModulo = InsertarModulo(conexion, transaccion, nombreModulo);
                    }

                    // Paso 2: Insertar los permisos (si no existen)
                    foreach (var nombrePermiso in nombresPermisos)
                        if (!ExistePermiso(conexion, transaccion, idModulo, nombrePermiso)) {
                            InsertarPermiso(conexion, transaccion, idModulo, nombrePermiso);
                            Console.WriteLine($"Permiso '{nombrePermiso}' creado para el módulo '{nombreModulo}'.");
                        }
                        else {
                            Console.WriteLine($"Permiso '{nombrePermiso}' ya existe para el módulo '{nombreModulo}'.");
                        }

                    transaccion.Commit();
                }
                catch (Exception) {
                    transaccion.Rollback();
                    throw;
                }
            }
        }
    }

    private static long ObtenerIdModulo(MySqlConnection conexion, MySqlTransaction transaccion, string nombreModulo) {
        using (var comando = conexion.CreateCommand()) {
            comando.Transaction = transaccion;
            comando.CommandText = @"SELECT id_modulo FROM adv__modulo WHERE nombre = @nombreModulo;";
            comando.Parameters.AddWithValue("@nombreModulo", nombreModulo);

            var resultado = comando.ExecuteScalar();

            return resultado != null ? Convert.ToInt64(resultado) : 0;
        }
    }

    private static long InsertarModulo(MySqlConnection conexion, MySqlTransaction transaccion, string nombreModulo) {
        using (var comando = conexion.CreateCommand()) {
            comando.Transaction = transaccion;
            comando.CommandText = @"INSERT INTO adv__modulo (nombre) VALUES (@nombreModulo); SELECT LAST_INSERT_ID();";
            comando.Parameters.AddWithValue("@nombreModulo", nombreModulo);

            return Convert.ToInt64(comando.ExecuteScalar());
        }
    }

    private static bool ExistePermiso(MySqlConnection conexion, MySqlTransaction transaccion, long idModulo,
        string nombrePermiso) {
        using (var comando = conexion.CreateCommand()) {
            comando.Transaction = transaccion;
            comando.CommandText =
                @"SELECT COUNT(*) FROM adv__permiso WHERE id_modulo = @idModulo AND nombre = @nombrePermiso;";
            comando.Parameters.AddWithValue("@idModulo", idModulo);
            comando.Parameters.AddWithValue("@nombrePermiso", nombrePermiso);

            return Convert.ToInt32(comando.ExecuteScalar()) > 0;
        }
    }

    private static void InsertarPermiso(MySqlConnection conexion, MySqlTransaction transaccion, long idModulo,
        string nombrePermiso) {
        using (var comando = conexion.CreateCommand()) {
            comando.Transaction = transaccion;
            comando.CommandText = @"INSERT INTO adv__permiso (id_modulo, nombre) VALUES (@idModulo, @nombrePermiso);";
            comando.Parameters.AddWithValue("@idModulo", idModulo);
            comando.Parameters.AddWithValue("@nombrePermiso", nombrePermiso);
            comando.ExecuteNonQuery();
        }
    }
}