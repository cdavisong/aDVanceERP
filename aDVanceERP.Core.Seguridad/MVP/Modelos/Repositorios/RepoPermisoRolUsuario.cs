using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios.Plantillas;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Seguridad.MVP.Modelos.Repositorios;

public class RepoPermisoRolUsuario : RepoEntidadBaseDatos<PermisoRolUsuario, FiltroBusquedaPermisoRolUsuario>, IRepoPermisoRolUsuario {
    public RepoPermisoRolUsuario() : base("adv__rol_permiso", "id_rol_permiso") { }

    protected override string GenerarComandoAdicionar(PermisoRolUsuario objeto) {
        return $"INSERT INTO adv__rol_permiso (id_rol_usuario, id_permiso) VALUES ('{objeto.IdRolUsuario}', '{objeto.IdPermiso}');";
    }

    protected override string GenerarComandoEditar(PermisoRolUsuario objeto) {
        return $"UPDATE adv__rol_permiso SET id_rol_usuario = '{objeto.IdRolUsuario}', id_permiso = '{objeto.IdPermiso}' WHERE id_rol_permiso = {objeto.Id};";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__rol_permiso WHERE id_rol_permiso = {id};";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaPermisoRolUsuario criterio, string dato) {
        string comando;
        switch (criterio) {
            case FiltroBusquedaPermisoRolUsuario.Id:
                comando = $"SELECT * FROM adv__rol_permiso WHERE id_rol_permiso = {dato};";
                break;
            default:
                comando = "SELECT * FROM adv__rol_usuario;";
                break;
        }

        return comando;
    }

    protected override PermisoRolUsuario MapearEntidad(MySqlDataReader lectorDatos) {
        return new PermisoRolUsuario(
            lectorDatos.GetInt64("id_rol_permiso"),
            lectorDatos.GetInt64("id_rol_usuario"),
            lectorDatos.GetInt64("id_permiso")
        );
    }

    public void EliminarPorRol(long idRolUsuario) {
        using (var conexion = new MySqlConnection(ContextoBaseDatos.Configuracion.ToStringConexion())) {
            try {
                if (conexion.State != System.Data.ConnectionState.Open) conexion.Open();
            } catch (Exception) {
                throw new ExcepcionConexionServidorMySQL();
            }

            using (var comando = conexion.CreateCommand()) {
                comando.CommandText = "DELETE FROM adv__rol_permiso WHERE id_rol_usuario = @idRolUsuario";
                comando.Parameters.AddWithValue("@idRolUsuario", idRolUsuario);
                comando.ExecuteNonQuery();
            }
        }
    }
}