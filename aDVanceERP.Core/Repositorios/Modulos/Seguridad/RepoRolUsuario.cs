using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad;

public class RepoRolUsuario : RepoEntidadBaseDatos<RolUsuario, FiltroBusquedaRolUsuario> {
    public RepoRolUsuario() : base("adv__rol_usuario", "id_rol_usuario") { }

    protected override string GenerarComandoAdicionar(RolUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
        var consulta = $"""
            INSERT INTO adv__rol_usuario (
                nombre
            ) VALUES (
                @nombre
            );
            """;

        parametros = new Dictionary<string, object> {
            { "@nombre", objeto.Nombre }
        };

        return consulta;
    }

    protected override string GenerarComandoEditar(RolUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
        var consulta = $"""
            UPDATE adv__rol_usuario 
            SET 
                nombre = @nombre 
            WHERE id_rol_usuario = @idRolUsuario;
            """;

        parametros = new Dictionary<string, object> {
            { "@idRolUsuario", objeto.Id },
            { "@nombre", objeto.Nombre }
        };

        return consulta;
    }

    protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
        var consulta = $"""
            DELETE FROM adv__rol_usuario 
            WHERE id_rol_usuario = @idRolUsuario;
            """;

        parametros = new Dictionary<string, object> {
            { "@idRolUsuario", id }
        };

        return consulta;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaRolUsuario filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
        var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
        var consultaComun = $"""
            SELECT ru.*, COUNT(DISTINCT rp.id_permiso) AS cantidad_permisos, COUNT(DISTINCT cu.id_cuenta_usuario) AS cantidad_usuarios_asignados
            FROM adv__rol_usuario ru
            LEFT JOIN adv__rol_permiso rp ON ru.id_rol_usuario = rp.id_rol_usuario
            LEFT JOIN adv__cuenta_usuario cu ON ru.id_rol_usuario = cu.id_rol_usuario
            """;
        var consulta = filtroBusqueda switch {
            FiltroBusquedaRolUsuario.Id => $"""
                {consultaComun}
                WHERE ru.id_rol_usuario = @id_rol_usuario
                GROUP BY ru.id_rol_usuario, ru.nombre
                ORDER BY ru.id_rol_usuario;
                """,
            FiltroBusquedaRolUsuario.Nombre => $"""
                {consultaComun}
                WHERE LOWER(ru.nombre) LIKE LOWER(@nombre)
                GROUP BY ru.id_rol_usuario, ru.nombre
                ORDER BY ru.id_rol_usuario;
                """,
            _ => $"""
                {consultaComun}
                GROUP BY ru.id_rol_usuario, ru.nombre
                ORDER BY ru.id_rol_usuario;
                """
        };

        parametros = filtroBusqueda switch {
            FiltroBusquedaRolUsuario.Id => new Dictionary<string, object> {
                { "@id_rol_usuario", Convert.ToInt64(criterio) }
            },
            FiltroBusquedaRolUsuario.Nombre => new Dictionary<string, object> {
                { "@nombre", $"%{criterio}%" }
            },
            _ => new Dictionary<string, object>()
        };

        return consulta;
    }

    protected override (RolUsuario, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
        return (new RolUsuario(
            id: Convert.ToInt64(lector["id_rol_usuario"]),
            nombre: Convert.ToString(lector["nombre"])) {
            CantidadPermisos = lector.HasColumn("cantidad_permisos") ? Convert.ToInt32(lector["cantidad_permisos"]) : 0,
            CantidadUsuariosAsignados = lector.HasColumn("cantidad_usuarios_asignados") ? Convert.ToInt32(lector["cantidad_usuarios_asignados"]) : 0
        }, new List<IEntidadBaseDatos>());
    }

    #region STATIC

    public static RepoRolUsuario Instancia { get; } = new RepoRolUsuario();

    #endregion

    #region UTILES



    #endregion
}