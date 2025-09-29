using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Repositorios.Modulos.Contactos;

using MySql.Data.MySqlClient;

using System.Globalization;

namespace aDVanceERP.Core.Repositorios.Modulos.Finanzas;

public class RepoCaja : RepoEntidadBaseDatos<Caja, FiltroBusquedaCaja> {
    public RepoCaja() : base("adv__caja", "id_caja") { }

    protected override string GenerarComandoAdicionar(Caja objeto) {
        return $"""
            INSERT INTO adv__caja (
                fecha_apertura, 
                saldo_inicial, 
                saldo_actual, 
                fecha_cierre, 
                estado, 
                id_cuenta_usuario
            ) VALUES (
                '{objeto.FechaApertura:yyyy-MM-dd HH:mm:ss}', 
                {objeto.SaldoInicial.ToString(CultureInfo.InvariantCulture)}, 
                {objeto.SaldoActual.ToString(CultureInfo.InvariantCulture)}, 
                '{objeto.FechaCierre:yyyy-MM-dd HH:mm:ss}', 
                '{objeto.Estado}', 
                {objeto.IdCuentaUsuario}
            );
            """;
    }

    protected override string GenerarComandoEditar(Caja objeto) {
        return $"""
            UPDATE adv__caja 
            SET 
                estado = '{objeto.Estado}', 
                saldo_actual = {objeto.SaldoActual.ToString(CultureInfo.InvariantCulture)}, 
                fecha_cierre = '{objeto.FechaCierre:yyyy-MM-dd HH:mm:ss}' 
            WHERE id_caja = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__movimiento_caja 
            WHERE id_caja = {id};
                
            DELETE FROM adv__caja  
            WHERE id_caja = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaCaja filtroBusqueda, string criterio) {
        string? comando;

        switch (filtroBusqueda) {
            case FiltroBusquedaCaja.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__caja 
                    WHERE id_caja = {criterio};
                    """;
                break;
            case FiltroBusquedaCaja.FechaApertura:
                comando = $"""
                    SELECT * 
                    FROM adv__caja 
                    WHERE DATE(fecha_apertura) = '{criterio}';
                    """;
                break;
            case FiltroBusquedaCaja.Estado:
                comando = $"""
                    SELECT * 
                    FROM adv__caja 
                    WHERE estado = '{criterio}';
                    """;
                break;
            case FiltroBusquedaCaja.FechaCierre:
                comando = $"""
                    SELECT * 
                    FROM adv__caja 
                    WHERE DATE(fecha_cierre) = '{criterio}';
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__caja;
                    """;
                break;
        }

        return comando;
    }

    protected override Caja MapearEntidad(MySqlDataReader lectorDatos) {
        return new Caja(
            id: Convert.ToInt64(lectorDatos["id_caja"]),
            fechaApertura: Convert.ToDateTime(lectorDatos["fecha_apertura"]),
            saldoInicial: Convert.ToDecimal(lectorDatos["saldo_inicial"], CultureInfo.InvariantCulture),
            saldoActual: Convert.ToDecimal(lectorDatos["saldo_actual"], CultureInfo.InvariantCulture),
            fechaCierre: lectorDatos["fecha_cierre"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(lectorDatos["fecha_cierre"]),
            idCuentaUsuario: Convert.ToInt64(lectorDatos["id_cuenta_usuario"])) {
            Estado = Enum.TryParse<EstadoCaja>(lectorDatos["estado"].ToString(), out var estado) ? estado : EstadoCaja.Inactiva
        };
    }

    #region STATIC

    public static RepoCaja Instancia { get; } = new RepoCaja();

    #endregion
}
