using aDVanceERP.Core.Modelos.Comun.Interfaces;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario {
    public class RepoAlmacen : RepoEntidadBaseDatos<Almacen, FiltroBusquedaAlmacen> {
        public RepoAlmacen() : base("adv__almacen", "id_almacen") { }

        protected override string GenerarComandoAdicionar(Almacen objeto, out Dictionary<string, object> parametros, params IEntidadBaseDatos[] entidadesExtra) {
            var consulta = $"""
                INSERT INTO adv__almacen (
                nombre, 
                descripcion,
                direccion, 
                capacidad,
                tipo,
                estado,
                coordenadas_latitud,
                coordenadas_longitud
            ) 
            VALUES (
                @nombre,
                @descripcion,
                @direccion,
                @capacidad,
                @tipo,
                @estado,
                @coordenadas_latitud,
                @coordenadas_longitud
            );
            """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@descripcion", objeto.Descripcion ?? string.Empty },
                { "@direccion", objeto.Direccion ?? string.Empty },
                { "@capacidad", objeto.Capacidad.HasValue ? objeto.Capacidad.Value : 0 },
                { "@tipo", objeto.Tipo.ToString() },
                { "@estado", objeto.Estado ? 1 : 0 },
                { "@coordenadas_latitud", objeto.Coordenadas?.Latitud ?? 0 },
                { "@coordenadas_longitud", objeto.Coordenadas?.Longitud ?? 0 }
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
               capacidad = @capacidad, 
               tipo = @tipo, 
               estado = @estado, 
               coordenadas_latitud = @coordenadas_latitud, 
               coordenadas_longitud = @coordenadas_longitud
            WHERE id_almacen = @id_almacen;
            """;

            parametros = new Dictionary<string, object> {
                { "@nombre", objeto.Nombre },
                { "@descripcion", objeto.Descripcion ?? string.Empty },
                { "@direccion", objeto.Direccion ?? string.Empty },
                { "@capacidad", objeto.Capacidad.HasValue ? objeto.Capacidad.Value : 0 },
                { "@tipo", objeto.Tipo.ToString() },
                { "@estado", objeto.Estado ? 1 : 0 },
                { "@coordenadas_latitud", objeto.Coordenadas?.Latitud ?? 0 },
                { "@coordenadas_longitud", objeto.Coordenadas?.Longitud ?? 0 },
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
                    { "@id_almacen", Convert.ToInt64(criterio) }
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
                capacidad: lectorDatos["capacidad"] != DBNull.Value ? Convert.ToSingle(lectorDatos["capacidad"], CultureInfo.InvariantCulture) : 0,
                tipo: Enum.TryParse<TipoAlmacen>(Convert.ToString(lectorDatos["tipo"]) ?? string.Empty, out var categoria) ? categoria : TipoAlmacen.Secundario,
                estado: Convert.ToBoolean(lectorDatos["estado"]),
                coordenadas: new Modelos.Comun.CoordenadasGeograficas(
                    latitud: lectorDatos["coordenadas_latitud"] != DBNull.Value ? Convert.ToDouble(lectorDatos["coordenadas_latitud"]) : 0,
                    longitud: lectorDatos["coordenadas_longitud"] != DBNull.Value ? Convert.ToDouble(lectorDatos["coordenadas_longitud"]) : 0)
            ), new List<IEntidadBaseDatos>());
        }

        #region STATIC

        public static RepoAlmacen Instancia { get; } = new RepoAlmacen();

        #endregion

        #region UTILES



        #endregion
    }
}