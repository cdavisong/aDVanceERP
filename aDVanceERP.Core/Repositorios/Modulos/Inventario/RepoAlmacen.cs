using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoAlmacen : RepoEntidadBaseDatos<Almacen, FiltroBusquedaAlmacen> {
    public RepoAlmacen() : base("adv__almacen", "id_almacen") { }

    protected override string GenerarComandoAdicionar(Almacen objeto) {
        return $"""
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
                '{objeto.Nombre}', 
                '{objeto.Descripcion}', 
                '{objeto.Direccion}', 
                {(objeto.Capacidad.HasValue ? objeto.Capacidad.Value.ToString("0,00", CultureInfo.InvariantCulture) : 0)}, 
                '{objeto.Tipo}', 
                {(objeto.Estado ? 1 : 0)}, 
                {objeto.Coordenadas?.Latitud.ToString(CultureInfo.InvariantCulture)}, 
                {objeto.Coordenadas?.Longitud.ToString(CultureInfo.InvariantCulture)}
            );
            """;
    }

    protected override string GenerarComandoEditar(Almacen objeto) {
        return $"""
            UPDATE adv__almacen 
            SET 
               nombre = '{objeto.Nombre}', 
               descripcion = '{objeto.Descripcion}', 
               direccion = '{objeto.Direccion}', 
               capacidad = {(objeto.Capacidad.HasValue ? objeto.Capacidad.Value.ToString("0,00", CultureInfo.InvariantCulture) : 0)}, 
               tipo = '{objeto.Tipo}', 
               estado = {(objeto.Estado ? 1 : 0)}, 
               coordenadas_latitud = {objeto.Coordenadas?.Latitud.ToString(CultureInfo.InvariantCulture)}, 
               coordenadas_longitud = {objeto.Coordenadas?.Longitud.ToString(CultureInfo.InvariantCulture)}
            WHERE id_almacen = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__almacen 
            WHERE id_almacen = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaAlmacen criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaAlmacen.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__almacen 
                    WHERE id_almacen = {dato};
                    """;
                break;
            case FiltroBusquedaAlmacen.Nombre:
                comando = $"""
                    SELECT * 
                    FROM adv__almacen 
                    WHERE LOWER(nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__almacen;
                    """;
                break;
        }

        return comando;
    }

    protected override Almacen MapearEntidad(MySqlDataReader lectorDatos) {
        return new Almacen(
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
        );
    }

    #region STATIC

    public static RepoAlmacen Instancia { get; } = new RepoAlmacen();

    #endregion

    #region UTILES

    

    #endregion
}