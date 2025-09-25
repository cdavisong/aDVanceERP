using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroMensajero? _registroMensajero;

    private Task InicializarVistaRegistroMensajero() {
        _registroMensajero = new PresentadorRegistroMensajero(new VistaRegistroMensajero());
        _registroMensajero.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroMensajero.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroMensajero.EntidadRegistradaActualizada += delegate {
            if (_gestionMensajeros == null)
                return;

            _gestionMensajeros.ActualizarResultadosBusqueda();
        };

        return Task.CompletedTask;
    }

    private async void MostrarVistaRegistroMensajero(object? sender, EventArgs e) {
        await InicializarVistaRegistroMensajero();

        if (_registroMensajero == null)
            return;

        _registroMensajero.EntidadRegistradaActualizada += delegate {
            if (_registroMensajeria == null)
                return;

            _registroMensajeria.Vista.CargarNombresMensajeros(UtilesMensajero.ObtenerNombresMensajeros().Result);
            _registroMensajeria.Vista.NombreMensajero = _registroMensajero.Vista.NombreMensajero;
        };

        _registroMensajero.Vista.Mostrar();
        _registroMensajero.Dispose();
    }

    private async void MostrarVistaEdicionMensajero(object? sender, EventArgs e) {
        await InicializarVistaRegistroMensajero();

        if (sender is Mensajero mensajero) {
            if (_registroMensajero != null) {
                _registroMensajero.PopularVistaDesdeEntidad(mensajero);
                _registroMensajero.Vista.Mostrar();
            }
        }

        _registroMensajero?.Dispose();
    }
}