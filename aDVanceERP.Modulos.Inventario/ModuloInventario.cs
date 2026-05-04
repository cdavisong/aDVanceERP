using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos;
using aDVanceERP.Core.Eventos.Modulos.Inventario;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Eventos.Modulos.Venta;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Inventario.Manejadores;
using aDVanceERP.Modulos.Inventario.Presentadores;
using aDVanceERP.Modulos.Inventario.Properties;
using aDVanceERP.Modulos.Inventario.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Inventario { 
    public sealed class ModuloInventario : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();

        // Presentadores
        private PresentadorEstadisticasInventario _estadisticasGenerales = null!;
        private PresentadorMenuInventario _menuInventario = null!;
        private PresentadorMenuMaestros _menuMaestros = null!;
        private PresentadorGestionProductos _productos = null!;
        private PresentadorRegistroProducto _registroProducto = null!;
        private PresentadorGestionPresentacionesProducto _presentacionesProducto = null!;
        private PresentadorGestionMovimientos _movimientos = null!;
        private PresentadorRegistroMovimiento _registroMovimiento = null!;
        private PresentadorGestionAlmacenes _almacenes = null!;
        private PresentadorRegistroAlmacen _registroAlmacen = null!;
        private PresentadorGestionClasificaciones _clasificaciones = null!;
        private PresentadorRegistroClasificacion _registroClasificacion = null!;
        private PresentadorGestionUnidadesMedida _unidadesMedida = null!;
        private PresentadorRegistroUnidadMedida _registroUnidadMedida = null!;

        // Manejadores de eventos
        private ManejadorInventario _manejadorInventario = null!;
        private ManejadorMovimiento _manejadorMovimiento = null!;

        public ModuloInventario() {
            Nombre = ModuloSistemaEnum.MOD_INVENTARIO;
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloInventario";
            _btnAccesoModulo.CustomImages.Image = Resources.inventory_24px;
            _btnAccesoModulo.TabIndex = 1;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar(new EventoCambioModulo());
                AgregadorEventos.Publicar(new EventoCambioMenu());
                AgregadorEventos.Publicar(new EventoMostrarVistaMenuInventario());
            };

            // Menu
            _menuInventario = new PresentadorMenuInventario(new VistaMenuInventario());
            _menuMaestros = new PresentadorMenuMaestros(new VistaMenuMaestros());

            // Contenedor de módulos
            // Estadísticas generales
            _estadisticasGenerales = new PresentadorEstadisticasInventario(new VistaEstadisticasInventario());
            // Productos
            _productos = new PresentadorGestionProductos(new VistaGestionProductos());
            _registroProducto = new PresentadorRegistroProducto(new VistaRegistroProducto());        
            _registroProducto.EntidadRegistradaActualizada += (s, e) => _productos.ActualizarResultadosBusqueda();
            // Presentaciones de productos
            _presentacionesProducto = new PresentadorGestionPresentacionesProducto(new VistaGestionPresentacionesProducto());
            _presentacionesProducto.Vista.RegistrarEntidad += (s, e) => _productos.ActualizarResultadosBusqueda();
            _presentacionesProducto.Vista.EliminarEntidad += (s, e) => _productos.ActualizarResultadosBusqueda();
            // Movimientos
            _movimientos = new PresentadorGestionMovimientos(new VistaGestionMovimientos());
            _registroMovimiento = new PresentadorRegistroMovimiento(new VistaRegistroMovimiento());
            _registroMovimiento.EntidadRegistradaActualizada += (s, e) => _movimientos.ActualizarResultadosBusqueda();
            _registroMovimiento.EntidadRegistradaActualizada += (s, e) => _productos.ActualizarResultadosBusqueda();
            // Almacenes
            _almacenes = new PresentadorGestionAlmacenes(new VistaGestionAlmacenes());
            _registroAlmacen = new PresentadorRegistroAlmacen(new VistaRegistroAlmacen());
            _registroAlmacen.EntidadRegistradaActualizada += (s, e) => _almacenes.ActualizarResultadosBusqueda();
            // Clasificaciones
            _clasificaciones = new PresentadorGestionClasificaciones(new VistaGestionClasificaciones());
            _registroClasificacion = new PresentadorRegistroClasificacion(new VistaRegistroClasificacion());
            _registroClasificacion.EntidadRegistradaActualizada += (s, e) => _clasificaciones.ActualizarResultadosBusqueda();
            // Unidades de medida
            _unidadesMedida = new PresentadorGestionUnidadesMedida(new VistaGestionUnidadesMedida());
            _registroUnidadMedida = new PresentadorRegistroUnidadMedida(new VistaRegistroUnidadMedida());
            _registroUnidadMedida.EntidadRegistradaActualizada += (s, e) => _unidadesMedida.ActualizarResultadosBusqueda();

            // Manejadores de eventos
            _manejadorInventario = new ManejadorInventario();
            _manejadorMovimiento = new ManejadorMovimiento();

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Inventario");

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuInventario.Vista);
            _principal.Vista.BarraTitulo.Registrar(_menuMaestros.Vista);

            // Contenedor de módulos
            // Estadísticas generales
            _principal.Modulos.Vista.PanelCentral.Registrar(_estadisticasGenerales.Vista);
            // Productos
            _principal.Modulos.Vista.PanelCentral.Registrar(_productos.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroProducto.Vista);
            // Presentaciones producto
            _principal.Modulos.Vista.PanelCentral.Registrar(_presentacionesProducto.Vista);
            // Movimientos
            _principal.Modulos.Vista.PanelCentral.Registrar(_movimientos.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroMovimiento.Vista);
            // Almacenes
            _principal.Modulos.Vista.PanelCentral.Registrar(_almacenes.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroAlmacen.Vista);
            // Clasificaciones
            _principal.Modulos.Vista.PanelCentral.Registrar(_clasificaciones.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroClasificacion.Vista);
            // Unidades de medida
            _principal.Modulos.Vista.PanelCentral.Registrar(_unidadesMedida.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroUnidadMedida.Vista);
        }

        protected override void InicializarEventos() {
            AgregadorEventos.Suscribir<EventoInventarioModificado>(_manejadorInventario.Manejar);
            AgregadorEventos.Suscribir<EventoProductoRegistrado>(_manejadorMovimiento.Manejar);
            AgregadorEventos.Suscribir<EventoVentaRegistrada>(_manejadorMovimiento.Manejar);
            AgregadorEventos.Suscribir<EventoVentaAnulada>(_manejadorMovimiento.Manejar);
            AgregadorEventos.Suscribir<EventoUsuarioAutenticado>(HabilitarBtnModulo);
        }

        private void HabilitarBtnModulo(EventoUsuarioAutenticado autenticado) {
            _btnAccesoModulo.Visible = ContextoSeguridad.TieneAccesoModulo(ModuloSistemaEnum.MOD_INVENTARIO);
        }

        public override void Apagar() {
            _estadisticasGenerales?.Dispose();
            _menuInventario?.Dispose();
            _menuMaestros?.Dispose();
            _productos?.Dispose();
            _registroProducto?.Dispose();
            _presentacionesProducto?.Dispose();
            _movimientos?.Dispose();
            _registroMovimiento?.Dispose();
            _almacenes?.Dispose();
            _registroAlmacen?.Dispose();
            _clasificaciones?.Dispose();
            _registroClasificacion?.Dispose();
            _unidadesMedida?.Dispose();
            _registroUnidadMedida?.Dispose();

            AgregadorEventos.Desuscribir<EventoInventarioModificado>(_manejadorInventario.Manejar);
            AgregadorEventos.Desuscribir<EventoProductoRegistrado>(_manejadorMovimiento.Manejar);
            AgregadorEventos.Desuscribir<EventoVentaRegistrada>(_manejadorMovimiento.Manejar);
            AgregadorEventos.Desuscribir<EventoVentaAnulada>(_manejadorMovimiento.Manejar);
            AgregadorEventos.Desuscribir<EventoUsuarioAutenticado>(HabilitarBtnModulo);
        }
    }
}