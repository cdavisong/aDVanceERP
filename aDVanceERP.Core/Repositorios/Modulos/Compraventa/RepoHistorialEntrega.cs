using aDVanceERP.Core.Modelos.Modulos.Compraventa;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compraventa;

public class RepoHistorialEntrega : RepoEntidadBaseDatos<HistorialEntrega, FiltroBusquedaHistorialEntrega> {
    public RepoHistorialEntrega() : base("adv__historial_entrega", "id_historial") { }

    protected override string GenerarComandoAdicionar(HistorialEntrega objeto) {
        return $"""
            INSERT INTO adv__historial_entrega (
                id_seguimiento_entrega,
                id_estado,
                fecha_registro,
                id_usuario,
                observaciones
            ) 
            VALUES (
                {objeto.IdSeguimientoEntrega},
                {objeto.IdEstadoEntrega},
                '{objeto.FechaRegistro:yyyy-MM-dd HH:mm:ss}',
                {objeto.IdUsuario},
                '{objeto.Observaciones}'
            );
            """;
    }

    protected override string GenerarComandoEditar(HistorialEntrega objeto) {
        return $"""
            UPDATE adv__historial_entrega 
            SET 
                id_seguimiento_entrega = {objeto.IdSeguimientoEntrega},
                id_estado = {objeto.IdEstadoEntrega},
                fecha_registro = '{objeto.FechaRegistro:yyyy-MM-dd HH:mm:ss}',
                id_usuario = {objeto.IdUsuario},
                observaciones = '{objeto.Observaciones}'
            WHERE id_historial = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__historial_entrega 
            WHERE id_historial = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaHistorialEntrega criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaHistorialEntrega.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__historial_entrega 
                    WHERE id_historial = {dato};
                    """;
                break;
            case FiltroBusquedaHistorialEntrega.IdSeguimientoEntrega:
                comando = $"""
                    SELECT * 
                    FROM adv__historial_entrega 
                    WHERE id_seguimiento_entrega = {dato};
                    """;
                break;
            case FiltroBusquedaHistorialEntrega.IdEstadoEntrega:
                comando = $"""
                    SELECT * 
                    FROM adv__historial_entrega 
                    WHERE id_estado = {dato};
                    """;
                break;
            case FiltroBusquedaHistorialEntrega.IdUsuario:
                comando = $"""
                    SELECT * 
                    FROM adv__historial_entrega 
                    WHERE id_usuario = {dato};
                    """;
                break;
            case FiltroBusquedaHistorialEntrega.Todos:
            default:
                comando = """
                    SELECT * 
                    FROM adv__historial_entrega;
                    """;
                break;
        }

        return comando;
    }

    protected override HistorialEntrega MapearEntidad(MySqlDataReader lector) {
        return new HistorialEntrega(
            id: Convert.ToInt64(lector["id_historial"]),
            idSeguimientoEntrega: Convert.ToInt64(lector["id_seguimiento_entrega"]),
            idEstadoEntrega: Convert.ToInt64(lector["id_estado"]),
            fechaRegistro: Convert.ToDateTime(lector["fecha_registro"]),
            idUsuario: Convert.ToInt64(lector["id_usuario"]),
            observaciones: Convert.ToString(lector["observaciones"] ?? "No hay observaciones para la entrega actual")
            );
    }

    #region STATIC

    public static RepoHistorialEntrega Instancia { get; } = new RepoHistorialEntrega();

    #endregion
}
