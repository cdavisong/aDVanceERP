using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoUnidadMedida : RepoEntidadBaseDatos<UnidadMedida, FiltroBusquedaUnidadMedida> {
    public RepoUnidadMedida() : base("adv__unidad_medida", "id_unidad_medida") { }

    protected override string GenerarComandoAdicionar(UnidadMedida objeto) {
        return $"""
                INSERT INTO adv__unidad_medida (
                    nombre,
                    abreviatura,
                    descripcion
                )
                VALUES (
                    '{objeto.Nombre}',
                    '{objeto.Abreviatura}',
                    '{objeto.Descripcion}'
                );
                """;
    }

    protected override string GenerarComandoEditar(UnidadMedida objeto) {
        return $"""
                UPDATE adv__unidad_medida
                SET
                    nombre = '{objeto.Nombre}',
                    abreviatura = '{objeto.Abreviatura}',
                    descripcion = '{objeto.Descripcion}'
                WHERE id_unidad_medida = {objeto.Id};
                """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
                UPDATE adv__detalle_producto
                SET id_unidad_medida = 0
                WHERE id_unidad_medida = {id};

                DELETE FROM adv__unidad_medida 
                WHERE id_unidad_medida = {id};
                """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaUnidadMedida criterio, string dato) {
        var comando = criterio switch {
            FiltroBusquedaUnidadMedida.Todos => """
                SELECT * 
                FROM adv__unidad_medida;
                """,
            FiltroBusquedaUnidadMedida.Id => $"""
                SELECT * 
                FROM adv__unidad_medida 
                WHERE id_unidad_medida = {dato};
                """,
            FiltroBusquedaUnidadMedida.Nombre => $"""
                SELECT * 
                FROM adv__unidad_medida 
                WHERE nombre LIKE '%{dato}%';
                """,
            FiltroBusquedaUnidadMedida.Abreviatura => $"""
                SELECT * 
                FROM adv__unidad_medida 
                WHERE abreviatura LIKE '%{dato}%'; 
                """,
            _ => throw new ArgumentOutOfRangeException(nameof(criterio), criterio, null)
        };

        return comando;
    }

    protected override UnidadMedida MapearEntidad(MySqlDataReader lectorDatos) {
        return new UnidadMedida(
            lectorDatos.GetInt64("id_unidad_medida"),
            lectorDatos.GetString("nombre"),
            lectorDatos.GetString("abreviatura"),
            lectorDatos.GetString("descripcion")
        );
    }

    #region STATIC

    public static RepoUnidadMedida Instancia = new RepoUnidadMedida();

    #endregion
}

