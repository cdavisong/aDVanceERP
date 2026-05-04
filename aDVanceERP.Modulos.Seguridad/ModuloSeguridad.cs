using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.Seguridad.Manejadores;
using aDVanceERP.Modulos.Seguridad.Presentadores;
using aDVanceERP.Modulos.Seguridad.Properties;
using aDVanceERP.Modulos.Seguridad.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.Seguridad {
    public sealed class ModuloSeguridad : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();

        // Presentadores
        private PresentadorMenuSeguridad _menuSeguridad = null!;
        private PresentadorAutenticacionUsuario _autenticacionCuentaUsuario = null!;
        private PresentadorRegistroCuentaUsuarioLogin _registroCuentaUsuarioLogin = null!;
        private PresentadorAprobacionUsuario _aprobacionCuentaUsuario = null!;
        private PresentadorGestionCuentasUsuarios _cuentasUsuarios = null!;
        private PresentadorRegistroCuentaUsuario _registroCuentaUsuario = null!;

        // Manejadores de eventos
        private ManejadorCuentaUsuario _manejadorCuentaUsuario = null!;

        public ModuloSeguridad() {
            Nombre = ModuloSistemaEnum.MOD_SEGURIDAD;
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloSeguridad";
            _btnAccesoModulo.CustomImages.Image = Resources.security_configurationB_24px;
            _btnAccesoModulo.TabIndex = 9;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar(new EventoCambioModulo());
                AgregadorEventos.Publicar(new EventoCambioMenu());
                AgregadorEventos.Publicar(new EventoMostrarVistaMenuSeguridad());
            };

            // Menu
            _menuSeguridad = new PresentadorMenuSeguridad(new VistaMenuSeguridad());

            // Contenedor de seguridad
            // Autenticación
            _autenticacionCuentaUsuario = new PresentadorAutenticacionUsuario(new VistaAutenticacionCuentaUsuario());
            _registroCuentaUsuarioLogin = new PresentadorRegistroCuentaUsuarioLogin(new VistaRegistroCuentaUsuarioLogin());
            _aprobacionCuentaUsuario = new PresentadorAprobacionUsuario(new VistaAprobacionCuentaUsuario());

            // Contenedor de módulos
            // Cuentas de usuario
            _cuentasUsuarios = new PresentadorGestionCuentasUsuarios(new VistaGestionCuentasUsuarios());
            _registroCuentaUsuario = new PresentadorRegistroCuentaUsuario(new VistaRegistroCuentaUsuario());
            _registroCuentaUsuario.EntidadRegistradaActualizada += (s, e) => _cuentasUsuarios.ActualizarResultadosBusqueda();

            // Manejadores de eventos
            _manejadorCuentaUsuario = new ManejadorCuentaUsuario();

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Seguridad");

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuSeguridad.Vista);

            // Contenedor de seguridad
            // Registrar vistas de autenticación
            _principal.Seguridad.Vista.PanelCentral.Registrar(_autenticacionCuentaUsuario.Vista);
            _principal.Seguridad.Vista.PanelCentral.Registrar(_registroCuentaUsuarioLogin.Vista);
            _principal.Seguridad.Vista.PanelCentral.Registrar(_aprobacionCuentaUsuario.Vista);

            // Contenedor de módulos
            // Registrar vistas de gestión de cuentas de usuario
            _principal.Modulos.Vista.PanelCentral.Registrar(_cuentasUsuarios.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_registroCuentaUsuario.Vista);
            
            // Mostrar la vista de autenticación por defecto
            _principal.Seguridad.Vista.PanelCentral.Mostrar(nameof(VistaAutenticacionCuentaUsuario));
        }

        protected override void InicializarEventos() {
            AgregadorEventos.Suscribir<EventoCuentaUsuarioRegistrada>(_manejadorCuentaUsuario.Manejar);
            AgregadorEventos.Suscribir<EventoAprobarCuentaUsuario>(_manejadorCuentaUsuario.Manejar);
            AgregadorEventos.Suscribir<EventoUsuarioAutenticado>(OnUsuarioAutenticado);
        }

        private void OnUsuarioAutenticado(EventoUsuarioAutenticado e) {
            _btnAccesoModulo.Visible = ContextoSeguridad.EsAdministrador;
        }

        public override void Apagar() {
            _menuSeguridad?.Dispose();
            _autenticacionCuentaUsuario?.Dispose();
            _registroCuentaUsuarioLogin?.Dispose();
            _aprobacionCuentaUsuario?.Dispose();
            _cuentasUsuarios?.Dispose();
            _registroCuentaUsuario?.Dispose();

            AgregadorEventos.Desuscribir<EventoCuentaUsuarioRegistrada>(_manejadorCuentaUsuario.Manejar);
            AgregadorEventos.Desuscribir<EventoAprobarCuentaUsuario>(_manejadorCuentaUsuario.Manejar);
            AgregadorEventos.Desuscribir<EventoUsuarioAutenticado>(OnUsuarioAutenticado);
        }
    }
}
