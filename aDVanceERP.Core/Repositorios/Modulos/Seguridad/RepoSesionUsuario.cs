using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad;

public class RepoSesionUsuario : RepoEntidadBaseDatos<SesionUsuario, FiltroBusquedaSesionUsuario> {
    public RepoSesionUsuario() : base("adv__sesion_usuario", "id_sesion_usuario") { }

    protected override string GenerarComandoAdicionar(SesionUsuario objeto) {
        return $"""
            INSERT INTO adv__sesion_usuario (
                id_cuenta_usuario, 
                token, 
                fecha_inicio, 
                fecha_fin
            ) VALUES (
                {objeto.IdCuentaUsuario}, 
                '{objeto.Token}', 
                '{objeto.FechaInicio:yyyy-MM-dd HH:mm:ss}', 
                {(objeto.FechaFin.HasValue ? $"'{objeto.FechaFin:yyyy-MM-dd HH:mm:ss}'" : "NULL")}
            );
            """;
    }

    protected override string GenerarComandoEditar(SesionUsuario objeto) {
        return $"""
            UPDATE adv__sesion_usuario 
            SET 
                id_cuenta_usuario = {objeto.IdCuentaUsuario}, 
                token = '{objeto.Token}', 
                fecha_inicio = '{objeto.FechaInicio:yyyy-MM-dd HH:mm:ss}', 
                fecha_fin = {(objeto.FechaFin.HasValue ? $"'{objeto.FechaFin:yyyy-MM-dd HH:mm:ss}'" : "NULL")} 
            WHERE id_sesion_usuario = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__sesion_usuario 
            WHERE id_sesion_usuario = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaSesionUsuario filtroBusqueda, string criterio) {
        string comando;
        switch (filtroBusqueda) {
            case FiltroBusquedaSesionUsuario.NombreUsuario:
                comando = $"""
                    SELECT * 
                    FROM adv__sesion_usuario su 
                    JOIN adv__cuenta_usuario cu ON su.id_cuenta_usuario = cu.id_cuenta_usuario 
                    WHERE LOWER(cu.nombre) LIKE LOWER('%{criterio}%');
                    """;
                break;
            case FiltroBusquedaSesionUsuario.SesionActiva:
                comando = """
                    SELECT * 
                    FROM adv__sesion_usuario 
                    WHERE fecha_fin IS NULL;
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__sesion_usuario;
                    """;
                break;
        }

        return comando;
    }

    protected override SesionUsuario MapearEntidad(MySqlDataReader lector) {
        return new SesionUsuario(
            id: Convert.ToInt64(lector["id_sesion_usuario"]),
            idCuentaUsuario: Convert.ToInt64(lector["id_cuenta_usuario"]),
            token: Convert.ToString(lector["token"]),
            fechaInicio: Convert.ToDateTime(lector["fecha_inicio"])) {
            FechaFin = lector["fecha_fin"] == DBNull.Value
                ? null
                : Convert.ToDateTime(lector["fecha_fin"])
        };
    }

    #region STATIC

    public static RepoSesionUsuario Instancia { get; } = new RepoSesionUsuario();

    #endregion
}