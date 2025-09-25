using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Modelos.Repositorios;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Mensajeria;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorModulos {
    private PresentadorRegistroMensajeria? _registroMensajeria;

    private async void InicializarVistaRegistroMensajeria() {
        _registroMensajeria = new PresentadorRegistroMensajeria(new VistaRegistroMensajeria());
        _registroMensajeria.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroMensajeria.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroMensajeria.Vista.CargarNombresMensajeros(await UtilesMensajero.ObtenerNombresMensajeros());
        _registroMensajeria.Vista.CargarTiposEntrega();
        _registroMensajeria.Vista.CargarRazonesSocialesClientes(UtilesCliente.ObtenerRazonesSocialesClientes());
        _registroMensajeria.EntidadRegistradaActualizada += delegate {
            if (_registroVentaProducto == null) 
                return;

            _registroVentaProducto.Vista.RazonSocialCliente = _registroMensajeria.Vista.RazonSocialCliente;
            _registroVentaProducto.Vista.Direccion = _registroMensajeria.Vista.Direccion;
            _registroVentaProducto.Vista.TipoEntrega = _registroMensajeria.Vista.TipoEntrega;
            _registroVentaProducto.Vista.EstadoEntrega = "Pendiente";
            _registroVentaProducto.Vista.MensajeriaConfigurada = true;
        };
    }

    private void MostrarVistaRegistroMensajeria(object? sender, EventArgs e) {
        InicializarVistaRegistroMensajeria();

        if (_registroMensajeria != null && _registroVentaProducto != null) {
            _registroMensajeria.Vista.IdVenta = _proximoIdVenta;
            _registroMensajeria.Vista.PopularProductosVenta(_registroVentaProducto.Vista.Productos);
            _registroMensajeria.Vista.AsignarNuevoMensajero += MostrarVistaRegistroMensajero;
            _registroMensajeria.Vista.AsignarNuevoCliente += MostrarVistaRegistroCliente;
            _registroMensajeria.Vista.Mostrar();
        }

        _registroMensajeria?.Dispose();
    }

    private void MostrarVistaEdicionMensajeria(object? sender, EventArgs e) {
        InicializarVistaRegistroMensajeria();

        if (sender is Venta venta) {
            if (_registroMensajeria != null && _registroVentaProducto != null) {
                using (var datosSeguimientoEntrega = new RepoSeguimientoEntrega()) {
                    var seguimientoEntrega = datosSeguimientoEntrega.Buscar(FiltroBusquedaSeguimientoEntrega.IdVenta, venta.Id.ToString()).resultados.FirstOrDefault();

                    if (seguimientoEntrega != null) {
                        _registroMensajeria.PopularVistaDesdeEntidad(seguimientoEntrega);
                        _registroMensajeria.Vista.RazonSocialCliente = _registroVentaProducto.Vista.RazonSocialCliente;
                        _registroMensajeria.Vista.PopularProductosVenta(_registroVentaProducto.Vista.Productos);
                        _registroMensajeria.Vista.Mostrar();
                    }
                }
            }
        }

        _registroMensajeria?.Dispose();
    }
}