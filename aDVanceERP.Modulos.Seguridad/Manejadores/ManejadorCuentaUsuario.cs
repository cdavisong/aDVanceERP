using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;

namespace aDVanceERP.Modulos.Seguridad.Manejadores {
    internal class ManejadorCuentaUsuario {
        private RepoCuentaUsuario _repoCuentaUsuario = null!;
        private RepoPersona _repoPersona = null!;
        private RepoCorreoContacto _repoCorreoContacto = null!;

        internal ManejadorCuentaUsuario() {
            _repoCuentaUsuario = RepoCuentaUsuario.Instancia;
            _repoPersona = RepoPersona.Instancia;
            _repoCorreoContacto = RepoCorreoContacto.Instancia;
        }

        internal void Manejar(EventoCuentaUsuarioRegistrada e) {
            // Si es el primer usuario, hacerlo administrador automáticamente
            if (_repoCuentaUsuario.Cantidad() == 1 && e.CuentaUsuario.Id > 0) {
                try {
                    _repoCuentaUsuario.ConvertirEnAdministrador(e.CuentaUsuario.Id);

                    CentroNotificaciones.MostrarNotificacion(
                        "¡Bienvenido! Su cuenta ha sido creada como Administrador del sistema.",
                        TipoNotificacionEnum.Info);

                    AgregadorEventos.Publicar(new EventoMostrarVistaAutenticacionCuentaUsuario());
                } catch (Exception ex) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"Error al configurar administrador: {ex.Message}",
                        TipoNotificacionEnum.Error);
                }
            } else if (e.CuentaUsuario.Id > 0) {
                if (!e.CuentaUsuario.Aprobado && !e.RegistroDesdeAdmin) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Su solicitud de registro ha sido enviada. Espere la aprobación del administrador.",
                        TipoNotificacionEnum.Info);

                    AgregadorEventos.Publicar(new EventoMostrarVistaAprobacionCuentaUsuario() {
                        CuentaUsuario = e.CuentaUsuario
                    });
                }
            }

            // Agregar datos de la persona a la cuenta de usuario si existen y actualizar la cuenta de usuario con el IdPersona
            if (e.Persona != null) {
                var idPersona = _repoPersona.Adicionar(e.Persona);

                e.CuentaUsuario.IdPersona = idPersona;
                
                _repoCuentaUsuario.Editar(e.CuentaUsuario);

                // Agregar datos de correo electrónico de contacto si existen y asociarlos a la persona
                if (e.CorreoContacto != null) {
                    e.CorreoContacto.IdPersona = e.CuentaUsuario.IdPersona;

                    _repoCorreoContacto.Adicionar(e.CorreoContacto);
                }
            }
        }

        internal void Manejar(EventoAprobarCuentaUsuario e) {
            if (e.CuentaUsuario.Id > 0) {
                _repoCuentaUsuario.AprobarCuentaUsuario(e.CuentaUsuario.Id);

                CentroNotificaciones.MostrarNotificacion(
                    $"La cuenta de {e.CuentaUsuario.Nombre} ha sido aprobada.",
                    TipoNotificacionEnum.Ok);
            }
        }
    }
}
