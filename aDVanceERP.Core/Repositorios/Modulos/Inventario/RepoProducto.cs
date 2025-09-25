using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoProducto : RepoEntidadBaseDatos<Producto, FiltroBusquedaProducto> {
    public RepoProducto() : base("adv__producto", "id_producto") { }

    protected override string GenerarComandoAdicionar(Producto objeto) {
        return $"""
                INSERT INTO adv__producto (
                    codigo,
                    categoria,
                    nombre,
                    id_detalle_producto,
                    id_proveedor,
                    id_tipo_materia_prima,
                    es_vendible,
                    precio_compra,
                    costo_produccion_unitario,
                    precio_venta_base
                )
                VALUES (
                    '{objeto.Codigo}',
                    '{objeto.Categoria}',
                    '{objeto.Nombre}',
                    {objeto.IdDetalleProducto},
                    {objeto.IdProveedor},
                    {objeto.IdTipoMateriaPrima},
                    '{(objeto.EsVendible ? 1 : 0)}',
                    {objeto.PrecioCompra.ToString(CultureInfo.InvariantCulture)},
                    {objeto.CostoProduccionUnitario.ToString(CultureInfo.InvariantCulture)},
                    {objeto.PrecioVentaBase.ToString(CultureInfo.InvariantCulture)}
                );
                """;
    }

    protected override string GenerarComandoEditar(Producto objeto) {
        return $"""
                UPDATE adv__producto
                SET
                    codigo = '{objeto.Codigo}',
                    categoria = '{objeto.Categoria}',
                    nombre = '{objeto.Nombre}',
                    id_detalle_producto = {objeto.IdDetalleProducto},
                    id_proveedor = {objeto.IdProveedor},
                    id_tipo_materia_prima = {objeto.IdTipoMateriaPrima},
                    es_vendible = '{(objeto.EsVendible ? 1 : 0)}',
                    precio_compra = {objeto.PrecioCompra.ToString(CultureInfo.InvariantCulture)},
                    costo_produccion_unitario = {objeto.CostoProduccionUnitario.ToString(CultureInfo.InvariantCulture)},
                    precio_venta_base = {objeto.PrecioVentaBase.ToString(CultureInfo.InvariantCulture)}
                WHERE id_producto = {objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__inventario
            WHERE id_producto = {id};

            DELETE FROM adv__detalle_producto
            WHERE id_detalle_producto = (
                SELECT id_detalle_producto 
                FROM adv__producto 
                WHERE id_producto = {id}
            );

            DELETE FROM adv__producto 
            WHERE id_producto = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaProducto filtroBusqueda, string criterio) {
        if (string.IsNullOrEmpty(criterio))
            criterio = "Todos";

        string? comando;
        var criterioMultiple = criterio.Split(';');

        // Procesamiento de parámetros
        var todosLosAlmacenes = criterioMultiple.Length > 1 && criterioMultiple[0].Contains("Todos");
        var todasLasCategorias = criterioMultiple.Length > 2 && criterioMultiple[1].Equals("-1");
        var aplicarFiltroAlmacen = criterioMultiple.Length > 1 && !todosLosAlmacenes;
        var aplicarFiltroCategoria = criterioMultiple.Length > 2 && !todasLasCategorias;

        // Partes adicionales de la consulta
        const string comandoAdicionalSelect = ", i.cantidad, a.nombre AS nombre_almacen";
        const string comandoAdicionalJoin = "JOIN adv__inventario i ON p.id_producto = i.id_producto JOIN adv__almacen a ON i.id_almacen = a.id_almacen ";

        // Construcción de condiciones WHERE
        var condiciones = new List<string> {
            $"dp.activo = {(filtroBusqueda == FiltroBusquedaProducto.Inactivos ? 0 : 1)}"
        };

        if (aplicarFiltroAlmacen)
            condiciones.Add($"a.nombre = '{criterioMultiple[0]}'");

        if (aplicarFiltroCategoria)
            condiciones.Add($"p.categoria = '{(CategoriaProducto)int.Parse(criterioMultiple[1])}'");

        string whereClause = condiciones.Count > 0 ? $"WHERE {string.Join(" AND ", condiciones)}" : "";

        switch (filtroBusqueda) {
            case FiltroBusquedaProducto.Id:
                comando = $"""
                         SELECT *{(aplicarFiltroAlmacen ? comandoAdicionalSelect : string.Empty)}
                         FROM adv__producto p
                         {(aplicarFiltroAlmacen ? comandoAdicionalJoin : string.Empty)}
                         JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
                         {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                         p.id_producto = '{(criterioMultiple.Length > (aplicarFiltroCategoria ? 2 : 1) ? criterioMultiple[2] : criterio)}';
                         """;
                break;
            case FiltroBusquedaProducto.Codigo:
                comando = $"""
                         SELECT *{(aplicarFiltroAlmacen ? comandoAdicionalSelect : string.Empty)}
                         FROM adv__producto p
                         {(aplicarFiltroAlmacen ? comandoAdicionalJoin : string.Empty)}
                         JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
                         {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                         LOWER(p.codigo) LIKE LOWER('%{(criterioMultiple.Length > (aplicarFiltroCategoria ? 2 : 1) ? criterioMultiple[2] : criterio)}%');
                         """;
                break;
            case FiltroBusquedaProducto.Nombre:
                comando = $"""
                         SELECT *{(aplicarFiltroAlmacen ? comandoAdicionalSelect : string.Empty)}
                         FROM adv__producto p
                         {(aplicarFiltroAlmacen ? comandoAdicionalJoin : string.Empty)}
                         JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
                         {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                         LOWER(p.nombre) LIKE LOWER('%{(criterioMultiple.Length > (aplicarFiltroCategoria ? 2 : 1) ? criterioMultiple[2] : criterio)}%');
                         """;
                break;
            case FiltroBusquedaProducto.Descripcion:
                comando = $"""
                         SELECT *{(aplicarFiltroAlmacen ? comandoAdicionalSelect : string.Empty)}
                         FROM adv__producto p
                         {(aplicarFiltroAlmacen ? comandoAdicionalJoin : string.Empty)}
                         JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
                         {(condiciones.Count > 0 ? whereClause + " AND " : "WHERE ")}
                         LOWER(dp.descripcion) LIKE LOWER('%{(criterioMultiple.Length > (aplicarFiltroCategoria ? 2 : 1) ? criterioMultiple[2] : criterio)}%');
                         """;
                break;
            default:
                comando = $"""
                         SELECT *{(aplicarFiltroAlmacen ? comandoAdicionalSelect : string.Empty)}
                         FROM adv__producto p
                         {(aplicarFiltroAlmacen ? comandoAdicionalJoin : string.Empty)}
                         JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
                         {whereClause};
                         """;
                break;
        }

        return comando;
    }

    protected override Producto MapearEntidad(MySqlDataReader lectorDatos) {
        return new Producto(
            id: lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_producto")),
            categoria: (CategoriaProducto)Enum.Parse(typeof(CategoriaProducto), lectorDatos.GetValue(lectorDatos.GetOrdinal("categoria")).ToString()),
            nombre: lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")),
            codigo: lectorDatos.GetString(lectorDatos.GetOrdinal("codigo")),
            idDetalleProducto: lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_detalle_producto")),
            idTipoMateriaPrima: lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_tipo_materia_prima")),
            idProveedor: lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_proveedor")),
            esVendible: lectorDatos.GetBoolean(lectorDatos.GetOrdinal("es_vendible")),
            precioCompra: lectorDatos.GetDecimal(lectorDatos.GetOrdinal("precio_compra")),
            costoProduccionUnitario: lectorDatos.GetDecimal(lectorDatos.GetOrdinal("costo_produccion_unitario")),
            precioVentaBase: lectorDatos.GetDecimal(lectorDatos.GetOrdinal("precio_venta_base"))
        );
    }

    #region STATIC

    public static RepoProducto Instancia = new RepoProducto();

    #endregion

    #region UTILES

    public decimal ObtenerValorTotalBruto(long idAlmacen = 0) {
        var consulta = $"""
            SELECT SUM((p.precio_compra + p.costo_produccion_unitario) * i.cantidad) as valor_total
            FROM adv__producto p 
            JOIN adv__detalle_producto dp ON p.id_detalle_producto = dp.id_detalle_producto
            JOIN adv__inventario i ON p.id_producto = i.id_producto
            {(idAlmacen != 0
                ? "WHERE dp.activo = 1 AND i.id_almacen = @IdAlmacen"
                : "WHERE dp.activo = 1")};
            """;
        var parametros = idAlmacen != 0
            ? new Dictionary<string, object> {
                { "@IdAlmacen", idAlmacen }
            } : null;

        return ContextoBaseDatos.EjecutarConsultaEscalar<decimal>(consulta, parametros);
    }

    #endregion
}