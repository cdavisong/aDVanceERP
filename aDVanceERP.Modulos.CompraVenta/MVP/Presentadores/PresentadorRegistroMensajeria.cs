using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Mensajeria.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class PresentadorRegistroMensajeria : PresentadorVistaRegistro<IVistaRegistroMensajeria, SeguimientoEntrega, RepoSeguimientoEntrega, FiltroBusquedaSeguimientoEntrega> {
    public PresentadorRegistroMensajeria(IVistaRegistroMensajeria vista) : base(vista) {
    }

    public override async void PopularVistaDesdeEntidad(SeguimientoEntrega objeto) {
        Vista.ModoEdicion = true;
        Vista.NombreMensajero = await UtilesMensajero.ObtenerNombreMensajero(objeto.IdMensajero);

        using (var datosVenta = new RepoVenta()) {
            var venta = datosVenta.Buscar(FiltroBusquedaVenta.Id, objeto.IdVenta.ToString()).resultados.FirstOrDefault();

            if (venta == null)
                return;

            Vista.TipoEntrega = await UtilesEntrega.ObtenerNombreTipoEntrega(venta.IdTipoEntrega);
            Vista.Direccion = venta.DireccionEntrega;
            Vista.Observaciones = objeto.Observaciones;
        }

        _entidad = objeto;
    }

    protected override bool EntidadCorrecta() {
        var nombreMensajeroOk = !string.IsNullOrEmpty(Vista.NombreMensajero) && !Vista.NombreMensajero.Equals("Ninguno");
        var tipoEntregaOk = !string.IsNullOrEmpty(Vista.TipoEntrega) && !Vista.TipoEntrega.Equals("Presencial");
        var razonSocialValida = UtilesCliente.ObtenerIdCliente(Vista.RazonSocialCliente) != 0;
        var razonSocialClienteOk = !string.IsNullOrEmpty(Vista.RazonSocialCliente) && razonSocialValida;
        var direccionOk = !string.IsNullOrEmpty(Vista.Direccion);

        if (!nombreMensajeroOk)
            CentroNotificaciones.Mostrar("El nombre del mensajero es obligatorio para registro de una orden de mensajería, elija un mensajero desde la lista correspondiente", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!tipoEntregaOk)
            CentroNotificaciones.Mostrar("Debe especificarse el tipo de entrega (no presencial) para la orden de mensajería, elija un tipo entrega desde la lista correspondiente", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!razonSocialClienteOk)
            CentroNotificaciones.Mostrar("Debe especificarse un nombre de cliente válido para el envío, si no existe el cliente que busca, regístrelo haciendo click en el botón a la derecha del campo cliente", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!direccionOk)
            CentroNotificaciones.Mostrar("Debe especificarse una dirección válida de envío para la orden de mensajería, rellene el campo dirección correctamente", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

        return nombreMensajeroOk && tipoEntregaOk && direccionOk;
    }

    protected override SeguimientoEntrega? ObtenerEntidadDesdeVista() {
        return new SeguimientoEntrega(
            Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
            Vista.IdVenta,
            UtilesMensajero.ObtenerIdMensajero(Vista.NombreMensajero).Result,
            DateTime.Now,
            DateTime.MinValue,
            DateTime.MinValue,
            Vista.Observaciones);
    }
}