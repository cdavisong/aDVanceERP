using aDVanceERP.Core.Modelos.Modulos.Compraventa;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compraventa;

public class RepoEstadoEntrega : RepoEntidadBaseDatos<EstadoEntrega, FiltroBusquedaEstadoEntrega> {
    public RepoEstadoEntrega() : base("adv__estado_entrega", "id_estado") { }

    protected override string GenerarComandoAdicionar(EstadoEntrega entidad) {
        return $"""
            INSERT INTO adv__estado_entrega (
                nombre, 
                descripcion,
                orden
            ) 
            VALUES (
                '{entidad.Nombre}', 
                '{entidad.Descripcion}',
                {entidad.Orden}
            );
            """;
    }

    protected override string GenerarComandoEditar(EstadoEntrega entidad) {
        return $"""
            UPDATE adv__estado_entrega 
            SET 
                nombre = '{entidad.Nombre}', 
                descripcion = '{entidad.Descripcion}',
                orden = {entidad.Orden}
            WHERE id_estado = {entidad.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__estado_entrega 
            WHERE id_estado = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaEstadoEntrega filtroBusqueda, string criterio) {
        var comando = string.Empty;

        switch (filtroBusqueda) {
            case FiltroBusquedaEstadoEntrega.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__estado_entrega 
                    WHERE id_estado = {criterio};
                    """;
                break;
            case FiltroBusquedaEstadoEntrega.Nombre:
                comando = $"""
                    SELECT * 
                    FROM adv__estado_entrega 
                    WHERE nombre LIKE '%{criterio}%';
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__estado_entrega;
                    """;
                break;
        }

        return comando;
    }

    protected override EstadoEntrega MapearEntidad(MySqlDataReader lector) {
        return new EstadoEntrega(
            id: Convert.ToInt64(lector["id_estado"]),
            nombre: Convert.ToString(lector["nombre"]) ?? string.Empty,
            descripcion: Convert.ToString(lector["descripcion"]) ?? string.Empty,
            orden: Convert.ToInt32(lector["orden"])
        );
    }

    #region STATIC

    public static RepoEstadoEntrega Instancia { get; } = new RepoEstadoEntrega();

    #endregion
}
