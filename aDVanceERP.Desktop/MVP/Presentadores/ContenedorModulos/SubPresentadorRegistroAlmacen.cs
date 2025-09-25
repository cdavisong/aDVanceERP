using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Almacen;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorRegistroAlmacen? _registroAlmacen;

    private void InicializarVistaRegistroAlmacen() {
        _registroAlmacen = new PresentadorRegistroAlmacen(new VistaRegistroAlmacen());
        _registroAlmacen.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones);
        _registroAlmacen.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height);
        _registroAlmacen.EntidadRegistradaActualizada += delegate {
            if (_gestionAlmacenes == null)
                return;

            _gestionAlmacenes.ActualizarResultadosBusqueda();
        };

        Vista.PanelCentral.Registrar(
            _registroAlmacen.Vista, 
            new Point(Vista.Dimensiones.Width - _registroAlmacen.Vista.Dimensiones.Width - 40, -10),
            _registroAlmacen.Vista.Dimensiones,
            TipoRedimensionadoVista.Vertical);

        // Estado de habilitacion de la vista gestionable.
        //TODO: Implementar esto para todas las vistas de registro.
        var formRegistro = _registroAlmacen?.Vista as Form;

        if (formRegistro != null && _gestionAlmacenes != null)
            formRegistro.VisibleChanged += delegate {
                _gestionAlmacenes.Vista.Habilitada = !formRegistro.Visible;
            };
    }

    private void MostrarVistaRegistroAlmacen(object? sender, EventArgs e) {
        if (_registroAlmacen == null)
            return;

        _registroAlmacen.Vista.Restaurar();
        _registroAlmacen.Vista.Mostrar();
    }

    private void MostrarVistaEdicionAlmacen(object? sender, EventArgs e) {
        if (sender is Almacen almacen) {
            if (_registroAlmacen != null) {
                _registroAlmacen.PopularVistaDesdeEntidad(almacen);
                _registroAlmacen.Vista.Mostrar();
            }
        }
    }
}