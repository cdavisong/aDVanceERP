using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoTipoMovimiento : RepoEntidadBaseDatos<TipoMovimiento, FiltroBusquedaTipoMovimiento> {
    public RepoTipoMovimiento() : base("adv__tipo_movimiento", "id_tipo_movimiento") { }

    protected override string GenerarComandoAdicionar(TipoMovimiento objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
        var consulta = $"""
            INSERT INTO adv__tipo_movimiento (
                nombre, 
                efecto
            ) 
            VALUES (
                @nombre, 
                @efecto
            );
            """;

        parametros = new Dictionary<string, object> {
            { "@nombre", objeto.Nombre },
            { "@efecto", objeto.Efecto.ToString() }
        };

        return consulta;
    }

    protected override string GenerarComandoEditar(TipoMovimiento objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
        var consulta = $"""
            UPDATE adv__tipo_movimiento 
            SET 
                nombre = @nombre, 
                efecto = @efecto 
            WHERE id_tipo_movimiento = @id;
            """;

        parametros = new Dictionary<string, object> {
            { "@id", objeto.Id },
            { "@nombre", objeto.Nombre },
            { "@efecto", objeto.Efecto.ToString() } 
        };

        return consulta;
    }

    protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
        var consulta = $"""
            DELETE FROM adv__tipo_movimiento 
            WHERE id_tipo_movimiento = @id;
            """;

        parametros = new Dictionary<string, object> {
            { "@id", id }
        };

        return consulta;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaTipoMovimiento filtroBusqueda, out Dictionary<string, object> parametros, params string[] criterios) {
        var criterio = criterios.Length > 0 ? criterios[0] : string.Empty;
        var consulta = filtroBusqueda switch {
            FiltroBusquedaTipoMovimiento.Id => $"""
                SELECT * 
                FROM adv__tipo_movimiento 
                WHERE id_tipo_movimiento = @id;
                """,
            FiltroBusquedaTipoMovimiento.Nombre => $"""
                SELECT *
                FROM adv__tipo_movimiento 
                WHERE LOWER(nombre) LIKE LOWER(@nombre);
                """,
            _ => """
                SELECT * 
                FROM adv__tipo_movimiento;
                """
        };

        parametros = filtroBusqueda switch {
            FiltroBusquedaTipoMovimiento.Id => new Dictionary<string, object> {
                { "@id", Convert.ToInt64(criterio) }
            },
            FiltroBusquedaTipoMovimiento.Nombre => new Dictionary<string, object> {
                { "@nombre", $"%{criterio}%" }
            },
            _ => new Dictionary<string, object>()
        };

        return consulta;
    }

    protected override (TipoMovimiento, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
        return (new TipoMovimiento(
            id: Convert.ToInt64(lectorDatos["id_tipo_movimiento"]),
            nombre: Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
            efecto: Enum.TryParse<EfectoMovimiento>(Convert.ToString(lectorDatos["efecto"]) ?? string.Empty, out var efecto) ? efecto : EfectoMovimiento.Ninguno
        ), new List<IEntidadBaseDatos>());
    }

    #region STATIC

    public static RepoTipoMovimiento Instancia { get; } = new RepoTipoMovimiento();

    #endregion
}