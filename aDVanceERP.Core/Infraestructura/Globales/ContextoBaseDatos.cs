using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Modelos.BD;

using MySql.Data.MySqlClient;

using System.Data;

namespace aDVanceERP.Core.Infraestructura.Globales;

public static class ContextoBaseDatos {
    private static readonly ConfiguracionBaseDatos _configuracion = ConfiguracionBaseDatos.Default;
    private static readonly object _lockObject = new();

    static ContextoBaseDatos() {

    }

    public static bool EsConfiguracionCargada { get; private set; }

    public static ConfiguracionBaseDatos Configuracion => _configuracion;

    public static bool AbrirConexion(MySqlConnection conexion) {
        if (!EsConfiguracionCargada)
            throw new InvalidOperationException("La configuración de la base de datos no ha sido cargada.");

        if (conexion.State == ConnectionState.Open)
            return true;

        try {
            conexion.Open();
            return true;
        }
        catch (MySqlException ex) {
            throw new InvalidOperationException("Error al abrir la conexión a la base de datos.", ex);
        }
    }

    public static void CerrarConexion(MySqlConnection conexion) {
        if (conexion?.State == ConnectionState.Open)
            conexion.Close();
    }

    #region Métodos optimizados de ejecución de consultas

    public static MySqlCommand CrearComando(string consulta, Dictionary<string, object>? parametros = null, MySqlConnection? conexion = null) {
        ValidarConfiguracionCargada();

        conexion ??= ObtenerConexionOptimizada();

        AbrirConexion(conexion);

        var comando = new MySqlCommand(consulta, conexion) {
            CommandType = CommandType.Text,
            CommandTimeout = 120
        };

        if (parametros != null)
            foreach (var parametro in parametros)
                comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);

        return comando;
    }

    public static IEnumerable<T> EjecutarConsulta<T>(string consulta, Dictionary<string, object>? parametros, Func<MySqlDataReader, T> mapeador, MySqlConnection? conexion = null) {
        ValidarConfiguracionCargada();

        var conexionLocal = conexion == null; // Determinar si es una conexión local
        using var comando = CrearComando(consulta, parametros, conexion);
        using var lectorDatos = comando.ExecuteReader();

        while (lectorDatos.Read()) {
            yield return mapeador(lectorDatos);
        }

        if (conexionLocal)
            CerrarConexion(comando.Connection);
    }

    public static T? EjecutarConsultaEscalar<T>(string consulta, Dictionary<string, object>? parametros = null, MySqlConnection? conexion = null) {
        ValidarConfiguracionCargada();

        var conexionLocal = conexion == null;
        using var comando = CrearComando(consulta, parametros, conexion);
        var resultado = comando.ExecuteScalar();

        if (conexionLocal)
            CerrarConexion(comando.Connection);

        return resultado == null || resultado == DBNull.Value
            ? default
            : (T)Convert.ChangeType(resultado, typeof(T));
    }

    public static int EjecutarComandoNoQuery(string consulta, Dictionary<string, object>? parametros, MySqlConnection? conexion = null, MySqlTransaction? transaccion = null) {
        ValidarConfiguracionCargada();

        using var comando = CrearComando(consulta, parametros, conexion);

        if (transaccion != null)
            comando.Transaction = transaccion;

        var filasAfectadas = comando.ExecuteNonQuery();

        if (conexion == null)
            CerrarConexion(comando.Connection);

        return filasAfectadas;
    }

    public static long EjecutarComandoInsert(string consulta, Dictionary<string, object> parametros, MySqlConnection? conexion = null, MySqlTransaction? transaccion = null) {
        ValidarConfiguracionCargada();

        using var comando = CrearComando(consulta, parametros, conexion);

        if (transaccion != null)
            comando.Transaction = transaccion;

        comando.ExecuteNonQuery();
        var idInsertado = comando.LastInsertedId;

        if (conexion == null)
            CerrarConexion(comando.Connection);

        return idInsertado;
    }

    #endregion

    public static void ActualizarConfiguracion(ConfiguracionBaseDatos configuracion) {
        if (configuracion == null)
            throw new ArgumentNullException(nameof(configuracion), "La configuración de la base de datos no puede ser nula.");

        lock (_lockObject) {
            _configuracion.Servidor = configuracion.Servidor;
            _configuracion.BaseDatos = configuracion.BaseDatos;
            _configuracion.Usuario = configuracion.Usuario;
            _configuracion.Password = configuracion.Password;
            _configuracion.RecordarConfiguracion = configuracion.RecordarConfiguracion;

            // Validar la nueva configuración
            ValidarConexion();

            EsConfiguracionCargada = true;
        }
    }

    private static void ValidarConfiguracionCargada() {
        if (!EsConfiguracionCargada)
            throw new InvalidOperationException("La configuración de la base de datos no ha sido cargada.");
    }

    private static void ValidarConexion() {
        using var connection = new MySqlConnection(_configuracion.ToStringConexion());
        try {
            connection.Open();
        }
        catch (MySqlException ex) {
            throw new InvalidOperationException("Error al conectar a la base de datos con la nueva configuración.", ex);
        }
    }

    public static MySqlConnection ObtenerConexion() {
        ValidarConfiguracionCargada();

        var conexion = new MySqlConnection(_configuracion.ToStringConexion());

        AbrirConexion(conexion);

        return conexion;
    }

    public static MySqlConnection ObtenerConexionOptimizada() {
        ValidarConfiguracionCargada();

        var conexion = new MySqlConnection(_configuracion.ToStringConexion());

        return conexion;
    }
}