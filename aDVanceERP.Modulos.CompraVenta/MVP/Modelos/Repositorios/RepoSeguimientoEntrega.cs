using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios.Plantillas;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;

public class RepoSeguimientoEntrega : RepoEntidadBaseDatos<SeguimientoEntrega, FiltroBusquedaSeguimientoEntrega>, IRepoSeguimientoEntrega {
    public RepoSeguimientoEntrega() : base("adv__seguimiento_entrega", "id_seguimiento_entrega") { }

    protected override string GenerarComandoAdicionar(SeguimientoEntrega objeto) {
        return $"INSERT INTO adv__seguimiento_entrega (id_venta, id_mensajero, fecha_asignacion, fecha_entrega, fecha_pago, observaciones) VALUES ({objeto.IdVenta}, {objeto.IdMensajero}, '{objeto.FechaAsignacion:yyyy-MM-dd HH:mm:ss}', '{objeto.FechaEntrega:yyyy-MM-dd HH:mm:ss}', '{objeto.FechaPago:yyyy-MM-dd HH:mm:ss}', '{objeto.Observaciones}')";
    }

    protected override string GenerarComandoEditar(SeguimientoEntrega objeto) {
        return $"UPDATE adv__seguimiento_entrega SET id_venta = {objeto.IdVenta}, id_mensajero = {objeto.IdMensajero}, fecha_asignacion = '{objeto.FechaAsignacion:yyyy-MM-dd HH:mm:ss}', fecha_entrega = '{objeto.FechaEntrega:yyyy-MM-dd HH:mm:ss}', fecha_pago = '{objeto.FechaPago:yyyy-MM-dd HH:mm:ss}', observaciones = '{objeto.Observaciones}' WHERE id_seguimiento_entrega = {objeto.Id}";
    }

    protected override string GenerarComandoEliminar(long id) {
        return $"DELETE FROM adv__seguimiento_entrega WHERE id_seguimiento_entrega = {id}";
    }

    protected override string GenerarComandoObtener(FiltroBusquedaSeguimientoEntrega criterio, string dato) {
        var comando = string.Empty;

        switch (criterio) {
            case FiltroBusquedaSeguimientoEntrega.Id:
                comando = $"SELECT * FROM adv__seguimiento_entrega WHERE id_seguimiento_entrega = {dato}";
                break;
            case FiltroBusquedaSeguimientoEntrega.IdVenta:
                comando = $"SELECT * FROM adv__seguimiento_entrega WHERE id_venta = {dato}";
                break;
            case FiltroBusquedaSeguimientoEntrega.NombreMensajero:
                comando =
                    $"SELECT se.* FROM adv__seguimiento_entrega se JOIN adv__mensajero m ON se.id_mensajero = m.id_mensajero WHERE m.nombre = {dato}";
                break;
            case FiltroBusquedaSeguimientoEntrega.FechaAsignacion:
                comando = $"SELECT * FROM adv__seguimiento_entrega WHERE DATE(fecha_asignacion) = {dato}";
                break;
            case FiltroBusquedaSeguimientoEntrega.FechaEntrega:
                comando = $"SELECT * FROM adv__seguimiento_entrega WHERE DATE(fecha_entrega) = {dato}";
                break;
            case FiltroBusquedaSeguimientoEntrega.FechaConfirmacion:
                comando = $"SELECT * FROM adv__seguimiento_entrega WHERE DATE(fecha_confirmacion) = {dato}";
                break;
            case FiltroBusquedaSeguimientoEntrega.FechaPago:
                comando = $"SELECT * FROM adv__seguimiento_entrega WHERE DATE(fecha_pago) = {dato}";
                break;
            default:
                comando = "SELECT * FROM adv__detalle_compra_producto;";
                break;
        }

        return comando;
    }

    protected override SeguimientoEntrega MapearEntidad(MySqlDataReader lectorDatos) {
        return new SeguimientoEntrega(
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_seguimiento_entrega")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_venta")),
            lectorDatos.GetInt32(lectorDatos.GetOrdinal("id_mensajero")),
            lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha_asignacion")),
            lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha_entrega")),
            lectorDatos.GetDateTime(lectorDatos.GetOrdinal("fecha_pago")),
            lectorDatos.GetString(lectorDatos.GetOrdinal("observaciones"))
        );
    }
}