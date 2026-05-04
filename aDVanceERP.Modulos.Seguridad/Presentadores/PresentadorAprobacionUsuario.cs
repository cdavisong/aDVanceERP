using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorAprobacionUsuario : PresentadorVistaBase<IVistaAprobacionUsuario> {
        public PresentadorAprobacionUsuario(IVistaAprobacionUsuario vista) : base(vista) {
            AgregadorEventos.Suscribir<EventoMostrarVistaAprobacionCuentaUsuario>(OnMostrarVistaAprobacionUsuario);
        }

        private void OnMostrarVistaAprobacionUsuario(EventoMostrarVistaAprobacionCuentaUsuario e) {
            Vista.NombreUsuario = e.CuentaUsuario.Nombre ?? "usuario";
            Vista.Mostrar();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir<EventoMostrarVistaAprobacionCuentaUsuario>(OnMostrarVistaAprobacionUsuario);
        }
    }
}