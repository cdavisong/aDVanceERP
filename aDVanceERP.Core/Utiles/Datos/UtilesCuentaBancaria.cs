using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos; 

public static class UtilesCuentaBancaria {
    public static string NumeroConfirmacion = string.Empty;

    public static long ObtenerIdCuenta(string aliasCuenta) {
        var idCuenta = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    $"SELECT id_cuenta_bancaria FROM adv__cuenta_bancaria WHERE alias='{aliasCuenta}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        idCuenta = lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_cuenta_bancaria"));
                }
            }
        }

        return idCuenta;
    }

    public static string? ObtenerAliasCuenta(long idCuenta) {
        var aliasCuenta = string.Empty;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = $"SELECT alias FROM adv__cuenta_bancaria WHERE id_cuenta_bancaria='{idCuenta}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        aliasCuenta = lectorDatos.GetString(lectorDatos.GetOrdinal("alias"));
                }
            }
        }

        return aliasCuenta;
    }

    public static string[] ObtenerAliasesCuentas() {
        var aliasesCuentas = new List<string>();

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "SELECT alias FROM adv__cuenta_bancaria;";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null)
                        while (lectorDatos.Read())
                            aliasesCuentas.Add(lectorDatos.GetString(lectorDatos.GetOrdinal("alias")));
                }
            }
        }

        return aliasesCuentas.ToArray();
    }

    public static string? ObtenerNumeroTarjeta(long idCuenta) {
        var numeroTarjeta = string.Empty;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    $"SELECT numero_tarjeta FROM adv__cuenta_bancaria WHERE id_cuenta_bancaria='{idCuenta}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        numeroTarjeta = lectorDatos.GetString(lectorDatos.GetOrdinal("numero_tarjeta"));
                }
            }
        }

        return numeroTarjeta;
    }

    public static long ObtenerIdPropietario(long idCuenta) {
        var idPropietario = 0;

        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            }
            catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText =
                    $"SELECT id_contacto FROM adv__cuenta_bancaria WHERE id_cuenta_bancaria='{idCuenta}';";

                using (var lectorDatos = comando.ExecuteReader()) {
                    if (lectorDatos != null && lectorDatos.Read())
                        idPropietario = lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_contacto"));
                }
            }
        }

        return idPropietario;
    }
}