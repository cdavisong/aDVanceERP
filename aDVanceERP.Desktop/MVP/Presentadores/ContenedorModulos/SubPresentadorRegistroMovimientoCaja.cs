using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Finanzas.MVP.Modelos;
using aDVanceERP.Modulos.Finanzas.MVP.Presentadores;
using aDVanceERP.Modulos.Finanzas.MVP.Vistas.Caja;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroMovimientoCaja? _registroMovimientoCaja;

    private void InicializarVistaRegistroMovimientoCaja() {
        _registroMovimientoCaja = new PresentadorRegistroMovimientoCaja(new VistaRegistroMovimientoCaja());
        _registroMovimientoCaja.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroMovimientoCaja.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroMovimientoCaja.EntidadRegistradaActualizada += delegate {
            if (_gestionCajas == null)
                return;

            ActualizarMontoCaja(UtilesCaja.ObtenerIdCajaActiva());

            _gestionCajas.ActualizarResultadosBusqueda();
        };
    }

    private void MostrarVistaRegistroMovimientoCaja(object? sender, EventArgs e) {
        InicializarVistaRegistroMovimientoCaja();

        if (_registroMovimientoCaja == null)
            return;

        _registroMovimientoCaja.Vista.Restaurar();
        _registroMovimientoCaja.Vista.Mostrar();
        _registroMovimientoCaja.Dispose();
    }

    private void MostrarVistaEdicionMovimientoCaja(object? sender, EventArgs e) {
        InicializarVistaRegistroMovimientoCaja();

        if (sender is MovimientoCaja movimientoCaja) {
            if (_registroMovimientoCaja != null) {
                _registroMovimientoCaja.PopularVistaDesdeEntidad(movimientoCaja);
                _registroMovimientoCaja.Vista.Mostrar();
            }
        }

        _registroMovimientoCaja?.Dispose();
    }
}
