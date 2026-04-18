using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Core.Servicios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorAutenticacionUsuario : PresentadorVistaBase<IVistaAutenticacionUsuario> {
        private readonly ServicioAutenticacion _servicioAutenticacion;
        private readonly RepoRol _repoRol;

        public PresentadorAutenticacionUsuario(IVistaAutenticacionUsuario vista) : base(vista) {
            _servicioAutenticacion = ServicioAutenticacion.Instancia;
            _repoRol = RepoRol.Instancia;

            AgregadorEventos.Suscribir("MostrarVistaAutenticacionUsuario", OnMostrarVistaAutenticacionUsuario);
            AgregadorEventos.Suscribir("EventoAutenticarCuentaUsuario", OnAutenticarCuentaUsuario);
        }

        private void OnMostrarVistaAutenticacionUsuario(string obj) {
            Vista.Restaurar();
            Vista.Mostrar();
        }

        private void OnAutenticarCuentaUsuario(string obj) {
            if (string.IsNullOrEmpty(Vista.NombreUsuario) || Vista.Password.Length == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe especificar un usuario y contraseña para autenticarse en el sistema. Por favor, rellene los campos correctamente.",
                    TipoNotificacionEnum.Advertencia);

                return;
            }

            try {
                using (var repoUsuario = new RepoCuentaUsuario()) {
                    var (cantidad, resultados) = repoUsuario.Buscar(FiltroBusquedaCuentaUsuario.Nombre, Vista.NombreUsuario);
                    var usuario = resultados.FirstOrDefault().entidadBase;

                    if (usuario == null) {
                        CentroNotificaciones.MostrarNotificacion(
                            "El usuario especificado no existe en la base de datos o no se ha registrado aún en el sistema, verifique los datos entrados.",
                            TipoNotificacionEnum.Advertencia);

                        return;
                    }

                    // Verificar estado del usuario
                    if (!usuario.Estado) {
                        CentroNotificaciones.MostrarNotificacion(
                            "La cuenta de usuario está desactivada. Contacte al administrador del sistema.",
                            TipoNotificacionEnum.Advertencia);
                        return;
                    }

                    if (Vista.Password.VerificarPassword(usuario.PasswordHash, usuario.PasswordSalt)) {
                        // Obtener rol del usuario
                        var rol = _repoRol.ObtenerPorId(usuario.IdRol);

                        if (usuario.Aprobado) {
                            // Cargar permisos del usuario
                            var gestorPermisos = _servicioAutenticacion.ObtenerGestorPermisos(usuario.Id);

                            // Establecer contexto de seguridad completo
                            ContextoSeguridad.UsuarioAutenticado = usuario;
                            ContextoSeguridad.RolUsuario = rol;
                            ContextoSeguridad.GestorPermisos = gestorPermisos;

                            // Iniciar sesión en el sistema estático
                            SesionUsuario.IniciarSesion(usuario, rol);
                            SesionUsuario.EstablecerGestorPermisos(gestorPermisos);

                            // Actualizar último acceso
                            RepoCuentaUsuario.Instancia.ActualizarUltimoAcceso(usuario.Id);

                            AgregadorEventos.Publicar("EventoUsuarioAutenticado", AgregadorEventos.SerializarPayload(usuario));
                        } else {
                            AgregadorEventos.Publicar("MostrarVistaAprobacionUsuario", AgregadorEventos.SerializarPayload(usuario));
                        }
                    } else {
                        CentroNotificaciones.MostrarNotificacion(
                            "La contraseña especificada es incorrecta para el usuario especificado, verifique los datos entrados.",
                            TipoNotificacionEnum.Advertencia);
                    }
                }
            } catch (ExcepcionConexionServidorMySQL e) {
                CentroNotificaciones.MostrarNotificacion(e.Message, TipoNotificacionEnum.Error);
            }
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir("MostrarVistaAutenticacionUsuario", OnMostrarVistaAutenticacionUsuario);
            AgregadorEventos.Desuscribir("EventoAutenticarCuentaUsuario", OnAutenticarCuentaUsuario);
        }
    }
}