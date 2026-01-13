using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoClasificacionProducto : RepoEntidadBaseDatos<ClasificacionProducto, FiltroBusquedaClasificacionProducto> {
    public RepoClasificacionProducto() : base("adv__clasificacion_producto", "id_clasificacion_producto") { }

    protected override string GenerarComandoAdicionar(ClasificacionProducto objeto) {
        return $"""
            INSERT INTO adv__clasificacion_producto (
                nombre,
                descripcion
            )
            VALUES (
                '{objeto.Nombre}',
                '{objeto.Descripcion}'
            );
            """;
    }

    protected override string GenerarComandoEditar(ClasificacionProducto objeto) {
        return $"""
            UPDATE adv__clasificacion_producto
            SET
                nombre = '{objeto.Nombre}',
                descripcion = '{objeto.Descripcion}'
            WHERE id_clasificacion_producto = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            UPDATE adv__producto
            SET id_clasificacion_producto = 0
            WHERE id_clasificacion_producto = {id};

            DELETE FROM adv__clasificacion_producto 
            WHERE id_clasificacion_producto = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaClasificacionProducto criterio, string dato) {
        return criterio switch {
            FiltroBusquedaClasificacionProducto.Todos => """
                SELECT * 
                FROM adv__clasificacion_producto;
                """,
            FiltroBusquedaClasificacionProducto.Id => $"""
                SELECT * 
                FROM adv__clasificacion_producto 
                WHERE id_clasificacion_producto = {dato};
                """,
            FiltroBusquedaClasificacionProducto.Nombre => $"""
                SELECT * 
                FROM adv__clasificacion_producto 
                WHERE LOWER(nombre) LIKE LOWER('%{dato}%');
                """,
            _ => throw new ArgumentOutOfRangeException(nameof(criterio), criterio, null)
        };
    }

    protected override ClasificacionProducto MapearEntidad(MySqlDataReader lectorDatos) {
        return new ClasificacionProducto(
            id: Convert.ToInt64(lectorDatos["id_clasificacion_producto"]),
            nombre: Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
            descripcion: Convert.ToString(lectorDatos["descripcion"]) ?? "No disponible"
        );
    }

    #region STATIC

    public static RepoClasificacionProducto Instancia { get; } = new RepoClasificacionProducto();

    #endregion
}
