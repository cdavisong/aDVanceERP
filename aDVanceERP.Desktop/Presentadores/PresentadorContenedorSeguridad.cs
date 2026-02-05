using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.BD;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Presentadores {
    public partial class PresentadorContenedorSeguridad : IPresentadorVistaContenedorSeguridad<IVistaContenedorSeguridad> {
        private readonly VistaConfiguracionBaseDatos _configuracionBaseDatos = new VistaConfiguracionBaseDatos();

        public PresentadorContenedorSeguridad(IVistaPrincipal vistaPrincipal, IVistaContenedorSeguridad vistaSeguridad) {
            VistaPrincipal = vistaPrincipal;
            Vista = vistaSeguridad;

            // Adicionar vistas al panel de central
            Vista.PanelCentral.Registrar(_configuracionBaseDatos);

            // Eventos de la vista seguridad
            ((Form)Vista).Shown += OnVistaContenedorSeguridadMostrada;
        }

        public IVistaPrincipal VistaPrincipal { get; }

        public IVistaContenedorSeguridad Vista { get; }

        public VistaConfiguracionBaseDatos ConfiguracionBaseDatos => _configuracionBaseDatos;

        private void OnVistaContenedorSeguridadMostrada(object? sender, EventArgs e) {
            Vista.PanelCentral.Mostrar(nameof(VistaConfiguracionBaseDatos));
        }

        public void Dispose() {
            Vista.Dispose();
        }
    }
}