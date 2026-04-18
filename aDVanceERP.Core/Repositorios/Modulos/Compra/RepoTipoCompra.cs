using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Compra;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Compra {
    public class RepoTipoCompra : RepoEntidadBaseDatos<TipoCompra, FiltroBusquedaTipoCompra> {
        public RepoTipoCompra() : base("adv__tipo_compra", "id_tipo_compra") {
        }

        protected override string GenerarComandoAdicionar(TipoCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__tipo_compra (
                    nombre,
                    descripcion,
                    activo
                ) VALUES (
                    @nombre,
                    @descripcion,
                    @activo
                )
                """;

            parametros = new Dictionary<string, object>
            {
                { "@nombre", entidad.Nombre },
                { "@descripcion", entidad.Descripcion },
                { "@activo", entidad.Activo }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(TipoCompra entidad, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__tipo_compra 
                SET 
                    nombre = @nombre,
                    descripcion = @descripcion,
                    activo = @activo
                WHERE id_tipo_compra = @id_tipo_compra
                """;

            parametros = new Dictionary<string, object>
            {
                { "@nombre", entidad.Nombre },
                { "@descripcion", entidad.Descripcion },
                { "@activo", entidad.Activo },
                { "@id_tipo_compra", entidad.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            const string consulta = """
                -- Verificar si hay compras usando este tipo antes de eliminar
                UPDATE adv__compra 
                SET id_tipo_compra = 1 -- Tipo por defecto (General)
                WHERE id_tipo_compra = @id_tipo_compra;

                DELETE FROM adv__tipo_compra
                WHERE id_tipo_compra = @id_tipo_compra;
                """;

            parametros = new Dictionary<string, object>
            {
                { "@id_tipo_compra", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaTipoCompra filtroBusqueda, out Dictionary<string, object> parametros, params string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
            var consultaComun = $"SELECT * FROM adv__tipo_compra";

            var consulta = filtroBusqueda switch {
                FiltroBusquedaTipoCompra.Id => $"""
                    {consultaComun}
                    WHERE id_tipo_compra = @id_tipo_compra
                    """,
                FiltroBusquedaTipoCompra.Nombre => $"""
                    {consultaComun}
                    WHERE nombre LIKE @nombre
                    """,
                FiltroBusquedaTipoCompra.Activos => $"""
                    {consultaComun}
                    WHERE activo = 1
                    """,
                _ => consultaComun
            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaTipoCompra.Id => new Dictionary<string, object>
                {
                    { "@id_tipo_compra", long.Parse(criterio) }
                },
                FiltroBusquedaTipoCompra.Nombre => new Dictionary<string, object>
                {
                    { "@nombre", $"%{criterio}%" }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (TipoCompra, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lector) {
            return (new TipoCompra {
                Id = Convert.ToInt64(lector["id_tipo_compra"]),
                Nombre = Convert.ToString(lector["nombre"]) ?? "N/A",
                Descripcion = lector["descripcion"] != DBNull.Value ? Convert.ToString(lector["descripcion"]) ?? "N/A" : "N/A",
                Activo = Convert.ToBoolean(lector["activo"])
            }, []);
        }

        #region SINGLETON

        public static RepoTipoCompra Instancia { get; } = new RepoTipoCompra();

        #endregion

        #region UTILES

        public TipoCompra? ObtenerPorNombre(string nombre) {
            var consulta = """
                SELECT * FROM adv__tipo_compra 
                WHERE nombre = @nombre 
                LIMIT 1
                """;
            var parametros = new Dictionary<string, object>
            {
                { "@nombre", nombre }
            };

            return ContextoBaseDatos.EjecutarConsulta(consulta, parametros, MapearEntidad)
                .FirstOrDefault().entidadBase;
        }

        #endregion
    }
}