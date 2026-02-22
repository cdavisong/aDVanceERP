using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Venta.Presentadores;
using aDVanceERP.Modulos.Venta.Properties;
using aDVanceERP.Modulos.Venta.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Venta {
    internal class ModuloVenta : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
        private PresentadorMenuVenta _menuVenta = null!;
        private PresentadorMenuMaestros _menuMaestros = null!;
        private PresentadorGestionPedidos _pedidos = null!;
        private PresentadorRegistroPedido _registroPedido = null!;
        private PresentadorGestionVentas _ventas = null!;
        private PresentadorRegistroVenta _registroVenta = null!;
        private PresentadorGestionPagos _pagos = null!;
        private PresentadorRegistroPago _registroPago = null!;
        private PresentadorGestionEnvios _envios = null!;
        private PresentadorRegistroEnvio _registroEnvio = null!;
        private PresentadorGestionClientes _clientes = null!;
        private PresentadorRegistroCliente _registroCliente = null!;
        private PresentadorGestionMensajeros _mensajeros = null!;
        private PresentadorRegistroMensajero _registroMensajero = null!;

        public ModuloVenta() {
            Nombre = "MOD_VENTA";
            Descripcion = "Proporciona funcionalidades de gestión de ventas.";
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloVenta";
            _btnAccesoModulo.CustomImages.Image = Resources.best_salesB_24px;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuVenta", string.Empty);
            };

            // Menu
            _menuVenta = new PresentadorMenuVenta(new VistaMenuVenta());
            _menuMaestros = new PresentadorMenuMaestros(new VistaMenuMaestros());

            // Contenedor de módulos
            // Pedidos
            _pedidos = new PresentadorGestionPedidos(new VistaGestionPedidos());
            _registroPedido = new PresentadorRegistroPedido(new VistaRegistroPedido());
            _registroPedido.EntidadRegistradaActualizada += (s, e) => _pedidos.ActualizarResultadosBusqueda();
            // Ventas
            _ventas = new PresentadorGestionVentas(new VistaGestionVentas());
            _registroVenta = new PresentadorRegistroVenta(new VistaRegistroVenta());
            _registroVenta.EntidadRegistradaActualizada += (s, e) => _ventas.ActualizarResultadosBusqueda();
            // Pagos
            _pagos = new PresentadorGestionPagos(new VistaGestionPagos());
            _registroPago = new PresentadorRegistroPago(new VistaRegistroPago());
            _registroPago.EntidadRegistradaActualizada += (s, e) => _pagos.ActualizarResultadosBusqueda();
            // Envíos
            _envios = new PresentadorGestionEnvios(new VistaGestionEnvios());
            _registroEnvio = new PresentadorRegistroEnvio(new VistaRegistroEnvio());
            _registroEnvio.EntidadRegistradaActualizada += (s, e) => _envios.ActualizarResultadosBusqueda();
            // Clientes
            _clientes = new PresentadorGestionClientes(new VistaGestionClientes());
            _registroCliente = new PresentadorRegistroCliente(new VistaRegistroCliente());
            _registroCliente.EntidadRegistradaActualizada += (s, e) => _clientes.ActualizarResultadosBusqueda();
            // Mensajeros
            _mensajeros = new PresentadorGestionMensajeros(new VistaGestionMensajeros());
            _registroMensajero = new PresentadorRegistroMensajero(new VistaRegistroMensajero());
            _registroMensajero.EntidadRegistradaActualizada += (s, e) => _mensajeros.ActualizarResultadosBusqueda();

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Venta");

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuVenta.Vista);
            _principal.Vista.BarraTitulo.Registrar(_menuMaestros.Vista);

            // Contenedor de módulos
            // Pedidos
            _principal.Modulos.Vista.PanelCentral.Registrar(_pedidos.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroPedido.Vista);
            // Ventas
            _principal.Modulos.Vista.PanelCentral.Registrar(_ventas.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroVenta.Vista);
            // Pagos
            _principal.Modulos.Vista.PanelCentral.Registrar(_pagos.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroPago.Vista);
            // Envíos
            _principal.Modulos.Vista.PanelCentral.Registrar(_envios.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroEnvio.Vista);
            // Clientes
            _principal.Modulos.Vista.PanelCentral.Registrar(_clientes.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroCliente.Vista);
            // Mensajeros
            _principal.Modulos.Vista.PanelCentral.Registrar(_mensajeros.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroMensajero.Vista);
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
