using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Cliente;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorGestionClientes? _gestionClientes;

    private async void InicializarVistaGestionClientes() {
        _gestionClientes = new PresentadorGestionClientes(new VistaGestionClientes());
        _gestionClientes.EditarEntidad += MostrarVistaEdicionCliente;
        _gestionClientes.Vista.RegistrarEntidad += MostrarVistaRegistroCliente;

        Vista.PanelCentral.Registrar(_gestionClientes.Vista);
    }

    private void MostrarVistaGestionClientes(object? sender, EventArgs e) {
        if (_gestionClientes?.Vista == null)
            return;

        _gestionClientes.Vista.CargarFiltrosBusqueda(UtilesBusquedaCliente.FiltroBusquedaCliente);
        _gestionClientes.Vista.Restaurar();
        _gestionClientes.Vista.Mostrar();

        _gestionClientes.ActualizarResultadosBusqueda();
    }
}