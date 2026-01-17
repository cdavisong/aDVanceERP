using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoClasificacionProducto : RepoEntidadBaseDatos<ClasificacionProducto, FiltroBusquedaClasificacionProducto> {
    public RepoClasificacionProducto() : base("adv__clasificacion_producto", "id_clasificacion_producto") { }

    protected override string GenerarComandoAdicionar(ClasificacionProducto objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
        var consulta = $"""
            INSERT INTO adv__clasificacion_producto (
                nombre,
                descripcion
            )
            VALUES (
                @nombre,
                @descripcion
            );
            """;

        parametros = new Dictionary<string, object> {
            { "@nombre", objeto.Nombre },
            { "@descripcion", objeto.Descripcion ?? string.Empty }
        };

        return consulta;
    }

    protected override string GenerarComandoEditar(ClasificacionProducto objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
        var consulta = $"""
            UPDATE adv__clasificacion_producto
            SET
                nombre = @nombre,
                descripcion = @descripcion
            WHERE id_clasificacion_producto = @id_clasificacion_producto;
            """;

        parametros = new Dictionary<string, object> {
            { "@nombre", objeto.Nombre },
            { "@descripcion", objeto.Descripcion ?? string.Empty },
            { "@id_clasificacion_producto", objeto.Id }
        };

        return consulta;
    }

    protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
        var consulta = $"""
            UPDATE adv__producto
            SET id_clasificacion_producto = 0
            WHERE id_clasificacion_producto = @id_clasificacion_producto;

            DELETE FROM adv__clasificacion_producto 
            WHERE id_clasificacion_producto = @id_clasificacion_producto;
            """;

        parametros = new Dictionary<string, object> {
            { "@id_clasificacion_producto", id }
        };

        return consulta;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaClasificacionProducto filtroBusqueda, out Dictionary<string, object> parametros, string[] criteriosBusqueda) {
        var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
        var consulta = filtroBusqueda switch {
            FiltroBusquedaClasificacionProducto.Id => $"""
                SELECT * 
                FROM adv__clasificacion_producto 
                WHERE id_clasificacion_producto = @id;
                """,
            FiltroBusquedaClasificacionProducto.Nombre => $"""
                SELECT * 
                FROM adv__clasificacion_producto 
                WHERE LOWER(nombre) LIKE LOWER(@nombre);
                """,
            _ => """
                SELECT * 
                FROM adv__clasificacion_producto;
                """
        };

        parametros = filtroBusqueda switch {
            FiltroBusquedaClasificacionProducto.Id => new Dictionary<string, object> {
                { "@id", Convert.ToInt64(criterio) }
            },
            FiltroBusquedaClasificacionProducto.Nombre => new Dictionary<string, object> {
                { "@nombre", $"%{criterio}%" }
            },
            _ => new Dictionary<string, object>()
        };

        return consulta;
    }

    protected override (ClasificacionProducto, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
        return (new ClasificacionProducto(
            id: Convert.ToInt64(lectorDatos["id_clasificacion_producto"]),
            nombre: Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
            descripcion: Convert.ToString(lectorDatos["descripcion"]) ?? "No disponible"
        ), new List<IEntidadBaseDatos>());
    }

    #region STATIC

    public static RepoClasificacionProducto Instancia { get; } = new RepoClasificacionProducto();

    #endregion
}
