using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorRegistroProveedor? _registroProveedor;

    private void InicializarVistaRegistroProveedor() {
        _registroProveedor = new PresentadorRegistroProveedor(new VistaRegistroProveedor());
        _registroProveedor.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroProveedor.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroProveedor.EntidadRegistradaActualizada += delegate {
            if (_gestionProveedores == null)
                return;

            _gestionProveedores.ActualizarResultadosBusqueda();
        };
    }

    private void MostrarVistaRegistroProveedor(object? sender, EventArgs e) {
        InicializarVistaRegistroProveedor();

        if (_registroProveedor == null)
            return;

        _registroProveedor.Vista.Mostrar();
        _registroProveedor.Dispose();
    }

    private void MostrarVistaEdicionProveedor(object? sender, EventArgs e) {
        InicializarVistaRegistroProveedor();

        if (sender is Proveedor proveedor) {
            if (_registroProveedor != null) {
                _registroProveedor.PopularVistaDesdeEntidad(proveedor);
                _registroProveedor.Vista.Mostrar();
            }
        }

        _registroProveedor?.Dispose();
    }
}