using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.BD;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.MVP.Presentadores.ContenedorSeguridad;

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

            // Mostrar la vista de autenticación de usuario
            MostrarVistaAutenticacionUsuario("first-login", EventArgs.Empty);
        };

        Vista.PanelCentral.Registrar(_vistaConfiguracionBaseDatos);

        #endregion
        #region Gestión de autenticación y registro primario de usuarios

        InicializarVistaAutenticacionUsuario();
        InicializarVistaRegistroUsuario();
        InicializarVistaAprobacionUsuario();

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