using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Proveedor;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorGestionProveedores? _gestionProveedores;

    private async void InicializarVistaGestionProveedores() {
        _gestionProveedores = new PresentadorGestionProveedores(new VistaGestionProveedores());
        _gestionProveedores.EditarEntidad += MostrarVistaEdicionProveedor;
        _gestionProveedores.Vista.RegistrarEntidad += MostrarVistaRegistroProveedor;

        Vista.PanelCentral.Registrar(_gestionProveedores.Vista);
    }

    private void MostrarVistaGestionProveedores(object? sender, EventArgs e) {
        if (_gestionProveedores == null)
            return;

        _gestionProveedores.Vista.CargarFiltrosBusqueda(UtilesBusquedaProveedor.FiltroBusquedaProveedor);
        _gestionProveedores.Vista.Restaurar();
        _gestionProveedores.Vista.Mostrar();

        _gestionProveedores.ActualizarResultadosBusqueda();
    }
}