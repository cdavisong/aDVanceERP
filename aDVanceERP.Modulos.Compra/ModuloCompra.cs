using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
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
        private PresentadorGestionSolicitudesCompra _solicitudesCompra = null!;
        private PresentadorRegistroSolicitudCompra _registroSolicitudCompra = null!;
        private PresentadorGestionCompras _compras = null!;
        private PresentadorRegistroCompra _registroCompra = null!;
        private PresentadorGestionPagos _pagos = null!;
        private PresentadorRegistroPago _registroPago = null!;
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
            // Solicitudes de compra
            _solicitudesCompra = new PresentadorGestionSolicitudesCompra(new VistaGestionSolicitudesCompra());
            _registroSolicitudCompra = new PresentadorRegistroSolicitudCompra(new VistaRegistroSolicitudCompra());
            _registroSolicitudCompra.EntidadRegistradaActualizada += (s, e) => _solicitudesCompra.ActualizarResultadosBusqueda();
            // Compras
            _compras = new PresentadorGestionCompras(new VistaGestionCompras());
            _registroCompra = new PresentadorRegistroCompra(new VistaRegistroCompra());
            _registroCompra.EntidadRegistradaActualizada += (s, e) => _compras.ActualizarResultadosBusqueda();
            // Pagos
            _pagos = new PresentadorGestionPagos(new VistaGestionPagos());
            _registroPago = new PresentadorRegistroPago(new VistaRegistroPago());
            _registroPago.EntidadRegistradaActualizada += (s, e) => _pagos.ActualizarResultadosBusqueda();
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
            // Solicitudes de compra
            _principal.Modulos.Vista.PanelCentral.Registrar(_solicitudesCompra.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroSolicitudCompra.Vista);
            // Compras
            _principal.Modulos.Vista.PanelCentral.Registrar(_compras.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroCompra.Vista);
            // Pagos
            _principal.Modulos.Vista.PanelCentral.Registrar(_pagos.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroPago.Vista);
            // Proveedores
            _principal.Modulos.Vista.PanelCentral.Registrar(_proveedores.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroProveedor.Vista);
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
