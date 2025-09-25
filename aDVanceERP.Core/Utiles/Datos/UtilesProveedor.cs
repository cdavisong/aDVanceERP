using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos; 

public static class UtilesProveedor {
    public static async Task<long> ObtenerIdProveedor(string? nombreProveedor) {
        var idProveedor = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                await conexion.OpenAsync().ConfigureAwait(false);
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    $"SELECT id_proveedor FROM adv__proveedor WHERE razon_social='{nombreProveedor}';";

                using (var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false)) {
                    if (lectorDatos != null && await lectorDatos.ReadAsync().ConfigureAwait(false))
                        idProveedor = lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_proveedor"));
                }
            }
        }

        return idProveedor;
    }

    public static string? ObtenerRazonSocialProveedor(long idProveedor) {
        var razonSocialProveedor = string.Empty;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = $"SELECT razon_social FROM adv__proveedor WHERE id_proveedor='{idProveedor}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        razonSocialProveedor = lectorDatos.GetString(lectorDatos.GetOrdinal("razon_social"));
                }
            }
        }

        return razonSocialProveedor;
    }

    public static object[] ObtenerRazonesSocialesProveedores() {
        var nombresProveedores = new List<string>();

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "SELECT razon_social FROM adv__proveedor;";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null)
                        while (lectorDatos.Read())
                            nombresProveedores.Add(lectorDatos.GetString(lectorDatos.GetOrdinal("razon_social")));
                }
            }
        }

        return nombresProveedores.ToArray();
    }
}