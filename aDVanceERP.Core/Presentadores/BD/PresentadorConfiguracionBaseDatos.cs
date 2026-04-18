using aDVanceERP.Core.Eventos;
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

            AgregadorEventos.Suscribir("MostrarVistaConfiguracionBaseDatos", OnMostrarVistaVistaConfiguracionBaseDatos);
        }

        private void OnMostrarVistaVistaConfiguracionBaseDatos(string obj) {
            Vista.Restaurar();
                
            CargarConfiguracion();

            Vista.Mostrar();
        }

        public void CargarConfiguracion() {
            var config = RepoConfiguracionBaseDatos.Instancia.ObtenerPorId(0);

            if (config != null) {
                Vista.NombreDireccionServidor = config.Servidor;
                Vista.NombreBaseDatos = config.BaseDatos;
                Vista.NombreUsuario = config.Usuario;
                Vista.Password = config.Password;
                Vista.RecordarConfiguracion = config.RecordarConfiguracion;

                if (string.IsNullOrWhiteSpace(Vista.NombreUsuario) || string.IsNullOrWhiteSpace(Vista.Password)) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Debe configurar las credenciales de base de datos.", TipoNotificacionEnum.Advertencia);
                    return;
                }

                try {
                    ContextoBaseDatos.ActualizarConfiguracion(config);
                } catch (InvalidOperationException) {
                    return;
                }

                AgregadorEventos.Publicar("ConfiguracionBaseDatosCargada", AgregadorEventos.SerializarPayload(config));
            }
        }
        
        private void OnAlmacenarConfiguracion(object? sender, ConfiguracionBaseDatos e) {
            if (e == null) {
                CentroNotificaciones.MostrarNotificacion("La configuración del servidor MySQL no puede ser nula.", TipoNotificacionEnum.Error);
            }
            try {
                try {
                    ContextoBaseDatos.ActualizarConfiguracion(e);
                } catch (InvalidOperationException) {
                    return;
                }

                RepoConfiguracionBaseDatos.Instancia.Salvar(string.Empty, e);

                // Disparar el evento de configuración cargada
                AgregadorEventos.Publicar("ConfiguracionBaseDatosCargada", AgregadorEventos.SerializarPayload(e));
            } catch (Exception) {
                CentroNotificaciones.MostrarNotificacion("Error al guardar la configuración del servidor MySQL.", Modelos.Comun.TipoNotificacionEnum.Error);
            }
        }

        public override void Dispose() {
            Vista.AlmacenarConfiguracion -= OnAlmacenarConfiguracion;

            AgregadorEventos.Desuscribir("MostrarVistaVistaConfiguracionBaseDatos", OnMostrarVistaVistaConfiguracionBaseDatos);
        }
    }
}