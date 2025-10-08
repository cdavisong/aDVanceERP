using aDVanceERP.Core.Modelos.Modulos.Finanzas;
using aDVanceERP.Core.Repositorios.BD;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Repositorios.Modulos.Finanzas;

public class RepoCuentaBancaria : RepoEntidadBaseDatos<CuentaBancaria, FiltroBusquedaCuentaBancaria> {
    public RepoCuentaBancaria() : base("adv__cuenta_bancaria", "id_cuenta_bancaria") { }

    protected override string GenerarComandoAdicionar(CuentaBancaria objeto) {
        return $"""
            INSERT INTO adv__cuenta_bancaria (
                alias, 
                numero_tarjeta, 
                moneda, 
                id_contacto
            ) VALUES (
                '{objeto.Alias}', 
                '{objeto.NumeroTarjeta}', 
                '{objeto.Moneda}', 
                {objeto.IdContacto}
            );
            """;
    }

    protected override string GenerarComandoEditar(CuentaBancaria objeto) {
        return $"""
            UPDATE adv__cuenta_bancaria 
            SET 
                alias = '{objeto.Alias}', 
                numero_tarjeta = '{objeto.NumeroTarjeta}', 
                moneda = '{objeto.Moneda}', 
                id_contacto = {objeto.IdContacto} 
            WHERE id_cuenta_bancaria = {objeto.Id};
            """;
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"""
            DELETE FROM adv__cuenta_bancaria 
            WHERE id_cuenta_bancaria = {id};
            """;
    }

    protected override string GenerarComandoObtener(FiltroBusquedaCuentaBancaria criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaCuentaBancaria.Id:
                comando = $"""
                    SELECT * 
                    FROM adv__cuenta_bancaria 
                    WHERE id_cuenta_bancaria = {dato};
                    """;
                break;
            case FiltroBusquedaCuentaBancaria.Alias:
                comando = $"""
                    SELECT * 
                    FROM adv__cuenta_bancaria 
                    WHERE alias LIKE '%{dato}%';
                    """;
                break;
            default:
                comando = """
                    SELECT * 
                    FROM adv__cuenta_bancaria;
                    """;
                break;
        }

        return comando;
    }

    protected override CuentaBancaria MapearEntidad(MySqlDataReader lector) {
        return new CuentaBancaria(
            id: Convert.ToInt64(lector["id_cuenta_bancaria"]),
            alias: Convert.ToString(lector["alias"]) ?? string.Empty,
            numeroTarjeta: Convert.ToString(lector["numero_tarjeta"]) ?? "0000 0000 0000 0000",
            moneda: Enum.TryParse<TipoMoneda>(Convert.ToString(lector["moneda"]) ?? "0", out var moneda) ? moneda : TipoMoneda.CUP,
            idContacto: lector["id_contacto"] != DBNull.Value ? Convert.ToInt64(lector["id_contacto"]) : 0
        );
    }

    #region STATIC

    public static RepoCuentaBancaria Instancia { get; } = new RepoCuentaBancaria();

    #endregion
}