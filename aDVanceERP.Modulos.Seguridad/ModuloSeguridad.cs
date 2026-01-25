using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Seguridad.Presentadores;
using aDVanceERP.Modulos.Seguridad.Properties;
using aDVanceERP.Modulos.Seguridad.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Seguridad {
    public sealed class ModuloSeguridad : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
        private PresentadorMenuSeguridad _menuSeguridad = null!;
        private PresentadorAutenticacionUsuario _autenticacionUsuario = null!;
        private PresentadorRegistroUsuario _registroUsuario = null!;
        private PresentadorAprobacionUsuario _aprobacionUsuario = null!;
        private PresentadorGestionCuentasUsuarios _cuentasUsuarios = null!;
        private PresentadorRegistroCuentaUsuario _registroCuentaUsuario = null!;
        private PresentadorGestionRolesUsuarios _rolesUsuarios = null!;
        private PresentadorRegistroRolUsuario _registroRolUsuario = null!;

        public ModuloSeguridad() {
            Nombre = "MOD_SEGURIDAD";
            Descripcion = "Proporciona funcionalidades de seguridad y gestión de usuarios.";
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloSeguridad";
            _btnAccesoModulo.CustomImages.Image = Resources.security_configurationB_24px;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuSeguridad", string.Empty);
            };

            // Menu
            _menuSeguridad = new PresentadorMenuSeguridad(new VistaMenuSeguridad());

            // Contenedor de seguridad
            // Autenticación
            _autenticacionUsuario = new PresentadorAutenticacionUsuario(new VistaAutenticacionUsuario());
            _registroUsuario = new PresentadorRegistroUsuario(new VistaRegistroUsuario());
            _aprobacionUsuario = new PresentadorAprobacionUsuario(new VistaAprobacionUsuario());

            // Contenedor de módulos
            // Cuentas de usuario
            _cuentasUsuarios = new PresentadorGestionCuentasUsuarios(new VistaGestionCuentasUsuarios());
            _registroCuentaUsuario = new PresentadorRegistroCuentaUsuario(new VistaRegistroCuentaUsuario());
            _registroCuentaUsuario.EntidadRegistradaActualizada += (s, e) => _cuentasUsuarios.ActualizarResultadosBusqueda();
            // Roles de usuario
            _rolesUsuarios = new PresentadorGestionRolesUsuarios(new VistaGestionRolesUsuarios());
            _registroRolUsuario = new PresentadorRegistroRolUsuario(new VistaRegistroRolUsuario());
            _registroRolUsuario.EntidadRegistradaActualizada += (s, e) => _rolesUsuarios.ActualizarResultadosBusqueda();

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo);

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuSeguridad.Vista);

            // Contenedor de seguridad
            // Registrar vistas de autenticación
            _principal.Seguridad.Vista.PanelCentral.Registrar(_autenticacionUsuario.Vista);
            _principal.Seguridad.Vista.PanelCentral.Registrar(_registroUsuario.Vista);
            _principal.Seguridad.Vista.PanelCentral.Registrar(_aprobacionUsuario.Vista);

            // Contenedor de módulos
            // Registrar vistas de gestión de cuentas de usuario
            _principal.Modulos.Vista.PanelCentral.Registrar(_cuentasUsuarios.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(
                _registroCuentaUsuario.Vista, 
                new Point(_principal.Modulos.Vista.PanelCentral.Dimensiones.Width - _registroCuentaUsuario.Vista.Dimensiones.Width, - 10), 
                _registroCuentaUsuario.Vista.Dimensiones, 
                TipoRedimensionadoVista.Vertical);
            // Registrar vistas de gestión de roles de usuario
            _principal.Modulos.Vista.PanelCentral.Registrar(_rolesUsuarios.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(
                _registroRolUsuario.Vista, 
                new Point(_principal.Modulos.Vista.PanelCentral.Dimensiones.Width - _registroRolUsuario.Vista.Dimensiones.Width, -10), 
                _registroRolUsuario.Vista.Dimensiones, 
                TipoRedimensionadoVista.Vertical);

            // Mostrar la vista de autenticación por defecto
            _principal.Seguridad.Vista.PanelCentral.Mostrar(nameof(VistaAutenticacionUsuario));
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
