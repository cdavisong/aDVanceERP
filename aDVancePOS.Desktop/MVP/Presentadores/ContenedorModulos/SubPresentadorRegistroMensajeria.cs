using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Mensajeria;

using aDVancePOS.Desktop.Utiles;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorContenedorModulos {
    private PresentadorRegistroMensajeria? _registroMensajeria;

    private async void InicializarVistaRegistroMensajeria() {
        _registroMensajeria = new PresentadorRegistroMensajeria(new VistaRegistroMensajeria());
        _registroMensajeria.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroMensajeria.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroMensajeria.Vista.CargarNombresMensajeros(await UtilesMensajero.ObtenerNombresMensajeros());
        _registroMensajeria.Vista.CargarTiposEntrega();
        _registroMensajeria.Vista.CargarRazonesSocialesClientes(UtilesCliente.ObtenerRazonesSocialesClientes());
        _registroMensajeria.DatosRegistradosActualizados += delegate {
            if (_terminalVenta == null)
                return;

            _terminalVenta.Vista.RazonSocialCliente = _registroMensajeria.Vista.RazonSocialCliente;
            _terminalVenta.Vista.Direccion = _registroMensajeria.Vista.Direccion;
            _terminalVenta.Vista.TipoEntrega = _registroMensajeria.Vista.TipoEntrega;
            _terminalVenta.Vista.EstadoEntrega = "Pendiente";
            _terminalVenta.Vista.MensajeriaConfigurada = true;
        };
    }

    private void MostrarVistaRegistroMensajeria(object? sender, EventArgs e) {
        InicializarVistaRegistroMensajeria();

        if (_registroMensajeria != null && _terminalVenta != null) {
            _registroMensajeria.Vista.IdVenta = _proximoIdVenta;
            _registroMensajeria.Vista.PopularProductosVenta(_terminalVenta.Vista.Productos);
            _registroMensajeria.Vista.AsignarNuevoMensajero += MostrarVistaRegistroMensajero;
            //TODO: _registroMensajeria.Vista.AsignarNuevoCliente += MostrarVistaRegistroCliente;
            _registroMensajeria.Vista.Mostrar();
        }

        _registroMensajeria?.Dispose();
    }

    private void MostrarVistaEdicionMensajeria(object? sender, EventArgs e) {
        InicializarVistaRegistroMensajeria();

        if (sender is Venta venta) {
            if (_registroMensajeria != null && _terminalVenta != null) {
                using (var datosSeguimientoEntrega = new DatosSeguimientoEntrega()) {
                    var seguimientoEntrega = datosSeguimientoEntrega.Obtener(CriterioBusquedaSeguimientoEntrega.IdVenta, venta.Id.ToString()).FirstOrDefault();

                    if (seguimientoEntrega != null) {
                        _registroMensajeria.PopularVistaDesdeObjeto(seguimientoEntrega);
                        _registroMensajeria.Vista.RazonSocialCliente = _terminalVenta.Vista.RazonSocialCliente;
                        _registroMensajeria.Vista.PopularProductosVenta(_terminalVenta.Vista.Productos);
                        _registroMensajeria.Vista.Mostrar();
                    }
                }
            }
        }

        _registroMensajeria?.Dispose();
    }
}