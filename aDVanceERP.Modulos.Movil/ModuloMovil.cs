using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Movil.Presentadores;
using aDVanceERP.Modulos.Movil.Properties;
using aDVanceERP.Modulos.Movil.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Movil {
    public class ModuloMovil : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
        private PresentadorMenuMovil _menuMovil = null!;
        private PresentadorGestionAdvancePos _advancePos = null!;
        PresentadorGestionAdvanceStock _advanceStock = null!;

        public ModuloMovil() {
            Nombre = ModuloSistemaEnum.MOD_MOVIL;
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloMovil";
            _btnAccesoModulo.CustomImages.Image = Resources.smartphone_tablet_24px;
            _btnAccesoModulo.TabIndex = 6;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuMovil", string.Empty);
            };

            // Menu
            _menuMovil = new PresentadorMenuMovil(new VistaMenuMovil());

            // Contenedor de módulos
            // aDVance POS
            _advancePos = new PresentadorGestionAdvancePos(new VistaGestionAdvancePos());
            // aDVance STOCK
            _advanceStock = new PresentadorGestionAdvanceStock(new VistaGestionAdvanceStock());

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Dispositivos móviles");
                
            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuMovil.Vista);

            // Contenedor de módulos
            // aDVance POS
            _principal.Modulos.Vista.PanelCentral.Registrar(_advancePos.Vista);
            // aDVance STOCK
            _principal.Modulos.Vista.PanelCentral.Registrar (_advanceStock.Vista);
        }

        public override void Apagar() {
            _menuMovil.Dispose();
        }
    }
}
