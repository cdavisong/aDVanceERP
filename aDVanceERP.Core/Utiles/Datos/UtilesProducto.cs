using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Utiles.Datos; 

public static class UtilesProducto {
    // Método auxiliar para ejecutar consultas y devolver un valor escalar
    private static async Task<T?> EjecutarConsultaEscalar<T>(string query, Func<MySqlDataReader, T> mapper,
        params MySqlParameter[]? parameters) {
        await using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                await conexion.OpenAsync().ConfigureAwait(false);

                await using var comando = new MySqlCommand(query, conexion);

                if (parameters != null && parameters.Length > 0)
                    comando.Parameters.AddRange(parameters);

                await using var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false);

                if (!await lectorDatos.ReadAsync().ConfigureAwait(false)) 
                    return default;
                
                // Verificar si el valor es DBNull antes de mapear
                return !lectorDatos.IsDBNull(0) ? mapper((MySqlDataReader)lectorDatos) : default;
            } catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            } catch (Exception ex) {
                throw new Exception("Error inesperado al ejecutar la consulta.", ex);
            }
        }
    }

    // Método auxiliar para ejecutar consultas y devolver una lista
    private static async Task<List<T>> EjecutarConsultaLista<T>(string query, Func<MySqlDataReader, T> mapper,
        params MySqlParameter[]? parameters) {
        var resultados = new List<T>();

        await using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                await conexion.OpenAsync().ConfigureAwait(false);

                await using (var comando = new MySqlCommand(query, conexion)) {
                    if (parameters != null) comando.Parameters.AddRange(parameters);

                    await using (var lectorDatos = await comando.ExecuteReaderAsync().ConfigureAwait(false)) {
                        while (await lectorDatos.ReadAsync().ConfigureAwait(false))
                            if (!lectorDatos.IsDBNull(0))
                                resultados.Add(mapper((MySqlDataReader)lectorDatos));
                    }
                }
            }
            catch (MySqlException) {
                throw new ExcepcionConexionServidorMySQL();
            }
            catch (Exception ex) {
                throw new Exception("Error inesperado al ejecutar la consulta.", ex);
            }
        }

        return resultados;
    }

    public static async Task<long> ObtenerIdProducto(string nombreProducto) {
        const string query = """
                             SELECT
                                id_producto
                             FROM adv__producto
                             WHERE nombre = @nombreProducto;
                             """;
        var parametros = new[] {
            new MySqlParameter("@nombreProducto", nombreProducto)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetInt64("id_producto"), parametros);
    }

    public static async Task<string?> ObtenerNombreProducto(long idProducto) {
        const string query = """
                             SELECT
                                nombre
                             FROM adv__producto
                             WHERE id_producto = @idProducto;
                             """;
        var parametros = new[] {
            new MySqlParameter("@idProducto", idProducto)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetString(lector.GetOrdinal("nombre")),
            parametros);
    }

    public static async Task<string?> ObtenerNombreProducto(string codigo) {
        const string query = """
                             SELECT
                                nombre
                             FROM adv__producto
                             WHERE codigo = @codigo;
                             """;
        var parametros = new[] {
            new MySqlParameter("@codigo", codigo)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetString(lector.GetOrdinal("nombre")),
            parametros);
    }

    public static async Task<string?> ObtenerCategoriaProducto(long idProducto) {
        const string query = """
                             SELECT
                                categoria
                             FROM adv__producto
                             WHERE id_producto = @idProducto;
                             """;
        var parametros = new[] {
            new MySqlParameter("@idProducto", idProducto)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetString(lector.GetOrdinal("categoria")), parametros);
    }

    public static async Task<string[]> ObtenerNombresProductos(long idAlmacen = 0, string categoria = "Todas", bool soloProductosVenta = false, bool incluirDescripcion = false) {
        var parametros = new List<MySqlParameter>();
        var condiciones = new List<string>();

        string query = """
            SELECT p.nombre
            """;

        if (incluirDescripcion) {
            query += ", dp.descripcion";
        }

        query += """

            FROM adv__producto p
            JOIN adv__inventario pa ON p.id_producto = pa.id_producto
            LEFT JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
            """;

        if (idAlmacen != 0) {
            condiciones.Add("pa.id_almacen = @IdAlmacen");
            parametros.Add(new MySqlParameter("@IdAlmacen", idAlmacen));
        }

        if (!categoria.Equals("Todas", StringComparison.OrdinalIgnoreCase)) {
            condiciones.Add("p.categoria = @Categoria");
            parametros.Add(new MySqlParameter("@Categoria", categoria));
        }

        if (soloProductosVenta) {
            condiciones.Add("p.es_vendible = @EsVendible");
            parametros.Add(new MySqlParameter("@EsVendible", true));
        }

        if (condiciones.Count > 0) {
            query += " WHERE " + string.Join(" AND ", condiciones);
        }

        var nombres = await EjecutarConsultaLista(query, lector =>
        {
            var nombre = lector.GetString(lector.GetOrdinal("nombre"));
            var descripcion = incluirDescripcion ?
                (lector.IsDBNull(lector.GetOrdinal("descripcion")) ?
                    string.Empty :
                    lector.GetString(lector.GetOrdinal("descripcion"))) :
                string.Empty;

            return $"{nombre}{(incluirDescripcion ? $"|{descripcion}" : string.Empty)}";
        }, parametros.ToArray());

        return nombres.ToArray();
    }

    public static async Task<decimal> ObtenerStockTotalProductos() {
        const string query = """
                             SELECT
                                SUM(aa.cantidad) AS total_productos
                             FROM adv__inventario aa
                             JOIN adv__producto a ON aa.id_producto = a.id_producto;
                             """;

        return await EjecutarConsultaEscalar(query, lector => lector.GetDecimal(lector.GetOrdinal("total_productos")));
    }

    public static async Task<decimal> ObtenerStockTotalProducto(long idProducto) {
        // Usamos COALESCE para devolver 0 si SUM(cantidad) es NULL
        const string query = """
                             SELECT
                                COALESCE(SUM(cantidad), 0) as stock_total
                             FROM adv__inventario
                             WHERE id_producto = @IdProducto;
                             """;
        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetDecimal(lector.GetOrdinal("stock_total")),
            parametros);
    }

    public static async Task<decimal> ObtenerStockProducto(string nombreProducto, string? nombreAlmacen) {
        const string query = """
                             SELECT
                                 aa.cantidad
                             FROM adv__inventario aa
                             JOIN adv__producto ar ON aa.id_producto = ar.id_producto
                             JOIN adv__almacen al ON aa.id_almacen = al.id_almacen
                             WHERE ar.nombre = @NombreProducto AND al.nombre = @NombreAlmacen;
                             """;
        var parametros = new[] {
            new MySqlParameter("@NombreProducto", nombreProducto),
            new MySqlParameter("@NombreAlmacen", nombreAlmacen)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetDecimal(lector.GetOrdinal("cantidad")), parametros);
    }

    public static async Task<Dictionary<long, decimal>> ObtenerStockProducto(long idProducto) {
        const string query = """
        SELECT 
            pa.id_almacen,
            pa.cantidad,
            um.abreviatura
        FROM adv__inventario pa
        JOIN adv__producto p ON pa.id_producto = p.id_producto
        JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
        JOIN adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
        WHERE pa.id_producto = @IdProducto;
        """;

        var parametros = new[] {
        new MySqlParameter("@IdProducto", idProducto)
    };

        var resultados = await EjecutarConsultaLista(query,
            lector => new {
                IdAlmacen = lector.GetInt64("id_almacen"),
                Stock = lector.GetDecimal("cantidad"),
                Unidad = lector.GetString("abreviatura")
            }, parametros);

        return resultados.ToDictionary(
            x => x.IdAlmacen,
            x => x.Stock // Aquí podrías aplicar conversiones si es necesario
        );
    }

    public static async Task<decimal> ObtenerPrecioVentaBase(long idProducto) {
        const string query = """
                             SELECT precio_venta_base
                             FROM adv__producto
                             WHERE id_producto = @IdProducto;
                             """;
        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto)
        };

        return await EjecutarConsultaEscalar(query, lector => lector.GetDecimal(lector.GetOrdinal("precio_venta_base")),
            parametros);
    }

    public static bool ActualizarPrecioVentaBase(long idProducto, decimal nuevoPrecioVenta) {
        const string queryVerificar = """
                                      SELECT precio_venta_base
                                      FROM adv__producto
                                      WHERE id_producto = @IdProducto;
                                      """;

        const string queryActualizar = """
                                       UPDATE adv__producto
                                       SET precio_venta_base = @PrecioVenta
                                       WHERE id_producto = @IdProducto;
                                       """;

        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto),
            new MySqlParameter("@PrecioVenta", nuevoPrecioVenta)
        };

        try {
            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                // Primero verificar el precio actual
                decimal precioActual;
                using (var comandoVerificar = new MySqlCommand(queryVerificar, conexion)) {
                    comandoVerificar.Parameters.Add(parametros[0]);
                    precioActual = Convert.ToDecimal(comandoVerificar.ExecuteScalar());
                }

                // Solo actualizar si es diferente
                if (precioActual == nuevoPrecioVenta)
                    return false; // No se actualizó porque el precio es igual

                using (var comandoActualizar = new MySqlCommand(queryActualizar, conexion)) {
                    comandoActualizar.Parameters.AddRange(parametros);
                    return comandoActualizar.ExecuteNonQuery() > 0;
                }
            }
        }
        catch {
            return false;
        }
    }

    public static async Task<decimal> ObtenerCostoUnitario(long idProducto) {
        var categoria = await ObtenerCategoriaProducto(idProducto);
        var column = categoria.Equals("ProductoTerminado") ? "costo_produccion_unitario" : "precio_compra";
        var query = $"""
            SELECT {column}
            FROM adv__producto
            WHERE id_producto = @IdProducto;
            """;

        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto)
        };

        return await EjecutarConsultaEscalar(query,
            lector => lector.GetDecimal(lector.GetOrdinal($"{column}")), parametros);
    }

    public static async Task<decimal> ObtenerPrecioCompra(long idProducto) {
        const string query = """
                             SELECT precio_compra
                             FROM adv__producto
                             WHERE id_producto = @IdProducto;
                             """;
        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto)
        };

        return await EjecutarConsultaEscalar(query,
            lector => lector.GetDecimal(lector.GetOrdinal("precio_compra")), parametros);
    }

    public static async Task<decimal> ObtenerCostoProduccionUnitario(long idProducto) {
        const string query = """
                             SELECT costo_produccion_unitario
                             FROM adv__producto
                             WHERE id_producto = @IdProducto;
                             """;
        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto)
        };

        return await EjecutarConsultaEscalar(query,
            lector => lector.GetDecimal(lector.GetOrdinal("costo_produccion_unitario")), parametros);
    }

    public static bool ActualizarPrecioCompra(long idProducto, decimal nuevoPrecioCompra) {
        const string queryVerificar = """
                                      SELECT precio_compra
                                      FROM adv__producto
                                      WHERE id_producto = @IdProducto;
                                      """;

        const string queryActualizar = """
                                       UPDATE adv__producto
                                       SET precio_compra = @PrecioCompra
                                       WHERE id_producto = @IdProducto;
                                       """;

        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto),
            new MySqlParameter("@PrecioCompra", nuevoPrecioCompra)
        };

        try {
            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                // Verificar el precio actual
                decimal precioActual;
                using (var comandoVerificar = new MySqlCommand(queryVerificar, conexion)) {
                    comandoVerificar.Parameters.Add(parametros[0]);
                    precioActual = Convert.ToDecimal(comandoVerificar.ExecuteScalar());
                }

                // Solo actualizar si es diferente
                if (precioActual == nuevoPrecioCompra)
                    return false; // No se actualizó porque el precio es igual

                using (var comandoActualizar = new MySqlCommand(queryActualizar, conexion)) {
                    comandoActualizar.Parameters.AddRange(parametros);
                    return comandoActualizar.ExecuteNonQuery() > 0;
                }
            }
        }
        catch {
            return false;
        }
    }

    public static bool ActualizarCostoProduccionUnitario(long idProducto, decimal nuevoCostoProduccion) {
        const string queryVerificar = """
                                      SELECT costo_produccion_unitario
                                      FROM adv__producto
                                      WHERE id_producto = @IdProducto;
                                      """;
        const string queryActualizar = """
                                       UPDATE adv__producto
                                       SET costo_produccion_unitario = @CostoProduccion
                                       WHERE id_producto = @IdProducto;
                                       """;
        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto),
            new MySqlParameter("@CostoProduccion", nuevoCostoProduccion)
        };

        try {
            using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();

                // Verificar el costo actual
                decimal costoActual;
                using (var comandoVerificar = new MySqlCommand(queryVerificar, conexion)) {
                    comandoVerificar.Parameters.Add(parametros[0]);
                    costoActual = Convert.ToDecimal(comandoVerificar.ExecuteScalar());
                }

                // Solo actualizar si es diferente
                if (costoActual == nuevoCostoProduccion)
                    return false; // No se actualizó porque el costo es igual

                using (var comandoActualizar = new MySqlCommand(queryActualizar, conexion)) {
                    comandoActualizar.Parameters.AddRange(parametros);
                    return comandoActualizar.ExecuteNonQuery() > 0;
                }
            }
        } catch {
            return false;
        }
    }

    public static async Task<bool> PuedeEliminarProducto(long idProducto) {
        const string queryVentas = """
                                   SELECT
                                    COUNT(*)
                                   FROM adv__detalle_venta_producto
                                   WHERE id_producto = @IdProducto;
                                   """;
        const string queryMovimientos = """
                                        SELECT
                                            COUNT(*)
                                        FROM adv__movimiento
                                        WHERE id_producto = @IdProducto;
                                        """;
        var parametros = new[] {
            new MySqlParameter("@IdProducto", idProducto)
        };
        var cantidadVentas = await EjecutarConsultaEscalar(queryVentas, lector => lector.GetInt32(0), parametros);
        var cantidadMovimientos =
            await EjecutarConsultaEscalar(queryMovimientos, lector => lector.GetInt32(0), parametros);

        return cantidadVentas == 0 && cantidadMovimientos == 0;
    }
}