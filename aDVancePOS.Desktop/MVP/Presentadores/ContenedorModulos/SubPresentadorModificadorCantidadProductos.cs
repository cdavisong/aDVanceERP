using aDVancePOS.Desktop.Utiles;
using aDVancePOS.Modulos.TerminalVenta.MVP.Presentadores;
using aDVancePOS.Modulos.TerminalVenta.MVP.Vistas.Venta;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos {
    public partial class PresentadorContenedorModulos {
        private PresentadorModificadorCantidadProducto? _modificadorCantidadProductos;

        private decimal Cantidad { get; set; } = 1;

        private void InicializarVistaModificadorCantidadProducto() {
            _modificadorCantidadProductos = new PresentadorModificadorCantidadProducto(new VistaModificadorCantidadProducto());
            _modificadorCantidadProductos.Vista.EstablecerCoordenadasVistaModal(Vista.Dimensiones.Width);
            _modificadorCantidadProductos.Vista.EstablecerDimensionesVistaModal(_modificadorCantidadProductos.Vista.Dimensiones);
            _modificadorCantidadProductos.Vista.Salir += delegate {
                Cantidad = _modificadorCantidadProductos.Vista.CantidadProducto;
            };
        }

        private void MostrarVistaModificadorCantidadProducto(object? sender, EventArgs e) {
            InicializarVistaModificadorCantidadProducto();

            if (_modificadorCantidadProductos?.Vista == null)
                return;

            _modificadorCantidadProductos.Vista.Restaurar();
            _modificadorCantidadProductos.Vista.Mostrar();
            
            // Actualizar la cantidad en el terminal de venta
            if (_terminalVenta != null) _terminalVenta.Vista.Cantidad = Cantidad;
        }
    }
}
