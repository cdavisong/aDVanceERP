using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad {
    public class RepoRolPermisoUsuario : RepoEntidadBaseDatos<RolPermisoUsuario, FiltroBusquedaPermisoRolUsuario> {
        public RepoRolPermisoUsuario() : base("adv__rol_permiso", "id_rol_permiso") { }

        protected override string GenerarComandoAdicionar(RolPermisoUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__rol_permiso (
                id_rol_usuario, 
                id_permiso
            ) VALUES (
                @idRolUsuario, 
                @idPermiso);
            """;

            parametros = new Dictionary<string, object> {
                { "@idRolUsuario", objeto.IdRolUsuario },
                { "@idPermiso", objeto.IdPermiso }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(RolPermisoUsuario objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__rol_permiso 
            SET 
                id_rol_usuario = @idRolUsuario, 
                id_permiso = @idPermiso 
            WHERE id_rol_permiso = @idRolPermiso;
            """;

            parametros = new Dictionary<string, object> {
                { "@idRolPermiso", objeto.Id },
                { "@idRolUsuario", objeto.IdRolUsuario },
                { "@idPermiso", objeto.IdPermiso }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__rol_permiso 
            WHERE id_rol_permiso = @idRolPermiso;
            """;

            parametros = new Dictionary<string, object> {
                { "@idRolPermiso", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaPermisoRolUsuario filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"""
                SELECT rp.*, p.id_modulo, p.nombre
            FROM adv__rol_permiso rp
            LEFT JOIN adv__permiso p ON rp.id_permiso = p.id_permiso
            """;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaPermisoRolUsuario.Id => $"""
                    {consultaComun} 
                WHERE rp.id_rol_permiso = @id_rol_permiso;            
                """,
                FiltroBusquedaPermisoRolUsuario.IdRolUsuario => $"""
                    {consultaComun} 
                WHERE rp.id_rol_usuario = @id_rol_usuario;            
                """,
                _ => $"""
                    {consultaComun};
                """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPermisoRolUsuario.Id => new Dictionary<string, object> {
                    { "@id_rol_permiso", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaPermisoRolUsuario.IdRolUsuario => new Dictionary<string, object> {
                    { "@id_rol_usuario", Convert.ToInt64(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (RolPermisoUsuario, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new RolPermisoUsuario(
                id: Convert.ToInt64(lector["id_rol_permiso"]),
                idRolUsuario: Convert.ToInt64(lector["id_rol_usuario"]),
                idPermiso: Convert.ToInt64(lector["id_permiso"])) {
                IdModulo = Convert.ToInt64(lector["id_modulo"]),
                NombrePermiso = Convert.ToString(lector["nombre"]) ?? string.Empty
            }, new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoRolPermisoUsuario Instancia { get; } = new RepoRolPermisoUsuario();

        #endregion

        #region UTILES

        public void EliminarPorRolUsuario(long idRolUsuario) {
            var consulta = """
            DELETE FROM adv__rol_permiso 
            WHERE id_rol_usuario = @idRolUsuario
            """;
            var parametros = new Dictionary<string, object> {
                { "@idRolUsuario", idRolUsuario }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
        }

        #endregion
    }
}