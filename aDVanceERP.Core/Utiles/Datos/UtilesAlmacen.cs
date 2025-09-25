using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos; 

public static class UtilesAlmacen {
    private static async Task<T?> EjecutarConsultaAsync<T>(string query, Func<MySqlDataReader, T> procesarResultado, params MySqlParameter[] parametros) {
        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());

        try {
            await conexion.OpenAsync().ConfigureAwait(false);
        } catch (MySqlException) {
            throw new ExcepcionConexionServidorMySQL();
        }

        using var comando = new MySqlCommand(query, conexion);
        comando.Parameters.AddRange(parametros);

        using var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false);
        return lectorDatos != null && await lectorDatos.ReadAsync().ConfigureAwait(false)
            ? procesarResultado((MySqlDataReader) lectorDatos)
            : default;
    }

    private static T? EjecutarConsulta<T>(string query, Func<MySqlDataReader, T> procesarResultado, params MySqlParameter[] parametros) {
        using var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion());
        try {
            if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
        } catch (MySqlException) {
            throw new ExcepcionConexionServidorMySQL();
        }

        using var comando = new MySqlCommand(query, conexion);
        comando.Parameters.AddRange(parametros);

        using var lectorDatos = comando.ExecuteReader();
        return lectorDatos != null && lectorDatos.Read()
            ? procesarResultado(lectorDatos)
            : default;
    }

    public static async Task<long> ObtenerIdAlmacen(string? nombreAlmacen) {
        const string query = "SELECT id_almacen FROM adv__almacen WHERE LOWER(nombre) LIKE LOWER(@NombreAlmacen);";
        var result = await EjecutarConsultaAsync(query, lector => lector.GetInt32("id_almacen"),
            new MySqlParameter("@NombreAlmacen", $"%{nombreAlmacen}%"));
        return result != 0 ? result : 0;
    }

    public static string? ObtenerNombreAlmacen(long idAlmacen) {
        const string query = "SELECT nombre FROM adv__almacen WHERE id_almacen = @IdAlmacen;";
        return EjecutarConsulta(query, lector => lector.GetString("nombre"),
            new MySqlParameter("@IdAlmacen", idAlmacen));
    }

    public static object[] ObtenerNombresAlmacenes(bool autorizoVenta = false) {
        var query = autorizoVenta
            ? "SELECT nombre FROM adv__almacen WHERE autorizo_venta = 1;"
            : "SELECT nombre FROM adv__almacen;";
        return EjecutarConsulta(query, lector => {
            var nombres = new List<string>();

            do {
                nombres.Add(lector.GetString("nombre"));
            } while (lector.Read());

            return nombres.ToArray();
        }) ?? Array.Empty<object>();
    }

    public static object[] ObtenerNombresAlmacenesProductosTerminados() {
        var query = """
            SELECT DISTINCT a.nombre 
            FROM adv__almacen a
            JOIN adv__inventario pa ON a.id_almacen = pa.id_almacen
            JOIN adv__producto p ON pa.id_producto = p.id_producto
            WHERE a.autorizo_venta = 0 
            AND p.categoria = 'ProductoTerminado'
            AND pa.cantidad > 0;
            """;

        return EjecutarConsulta(query, lector => {
            var nombres = new List<string>();

            do {
                nombres.Add(lector.GetString("nombre"));
            } while (lector.Read());

            return nombres.ToArray();
        }) ?? Array.Empty<object>();
    }

    public static string ObtenerProductosAlmacenJson(long idAlmacen) {
        const string query = @"
        SELECT 
            p.id_producto,
            p.codigo,
            p.nombre,
            p.categoria,
            p.precio_compra,
            p.costo_produccion_unitario,
            p.precio_venta_base,
            pa.cantidad,
            a.nombre AS nombre_almacen,
            IFNULL(um.nombre, '') AS unidad_medida,
            IFNULL(um.abreviatura, '') AS abreviatura_medida
        FROM 
            adv__producto p
        JOIN 
            adv__inventario pa ON p.id_producto = pa.id_producto
        JOIN 
            adv__almacen a ON pa.id_almacen = a.id_almacen
        LEFT JOIN
            adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
        LEFT JOIN
            adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
        WHERE 
            pa.id_almacen = @IdAlmacen;";

        try {
            var productos = EjecutarConsulta(query, lector => {
                var listaProductos = new List<Dictionary<string, object>>();

                do {
                    var producto = new Dictionary<string, object> {
                        ["id_producto"] = lector.GetInt32("id_producto"),
                        ["codigo"] = lector.GetString("codigo"),
                        ["nombre"] = lector.GetString("nombre"),
                        ["categoria"] = lector.GetString("categoria"),
                        ["precio_compra"] = lector.GetDecimal("precio_compra"),
                        ["costo_produccion_unitario"] = lector.GetDecimal("costo_produccion_unitario"),
                        ["precio_venta_base"] = lector.GetDecimal("precio_venta_base"),
                        ["cantidad"] = lector.GetInt32("cantidad"),
                        ["nombre_almacen"] = lector.GetString("nombre_almacen"),
                        ["unidad_medida"] = lector.GetString("unidad_medida"),
                        ["abreviatura_medida"] = lector.GetString("abreviatura_medida")
                    };

                    listaProductos.Add(producto);
                } while (lector.Read());

                return listaProductos;
            }, new MySqlParameter("@IdAlmacen", idAlmacen)) ?? new List<Dictionary<string, object>>();

            return System.Text.Json.JsonSerializer.Serialize(productos, new System.Text.Json.JsonSerializerOptions {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        } catch (Exception ex) {
            throw new Exception($"Error al exportar productos del almacén: {ex.Message}", ex);
        }
    }
}
