using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroCliente? _registroCliente;

    private void InicializarVistaRegistroCliente() {
        _registroCliente = new PresentadorRegistroCliente(new VistaRegistroCliente());
        _registroCliente.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroCliente.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroCliente.EntidadRegistradaActualizada += delegate {
            if (_gestionClientes == null)
                return;

            _gestionClientes.ActualizarResultadosBusqueda();
        };
    }

    private void MostrarVistaRegistroCliente(object? sender, EventArgs e) {
        InicializarVistaRegistroCliente();

        if (_registroCliente == null)
            return;

        _registroCliente.EntidadRegistradaActualizada += delegate {
            if (_registroMensajeria == null)
                return;

            _registroMensajeria.Vista.CargarRazonesSocialesClientes(UtilesCliente.ObtenerRazonesSocialesClientes());
            _registroMensajeria.Vista.RazonSocialCliente = _registroCliente.Vista.RazonSocial;
        };

        _registroCliente.Vista.Mostrar();
        _registroCliente.Dispose();
    }

    private void MostrarVistaEdicionCliente(object? sender, EventArgs e) {
        InicializarVistaRegistroCliente();

        if (sender is Cliente cliente) {
            if (_registroCliente != null) {
                _registroCliente.PopularVistaDesdeEntidad(cliente);
                _registroCliente.Vista.Mostrar();
            }
        }

        _registroCliente?.Dispose();
    }
}