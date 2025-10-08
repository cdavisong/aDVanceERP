using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

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
                id_rol_usuario = {objeto.IdRolUsuario},
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
                    SELECT cu.*, ru.nombre AS nombre_rol_usuario
                    FROM adv__cuenta_usuario cu
                    LEFT JOIN adv__rol_usuario ru ON cu.id_rol_usuario = ru.id_rol_usuario
                    WHERE LOWER(cu.nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            case FiltroBusquedaCuentaUsuario.IdRol:
                comando = $"""
                    SELECT cu.*, ru.nombre AS nombre_rol_usuario
                    FROM adv__cuenta_usuario cu
                    LEFT JOIN adv__rol_usuario ru ON cu.id_rol_usuario = ru.id_rol_usuario
                    WHERE cu.id_rol_usuario = {dato};
                    """;
                break;
            default:
                comando = """
                    SELECT cu.*, ru.nombre AS nombre_rol_usuario
                    FROM adv__cuenta_usuario cu
                    LEFT JOIN adv__rol_usuario ru ON cu.id_rol_usuario = ru.id_rol_usuario;
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
            Aprobado = Convert.ToBoolean(lectorDatos["aprobado"]),
            NombreRolUsuario = Convert.ToString(lectorDatos["nombre_rol_usuario"])
        };
    }

    #region STATIC

    public static RepoCuentaUsuario Instancia { get; } = new RepoCuentaUsuario();

    #endregion

    #region UTILES 

    public void CambiarPassword(long idCuentaUsuario, (string hash, string salt) passwordSeguro) {
        var consulta = $"""
            UPDATE adv__cuenta_usuario 
            SET 
                password_hash = @password_hash,
                password_salt = @password_salt, 
            WHERE id_cuenta_usuario = @id;
            """;
        var parametros = new Dictionary<string, object> {
            { "@password_hash", passwordSeguro.hash },
            { "@password_salt", passwordSeguro.salt },
            { "@id", idCuentaUsuario }
        };

        ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
    }

    #endregion
}