using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Seguridad.MVP.Presentadores;
using aDVanceERP.Core.Seguridad.MVP.Vistas.Menu;

namespace aDVanceERP.Desktop.MVP.Presentadores.Principal {
    public partial class PresentadorPrincipal {
        public PresentadorMenuUsuario? _menuUsuario;

        private void InicializarVistaMenuUsuario() {
            _menuUsuario = new PresentadorMenuUsuario(new VistaMenuUsuario());

            _menuUsuario.Vista.ConfigurarEmpresa += delegate (object? sender, EventArgs e) {
                MostrarVistaEdicionEmpresa(sender, e);
                ActualizarRepoEmpresa();
            };
            _menuUsuario.Vista.CerrarSesion += delegate {
                AgregadorEventos.Publicar("EventoSesionCerrada", "");
            };

            Vista.PanelCentral?.Registrar(
                _menuUsuario.Vista,
                new Point(Vista.Dimensiones.Width - _menuUsuario.Vista.Dimensiones.Width - 150, 0),
                _menuUsuario.Vista.Dimensiones,
                TipoRedimensionadoVista.Ninguno);
        }

        private void OnVerMenuUsuario(object? sender, EventArgs e) {
            if (_menuUsuario == null)
                return;

            _menuUsuario.Vista.Restaurar();
            _menuUsuario.Vista.Mostrar();
        }
    }
}
