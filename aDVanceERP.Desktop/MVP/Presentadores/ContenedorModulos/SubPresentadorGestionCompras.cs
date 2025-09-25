using aDVanceERP.Modulos.CompraVenta.MVP.Modelos;
using aDVanceERP.Modulos.CompraVenta.MVP.Presentadores;
using aDVanceERP.Modulos.CompraVenta.MVP.Vistas.Compra;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorGestionCompras? _gestionCompras;

    private async void InicializarVistaGestionCompras() {
        _gestionCompras = new PresentadorGestionCompras(new VistaGestionCompras());
        _gestionCompras.EditarEntidad += MostrarVistaEdicionCompraProducto;
        _gestionCompras.Vista.RegistrarEntidad += MostrarVistaRegistroCompraProducto;

        Vista.PanelCentral.Registrar(_gestionCompras.Vista);
    }

    private void MostrarVistaGestionCompras(object? sender, EventArgs e) {
        if (_gestionCompras?.Vista == null)
            return;

        _gestionCompras.Vista.CargarFiltrosBusqueda(UtilesBusquedaCompra.FiltroBusquedaCompra);
        _gestionCompras.Vista.Restaurar();
        _gestionCompras.Vista.Mostrar();

        _gestionCompras.ActualizarResultadosBusqueda();
    }
}