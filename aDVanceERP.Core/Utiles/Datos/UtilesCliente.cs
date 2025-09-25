using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos;

public static class UtilesCliente {
    private static T? EjecutarConsulta<T>(string query, Func<MySqlDataReader, T> procesarResultado) {
        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());

        try {
            if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
        } catch (Exception) {
            throw new ExcepcionConexionServidorMySQL();
        }

        using var comando = conexion.CreateCommand();
        comando.CommandText = query;

        using var lectorDatos = comando.ExecuteReader();
        return lectorDatos.Read() ? procesarResultado(lectorDatos) : default;
    }

    private static List<T> EjecutarConsultaMultiple<T>(string query, Func<MySqlDataReader, T> procesarFila) {
        var resultados = new List<T>();

        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());

        try {
            if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
        } catch (Exception) {
            throw new ExcepcionConexionServidorMySQL();
        }

        using var comando = conexion.CreateCommand();
        comando.CommandText = query;

        using var lectorDatos = comando.ExecuteReader();
        while (lectorDatos.Read()) {
            resultados.Add(procesarFila(lectorDatos));
        }

        return resultados;
    }

    public static long ObtenerIdCliente(string? razonSocialCliente) {
        string query = $"SELECT id_cliente FROM adv__cliente WHERE razon_social='{razonSocialCliente}';";
        return EjecutarConsulta(query, lector => lector.GetInt32(lector.GetOrdinal("id_cliente")));
    }

    public static string? ObtenerRazonSocialCliente(long idCliente) {
        string query = $"SELECT razon_social FROM adv__cliente WHERE id_cliente='{idCliente}';";
        return EjecutarConsulta(query, lector => lector.GetString(lector.GetOrdinal("razon_social")));
    }

    public static string[] ObtenerRazonesSocialesClientes() {
        string query = "SELECT razon_social FROM adv__cliente;";
        return EjecutarConsultaMultiple(query, lector => lector.GetString(lector.GetOrdinal("razon_social"))).ToArray();
    }

    public static string? ObtenerDireccionCliente(long idCliente) {
        string query = $@"SELECT co.direccion FROM adv__contacto co 
                         JOIN adv__cliente cl ON co.id_contacto = cl.id_contacto 
                         WHERE id_cliente = '{idCliente}';";

        return EjecutarConsulta(query, lector => lector.GetString(lector.GetOrdinal("direccion")));
    }

    public static string? ObtenerNumeroCliente(long idCliente) {
        string query = $@"SELECT numero FROM adv__cliente
                         WHERE id_cliente = '{idCliente}';";

        return EjecutarConsulta(query, lector => lector.GetString(lector.GetOrdinal("numero")));
    }
}