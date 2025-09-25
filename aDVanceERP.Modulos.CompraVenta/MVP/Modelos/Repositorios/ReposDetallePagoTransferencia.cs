using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios.Plantillas;
using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;

public class RepoDetallePagoTransferencia : RepoEntidadBaseDatos<DetallePagoTransferencia, FiltroBusquedaDetallePagoTransferencia>, IRepoDetallePagoTransferencia {
    public RepoDetallePagoTransferencia() : base("adv__detalle_pago_transferencia", "id_detalle_pago_transferencia") { }

    protected override string GenerarComandoAdicionar(DetallePagoTransferencia objeto) {
        return $"INSERT INTO adv__detalle_pago_transferencia (id_venta, id_tarjeta, numero_confirmacion, numero_transaccion) " +
               $"VALUES ({objeto.IdVenta}, {objeto.IdTarjeta}, '{objeto.NumeroConfirmacion}', '{objeto.NumeroTransaccion}');";
    }

    protected override string GenerarComandoEditar(DetallePagoTransferencia objeto) {
        return $"UPDATE adv__detalle_pago_transferencia SET id_venta = {objeto.IdVenta}, id_tarjeta = {objeto.IdTarjeta}, " +
               $"numero_confirmacion = '{objeto.NumeroConfirmacion}', numero_transaccion = '{objeto.NumeroTransaccion}' " +
               $"WHERE id_detalle_pago_transferencia = {objeto.Id};";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__detalle_pago_transferencia WHERE id_detalle_pago_transferencia = {id};";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaDetallePagoTransferencia criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaDetallePagoTransferencia.Id:
                comando =
                    $"SELECT * FROM adv__detalle_pago_transferencia WHERE id_detalle_pago_transferencia = {dato};";
                break;
            case FiltroBusquedaDetallePagoTransferencia.IdVenta:
                comando = $"SELECT * FROM adv__detalle_pago_transferencia WHERE id_venta = {dato};";
                break;
            case FiltroBusquedaDetallePagoTransferencia.IdTarjeta:
                comando = $"SELECT * FROM adv__detalle_pago_transferencia WHERE id_tarjeta = {dato};";
                break;
            default:
                comando = "SELECT * FROM adv__detalle_pago_transferencia;";
                break;
        }

        return comando;
    }

    protected override DetallePagoTransferencia MapearEntidad(MySqlDataReader lectorDatos) {
        return new DetallePagoTransferencia(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_detalle_pago_transferencia")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_venta")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_tarjeta")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("numero_confirmacion")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("numero_transaccion"))
        );
    }
}