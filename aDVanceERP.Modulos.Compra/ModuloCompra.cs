using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Modelos.Modulos.RecursosHumanos;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Compra.Presentadores;
using aDVanceERP.Modulos.Compra.Properties;
using aDVanceERP.Modulos.Compra.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Compra {
    public class ModuloCompra : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
        private PresentadorMenuCompra _menuCompra = null!;
        private PresentadorMenuMaestros _menuMaestros = null!;
        private PresentadorGestionProveedores _proveedores = null!;
        private PresentadorRegistroProveedor _registroProveedor = null!;

        public ModuloCompra() {
            Nombre = "MOD_COMPRA";
            Descripcion = "Proporciona funcionalidades de gestión de compras.";
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloCompra";
            _btnAccesoModulo.CustomImages.Image = Resources.buyingB_24px;
            _btnAccesoModulo.CustomImages.ImageOffset = new Point(0, 2);
            _btnAccesoModulo.TabIndex = 2;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuCompra", string.Empty);
            };

            // Menu
            _menuCompra = new PresentadorMenuCompra(new VistaMenuCompra());
            _menuMaestros = new PresentadorMenuMaestros(new VistaMenuMaestros());

            // Contenedor de módulos
            // Proveedores
            _proveedores = new PresentadorGestionProveedores(new VistaGestionProveedores());
            _registroProveedor = new PresentadorRegistroProveedor(new VistaRegistroProveedor());
            _registroProveedor.EntidadRegistradaActualizada += (s, e) => _proveedores.ActualizarResultadosBusqueda();

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Compra");

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuCompra.Vista);
            _principal.Vista.BarraTitulo.Registrar(_menuMaestros.Vista);

            // Contenedor de módulos
            // Proveedores
            _principal.Modulos.Vista.PanelCentral.Registrar(_proveedores.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroProveedor.Vista);
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
