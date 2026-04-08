using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Extension.Interfaces.BaseConcreta;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.Comun.Interfaces;
using aDVanceERP.Modulos.CajaRegistradora.Presentadores;
using aDVanceERP.Modulos.CajaRegistradora.Properties;
using aDVanceERP.Modulos.CajaRegistradora.Vistas;

using Guna.UI2.WinForms;

namespace aDVanceERP.Modulos.CajaRegistradora {
    public class ModuloCaja : ModuloExtensionBase {
        private Guna2CircleButton _btnAccesoModulo = new Guna2CircleButton();
        private PresentadorMenuCajaRegistradora _menuCajaRegistradora = null!;
        private PresentadorGestionCaja _cajaTurno = null!;
        private PresentadorAperturaTurno _aperturaTurno = null!;
        private PresentadorCierreTurno _cierreTurno = null!;
        private PresentadorMovimientoCaja _movimientoCaja = null!;
        private PresentadorDetalleTurno _detalleTurno = null!;

        public ModuloCaja() {
            Nombre = "MOD_CAJA";
            NombreAmigable = "Caja registradora";
            Descripcion = "Proporciona funcionalidades para la gestión de caja registradora.";
            Version = new Version(1, 0, 0, 0);
        }

        public override void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> principal) {
            // Botón de acceso al módulo
            _btnAccesoModulo.Name = "btnAccesoModuloCaja";
            _btnAccesoModulo.CustomImages.Image = Resources.cash_registerB_24px;
            _btnAccesoModulo.CustomImages.ImageOffset = new Point(0, 2);
            _btnAccesoModulo.TabIndex = 5;
            _btnAccesoModulo.Click += delegate {
                AgregadorEventos.Publicar("EventoCambioModulo", string.Empty);
                AgregadorEventos.Publicar("EventoCambioMenu", string.Empty);
                AgregadorEventos.Publicar("MostrarVistaMenuCajaRegistradora", string.Empty);
            };

            // Menu
            _menuCajaRegistradora = new PresentadorMenuCajaRegistradora(new VistaMenuCaja());

            // Contenedor de módulos
            // Caja - Turno
            _cajaTurno = new PresentadorGestionCaja(new VistaGestionCajaTurno());
            _aperturaTurno = new PresentadorAperturaTurno(new VistaAperturaTurno());
            _cierreTurno = new PresentadorCierreTurno(new VistaCierreTurno());
            _movimientoCaja = new PresentadorMovimientoCaja(new VistaMovimientoCaja());
            _aperturaTurno.EntidadRegistradaActualizada += (s, e) => _cajaTurno.ActualizarResultadosBusqueda();
            _cierreTurno.EntidadRegistradaActualizada += (s, e) => _cajaTurno.ActualizarResultadosBusqueda();
            _movimientoCaja.EntidadRegistradaActualizada += (s, e) => _cajaTurno.ActualizarResultadosBusqueda();

            // Detalle - Turno
            _detalleTurno = new PresentadorDetalleTurno(new VistaDetalleTurno());

            base.Inicializar(principal);
        }

        protected override void InicializarVistas() {
            // Agregar botón de acceso al módulo
            _principal.Modulos.AdicionarBotonAccesoModulo(_btnAccesoModulo, "Caja registradora");

            // Agregar menú del módulo
            _principal.Vista.BarraTitulo.Registrar(_menuCajaRegistradora.Vista);

            // Contenedor de módulos
            // Caja - Turno
            _principal.Modulos.Vista.PanelCentral.Registrar(_cajaTurno.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_aperturaTurno.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_cierreTurno.Vista);
            _principal.Modulos.Vista.PanelCentral.Registrar(_movimientoCaja.Vista);

            // Detalle - Turno
            _principal.Modulos.Vista.PanelCentral.Registrar(_detalleTurno.Vista);
        }

        public override void Apagar() {
            throw new NotImplementedException();
        }
    }
}
