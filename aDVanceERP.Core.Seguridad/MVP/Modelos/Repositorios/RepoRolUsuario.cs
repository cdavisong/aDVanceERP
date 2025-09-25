using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios;

public class RepoRolUsuario : RepoEntidadBaseDatos<RolUsuario, FiltroBusquedaRolUsuario>, IRepoRolUsuario {
    public RepoRolUsuario() : base("adv__rol_usuario", "id_rol_usuario") { }

    protected override string GenerarComandoAdicionar(RolUsuario objeto) {
        return $"INSERT INTO adv__rol_usuario (nombre) VALUES ('{objeto.Nombre}');";
    }

    protected override string GenerarComandoEditar(RolUsuario objeto) {
        return $"UPDATE adv__rol_usuario SET nombre = '{objeto.Nombre}' WHERE id_rol_usuario = {objeto.Id};";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__rol_usuario WHERE id_rol_usuario = {id};";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaRolUsuario criterio, string dato) {
        string comando;
        switch (criterio) {
            case FiltroBusquedaRolUsuario.Id:
                comando = $"SELECT * FROM adv__rol_usuario WHERE id_rol_usuario = {dato};";
                break;
            case FiltroBusquedaRolUsuario.Nombre:
                comando = $"SELECT * FROM adv__rol_usuario WHERE LOWER(nombre) LIKE LOWER('%{dato}%');";
                break;
            default:
                comando = "SELECT * FROM adv__rol_usuario;";
                break;
        }

        return comando;
    }

    protected override RolUsuario MapearEntidad(MySqlDataReader lectorDatos) {
        return new RolUsuario(
            lectorDatos.GetInt64("id_rol_usuario"),
            lectorDatos.GetString("nombre")
        );
    }
}