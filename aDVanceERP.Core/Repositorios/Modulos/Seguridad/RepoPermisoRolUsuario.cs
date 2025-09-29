using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Repositorios.Modulos.Inventario;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad;

public class RepoPermisoRolUsuario : RepoEntidadBaseDatos<PermisoRolUsuario, FiltroBusquedaPermisoRolUsuario> {
    public RepoPermisoRolUsuario() : base("adv__rol_permiso", "id_rol_permiso") { }

    protected override string GenerarComandoAdicionar(PermisoRolUsuario objeto) {
        return $"""
            INSERT INTO adv__rol_permiso (
                id_rol_usuario, 
                id_permiso
            ) VALUES (
                {objeto.IdRolUsuario}, 
                {objeto.IdPermiso});
            """;
    }

    protected override string GenerarComandoEditar(PermisoRolUsuario objeto) {
        return $"""
            UPDATE adv__rol_permiso 
            SET 
                id_rol_usuario = {objeto.IdRolUsuario}, 
                id_permiso = {objeto.IdPermiso} 
            WHERE id_rol_permiso = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__rol_permiso 
            WHERE id_rol_permiso = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaPermisoRolUsuario filtroBusqueda, string criterio) {
        string comando;
        
        switch (filtroBusqueda) {
            case FiltroBusquedaPermisoRolUsuario.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__rol_permiso
                    WHERE id_rol_permiso = {criterio};
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

    protected override PermisoRolUsuario MapearEntidad(MySqlDataReader lector) {
        return new PermisoRolUsuario(
            id: Convert.ToInt64(lector["id_rol_permiso"]),
            idRolUsuario: Convert.ToInt64(lector["id_rol_usuario"]),
            idPermiso: Convert.ToInt64(lector["id_permiso"]));
    }

    #region STATIC

    public static RepoPermisoRolUsuario Instancia { get; } = new RepoPermisoRolUsuario();

    #endregion

    #region UTILES

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

    #endregion
}