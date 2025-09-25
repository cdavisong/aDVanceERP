using aDVanceERP.Core.MVP.Presentadores;

using aDVancePOS.Desktop.MVP.Vistas.ContenedorSeguridad.Plantillas;
using aDVancePOS.Desktop.MVP.Vistas.Principal.Plantillas;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorSeguridad;

public partial class PresentadorContenedorSeguridad : PresentadorBase<IVistaContenedorSeguridad> {
    public PresentadorContenedorSeguridad(IVistaPrincipal vistaPrincipal, IVistaContenedorSeguridad vista) :
        base(vista) {
        VistaPrincipal = vistaPrincipal;

        // Eventos

        #region Gestión de autenticación y registro primario de usuarios

        InicializarVistaAutenticacionUsuario();
        InicializarVistaRegistroUsuario();
        InicializarVistaAprobacionUsuario();

        #endregion

        // Otros
        MostrarVistaAutenticacionUsuario("first-login", EventArgs.Empty);
    }

    private IVistaPrincipal VistaPrincipal { get; }

    public override void Dispose() {
        //...
    }
}