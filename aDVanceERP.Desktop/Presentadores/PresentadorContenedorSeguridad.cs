using aDVanceERP.Core.Eventos.BD;
using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Presentadores.BD;
using aDVanceERP.Core.Presentadores.Comun.Interfaces;
using aDVanceERP.Core.Vistas.BD;
using aDVanceERP.Core.Vistas.Comun;
using aDVanceERP.Core.Vistas.Comun.Interfaces;

namespace aDVanceERP.Desktop.Presentadores {
    public partial class PresentadorContenedorSeguridad : IPresentadorVistaContenedorSeguridad<IVistaContenedorSeguridad> {
        private VistaCargaDatos _cargaDatos = null!;
        private PresentadorConfiguracionBaseDatos _configuracionBaseDatos = null!;

        public PresentadorContenedorSeguridad(IPresentadorVistaPrincipal<IVistaPrincipal> vistaPrincipal, IVistaContenedorSeguridad vistaSeguridad) {
            Inicializar(vistaPrincipal, vistaSeguridad);
            InicializarVistas();
            InicializarEventos();
        }

        private void Inicializar(IPresentadorVistaPrincipal<IVistaPrincipal> vistaPrincipal, IVistaContenedorSeguridad vistaSeguridad) {
            // Vista principal
            VistaPrincipal = vistaPrincipal;

            // Vista seguridad
            Vista = vistaSeguridad;

            _cargaDatos = new VistaCargaDatos();
            // Configuración base de datos
            _configuracionBaseDatos = new PresentadorConfiguracionBaseDatos(new VistaConfiguracionBaseDatos());
        }

        private void InicializarVistas() {
            // Contenedor seguridad
            // Configuración base de datos
            Vista.PanelCentral.Registrar(_configuracionBaseDatos.Vista);
        }

        private void InicializarEventos() {
            // Contenedor seguridad
            AgregadorEventos.Suscribir<EventoMostrarVistaContenedorSeguridad>(OnMostrarVistaContenedorSeguridad);
            // Configuración base de datos
            AgregadorEventos.Suscribir<EventoConfiguracionBaseDatosCargada>(OnConfiguracionBaseDatosCargada);
        }

        public IPresentadorVistaPrincipal<IVistaPrincipal> VistaPrincipal { get; private set; } = null!;

        public IVistaContenedorSeguridad Vista { get; private set; } = null!;

        private void OnMostrarVistaContenedorSeguridad(EventoMostrarVistaContenedorSeguridad e) {
            Vista.Restaurar();
            Vista.Mostrar();

            AgregadorEventos.Publicar(new EventoMostrarVistaConfiguracionBaseDatos());
        }

        private async void OnConfiguracionBaseDatosCargada(EventoConfiguracionBaseDatosCargada e) {
            var progreso = new Progress<(string texto, int porcentaje)>(datos => {
                _cargaDatos.TextoProgreso = datos.texto;
                _cargaDatos.ProgresoValor = datos.porcentaje;
            });

            _cargaDatos.TextoProgreso = " Cargando la aplicación...";
            _cargaDatos.Mostrar();

            await Task.Run(() => {
                ((PresentadorContenedorModulos) VistaPrincipal.Modulos).CargarModulosExtension(VistaPrincipal, progreso);
            });

            _cargaDatos.Ocultar();
        }

        public void Dispose() {
            AgregadorEventos.Desuscribir<EventoMostrarVistaContenedorSeguridad>(OnMostrarVistaContenedorSeguridad);
            AgregadorEventos.Desuscribir<EventoConfiguracionBaseDatosCargada>(OnConfiguracionBaseDatosCargada);

            Vista.Dispose();
        }
    }
}