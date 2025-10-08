using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

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
                    SELECT ru.*, COUNT(DISTINCT rp.id_permiso) AS cantidad_permisos, COUNT(DISTINCT cu.id_cuenta_usuario) AS cantidad_usuarios_asignados
                    FROM adv__rol_usuario ru
                    LEFT JOIN adv__rol_permiso rp ON ru.id_rol_usuario = rp.id_rol_usuario
                    LEFT JOIN adv__cuenta_usuario cu ON ru.id_rol_usuario = cu.id_rol_usuario
                    WHERE ru.id_rol_usuario = ru.{criterio}
                    GROUP BY ru.id_rol_usuario, ru.nombre
                    ORDER BY ru.id_rol_usuario;
                    """;
                break;
            case FiltroBusquedaRolUsuario.Nombre:
                comando = $"""
                    SELECT ru.*, COUNT(DISTINCT rp.id_permiso) AS cantidad_permisos, COUNT(DISTINCT cu.id_cuenta_usuario) AS cantidad_usuarios_asignados
                    FROM adv__rol_usuario ru
                    LEFT JOIN adv__rol_permiso rp ON ru.id_rol_usuario = rp.id_rol_usuario
                    LEFT JOIN adv__cuenta_usuario cu ON ru.id_rol_usuario = cu.id_rol_usuario
                    WHERE LOWER(ru.nombre) LIKE LOWER('%{criterio}%')
                    GROUP BY ru.id_rol_usuario, ru.nombre
                    ORDER BY ru.id_rol_usuario;
                    """;
                break;
            default:
                comando = """
                    SELECT ru.*, COUNT(DISTINCT rp.id_permiso) AS cantidad_permisos, COUNT(DISTINCT cu.id_cuenta_usuario) AS cantidad_usuarios_asignados
                    FROM adv__rol_usuario ru
                    LEFT JOIN adv__rol_permiso rp ON ru.id_rol_usuario = rp.id_rol_usuario
                    LEFT JOIN adv__cuenta_usuario cu ON ru.id_rol_usuario = cu.id_rol_usuario
                    GROUP BY ru.id_rol_usuario, ru.nombre
                    ORDER BY ru.id_rol_usuario;
                    """;
                break;
        }

        return comando;
    }

    protected override RolUsuario MapearEntidad(MySqlDataReader lector) {
        return new RolUsuario(
            id: Convert.ToInt64(lector["id_rol_usuario"]),
            nombre: Convert.ToString(lector["nombre"])) {
            CantidadPermisos = lector.HasColumn("cantidad_permisos") ? Convert.ToInt32(lector["cantidad_permisos"]) : 0,
            CantidadUsuariosAsignados = lector.HasColumn("cantidad_usuarios_asignados") ? Convert.ToInt32(lector["cantidad_usuarios_asignados"]) : 0
        };
    }

    #region STATIC

    public static RepoRolUsuario Instancia { get; } = new RepoRolUsuario();

    #endregion

    #region UTILES



    #endregion
}