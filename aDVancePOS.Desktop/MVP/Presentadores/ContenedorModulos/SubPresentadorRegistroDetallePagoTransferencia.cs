using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.DetallePagoTransferencia;
using aDVancePOS.Desktop.Utiles;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorContenedorModulos {
    private PresentadorRegistroDetallePagoTransferencia? _registroDetallePagoTransferencia;

    private string[]? Transferencia { get; set; } = Array.Empty<string>();

    private void InicializarVistaRegistroDetallePagoTransferencia() {
        _registroDetallePagoTransferencia =
            new PresentadorRegistroDetallePagoTransferencia(new VistaRegistroDetallePagoTransferencia());
        _registroDetallePagoTransferencia.Vista.CargarAliasTarjetas(UtilesCuentaBancaria.ObtenerAliasesCuentas());
        _registroDetallePagoTransferencia.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroDetallePagoTransferencia.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroDetallePagoTransferencia.DatosRegistradosActualizados += delegate {
            Transferencia = [
                _registroDetallePagoTransferencia.Vista.Alias,
                _registroDetallePagoTransferencia.Vista.NumeroConfirmacion,
                _registroDetallePagoTransferencia.Vista.NumeroTransaccion
            ];
        };
    }

    private void MostrarVistaRegistroDetallePagoTransferencia(object? sender, EventArgs e) {
        InicializarVistaRegistroDetallePagoTransferencia();

        _registroDetallePagoTransferencia?.Vista.Mostrar();
        _registroDetallePagoTransferencia?.Dispose();
    }

    private void MostrarVistaEdicionDetallePagoTransferencia(object? sender, EventArgs e) {
        InicializarVistaRegistroDetallePagoTransferencia();

        if (_registroDetallePagoTransferencia != null && sender is DetallePagoTransferencia detallePagoTransferencia) {
            _registroDetallePagoTransferencia.PopularVistaDesdeObjeto(detallePagoTransferencia);
            _registroDetallePagoTransferencia.Vista.Mostrar();
        }

        _registroDetallePagoTransferencia?.Dispose();
    }

    private void RegistrarTransferenciaVenta() {
        if (Transferencia == null || Transferencia.Length == 0)
            return;

        using (var transferencia = new DatosDetallePagoTransferencia()) {
            transferencia.Adicionar(new DetallePagoTransferencia(
                0,
                UtilesBD.ObtenerUltimoIdTabla("venta"),
                UtilesCuentaBancaria.ObtenerIdCuenta(Transferencia[0]),
                Transferencia[1],
                Transferencia[2]
            ));
        }

        Transferencia = Array.Empty<string>();
    }
}