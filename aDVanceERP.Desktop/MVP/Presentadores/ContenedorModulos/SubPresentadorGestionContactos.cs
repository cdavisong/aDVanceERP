using aDVanceERP.Modulos.Contactos.MVP.Modelos;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Contacto;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorModulos; 

public partial class PresentadorModulos {
    private PresentadorGestionContactos? _gestionContactos;

    private async void InicializarVistaGestionContactos() {
        _gestionContactos = new PresentadorGestionContactos(new VistaGestionContactos());
        _gestionContactos.EditarEntidad += MostrarVistaEdicionContacto;
        _gestionContactos.Vista.RegistrarEntidad += MostrarVistaRegistroContacto;

        Vista.PanelCentral.Registrar(_gestionContactos.Vista);
    }

    private void MostrarVistaGestionContactos(object? sender, EventArgs e) {
        if (_gestionContactos?.Vista == null)
            return;

        _gestionContactos.Vista.CargarFiltrosBusqueda(UtilesBusquedaContacto.FiltroBusquedaContacto);
        _gestionContactos.Vista.Restaurar();
        _gestionContactos.Vista.Mostrar();

        _gestionContactos.ActualizarResultadosBusqueda();
    }
}