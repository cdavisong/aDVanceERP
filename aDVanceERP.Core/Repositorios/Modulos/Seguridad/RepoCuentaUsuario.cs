using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad;

public class RepoCuentaUsuario : RepoEntidadBaseDatos<CuentaUsuario, FiltroBusquedaCuentaUsuario> {
    public RepoCuentaUsuario() : base("adv__cuenta_usuario", "id_cuenta_usuario") { }

    protected override string GenerarComandoAdicionar(CuentaUsuario objeto) {
        return $"""
            INSERT INTO adv__cuenta_usuario (
                nombre, 
                password_hash, 
                password_salt, 
                id_rol_usuario, 
                administrador, 
                aprobado
            ) VALUES (
                '{objeto.Nombre}', 
                '{objeto.PasswordHash}', 
                '{objeto.PasswordSalt}', 
                {objeto.IdRolUsuario}, 
                {Convert.ToInt32(objeto.Administrador)}, 
                {Convert.ToInt32(objeto.Aprobado)}
            );
            """;
    }

    protected override string GenerarComandoEditar(CuentaUsuario objeto) {
        return $"""
            UPDATE adv__cuenta_usuario 
            SET 
                nombre = '{objeto.Nombre}', 
                password_hash = '{objeto.PasswordHash}', 
                password_salt = '{objeto.PasswordSalt}', 
                id_rol_usuario = {objeto.IdRolUsuario}, 
                administrador = {Convert.ToInt32(objeto.Administrador)}, 
                aprobado = {Convert.ToInt32(objeto.Aprobado)} 
            WHERE id_cuenta_usuario = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__cuenta_usuario 
            WHERE id_cuenta_usuario = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaCuentaUsuario criterio, string dato) {
        string comando;
        
        switch (criterio) {
            case FiltroBusquedaCuentaUsuario.Nombre:
                comando = $"""
                    SELECT * 
                    FROM adv__cuenta_usuario 
                    WHERE LOWER(nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            case FiltroBusquedaCuentaUsuario.IdRol:
                comando = $"""
                    SELECT * 
                    FROM adv__cuenta_usuario 
                    WHERE id_rol_usuario = {dato};
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__cuenta_usuario;
                    """;
                break;
        }

        return comando;
    }

    protected override CuentaUsuario MapearEntidad(MySqlDataReader lectorDatos) {
        return new CuentaUsuario(
            id: Convert.ToInt64(lectorDatos["id_cuenta_usuario"]),
            nombre: Convert.ToString(lectorDatos["nombre"]),
            passwordHash: Convert.ToString(lectorDatos["password_hash"]),
            passwordSalt: Convert.ToString(lectorDatos["password_salt"]),
            idRolUsuario: Convert.ToInt64(lectorDatos["id_rol_usuario"])) {
            Administrador = Convert.ToBoolean(lectorDatos["administrador"]),
            Aprobado = Convert.ToBoolean(lectorDatos["aprobado"])
        };
    }

    #region STATIC

    public static RepoCuentaUsuario Instancia { get; } = new RepoCuentaUsuario();

    #endregion
}