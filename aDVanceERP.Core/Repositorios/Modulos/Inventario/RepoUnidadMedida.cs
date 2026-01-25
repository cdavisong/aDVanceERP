using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario {
    public class RepoUnidadMedida : RepoEntidadBaseDatos<UnidadMedida, FiltroBusquedaUnidadMedida> {
        public RepoUnidadMedida() : base("adv__unidad_medida", "id_unidad_medida") { }

        protected override string GenerarComandoAdicionar(UnidadMedida objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                    INSERT INTO adv__unidad_medida (
                    nombre,
                    abreviatura,
                    descripcion
                )
                VALUES (
                    @nombre,
                    @abreviatura,
                    @descripcion
                );
                """;

            parametros = new Dictionary<string, object> {
                {  "@nombre", objeto.Nombre  },
                { "@abreviatura", objeto.Abreviatura },
                { "@descripcion", objeto.Descripcion }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(UnidadMedida objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidades) {
            var consulta = $"""
                    UPDATE adv__unidad_medida
                SET
                    nombre = @nombre,
                    abreviatura = @abreviatura,
                    descripcion = @descripcion
                WHERE id_unidad_medida = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", objeto.Id },
                {  "@nombre", objeto.Nombre  },
                { "@abreviatura", objeto.Abreviatura },
                { "@descripcion", objeto.Descripcion }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                    UPDATE adv__producto
                SET id_unidad_medida = 0
                WHERE id_unidad_medida = @id;

                DELETE FROM adv__unidad_medida 
                WHERE id_unidad_medida = @id;
                """;

            parametros = new Dictionary<string, object> {
                { "@id", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaUnidadMedida filtroBusqueda, out Dictionary<string, object> parametros, params string[] criterios) {
            var criterio = criterios.Length > 0 ? criterios[0] : string.Empty;
            var consulta = filtroBusqueda switch {
                FiltroBusquedaUnidadMedida.Todos => """
                SELECT * 
                FROM adv__unidad_medida;
                """,
                FiltroBusquedaUnidadMedida.Id => $"""
                    SELECT * 
                FROM adv__unidad_medida 
                WHERE id_unidad_medida = @id;
                """,
                FiltroBusquedaUnidadMedida.Nombre => $"""
                    SELECT * 
                FROM adv__unidad_medida 
                WHERE nombre LIKE @nombre;
                """,
                FiltroBusquedaUnidadMedida.Abreviatura => $"""
                    SELECT * 
                FROM adv__unidad_medida 
                WHERE abreviatura LIKE @abreviatura; 
                """,
                _ => throw new ArgumentOutOfRangeException(nameof(filtroBusqueda), filtroBusqueda, null)
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaUnidadMedida.Todos => new Dictionary<string, object>(),
                FiltroBusquedaUnidadMedida.Id => new Dictionary<string, object> {
                    { "@id", long.Parse(criterio) }
                },
                FiltroBusquedaUnidadMedida.Nombre => new Dictionary<string, object> {
                    { "@nombre", $"%{criterio}%" }
                },
                FiltroBusquedaUnidadMedida.Abreviatura => new Dictionary<string, object> {
                    { "@abreviatura", $"%{criterio}%" }
                },
                _ => throw new ArgumentOutOfRangeException(nameof(filtroBusqueda), filtroBusqueda, null)
            };

            return consulta;
        }

        protected override (UnidadMedida, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
            return (new UnidadMedida(
                lectorDatos.GetInt64("id_unidad_medida"),
                lectorDatos.GetString("nombre"),
                lectorDatos.GetString("abreviatura"),
                lectorDatos.GetString("descripcion")
            ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoUnidadMedida Instancia { get; } = new RepoUnidadMedida();

        #endregion
    }

}
