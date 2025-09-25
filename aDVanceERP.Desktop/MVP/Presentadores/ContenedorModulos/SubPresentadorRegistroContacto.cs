using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorRegistroContacto? _registroContacto;

    private Task InicializarVistaRegistroContacto() {
        _registroContacto = new PresentadorRegistroContacto(new VistaRegistroContacto());
        _registroContacto.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroContacto.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroContacto.EntidadRegistradaActualizada += delegate {
            if (_gestionContactos == null)
                return;

            _gestionContactos.ActualizarResultadosBusqueda();
        };

        return Task.CompletedTask;
    }

    private async void MostrarVistaRegistroContacto(object? sender, EventArgs e) {
        await InicializarVistaRegistroContacto();

        if (_registroContacto == null) 
            return;

        _registroContacto.Vista.Mostrar();
        _registroContacto.Dispose();
    }

    private async void MostrarVistaEdicionContacto(object? sender, EventArgs e) {
        await InicializarVistaRegistroContacto();

        if (sender is Contacto contacto) {
            if (_registroContacto != null) {
                _registroContacto.PopularVistaDesdeEntidad(contacto);
                _registroContacto.Vista.Mostrar();
            }
        }

        _registroContacto?.Dispose();
    }
}