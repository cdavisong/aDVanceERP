using System.Globalization;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;

public class RepoPago : RepoEntidadBaseDatos<Pago, FiltroBusquedaPago>, IRepoPago {
    public RepoPago() : base("adv__pago", "id_pago") { }

    protected override string GenerarComandoAdicionar(Pago objeto) {
        return $"INSERT INTO adv__pago (id_venta, metodo_pago, monto, fecha_confirmacion, estado) VALUES ({objeto.IdVenta}, '{objeto.MetodoPago}', {objeto.Monto.ToString(CultureInfo.InvariantCulture)}, '{objeto.FechaConfirmacion:yyyy-MM-dd HH:mm:ss}', '{objeto.Estado}');";
    }

    protected override string GenerarComandoEditar(Pago objeto) {
        return $"UPDATE adv__pago SET id_venta={objeto.IdVenta}, metodo_pago='{objeto.MetodoPago}', monto={objeto.Monto.ToString(CultureInfo.InvariantCulture)}, fecha_confirmacion='{objeto.FechaConfirmacion:yyyy-MM-dd HH:mm:ss}', estado='{objeto.Estado}' WHERE id_pago={objeto.Id};";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__pago WHERE id_pago={id};";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaPago criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaPago.Id:
                comando = $"SELECT * FROM adv__pago WHERE id_pago={dato};";
                break;
            case FiltroBusquedaPago.IdVenta:
                comando = $"SELECT * FROM adv__pago WHERE id_venta={dato};";
                break;
            default:
                comando = "SELECT * FROM adv__pago;";
                break;
        }

        return comando;
    }

    protected override Pago MapearEntidad(MySqlDataReader lectorDatos) {
        return new Pago(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_pago")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_venta")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("metodo_pago")),
            lectorDatos.GetDecimal(lectorDatos.GetOrdinal("monto"))
        ) {
            FechaConfirmacion = lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha_confirmacion")),
            Estado = lectorDatos.GetString(lectorDatos.GetOrdinal("estado"))
        };
    }
}