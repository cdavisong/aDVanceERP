using aDVanceERP.Core.Modelos.Modulos.Inventario;
using aDVanceERP.Core.Utiles.Datos;
using aDVanceERP.Modulos.Inventario.MVP.Presentadores;
using aDVanceERP.Modulos.Inventario.MVP.Vistas.Producto;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorGestionProductos? _gestionProductos;

    private async void InicializarVistaGestionProductos() {
        _gestionProductos = new PresentadorGestionProductos(new VistaGestionProductos());
        _gestionProductos.MovimientoPositivoStock += MostrarVistaRegistroMovimiento;
        _gestionProductos.MovimientoNegativoStock += MostrarVistaRegistroMovimiento;
        _gestionProductos.EditarEntidad += MostrarVistaEdicionProducto;
        _gestionProductos.Vista.RegistrarEntidad += MostrarVistaRegistroProducto;

        Vista.PanelCentral.Registrar(_gestionProductos.Vista);
    }

    private void MostrarVistaGestionProductos(object? sender, EventArgs e) {
        if (_gestionProductos?.Vista == null)
            return;

        _gestionProductos.Vista.CargarNombresAlmacenes(UtilesAlmacen.ObtenerNombresAlmacenes());
        _gestionProductos.Vista.CargarFiltrosBusqueda(UtilesBusquedaProducto.FiltroBusquedaProducto);
        _gestionProductos.Vista.Restaurar();
        _gestionProductos.Vista.Mostrar();

        _gestionProductos.ActualizarResultadosBusqueda();
    }
}