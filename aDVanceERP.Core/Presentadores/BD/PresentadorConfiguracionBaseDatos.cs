using aDVanceERP.Core.Eventos.BD;
using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.BD;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.BD;
using aDVanceERP.Core.Vistas.BD;

namespace aDVanceERP.Core.Presentadores.BD {
    public class PresentadorConfiguracionBaseDatos : PresentadorVistaBase<VistaConfiguracionBaseDatos> {
        public PresentadorConfiguracionBaseDatos(VistaConfiguracionBaseDatos vista) : base(vista) {
            vista.AlmacenarConfiguracion += OnAlmacenarConfiguracion;

            AgregadorEventos.Suscribir<EventoMostrarVistaConfiguracionBaseDatos>(OnMostrarVistaVistaConfiguracionBaseDatos);
        }

        private void OnMostrarVistaVistaConfiguracionBaseDatos(EventoMostrarVistaConfiguracionBaseDatos e) {
            Vista.Restaurar();
                
            CargarDatosComunes();

            Vista.Mostrar();
        }

        public void CargarDatosComunes() {
            var configracion = RepoConfiguracionBaseDatos.Instancia.ObtenerPorId(0);

            if (configracion != null) {
                Vista.NombreDireccionServidor = configracion.Servidor;
                Vista.NombreBaseDatos = configracion.BaseDatos;
                Vista.NombreUsuario = configracion.Usuario;
                Vista.Password = configracion.Password;
                Vista.RecordarConfiguracion = configracion.RecordarConfiguracion;

                if (string.IsNullOrWhiteSpace(Vista.NombreUsuario) || string.IsNullOrWhiteSpace(Vista.Password)) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Debe configurar las credenciales de base de datos.", TipoNotificacionEnum.Advertencia);
                    return;
                }

                try {
                    ContextoBaseDatos.ActualizarConfiguracion(configracion);
                } catch (InvalidOperationException) {
                    return;
                }

                AgregadorEventos.Publicar(new EventoConfiguracionBaseDatosCargada() {
                    Configuracion = configracion
                });
            }
        }
        
        private void OnAlmacenarConfiguracion(object? sender, ConfiguracionBaseDatos configuracion) {
            if (configuracion == null) {
                CentroNotificaciones.MostrarNotificacion("La configuración del servidor MySQL no puede ser nula.", TipoNotificacionEnum.Error);
            }
            try {
                try {
                    ContextoBaseDatos.ActualizarConfiguracion(configuracion);
                } catch (InvalidOperationException) {
                    return;
                }

                RepoConfiguracionBaseDatos.Instancia.Salvar(string.Empty, configuracion);

                AgregadorEventos.Publicar(new EventoConfiguracionBaseDatosCargada() {
                    Configuracion = configuracion
                });
            } catch (Exception) {
                CentroNotificaciones.MostrarNotificacion("Error al guardar la configuración del servidor MySQL.", TipoNotificacionEnum.Error);
            }
        }

        public override void Dispose() {
            Vista.AlmacenarConfiguracion -= OnAlmacenarConfiguracion;

            AgregadorEventos.Desuscribir<EventoMostrarVistaConfiguracionBaseDatos>(OnMostrarVistaVistaConfiguracionBaseDatos);

            Vista.Cerrar();
        }
    }
}