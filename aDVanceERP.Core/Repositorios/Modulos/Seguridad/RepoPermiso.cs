using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad {
    public class RepoPermiso : RepoEntidadBaseDatos<Permiso, FiltroBusquedaPermiso> {
        public RepoPermiso() : base("adv__permiso", "id_permiso") { }

        protected override string GenerarComandoAdicionar(Permiso objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__permiso (
                id_modulo, 
                nombre
            ) VALUES (
                @id_modulo, 
                @nombre
            );
            """;

            parametros = new Dictionary<string, object> {
                { "@id_modulo", objeto.IdModulo },
                { "@nombre", objeto.Nombre }
            };

            return consulta;
        }
        protected override string GenerarComandoEditar(Permiso objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__permiso 
            SET 
                id_modulo = @id_modulo, 
                nombre = @nombre 
            WHERE id_permiso = @id_permiso;
            """;

            parametros = new Dictionary<string, object> {
                { "@id_modulo", objeto.IdModulo },
                { "@nombre", objeto.Nombre },
                { "@id_permiso", objeto.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__permiso 
            WHERE id_permiso = @id_permiso;
            """;

            parametros = new Dictionary<string, object> {
                { "@id_permiso", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaPermiso filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaPermiso.Id => $"""
                    SELECT * 
                FROM adv__permiso 
                WHERE id_permiso = @id_permiso;
                """,
                FiltroBusquedaPermiso.IdModulo => $"""
                    SELECT * 
                FROM adv__permiso 
                WHERE id_modulo = @id_modulo;
                """,
                FiltroBusquedaPermiso.Nombre => $"""
                    SELECT * 
                FROM adv__permiso 
                WHERE nombre LIKE CONCAT('%', @nombre, '%');
                """,
                _ => """
                SELECT * 
                FROM adv__permiso;
                """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaPermiso.Id => new Dictionary<string, object> {
                    { "@id_permiso", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaPermiso.IdModulo => new Dictionary<string, object> {
                    { "@id_modulo", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaPermiso.Nombre => new Dictionary<string, object> {
                    { "@nombre", criterio }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Permiso, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new Permiso(
                id: Convert.ToInt64(lector["id_permiso"]),
                idModulo: Convert.ToInt64(lector["id_modulo"]),
                nombre: lector["nombre"].ToString()
            ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoPermiso Instancia { get; } = new RepoPermiso();

        #endregion

        #region UTILES

        public List<Permiso> ObtenerPorNombreModulo(string nombreModulo) {
            var comando = $"""
                SELECT p.* 
            FROM adv__permiso p
            JOIN adv__modulo m ON p.id_modulo = m.id_modulo
            WHERE m.nombre = @nombreModulo;
            """;
            var parametros = new Dictionary<string, object> {
                { "@nombreModulo", nombreModulo }
            };

            return [.. ContextoBaseDatos.EjecutarConsulta(comando, parametros, MapearEntidad).Select(p => p.entidadBase)];
        }

        public List<Permiso> ObtenerPorIdRolUsuario(long idRolUsuario) {
            var comando = $"""
                SELECT p.* 
            FROM adv__permiso p
            JOIN adv__rol_permiso rp ON p.id_permiso = rp.id_permiso
            WHERE rp.id_rol_usuario = @idRolUsuario;
            """;
            var parametros = new Dictionary<string, object> {
                { "@idRolUsuario", idRolUsuario }
            };

            return [.. ContextoBaseDatos.EjecutarConsulta(comando, parametros, MapearEntidad).Select(p => p.entidadBase)];
        }

        #endregion
    }
}
