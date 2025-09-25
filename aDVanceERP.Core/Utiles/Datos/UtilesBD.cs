using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos; 

public static class UtilesBD {
    public static long ObtenerUltimoIdTabla(string nombreEntidad) {
        var ultimoId = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    $"SELECT id_{nombreEntidad} FROM adv__{nombreEntidad} ORDER BY id_{nombreEntidad} DESC LIMIT 1;";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        ultimoId = lectorDatos.GetInt32(lectorDatos.GetOrdinal($"id_{nombreEntidad}"));
                }
            }
        }

        return ultimoId;
    }
}