using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoDetalleProducto : RepoEntidadBaseDatos<DetalleProducto, FiltroBusquedaDetalleProducto> {
    public RepoDetalleProducto() : base("adv__detalle_producto", "id_detalle_producto") { }

    protected override string GenerarComandoAdicionar(DetalleProducto objeto) {
        return $"""
            INSERT INTO adv__detalle_producto (
                id_unidad_medida,
                descripcion
            )
            VALUES (
                {objeto.IdUnidadMedida},
                '{objeto.Descripcion}'
            );
            """;
    }

    protected override string GenerarComandoEditar(DetalleProducto objeto) {
        return $"""
            UPDATE adv__detalle_producto
            SET
                id_unidad_medida = {objeto.IdUnidadMedida},
                descripcion = '{objeto.Descripcion}'
            WHERE id_detalle_producto = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__detalle_producto 
            WHERE id_detalle_producto = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaDetalleProducto criterio, string dato) {
        var comando = criterio switch {
            FiltroBusquedaDetalleProducto.Todos => """
                SELECT * 
                FROM adv__detalle_producto;
                """,
            FiltroBusquedaDetalleProducto.Id => $"""
                SELECT * 
                FROM adv__detalle_producto 
                WHERE id_detalle_producto = {dato};
                """,
            FiltroBusquedaDetalleProducto.UnidadMedida => $"""
                SELECT dp.*
                FROM adv__detalle_producto dp
                JOIN adv__unidad_medida um ON dp.id_unidad_medida = um.id_unidad_medida
                WHERE um.nombre = '{dato}';
                """,
            FiltroBusquedaDetalleProducto.Descripcion => $"""
                SELECT dp.* 
                FROM adv__detalle_producto dp
                WHERE LOWER(dp.descripcion) LIKE LOWER('%{dato}%');
                """,
            _ => throw new ArgumentOutOfRangeException(nameof(criterio), criterio, null)
        };

        return comando;
    }

    protected override DetalleProducto MapearEntidad(MySqlDataReader lectorDatos) {
        return new DetalleProducto(
            id: lectorDatos.GetInt64(lectorDatos.GetOrdinal("id_detalle_producto")),
            idUnidadMedida: lectorDatos.GetInt64(lectorDatos.GetOrdinal("id_unidad_medida")),
            descripcion: lectorDatos.GetString(lectorDatos.GetOrdinal("descripcion")) ?? "No hay descripción disponible"
        );
    }

    #region STATIC

    public static RepoDetalleProducto Instancia = new RepoDetalleProducto();

    public static void HabilitarDeshabilitarProducto(long idDetalleProducto) {
        var consulta = $"""
            UPDATE adv__detalle_producto
            SET
                activo = CASE
                    WHEN activo = 0 THEN 1
                    WHEN activo = 1 THEN 0
                    ELSE 0
                END
            WHERE id_detalle_producto = @IdDetalleProducto;
            """;
        var parametros = new Dictionary<string, object>() {
            { "@IdDetalleProducto", idDetalleProducto }
        };

        ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
    }

    #endregion
}
