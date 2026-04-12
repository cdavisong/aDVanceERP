using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario {
    public class RepoAlmacen : RepoEntidadBaseDatos<Almacen, FiltroBusquedaAlmacen> {
        public RepoAlmacen() : base("adv__almacen", "id_almacen") { }

        protected override string GenerarComandoAdicionar(Almacen objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__almacen (
                    nombre, 
                    descripcion,
                    direccion, 
                    tipo,
                    estado
                ) 
                VALUES (
                    @nombre,
                    @descripcion,
                    @direccion,
                    @tipo,
                    @estado
                );
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@descripcion", objeto.Descripcion ?? string.Empty },
                { "@direccion", objeto.Direccion ?? string.Empty },
                { "@tipo", objeto.Tipo.ToString() },
                { "@estado", objeto.Estado ? 1 : 0 }
            };

            return consulta;
        }

        protected override string GenerarComandoEditar(Almacen objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                UPDATE adv__almacen 
                SET 
                   nombre = @nombre, 
                   descripcion = @descripcion, 
                   direccion = @direccion, 
                   tipo = @tipo, 
                   estado = @estado
                WHERE id_almacen = @id_almacen;
                """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@descripcion", objeto.Descripcion ?? string.Empty },
                { "@direccion", objeto.Direccion ?? string.Empty },
                { "@tipo", objeto.Tipo.ToString() },
                { "@estado", objeto.Estado ? 1 : 0 },
                { "@id_almacen", objeto.Id }
            };

            return consulta;
        }

        protected override string GenerarComandoEliminar(long id, out Dictionary<string, object> parametros) {
            var consulta = $"""
                DELETE FROM adv__almacen 
                WHERE id_almacen = @id_almacen;
                """;

            parametros = new Dictionary<string, object> {
                { "@id_almacen", id }
            };

            return consulta;
        }

        protected override string GenerarComandoObtener(FiltroBusquedaAlmacen filtroBusqueda, out Dictionary<string, object> parametros, string[] criteriosBusqueda) {
            var criterio = criteriosBusqueda.Length > 0 ? criteriosBusqueda[0] : string.Empty;
        
            var consulta = filtroBusqueda switch {
                FiltroBusquedaAlmacen.Id => $"""
                    SELECT * FROM adv__almacen 
                    WHERE id_almacen = @id_almacen;
                    """,
                FiltroBusquedaAlmacen.Nombre => $"""
                    SELECT * FROM adv__almacen 
                    WHERE nombre LIKE @nombre;
                    """,
                _ => "SELECT * FROM adv__almacen;"

            };

            parametros = filtroBusqueda switch {
                FiltroBusquedaAlmacen.Id => new Dictionary<string, object> {
                    { "@id_almacen", Convert.ToInt64(string.IsNullOrEmpty(criterio) ? "0" : criterio) } ,
                },
                FiltroBusquedaAlmacen.Nombre => new Dictionary<string, object> {
                    { "@nombre", $"%{criterio}%" }
                },
                _ => new Dictionary<string, object>()
            };

            return consulta;
        }

        protected override (Almacen, List<IEntidadBaseDatos>) MapearEntidad(MySqlDataReader lectorDatos) {
            return (new Almacen(
                id: Convert.ToInt64(lectorDatos["id_almacen"]),
                nombre: Convert.ToString(lectorDatos["nombre"]) ?? string.Empty,
                descripcion: lectorDatos["descripcion"] != DBNull.Value ? Convert.ToString(lectorDatos["descripcion"]) : string.Empty,
                direccion: Convert.ToString(lectorDatos["direccion"]) ?? string.Empty,
                tipo: Enum.TryParse<TipoAlmacen>(Convert.ToString(lectorDatos["tipo"]) ?? string.Empty, out var categoria) ? categoria : TipoAlmacen.Secundario,
                estado: Convert.ToBoolean(lectorDatos["estado"])
            ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoAlmacen Instancia { get; } = new RepoAlmacen();

        #endregion

        #region UTILES

        public bool ExisteAlmacenPrimario() {
            var consulta = """
                SELECT COUNT(*) FROM adv__almacen 
                WHERE tipo = 'Primario' AND estado = 1;
                """;
            var parametros = new Dictionary<string, object>();
            var resultado = ContextoBaseDatos.EjecutarConsultaEscalar<long>(consulta, parametros);

            return resultado > 0;
        }

        #endregion
    }
}