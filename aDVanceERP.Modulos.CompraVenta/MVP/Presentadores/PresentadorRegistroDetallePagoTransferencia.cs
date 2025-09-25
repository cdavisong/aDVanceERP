using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetallePagoTransferencia.Plantillas;

namespace aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;

public class PresentadorRegistroDetallePagoTransferencia : PresentadorVistaRegistro<IVistaRegistroDetallePagoTransferencia, DetallePagoTransferencia, RepoDetallePagoTransferencia, FiltroBusquedaDetallePagoTransferencia> {
    public PresentadorRegistroDetallePagoTransferencia(IVistaRegistroDetallePagoTransferencia vista) : base(vista) { }

    public override void PopularVistaDesdeEntidad(DetallePagoTransferencia objeto) {
        throw new NotImplementedException();
    }

    protected override bool EntidadCorrecta() {
        var aliasOk = !string.IsNullOrEmpty(Vista.Alias);
        var numeroTelefonoOk = !string.IsNullOrEmpty(Vista.NumeroConfirmacion); ;
        var numeroTransaccionOk = !string.IsNullOrEmpty(Vista.NumeroTransaccion);

        if (!aliasOk)
            CentroNotificaciones.Mostrar("El campo de alias es obligatorio para registro de una transferencia bancaria, por favor, corrija los datos entrados", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!numeroTelefonoOk)
            CentroNotificaciones.Mostrar("Debe especificar un número de teléfono de confirmación para la transferencia bancaria, el campo no puede estar vacío ni contener caracteres incorrectos", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
        if (!numeroTransaccionOk)
            CentroNotificaciones.Mostrar("Debe especificar un número de transacción al recibir el SMS de confirmación para la transferencia bancaria, el campo no puede estar vacío", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);

        return aliasOk && numeroTelefonoOk && numeroTransaccionOk;
    }

    protected override DetallePagoTransferencia? ObtenerEntidadDesdeVista() {
        throw new NotImplementedException();
    }
}