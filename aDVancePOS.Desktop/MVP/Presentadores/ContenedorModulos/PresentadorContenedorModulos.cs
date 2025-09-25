using aDVanceERP.Core.MVP.Presentadores;

using aDVancePOS.Desktop.MVP.Vistas.ContenedorModulos.Plantillas;
using aDVancePOS.Desktop.MVP.Vistas.Principal.Plantillas;

namespace aDVancePOS.Desktop.MVP.Presentadores.ContenedorModulos;

public partial class PresentadorContenedorModulos : PresentadorBase<IVistaContenedorModulos> {
    public PresentadorContenedorModulos(IVistaPrincipal vistaPrincipal, IVistaContenedorModulos vista) : base(vista) {
        VistaPrincipal = vistaPrincipal;

        // Eventos
        Vista.MostrarVistaPuntoVenta += MostrarVistaTerminalVenta;

        #region Módulo : Terminal de Venta

        InicializarVistaTerminalVenta();

        #endregion
    }

    private IVistaPrincipal VistaPrincipal { get; }

    public override void Dispose() {
        //...
    }
}