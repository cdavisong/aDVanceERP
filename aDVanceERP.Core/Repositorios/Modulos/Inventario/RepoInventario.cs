using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoInventario : RepoEntidadBaseDatos<Modelos.Modulos.Inventario.Inventario, FiltroBusquedaInventario> {
    private Producto? _producto;

    public RepoInventario() : base("adv__inventario", "id_inventario") { }

    protected override string GenerarComandoAdicionar(Modelos.Modulos.Inventario.Inventario entidad) {
        return $"""
            INSERT INTO adv__inventario (
                id_producto, 
                id_almacen, 
                cantidad,
                costo_promedio,
                valor_total
            ) 
            VALUES (
                {entidad.IdProducto}, 
                {entidad.IdAlmacen}, 
                {entidad.Cantidad.ToString(CultureInfo.InvariantCulture)},
                {entidad.CostoPromedio.ToString(CultureInfo.InvariantCulture)},
                {entidad.ValorTotal.ToString(CultureInfo.InvariantCulture)}
            );
            """;
    }

    protected override string GenerarComandoEditar(Modelos.Modulos.Inventario.Inventario objeto) {
        return $"""
            UPDATE adv__inventario 
            SET 
                id_producto = {objeto.IdProducto}, 
                id_almacen = {objeto.IdAlmacen}, 
                cantidad = {objeto.Cantidad.ToString(CultureInfo.InvariantCulture)},
                costo_promedio = {objeto.CostoPromedio.ToString(CultureInfo.InvariantCulture)},
                valor_total = {objeto.ValorTotal.ToString(CultureInfo.InvariantCulture)}
            WHERE id_inventario = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__inventario 
            WHERE id_inventario = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaInventario criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaInventario.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__inventario 
                    WHERE id_inventario = {dato};
                    """;
                break;
            case FiltroBusquedaInventario.IdProducto:
                comando = $"""
                    SELECT * 
                    FROM adv__inventario 
                    WHERE id_producto = {dato};
                    """;
                break;
            case FiltroBusquedaInventario.IdAlmacen:
                comando = $"""
                    SELECT * 
                    FROM adv__inventario 
                    WHERE id_almacen = {dato};
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__inventario;
                    """;
                break;
        }

        return comando;
    }

    protected override Modelos.Modulos.Inventario.Inventario MapearEntidad(MySqlDataReader lectorDatos) {
        return new Modelos.Modulos.Inventario.Inventario(
           lectorDatos.GetInt64(lectorDatos.GetOrdinal("id_inventario")),
           lectorDatos.GetInt64(lectorDatos.GetOrdinal("id_producto")),
           lectorDatos.GetInt64(lectorDatos.GetOrdinal("id_almacen")),
           lectorDatos.GetDecimal(lectorDatos.GetOrdinal("cantidad")),
           lectorDatos.GetDecimal(lectorDatos.GetOrdinal("costo_promedio")),
           lectorDatos.GetDecimal(lectorDatos.GetOrdinal("valor_total")),
           lectorDatos.GetDateTime(lectorDatos.GetOrdinal("ultima_actualizacion"))
       );
    }

    #region STATIC

    public static RepoInventario Instancia = new RepoInventario();

    #endregion

    #region UTILES  



    public void ModificarInventario(string nombreProducto, string nombreAlmacenOrigen, string nombreAlmacenDestino, decimal cantidad) {
        _producto = RepoProducto.Instancia.Buscar(FiltroBusquedaProducto.Nombre, nombreProducto).resultados.FirstOrDefault(p => p.Nombre.Equals(nombreProducto));
        
        var almacenOrigen = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, nombreAlmacenOrigen).resultados.FirstOrDefault(a => a.Nombre.Equals(nombreAlmacenOrigen));
        var almacenDestino = RepoAlmacen.Instancia.Buscar(FiltroBusquedaAlmacen.Nombre, nombreAlmacenDestino).resultados.FirstOrDefault(a => a.Nombre.Equals(nombreAlmacenDestino));

        ModificarInventario(_producto?.Id ?? 0, almacenOrigen?.Id ?? 0, almacenDestino?.Id ?? 0, cantidad);
    }

    public void ModificarInventario(long idProducto, long idAlmacenOrigen, long idAlmacenDestino, decimal cantidad) {
        var producto = _producto ?? RepoProducto.Instancia.ObtenerPorId(idProducto);

        var costoUnitario = (producto?.Categoria == CategoriaProducto.ProductoTerminado 
            ? (producto?.CostoProduccionUnitario ?? 0) 
            : (producto?.PrecioCompra ?? 0)).ToString(CultureInfo.InvariantCulture);

        var consulta = string.Empty;
        var parametros = new Dictionary<string, object>();

        // Decrementar la cantidad en el almacen origen
        if (idAlmacenOrigen > 0) {
            consulta = """
                UPDATE adv__inventario
                SET 
                  cantidad = cantidad - @Cantidad,
                  valor_total = valor_total - (@Cantidad * @CostoUnitario),
                  costo_promedio = valor_total / cantidad
                WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenOrigen;
                """;
            parametros.Add("@Cantidad", cantidad);
            parametros.Add("@CostoUnitario", costoUnitario);
            parametros.Add("@IdProducto", idProducto);
            parametros.Add("@IdAlmacenOrigen", idAlmacenOrigen);

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
            parametros.Clear();
        }

        // Aumentar la cantidad en el almacen destino
        if (idAlmacenDestino > 0) {
            // Verificar primeramente si el producto existe en el almacén de destino
            consulta = """
                SELECT COUNT(*)
                FROM adv__inventario
                WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenDestino;
                """;
            parametros.Add("@IdProducto", idProducto);
            parametros.Add("@IdAlmacenDestino", idAlmacenDestino);

            var count = ContextoBaseDatos.EjecutarConsultaEscalar<int>(consulta, parametros);
            parametros.Clear();

            if (count > 0) {
                consulta = """
                    UPDATE adv__inventario
                    SET 
                      cantidad = cantidad + @Cantidad,
                      valor_total = valor_total + (@Cantidad * @CostoUnitario),
                      costo_promedio = valor_total / cantidad
                    WHERE id_producto = @IdProducto AND id_almacen = @IdAlmacenDestino;
                    """;
                parametros.Add("@Cantidad", cantidad);
                parametros.Add("@CostoUnitario", costoUnitario);
                parametros.Add("@IdProducto", idProducto);
                parametros.Add("@IdAlmacenDestino", idAlmacenDestino);

                ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
                parametros.Clear();
            } else {
                consulta = $"""
                    INSERT INTO adv__inventario (
                        id_producto, 
                        id_almacen, 
                        cantidad,
                        costo_promedio,
                        valor_total
                    ) 
                    VALUES (
                        @IdProducto,
                        @IdAlmacenDestino,
                        @Cantidad,
                        @CostoUnitario,
                        @Cantidad * @CostoUnitario
                    );
                    """;
                parametros.Add("@IdProducto", idProducto);
                parametros.Add("@IdAlmacenDestino", idAlmacenDestino);
                parametros.Add("@Cantidad", cantidad);
                parametros.Add("@CostoUnitario", costoUnitario);

                ContextoBaseDatos.EjecutarComandoInsert(consulta, parametros);
                parametros.Clear();
            }
        }
    }

    

    #endregion
}