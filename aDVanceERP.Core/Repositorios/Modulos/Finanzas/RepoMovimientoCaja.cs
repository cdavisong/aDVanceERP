using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Repositorios.Modulos.Contactos;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Finanzas;

public class RepoMovimientoCaja : RepoEntidadBaseDatos<MovimientoCaja, FiltroBusquedaMovimientoCaja> {
    public RepoMovimientoCaja() : base("adv__movimiento_caja", "id_movimiento_caja") { }

    protected override string GenerarComandoAdicionar(MovimientoCaja objeto) {
        return $"""
            INSERT INTO adv__movimiento_caja (id_caja, fecha, monto, tipo, concepto, id_pago, id_usuario, observaciones)
            VALUES (
                {objeto.IdCaja},
                '{objeto.Fecha:yyyy-MM-dd HH:mm:ss}',
                {objeto.Monto.ToString(CultureInfo.InvariantCulture)},
                '{objeto.Tipo}',
                '{objeto.Concepto}',
                {objeto.IdPago},
                {objeto.IdUsuario},
                '{objeto.Observaciones}'
            );
            """;
    }

    protected override string GenerarComandoEditar(MovimientoCaja objeto) {
        return $"""
            UPDATE adv__movimiento_caja
            SET
                fecha = '{objeto.Fecha:yyyy-MM-dd HH:mm:ss}',
                monto = {objeto.Monto.ToString(CultureInfo.InvariantCulture)},
                tipo = '{objeto.Tipo}',
                concepto= '{objeto.Concepto}',
                observaciones = '{objeto.Observaciones}'
            WHERE id_movimiento_caja = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__movimiento_caja 
            WHERE id_movimiento_caja = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaMovimientoCaja filtroBusqueda, string criterio) {
        var comando = string.Empty;

        switch (filtroBusqueda) {
            case FiltroBusquedaMovimientoCaja.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento_caja 
                    WHERE id_movimiento_caja = {criterio};
                    """;
                break;
            case FiltroBusquedaMovimientoCaja.IdPago:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento_caja 
                    WHERE id_pago = {criterio};
                    """;
                break;
            case FiltroBusquedaMovimientoCaja.IdCaja:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento_caja 
                    WHERE id_caja = {criterio};
                    """;
                break;
            case FiltroBusquedaMovimientoCaja.Fecha:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento_caja 
                    WHERE DATE(fecha) = '{criterio}';
                    """;
                break;
            case FiltroBusquedaMovimientoCaja.Tipo:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento_caja 
                    WHERE tipo = '{criterio}';
                    """;
                break;
            case FiltroBusquedaMovimientoCaja.Concepto:
                comando = $"""
                    SELECT * 
                    FROM adv__movimiento_caja 
                    WHERE concepto LIKE '%{criterio}%';
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__movimiento_caja;
                    """;
                break;
        }

        return comando;
    }

    protected override MovimientoCaja MapearEntidad(MySqlDataReader lectorDatos) {
        return new MovimientoCaja(
            id: Convert.ToInt64(lectorDatos["id_movimiento_caja"]),
            idCaja: Convert.ToInt64(lectorDatos["id_caja"]),
            fecha: Convert.ToDateTime(lectorDatos["fecha"]),
            monto: Convert.ToDecimal(lectorDatos["monto"], CultureInfo.InvariantCulture),
            tipo: Enum.TryParse<TipoMovimientoCaja>(Convert.ToString(lectorDatos["tipo"]) ?? string.Empty, out var tipo) ? tipo : TipoMovimientoCaja.Desconocido,
            concepto: Convert.ToString(lectorDatos["concepto"]) ?? string.Empty,
            idPago: lectorDatos["id_pago"] != DBNull.Value ? Convert.ToInt64(lectorDatos["id_pago"]) : 0,
            idUsuario: Convert.ToInt64(lectorDatos["id_usuario"]),
            observaciones: Convert.ToString(lectorDatos["observaciones"]) ?? string.Empty
        );
    }

    #region STATIC

    public static RepoMovimientoCaja Instancia { get; } = new RepoMovimientoCaja();

    #endregion
}
