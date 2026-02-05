using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.RecursosHumanos.Presentadores;
using aDVanceERP.Modulos.RecursosHumanos.Properties;
using aDVanceERP.Modulos.RecursosHumanos.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.RecursosHumanos { 
    public sealed class ModuloRecursosHumanos : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
        private PresentadorMenuRecursosHumanos _menuRecursosHumanos = null!;
        private PresentadorMenuMaestros _menuMaestros = null!;
        private PresentadorGestionEmpleados _empleados = null!;
        private PresentadorRegistroEmpleado _registroEmpleado = null!;
        private PresentadorGestionProveedores _proveedores = null!;
        private PresentadorRegistroProveedor _registroProveedor = null!;    
        private PresentadorGestionPersonas _personas = null!;
        private PresentadorRegistroPersona _registroPersona = null!;

        public ModuloRecursosHumanos() {
            Nombre = "MOD_RRHH";
            Descripcion = "Proporciona funcionalidades de gestión de recursos humanos.";
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Boton de acceso al módulo
            _btnAccesoModulo.Name = "btnModuloRecursosHumanos";
            _btnAccesoModulo.Image = Resources.businessmanB_24px;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuRecursosHumanos", string.Empty);
            };

            // Menu
            _menuRecursosHumanos = new PresentadorMenuRecursosHumanos(new VistaMenuRecursosHumanos());
            _menuMaestros = new PresentadorMenuMaestros(new VistaMenuMaestros());

            // Contenedor de módulos
            // Empleados
            _empleados = new PresentadorGestionEmpleados(new VistaGestionEmpleados());
            _registroEmpleado = new PresentadorRegistroEmpleado(new VistaRegistroEmpleado());
            _registroEmpleado.EntidadRegistradaActualizada += (s, e) => _empleados.ActualizarResultadosBusqueda();
            // Proveedores
            _proveedores = new PresentadorGestionProveedores(new VistaGestionProveedores());
            _registroProveedor = new PresentadorRegistroProveedor(new VistaRegistroProveedor());
            _registroProveedor.EntidadRegistradaActualizada += (s, e) => _proveedores.ActualizarResultadosBusqueda();
            // Personas
            _personas = new PresentadorGestionPersonas(new VistaGestionPersonas());
            _registroPersona = new PresentadorRegistroPersona(new VistaRegistroPersona());
            _registroPersona.EntidadRegistradaActualizada += (s, e) => _personas.ActualizarResultadosBusqueda();

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo);

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuRecursosHumanos.Vista);
            _principal.Vista.BarraTitulo.Registrar(_menuMaestros.Vista);

            // Contenedor de módulos
            // Empleados
            _principal.Modulos.Vista.PanelCentral.Registrar(_empleados.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroEmpleado.Vista);
            // Proveedores
            _principal.Modulos.Vista.PanelCentral.Registrar(_proveedores.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroProveedor.Vista);
            // Personas
            _principal.Modulos.Vista.PanelCentral.Registrar(_personas.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroPersona.Vista);
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}