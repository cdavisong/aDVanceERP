using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos {
    public class RepoModulo : RepoEntidadBaseDatos<Modulo, FiltroBusquedaModulo> {
        public RepoModulo() : base("adv__modulo", "id_modulo") {
        }

        protected override string GenerarComandoAdicionar(Modulo entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidade) {
            var consulta = $"""
                INSERT INTO adv__modulo (
                nombre
            ) VALUES (
                @nombre
            );
            """;

            parametros = new Dictionary<string, object> {
                { "@nombre", entidad.Nombre }
             };

            return consulta;
        }

        protected override string GenerarComandoEditar(Modulo entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__modulo 
            SET 
                nombre = @nombre 
            WHERE id_modulo = @id;
            """;

            parametros = new Dictionary<string, object> {
                { "@nombre", entidad.Nombre },
                { "@id", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__modulo 
            WHERE id_modulo = @id;
            """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaModulo filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaModulo.Id => $"""
                    SELECT * 
                FROM adv__modulo 
                WHERE id_modulo = @id;
                """,
                FiltroBusquedaModulo.Nombre => $"""
                    SELECT * 
                FROM adv__modulo 
                WHERE nombre LIKE @nombre;
                """,
                _ => """
                SELECT * 
                FROM adv__modulo;
                """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaModulo.Id => new Dictionary<string, object> {
                    { "@id", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaModulo.Nombre => new Dictionary<string, object> {
                    { "@nombre", $"%{criterio}%" }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Modulo, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new Modulo(
                id: Convert.ToInt64(lector["id_modulo"]),
                nombre: Convert.ToString(lector["nombre"]) ?? string.Empty
            ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoModulo Instancia { get; } = new RepoModulo();

        #endregion
    }
}
