using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad;

public class RepoRolUsuario : RepoEntidadBaseDatos<RolUsuario, FiltroBusquedaRolUsuario> {
    public RepoRolUsuario() : base("adv__rol_usuario", "id_rol_usuario") { }

    protected override string GenerarComandoAdicionar(RolUsuario objeto) {
        return $"""
            INSERT INTO adv__rol_usuario (
                nombre
            ) VALUES (
                '{objeto.Nombre}'
            );
            """;
    }

    protected override string GenerarComandoEditar(RolUsuario objeto) {
        return $"""
            UPDATE adv__rol_usuario 
            SET 
                nombre = '{objeto.Nombre}' 
            WHERE id_rol_usuario = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__rol_usuario 
            WHERE id_rol_usuario = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaRolUsuario filtroBusqueda, string criterio) {
        string comando;
        switch (filtroBusqueda) {
            case FiltroBusquedaRolUsuario.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__rol_usuario 
                    WHERE id_rol_usuario = {criterio};
                    """;
                break;
            case FiltroBusquedaRolUsuario.Nombre:
                comando = $"""
                    SELECT * 
                    FROM adv__rol_usuario 
                    WHERE LOWER(nombre) LIKE LOWER('%{criterio}%');
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__rol_usuario;
                    """;
                break;
        }

        return comando;
    }

    protected override RolUsuario MapearEntidad(MySqlDataReader lectorDatos) {
        return new RolUsuario(
            id: Convert.ToInt64(lectorDatos["id_rol_usuario"]),
            nombre: Convert.ToString(lectorDatos["nombre"]));
    }

    #region STATIC

    public static RepoRolUsuario Instancia { get; } = new RepoRolUsuario();

    #endregion
}