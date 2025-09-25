using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios.Plantillas;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.Finanzas.MVP.Modelos.Repositorios;

public class RepoCuentaBancaria : RepoEntidadBaseDatos<CuentaBancaria, FiltroBusquedaCuentaBancaria>, IRepoCuentaBancaria {
    public RepoCuentaBancaria() : base("adv__cuenta_bancaria", "id_cuenta_bancaria") { }

    protected override string GenerarComandoAdicionar(CuentaBancaria objeto) {
        return $"INSERT INTO adv__cuenta_bancaria (alias, numero_tarjeta, moneda, id_contacto) VALUES ('{objeto.Alias}', '{objeto.NumeroTarjeta}', {(int) objeto.Moneda}, {objeto.IdContacto});";
    }

    protected override string GenerarComandoEditar(CuentaBancaria objeto) {
        return $"UPDATE adv__cuenta_bancaria SET alias='{objeto.Alias}', numero_tarjeta='{objeto.NumeroTarjeta}', moneda={(int) objeto.Moneda}, id_contacto={objeto.IdContacto} WHERE id_cuenta_bancaria={objeto.Id};";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__cuenta_bancaria WHERE id_cuenta_bancaria={id};";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaCuentaBancaria criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaCuentaBancaria.Id:
                comando = $"SELECT * FROM adv__cuenta_bancaria WHERE id_cuenta_bancaria={dato};";
                break;
            case FiltroBusquedaCuentaBancaria.Alias:
                comando = $"SELECT * FROM adv__cuenta_bancaria WHERE alias LIKE '%{dato}%';";
                break;
            default:
                comando = "SELECT * FROM adv__cuenta_bancaria;";
                break;
        }

        return comando;
    }

    protected override CuentaBancaria MapearEntidad(MySqlDataReader lectorDatos) {
        return new CuentaBancaria(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_cuenta_bancaria")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("alias")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("numero_tarjeta")),
            (TipoMoneda) lectorDatos.GetInt32(lectorDatos.GetOrdinal("moneda")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_contacto"))
        );
    }
}