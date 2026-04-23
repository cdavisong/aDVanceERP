using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
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
        private PresentadorEstadisticasVenta _estadisticasVenta = null!;
        private PresentadorGestionVentas _ventas = null!;
        private PresentadorRegistroVenta _registroVenta = null!;
        private PresentadorGestionPagos _pagos = null!;
        private PresentadorRegistroPago _registroPago = null!;
        private PresentadorGestionClientes _clientes = null!;
        private PresentadorRegistroCliente _registroCliente = null!;

        public ModuloVenta() {
            Nombre = ModuloSistemaEnum.MOD_VENTA;
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloVenta";
            _btnAccesoModulo.CustomImages.Image = Resources.best_salesB_24px;
            _btnAccesoModulo.TabIndex = 3;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuVenta", string.Empty);
            };

            // Menu
            _menuVenta = new PresentadorMenuVenta(new VistaMenuVenta());
            _menuMaestros = new PresentadorMenuMaestros(new VistaMenuMaestros());

            // Contenedor de módulos
            // Estadísticas
            _estadisticasVenta = new PresentadorEstadisticasVenta(new VistaEstadisticasVenta());
            // Ventas
            _ventas = new PresentadorGestionVentas(new VistaGestionVentas());
            _registroVenta = new PresentadorRegistroVenta(new VistaRegistroVenta());
            _registroVenta.EntidadRegistradaActualizada += (s, e) => _ventas.ActualizarResultadosBusqueda();
            // Pagos
            _pagos = new PresentadorGestionPagos(new VistaGestionPagos());
            _registroPago = new PresentadorRegistroPago(new VistaRegistroPago());
            _registroPago.EntidadRegistradaActualizada += (s, e) => _pagos.ActualizarResultadosBusqueda();
            // Clientes
            _clientes = new PresentadorGestionClientes(new VistaGestionClientes());
            _registroCliente = new PresentadorRegistroCliente(new VistaRegistroCliente());
            _registroCliente.EntidadRegistradaActualizada += (s, e) => _clientes.ActualizarResultadosBusqueda();

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Venta");

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuVenta.Vista);
            _principal.Vista.BarraTitulo.Registrar(_menuMaestros.Vista);

            // Contenedor de módulos
            // Estadísticas
            _principal.Modulos.Vista.PanelCentral.Registrar(_estadisticasVenta.Vista);
            // Ventas
            _principal.Modulos.Vista.PanelCentral.Registrar(_ventas.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroVenta.Vista);
            // Pagos
            _principal.Modulos.Vista.PanelCentral.Registrar(_pagos.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroPago.Vista);
            // Clientes
            _principal.Modulos.Vista.PanelCentral.Registrar(_clientes.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroCliente.Vista);
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
