using aDVanceERP.Core.Mensajes.Utiles;
using aDVanceERP.Desktop.Utiles;
using aDVanceERP.Modulos.Contactos.MVP.Presentadores;
using aDVanceERP.Modulos.Contactos.MVP.Vistas.Empresa;

namespace aDVanceERP.Desktop.MVP.Presentadores.Principal {
    public partial class PresentadorPrincipal {
        private PresentadorRegistroEmpresa? _registroEmpresa;
        private bool _isRegistroEmpresa;

        private void InicializarVistaRegistroEmpresa() {
            _registroEmpresa = new PresentadorRegistroEmpresa(new VistaRegistroEmpresa());
            _registroEmpresa.Vista.EstablecerCoordenadasVistaRegistro(Vista.Dimensiones, true);
            _registroEmpresa.Vista.EstablecerDimensionesVistaRegistro(Vista.Dimensiones.Height, true);
            ((Form)_registroEmpresa.Vista).FormClosing += delegate {
                if (!_isRegistroEmpresa) {
                    _isRegistroEmpresa = false;

                    CentroNotificaciones.Mostrar("En la nueva versión es necesario registrar los datos de la empresa, reinicie la aplicación y tómese unos minutos para rellenar los datos necesarios", Core.Mensajes.MVP.Modelos.TipoNotificacion.Advertencia);
                }
            };
            _registroEmpresa.EntidadRegistradaActualizada += delegate {
                _isRegistroEmpresa = true;
            };            
        }

        private void MostrarVistaRegistroEmpresa(object? sender, EventArgs e) {
            InicializarVistaRegistroEmpresa();

            if (_registroEmpresa == null)
                return;

            _registroEmpresa?.Vista.Mostrar();
            _registroEmpresa?.Dispose();
        }

        private void MostrarVistaEdicionEmpresa(object? sender, EventArgs e) {
            InicializarVistaRegistroEmpresa();

            if (_empresa != null) {
                if (_registroEmpresa != null) {
                    _registroEmpresa.PopularVistaDesdeEntidad(_empresa);
                    _registroEmpresa.Vista.Mostrar();
                }
            }

            _registroEmpresa?.Dispose();
        }
    }
}
