using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Inventario;

public class RepoTipoMovimiento : RepoEntidadBaseDatos<TipoMovimiento, FiltroBusquedaTipoMovimiento> {
    public RepoTipoMovimiento() : base("adv__tipo_movimiento", "id_tipo_movimiento") { }

    protected override string GenerarComandoAdicionar(TipoMovimiento objeto) {
        return $"""
            INSERT INTO adv__tipo_movimiento (
                nombre, 
                efecto
            ) 
            VALUES (
                '{objeto.Nombre}', 
                '{(int)objeto.Efecto}'
            );
            """;
    }

    protected override string GenerarComandoEditar(TipoMovimiento objeto) {
        return $"""
            UPDATE adv__tipo_movimiento 
            SET 
                nombre = '{objeto.Nombre}', 
                efecto = '{(int)objeto.Efecto}' 
            WHERE id_tipo_movimiento = '{objeto.Id}';
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__tipo_movimiento 
            WHERE id_tipo_movimiento = '{id}';
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaTipoMovimiento criterio, string dato) {
        string? comando;

        switch (criterio) {
            case FiltroBusquedaTipoMovimiento.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__tipo_movimiento 
                    WHERE id_tipo_movimiento = {dato};
                    """;
                break;
            case FiltroBusquedaTipoMovimiento.Nombre:
                comando = $"""
                    SELECT *
                    FROM adv__tipo_movimiento 
                    WHERE LOWER(nombre) LIKE LOWER('%{dato}%');
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__tipo_movimiento;
                    """;
                break;
        }

        return comando;
    }

    protected override TipoMovimiento MapearEntidad(MySqlDataReader lectorDatos) {
        return new TipoMovimiento(
            lectorDatos.GetInt64(lectorDatos.GetOrdinal("id_tipo_movimiento")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("nombre")),
            (EfectoMovimiento)Enum.Parse(typeof(EfectoMovimiento), lectorDatos.GetValue(lectorDatos.GetOrdinal("efecto")).ToString())
        );
    }

    #region STATIC

    public static RepoTipoMovimiento Instancia = new RepoTipoMovimiento();

    #endregion
}