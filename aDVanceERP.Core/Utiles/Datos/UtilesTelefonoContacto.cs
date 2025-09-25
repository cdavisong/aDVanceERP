using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos;

public static class UtilesTelefonoContacto {
    private static string? ObtenerTelefonoDesdeBD(string query) {
        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());

        try {
            if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
        } catch (Exception) {
            throw new ExcepcionConexionServidorMySQL();
        }

        using var comando = conexion.CreateCommand();
        comando.CommandText = query;

        using var lectorDatos = comando.ExecuteReader();

        return lectorDatos.Read() ? lectorDatos.GetString(lectorDatos.GetOrdinal("numero")) : null;
    }

    public static string? ObtenerTelefonoContacto(long idContacto, bool categoriaMovil) {
        string query = $@"SELECT numero FROM adv__telefono_contacto 
                         WHERE id_contacto='{idContacto}' 
                         AND categoria='{(categoriaMovil ? "Movil" : "Fijo")}';";

        return ObtenerTelefonoDesdeBD(query);
    }

    public static string? ObtenerTelefonoCliente(long idCliente, bool categoriaMovil) {
        string query = $@"SELECT tc.numero FROM adv__telefono_contacto tc 
                         JOIN adv__contacto co ON tc.id_contacto = co.id_contacto 
                         JOIN adv__cliente cl ON co.id_contacto = cl.id_contacto 
                         WHERE cl.id_cliente = '{idCliente}' 
                         AND tc.categoria = '{(categoriaMovil ? "Movil" : "Fijo")}';";

        return ObtenerTelefonoDesdeBD(query);
    }
}