using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Mensajero;
using aDVancePOS.Desktop.Utiles;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorContenedorModulos {
    private PresentadorRegistroMensajero? _registroMensajero;

    private Task InicializarVistaRegistroMensajero() {
        _registroMensajero = new PresentadorRegistroMensajero(new VistaRegistroMensajero());
        _registroMensajero.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroMensajero.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroMensajero.DatosRegistradosActualizados += async delegate {
            //...
        };

        return Task.CompletedTask;
    }

    private async void MostrarVistaRegistroMensajero(object? sender, EventArgs e) {
        await InicializarVistaRegistroMensajero();

        if (_registroMensajero == null)
            return;

        _registroMensajero.DatosRegistradosActualizados += delegate {
            if (_registroMensajeria == null)
                return;

            _registroMensajeria.Vista.CargarNombresMensajeros(UtilesMensajero.ObtenerNombresMensajeros().Result);
            _registroMensajeria.Vista.NombreMensajero = _registroMensajero.Vista.Nombre;
        };

        _registroMensajero.Vista.Mostrar();
        _registroMensajero.Dispose();
    }

    private async void MostrarVistaEdicionMensajero(object? sender, EventArgs e) {
        await InicializarVistaRegistroMensajero();

        if (sender is Mensajero mensajero) {
            if (_registroMensajero != null) {
                _registroMensajero.PopularVistaDesdeObjeto(mensajero);
                _registroMensajero.Vista.Mostrar();
            }
        }

        _registroMensajero?.Dispose();
    }
}