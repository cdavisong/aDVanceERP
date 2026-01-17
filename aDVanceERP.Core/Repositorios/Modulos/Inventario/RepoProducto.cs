using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoProducto : RepoEntidadBaseDatos<Producto, FiltroBusquedaProducto> {
    public RepoProducto() : base("adv__producto", "id_producto") { }

    protected override string GenerarComandoAdicionar(Producto objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
        var consulta = $"""
                INSERT INTO adv__producto (
                    ruta_imagen,
                    categoria,
                    nombre,
                    codigo,
                    id_proveedor,
                    descripcion,
                    id_unidad_medida,
                    id_clasificacion_producto,
                    es_vendible,
                    costo_adquisicion_unitario,
                    costo_produccion_unitario,
                    impuesto_venta_porcentaje,
                    margen_ganancia_deseado,
                    precio_venta_base,
                    activo
                )
                VALUES (
                    @RutaImagen,
                    @Categoria,
                    @Nombre,
                    @Codigo,
                    @IdProveedor,
                    @Descripcion,
                    @IdUnidadMedida,
                    @IdClasificacionProducto,
                    @EsVendible,
                    @CostoAdquisicionUnitario,
                    @CostoProduccionUnitario,
                    @ImpuestoVentaPorcentaje,
                    @MargenGananciaDeseado,
                    @PrecioVentaBase,
                    @Activo
                );
                """;

        parametros = new Dictionary<string, object> {
            {  "@RutaImagen", objeto.RutaImagen  },
            { "@Categoria", objeto.Categoria.ToString() },
            { "@Nombre", objeto.Nombre },
            { "@Codigo", objeto.Codigo },
            { "@IdProveedor", objeto.IdProveedor },
            { "@Descripcion", objeto.Descripcion },
            { "@IdUnidadMedida", objeto.IdUnidadMedida },
            { "@IdClasificacionProducto", objeto.IdClasificacionProducto },
            { "@EsVendible", objeto.EsVendible },
            { "@CostoAdquisicionUnitario", objeto.CostoAdquisicionUnitario.ToString("N2", CultureInfo.InvariantCulture) },
            { "@CostoProduccionUnitario", objeto.CostoProduccionUnitario.ToString("N2", CultureInfo.InvariantCulture) },
            { "@ImpuestoVentaPorcentaje", objeto.ImpuestoVentaPorcentaje.ToString("N2", CultureInfo.InvariantCulture) },
            { "@MargenGananciaDeseado", objeto.MargenGananciaDeseado.ToString("N2", CultureInfo.InvariantCulture) },
            { "@PrecioVentaBase", objeto.PrecioVentaBase.ToString("N2", CultureInfo.InvariantCulture) },
            { "@Activo", objeto.Activo }
        };

        return consulta;
    }

    protected override string GenerarComandoEditar(Producto objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
        var consulta = $"""
                UPDATE adv__producto
                SET
                    ruta_imagen = @RutaImagen,
                    categoria = @Categoria,
                    nombre = @Nombre,
                    codigo = @Codigo,
                    id_proveedor = @IdProveedor,
                    descripcion = @Descripcion,
                    id_unidad_medida = @IdUnidadMedida,
                    id_clasificacion_producto = @IdClasificacionProducto,
                    es_vendible = @EsVendible,
                    costo_adquisicion_unitario = @CostoAdquisicionUnitario,
                    costo_produccion_unitario = @CostoProduccionUnitario,
                    impuesto_venta_porcentaje = @ImpuestoVentaPorcentaje,
                    margen_ganancia_deseado = @MargenGananciaDeseado,
                    precio_venta_base = @PrecioVentaBase,
                    activo = @Activo
                WHERE id_producto = @Id;
                """;

        parametros = new Dictionary<string, object> {
            {  "@Id", objeto.Id  },
            {  "@RutaImagen", objeto.RutaImagen  },
            { "@Categoria", objeto.Categoria.ToString() },
            { "@Nombre", objeto.Nombre },
            { "@Codigo", objeto.Codigo },
            { "@IdProveedor", objeto.IdProveedor },
            { "@Descripcion", objeto.Descripcion },
            { "@IdUnidadMedida", objeto.IdUnidadMedida },
            { "@IdClasificacionProducto", objeto.IdClasificacionProducto },
            { "@EsVendible", objeto.EsVendible },
            { "@CostoAdquisicionUnitario", objeto.CostoAdquisicionUnitario.ToString("N2", CultureInfo.InvariantCulture) },
            { "@CostoProduccionUnitario", objeto.CostoProduccionUnitario.ToString("N2", CultureInfo.InvariantCulture) },
            { "@ImpuestoVentaPorcentaje", objeto.ImpuestoVentaPorcentaje.ToString("N2", CultureInfo.InvariantCulture) },
            { "@MargenGananciaDeseado", objeto.MargenGananciaDeseado.ToString("N2", CultureInfo.InvariantCulture) },
            { "@PrecioVentaBase", objeto.PrecioVentaBase.ToString("N2", CultureInfo.InvariantCulture) },
            { "@Activo", objeto.Activo }
        };

        return consulta;
    }

    protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
        var consulta = $"""
            DELETE FROM adv__inventario
            WHERE id_producto = @id;

            DELETE FROM adv__producto 
            WHERE id_producto = @id;
            """;

        parametros = new Dictionary<string, object> {
            { "@id", id }
        };

        return consulta;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaProducto filtroBusqueda, out Dictionary<string, object> parametros, params string[]? criteriosBusqueda) {
        var criterio = criteriosBusqueda != null && criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;

        if (criteriosBusqueda == null || criteriosBusqueda.Length == 0 || string.IsNullOrEmpty(criteriosBusqueda[0]))
            criterio = string.Empty;

        // Procesamiento de parámetros
        var todosLosAlmacenes = criteriosBusqueda.Length > 1 && criteriosBusqueda[0].Contains("Todos");
        var todasLasCategorias = criteriosBusqueda.Length > 2 && criteriosBusqueda[1].Equals("-1");
        var aplicarFiltroAlmacen = criteriosBusqueda.Length > 1 && !todosLosAlmacenes;
        var aplicarFiltroCategoria = criteriosBusqueda.Length > 2 && !todasLasCategorias;

        // Partes adicionales de la consulta
        const string consultaAdicionalSelect = ", i.cantidad, a.nombre AS nombre_almacen";
        const string consultaAdicionalJoin = "JOIN adv__inventario i ON p.id_producto = i.id_producto JOIN adv__almacen a ON i.id_almacen = a.id_almacen ";

        // Construcción de condiciones WHERE
        var condiciones = new List<string> {
            $"p.activo = @activo"
        };

        if (aplicarFiltroAlmacen)
            condiciones.Add($"a.nombre = @nombre_almacen");

        if (aplicarFiltroCategoria)
            condiciones.Add($"p.categoria = @categoria");

        var whereClause = condiciones.Count > 0 ? $"WHERE {string.Join(" AND ", condiciones)}" : "";
        var consultaComun = $"""
            SELECT p.*{(aplicarFiltroAlmacen ? consultaAdicionalSelect : string.Empty)}
            FROM adv__producto p
            {(aplicarFiltroAlmacen ? consultaAdicionalJoin : string.Empty)}
            """;
        var consulta = filtroBusqueda switch {
            FiltroBusquedaProducto.Id => $"""
                {consultaComun}
                {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                p.id_producto = @id;
                """,
            FiltroBusquedaProducto.Codigo => $"""
                {consultaComun}
                {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                LOWER(p.codigo) LIKE LOWER(@codigo);
                """,
            FiltroBusquedaProducto.Nombre => $"""
                {consultaComun}
                {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                LOWER(p.nombre) LIKE LOWER(@nombre);
                """,
            FiltroBusquedaProducto.Descripcion => $"""
                {consultaComun}
                {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                LOWER(p.descripcion) LIKE LOWER(@descripcion);
                """,
            _ => $"""
                {consultaComun}
                {whereClause};
                """
        };

        parametros = filtroBusqueda switch {
            FiltroBusquedaProducto.Id => new Dictionary<string, object> {
                { "@id", Convert.ToInt64(criterio) },
                { "@activo", !criterio.Equals("Inactivos", StringComparison.OrdinalIgnoreCase) }
            },
            FiltroBusquedaProducto.Codigo => new Dictionary<string, object> {
                { "@codigo", $"%{criterio}%" },
                { "@activo", !criterio.Equals("Inactivos", StringComparison.OrdinalIgnoreCase) }
            },
            FiltroBusquedaProducto.Nombre => new Dictionary<string, object> {
                { "@nombre", $"%{criterio}%" },
                { "@activo", !criterio.Equals("Inactivos", StringComparison.OrdinalIgnoreCase) }
            },
            FiltroBusquedaProducto.Descripcion => new Dictionary<string, object> {
                { "@descripcion", $"%{criterio}%" },
                { "@activo", !criterio.Equals("Inactivos", StringComparison.OrdinalIgnoreCase) }
            },
            _ => new Dictionary<string, object> {
                { "@activo", !criterio.Equals("Inactivos", StringComparison.OrdinalIgnoreCase) }
            }
        };

        if (aplicarFiltroAlmacen) {
            parametros.Add("@nombre_almacen", criteriosBusqueda[0]);
        }

        if (aplicarFiltroCategoria) {
            parametros.Add("@categoria", criteriosBusqueda[1]);
        }

        return consulta;
    }

    /// <summary>
    /// Maps the current record from the specified data reader to a new instance of the Producto entity.
    /// </summary>
    /// <remarks>The data reader must include all columns required to construct a Producto. Field values are
    /// converted to the appropriate types; missing or invalid values may result in default values being used for some
    /// properties.</remarks>
    /// <param name="lectorDatos">A MySqlDataReader positioned at the record to map. Must not be null and must contain all required fields for
    /// Producto.</param>
    /// <returns>A Producto instance populated with values from the current record of the data reader.</returns>
    protected override (Producto, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
        return (new Producto(
            id: Convert.ToInt64(lectorDatos["id_producto"]),
            rutaImagen: Convert.ToString(lectorDatos["ruta_imagen"]) ?? string.Empty,
            categoria: Enum.TryParse<CategoriaProducto>(Convert.ToString(lectorDatos["categoria"]) ?? string.Empty, out var categoria) ? categoria : CategoriaProducto.Mercancia,
            nombre: Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
            codigo: Convert.ToString(lectorDatos["codigo"]) ?? string.Empty,
            idProveedor: Convert.ToInt64(lectorDatos["id_proveedor"]),
            descripcion: Convert.ToString(lectorDatos["descripcion"]) ?? string.Empty,
            idUnidadMedida: Convert.ToInt64(lectorDatos["id_unidad_medida"]),
            idClasificacionProducto: Convert.ToInt64(lectorDatos["id_clasificacion_producto"]),
            esVendible: Convert.ToBoolean(lectorDatos["es_vendible"]),
            costoAdquisicionUnitario: Convert.ToDecimal(lectorDatos["costo_adquisicion_unitario"], CultureInfo.InvariantCulture),
            costoProduccionUnitario: Convert.ToDecimal(lectorDatos["costo_produccion_unitario"], CultureInfo.InvariantCulture),
            impuestoVentaPorcentaje: Convert.ToDecimal(lectorDatos["impuesto_venta_porcentaje"], CultureInfo.InvariantCulture),
            margenGananciaDeseado: Convert.ToDecimal(lectorDatos["margen_ganancia_deseado"], CultureInfo.InvariantCulture),
            precioVentaBase: Convert.ToDecimal(lectorDatos["precio_venta_base"], CultureInfo.InvariantCulture),
            activo: Convert.ToBoolean(lectorDatos["activo"])
        ), new List<IEntidadBaseDatos>());
    }

    #region STATIC

    public static RepoProducto Instancia { get; } = new RepoProducto();

    #endregion

    #region UTILES

    private (object, List<IEntidadBaseDatos>) MapearEntidadJson(MySqlDataReader lector) {
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

        return (listaProductos, new List<IEntidadBaseDatos>()); ;
    }

    public string ObtenerProductosAlmacenJson(long idAlmacen) {
        var consulta = """
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
            FROM adv__producto p
            JOIN adv__inventario pa ON p.id_producto = pa.id_producto
            JOIN adv__almacen a ON pa.id_almacen = a.id_almacen
            LEFT JOIN adv__unidad_medida um ON p.id_unidad_medida = um.id_unidad_medida
            WHERE pa.id_almacen = @IdAlmacen;
        """;
        var parametros = idAlmacen != 0
            ? new Dictionary<string, object> {
                { "@IdAlmacen", idAlmacen }
            } : null;

        try {
            var productos = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidadJson);

            return System.Text.Json.JsonSerializer.Serialize(productos.Select(p => p.entidadBase), new System.Text.Json.JsonSerializerOptions {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        } catch (Exception ex) {
            throw new Exception($"Error al exportar productos del almacén: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Obtiene el valor total bruto de los productos en inventario, opcionalmente filtrado por almacén.
    /// </summary>
    /// <param name="idAlmacen"></param>
    /// <returns></returns>
    public decimal ObtenerValorTotalBruto(long idAlmacen = 0) {
        var consulta = $"""
            SELECT SUM(
                CASE 
                    WHEN p.categoria = 'ProductoTerminado' THEN (p.costo_produccion_unitario * i.cantidad)
                    ELSE (p.costo_adquisicion_unitario * i.cantidad)
                END
            ) AS valor_total_bruto
            FROM adv__producto p
            JOIN adv__inventario i ON p.id_producto = i.id_producto
            {(idAlmacen != 0
                ? "WHERE p.activo = 1 AND i.id_almacen = @IdAlmacen"
                : "WHERE p.activo = 1")};
            """;
        var parametros = idAlmacen != 0
            ? new Dictionary<string, object> {
                { "@IdAlmacen", idAlmacen }
            } : null;

        return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
    }

    public bool HabilitarDeshabilitarProducto(long id) {
        var consulta = $"""
            UPDATE adv__producto
            SET activo = NOT activo
            WHERE id_producto = @IdProducto;
            """;
        var parametros = new Dictionary<string, object> {
                { "@IdProducto", id }
            };

        ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);

        consulta = $"""
            SELECT activo
            FROM adv__producto
            WHERE id_producto = @IdProducto;
            """;

        return ContextoBaseDatos.EjecutarConsultaEscalar<bool>(consulta, parametros);
    }

    #endregion
}