using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.BD;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Maestros {
    public class RepoCorreoContacto : RepoEntidadBaseDatos<CorreoContacto, FiltroBusquedaCorreoContacto> {
        public RepoCorreoContacto() : base("adv__correo_contacto", "id_correo_contacto") { }

        protected override string GenerarComandoAdicionar(CorreoContacto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                INSERT INTO adv__correo_contacto (
                    direccion_correo,
                    categoria,
                    id_persona
                ) VALUES (
                    @direccion_correo,
                    @categoria,
                    @id_persona
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@direccion_correo", entidad.DireccionCorreo },
                { "@categoria", entidad.Categoria.ToString() },
                { "@id_persona", entidad.IdPersona }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(CorreoContacto entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                UPDATE adv__correo_contacto SET
                    direccion_correo = @direccion_correo,
                    categoria = @categoria,
                    id_persona = @id_persona
                WHERE id_correo_contacto = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@direccion_correo", entidad.DireccionCorreo },
                { "@categoria", entidad.Categoria.ToString() },
                { "@id_persona", entidad.IdPersona },
                { "@id", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__correo_contacto
                WHERE id_correo_contacto = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaCorreoContacto filtroBusqueda, out Dictionary<string, object> parametros, params string[] criterios) {
            var criterio = criterios.Length > 0 ? criterios[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaCorreoContacto.Id => $"""
                    SELECT *
                    FROM adv__correo_contacto
                    WHERE id_correo_contacto = @id_correo_electronico;
                    """,
                FiltroBusquedaCorreoContacto.DireccionCorreo => $"""
                    SELECT *
                    FROM adv__correo_contacto
                    WHERE direccion_correo = @direccion_correo;
                    """,
                FiltroBusquedaCorreoContacto.Categoria => $"""
                    SELECT *
                    FROM adv__correo_contacto
                    WHERE categoria = @categoria;
                    """,
                FiltroBusquedaCorreoContacto.IdPersona => $"""
                    SELECT *
                    FROM adv__correo_contacto
                    WHERE id_persona = @id_persona;
                    """,
                _ => $"""
                    SELECT *
                    FROM adv__correo_contacto;
                    """
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaCorreoContacto.Id => new Dictionary<string, object> {
                    { "@id_correo_electronico", Convert.ToInt64(criterio) }
                },
                FiltroBusquedaCorreoContacto.DireccionCorreo => new Dictionary<string, object> {
                    { "@direccion_correo", criterio }
                },
                FiltroBusquedaCorreoContacto.Categoria => new Dictionary<string, object> {
                    { "@categoria", criterio }
                },
                FiltroBusquedaCorreoContacto.IdPersona => new Dictionary<string, object> {
                    { "@id_persona", Convert.ToInt64(criterio) }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (CorreoContacto, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new CorreoContacto(
                id: Convert.ToInt64(lector["id_correo_contacto"]),
                direccionCorreo: Convert.ToString(lector["direccion_correo"]) ?? string.Empty,
                categoria: Enum.TryParse<CategoriaCorreoContacto>(Convert.ToString(lector["categoria"] ?? "Otro"), out var categoria) ? categoria : CategoriaCorreoContacto.Otro,
                idPersona: Convert.ToInt64(lector["id_persona"])
            ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoCorreoContacto Instancia { get; } = new RepoCorreoContacto();

        #endregion
    }
}
