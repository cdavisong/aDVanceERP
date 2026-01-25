using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.BD;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Presentadores;

public partial class PresentadorSeguridad : IPresentadorVistaSeguridad<IVistaSeguridad> {
    private readonly VistaConfiguracionBaseDatos _configuracionBaseDatos = new VistaConfiguracionBaseDatos();

    public PresentadorSeguridad(IVistaPrincipal vistaPrincipal, IVistaSeguridad vistaSeguridad) {
        VistaPrincipal = vistaPrincipal;
        Vista = vistaSeguridad;

        // Adicionar vistas al panel de central
        Vista.PanelCentral.Registrar(_configuracionBaseDatos);

        // Eventos de la vista seguridad
        ((Form)Vista).Shown += OnVistaSeguridadMostrada;
    }

    public IVistaPrincipal VistaPrincipal { get; }

    public IVistaSeguridad Vista { get; }

    public VistaConfiguracionBaseDatos ConfiguracionBaseDatos => _configuracionBaseDatos;

    private void OnVistaSeguridadMostrada(object? sender, EventArgs e) {
        Vista.PanelCentral.Mostrar(nameof(VistaConfiguracionBaseDatos));
    }

    public void Dispose() {
        Vista.Dispose();
    }
}