using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.BD;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Presentadores.ContenedorSeguridad;

public partial class PresentadorSeguridad : IPresentadorVistaSeguridad<IVistaSeguridad> {
    private VistaConfiguracionBaseDatos? _vistaConfiguracionBaseDatos;

    public PresentadorSeguridad(IVistaPrincipal vistaPrincipal, IVistaSeguridad vistaSeguridad) {
        VistaPrincipal = vistaPrincipal;
        Vista = vistaSeguridad;

        // Eventos
        #region Gestión de conexión a la base de datos

        _vistaConfiguracionBaseDatos = new VistaConfiguracionBaseDatos();
        _vistaConfiguracionBaseDatos.ConfiguracionCargada += (s, e) => {
            // Ocultar la vista de configuración de base de datos
            _vistaConfiguracionBaseDatos.Ocultar();

            //Mostrar la vista de autenticación de usuario
            AgregadorEventos.Publicar("MostrarVistaAutenticacion", string.Empty);
        };

        Vista.PanelCentral.Registrar(_vistaConfiguracionBaseDatos);

        #endregion

        // Otros
        _vistaConfiguracionBaseDatos.Mostrar();
    }

    public IVistaPrincipal VistaPrincipal { get; }

    public IVistaSeguridad Vista { get; }

    public void Dispose() {
        Vista.Dispose();
    }
}