using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Compraventa;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Compraventa;
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
            CentroNotificaciones.Mostrar("El campo de alias es obligatorio para registro de una transferencia bancaria, por favor, corrija los datos entrados", TipoNotificacion.Advertencia);
        if (!numeroTelefonoOk)
            CentroNotificaciones.Mostrar("Debe especificar un número de teléfono de confirmación para la transferencia bancaria, el campo no puede estar vacío ni contener caracteres incorrectos", TipoNotificacion.Advertencia);
        if (!numeroTransaccionOk)
            CentroNotificaciones.Mostrar("Debe especificar un número de transacción al recibir el SMS de confirmación para la transferencia bancaria, el campo no puede estar vacío", TipoNotificacion.Advertencia);

        return aliasOk && numeroTelefonoOk && numeroTransaccionOk;
    }

    protected override DetallePagoTransferencia? ObtenerEntidadDesdeVista() {
        throw new NotImplementedException();
    }
}