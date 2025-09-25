using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoAlmacen : RepoEntidadBaseDatos<Almacen, FiltroBusquedaAlmacen> {
    public RepoAlmacen() : base("adv__almacen", "id_almacen") { }

    protected override string GenerarComandoAdicionar(Almacen objeto) {
        return $"""
            INSERT INTO adv__almacen (
                nombre, 
                direccion, 
                autorizo_venta, 
                notas
            ) 
            VALUES (
                '{objeto.Nombre}', 
                '{objeto.Direccion}', 
                '1', 
                '{objeto.Descripcion}'
            );
            """;
    }

    protected override string GenerarComandoEditar(Almacen objeto) {
        return $"""
            UPDATE adv__almacen 
            SET 
                nombre = '{objeto.Nombre}', 
                direccion = '{objeto.Direccion}', 
                autorizo_venta = '1', 
                notas = '{objeto.Descripcion}' 
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
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_almacen")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("notas")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("direccion")),
            0,
            TipoAlmacen.Principal,
            true,
            null
        );
    }

    #region STATIC

    public static RepoAlmacen Instancia = new RepoAlmacen();

    #endregion
}