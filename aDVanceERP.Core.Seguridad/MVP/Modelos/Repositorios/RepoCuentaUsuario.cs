using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios;

public class RepoCuentaUsuario : RepoEntidadBaseDatos<CuentaUsuario, FiltroBusquedaCuentaUsuario>, IRepoCuentaUsuario {
    public RepoCuentaUsuario() : base("adv__cuenta_usuario", "id_cuenta_usuario") { }

    protected override string GenerarComandoAdicionar(CuentaUsuario objeto) {
        return $"INSERT INTO adv__cuenta_usuario (nombre, password_hash, password_salt, id_rol_usuario, administrador, aprobado) VALUES ('{objeto.Nombre}', '{objeto.PasswordHash}', '{objeto.PasswordSalt}', {objeto.IdRolUsuario}, {Convert.ToInt32(objeto.Administrador)}, {Convert.ToInt32(objeto.Aprobado)});";
    }

    protected override string GenerarComandoEditar(CuentaUsuario objeto) {
        return $"UPDATE adv__cuenta_usuario SET nombre = '{objeto.Nombre}', password_hash = '{objeto.PasswordHash}', password_salt = '{objeto.PasswordSalt}', id_rol_usuario = {objeto.IdRolUsuario}, administrador = {Convert.ToInt32(objeto.Administrador)}, aprobado = {Convert.ToInt32(objeto.Aprobado)} WHERE id_cuenta_usuario = {objeto.Id};";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__cuenta_usuario WHERE id_cuenta_usuario = {id};";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaCuentaUsuario criterio, string dato) {
        string comando;
        switch (criterio) {
            case FiltroBusquedaCuentaUsuario.Nombre:
                comando = $"SELECT * FROM adv__cuenta_usuario WHERE LOWER(nombre) LIKE LOWER('%{dato}%');";
                break;
            case FiltroBusquedaCuentaUsuario.Rol:
                comando = $"SELECT * FROM adv__cuenta_usuario WHERE id_rol_usuario = {dato};";
                break;
            default:
                comando = "SELECT * FROM adv__cuenta_usuario;";
                break;
        }

        return comando;
    }

    protected override CuentaUsuario MapearEntidad(MySqlDataReader lectorDatos) {
        return new CuentaUsuario(
            lectorDatos.GetInt64("id_cuenta_usuario"),
            lectorDatos.GetString("nombre"),
            lectorDatos.GetString("password_hash"),
            lectorDatos.GetString("password_salt"),
            lectorDatos.GetInt64("id_rol_usuario")) {
            Administrador = lectorDatos.GetBoolean("administrador"),
            Aprobado = lectorDatos.GetBoolean("aprobado")
        };
    }
}