using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorTuplaCuentaUsuario : PresentadorVistaTupla<IVistaTuplaCuentaUsuario, CuentaUsuario> {
        public PresentadorTuplaCuentaUsuario(IVistaTuplaCuentaUsuario vista, CuentaUsuario objeto) : base(vista, objeto) {
            vista.AprobarCuentaUsuario += OnAprobarCuentaUsuario;
            vista.EditarDatosTupla += MostrarVistaEdicionCuentaUsuario;
        }

        private void OnAprobarCuentaUsuario(object? sender, EventArgs e) {
            var cuentaUsuario = RepoCuentaUsuario.Instancia.ObtenerPorId(Vista.Id);

            try {
                AgregadorEventos.Publicar(new EventoAprobarCuentaUsuario() {
                    CuentaUsuario = cuentaUsuario!
                });
            } catch (Exception) {
                CentroNotificaciones.MostrarNotificacion(
                    "Ocurrió un error al aprobar la cuenta de usuario. Por favor, inténtelo de nuevo.", 
                    TipoNotificacionEnum.Error);
            }

            Vista.Aprobado = true;
        }

        private void MostrarVistaEdicionCuentaUsuario(object? sender, EventArgs e) {
            var cuentaUsuario = RepoCuentaUsuario.Instancia.ObtenerPorId(Vista.Id);

            AgregadorEventos.Publicar(new EventoMostrarVistaEdicionCuentaUsuario() {
                CuentaUsuario = cuentaUsuario!
            });
        }

        public override void Dispose() {
            Vista.EditarDatosTupla -= MostrarVistaEdicionCuentaUsuario;

            base.Dispose();
        }
    }
}