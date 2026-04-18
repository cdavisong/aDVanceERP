using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Seguridad {
    public class RepoRol : RepoEntidadBaseDatos<Rol, FiltroBusquedaRol> {
        public RepoRol() : base("adv__rol", "id_rol") { }

        protected override string GenerarComandoAdicionar(Rol objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__rol (
                    nombre,
                    descripcion,
                    activo
                ) VALUES (
                    @nombre,
                    @descripcion,
                    @activo
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@descripcion", objeto.Descripcion ?? (object)DBNull.Value },
                { "@activo", Convert.ToInt32(objeto.Activo) }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Rol objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__rol 
                SET 
                    nombre = @nombre,
                    descripcion = @descripcion,
                    activo = @activo
                WHERE id_rol = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@descripcion", objeto.Descripcion ?? (object)DBNull.Value },
                { "@activo", Convert.ToInt32(objeto.Activo) },
                { "@id", objeto.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__rol 
                WHERE id_rol = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaRol filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 
                ? criteriosBusqueda[0] 
                : string.Empty;
            var consultaComun = $"""
                SELECT r.*
                FROM adv__rol r
                """;

            var consulta = filtroBusqueda switch {
                FiltroBusquedaRol.Nombre => $"""
                    {consultaComun} 
                    WHERE LOWER(r.nombre) LIKE LOWER(@nombre)
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaRol.Nombre => new Dictionary<string, object> {
                    { "@nombre", $"%{criterio}%" }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Rol, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
            return (new Rol() {
                Id = Convert.ToInt64(lectorDatos["id_rol"]),
                Nombre = Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
                Descripcion = lectorDatos["descripcion"] != DBNull.Value ? Convert.ToString(lectorDatos["descripcion"]) : null,
                Activo = Convert.ToBoolean(lectorDatos["activo"])
            }, new List<IEntidadBaseDatos>());
        }

        #region SINGLETON

        public static RepoRol Instancia { get; } = new RepoRol();

        #endregion

        #region UTILES

        public List<Rol> ObtenerRolesActivos() {
            var consulta = $"""
                SELECT r.*
                FROM adv__rol r
                WHERE r.activo = 1
                ORDER BY r.nombre;
                """;

            var resultados = ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearEntidad);

            return resultados.Select(r => r.Item1).ToList();
        }

        public Rol? ObtenerPorNombre(string nombre) {
            var consulta = $"""
                SELECT r.*
                FROM adv__rol r
                WHERE LOWER(r.nombre) = LOWER(@nombre)
                LIMIT 1;
                """;

            var parametros = new Dictionary<string, object> {
                { "@nombre", nombre }
            };

            var resultados = ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad);

            return resultados.Select(r => r.Item1).FirstOrDefault();
        }

        #endregion
    }
}