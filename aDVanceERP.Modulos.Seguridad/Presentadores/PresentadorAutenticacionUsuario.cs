using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
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

            AgregadorEventos.Suscribir<EventoMostrarVistaAutenticacionCuentaUsuario>(OnMostrarVistaAutenticacionUsuario);
            AgregadorEventos.Suscribir<EventoAutenticarCuentaUsuario>(OnAutenticarCuentaUsuario);
        }

        private void OnMostrarVistaAutenticacionUsuario(EventoMostrarVistaAutenticacionCuentaUsuario e) {
            Vista.Restaurar();
            Vista.Mostrar();
        }

        private void OnAutenticarCuentaUsuario(EventoAutenticarCuentaUsuario e) {
            if (string.IsNullOrEmpty(Vista.NombreUsuario) || Vista.Password.Length == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe especificar un usuario y contraseña para autenticarse en el sistema. Por favor, rellene los campos correctamente.",
                    TipoNotificacionEnum.Advertencia);

                return;
            }

            try {
                using (var repoUsuario = new RepoCuentaUsuario()) {
                    var (cantidad, resultados) = repoUsuario.Buscar(FiltroBusquedaCuentaUsuario.Nombre, Vista.NombreUsuario);
                    var cuentaUsuario = resultados.FirstOrDefault().entidadBase;

                    if (cuentaUsuario == null) {
                        CentroNotificaciones.MostrarNotificacion(
                            "El usuario especificado no existe en la base de datos o no se ha registrado aún en el sistema, verifique los datos entrados.",
                            TipoNotificacionEnum.Advertencia);

                        return;
                    }

                    // Verificar estado del usuario
                    if (!cuentaUsuario.Estado) {
                        CentroNotificaciones.MostrarNotificacion(
                            "La cuenta de usuario está desactivada. Contacte al administrador del sistema.",
                            TipoNotificacionEnum.Advertencia);
                        return;
                    }

                    if (Vista.Password.VerificarPassword(cuentaUsuario.PasswordHash, cuentaUsuario.PasswordSalt)) {
                        // Obtener rol del usuario
                        var rol = _repoRol.ObtenerPorId(cuentaUsuario.IdRol);

                        if (cuentaUsuario.Aprobado) {
                            // Cargar permisos del usuario
                            var gestorPermisos = _servicioAutenticacion.ObtenerGestorPermisos(cuentaUsuario.Id);

                            // Establecer contexto de seguridad completo
                            ContextoSeguridad.UsuarioAutenticado = cuentaUsuario;
                            ContextoSeguridad.RolUsuario = rol;
                            ContextoSeguridad.GestorPermisos = gestorPermisos;

                            // Iniciar sesión en el sistema estático
                            SesionUsuario.IniciarSesion(cuentaUsuario, rol);
                            SesionUsuario.EstablecerGestorPermisos(gestorPermisos);

                            // Actualizar último acceso
                            RepoCuentaUsuario.Instancia.ActualizarUltimoAcceso(cuentaUsuario.Id);

                            AgregadorEventos.Publicar(new EventoUsuarioAutenticado() {
                                CuentaUsuario = cuentaUsuario
                            });
                        } else {
                            AgregadorEventos.Publicar(new EventoMostrarVistaAprobacionCuentaUsuario() {
                                CuentaUsuario = cuentaUsuario
                            });
                        }
                    } else {
                        CentroNotificaciones.MostrarNotificacion(
                            "La contraseña especificada es incorrecta para el usuario especificado, verifique los datos entrados.",
                            TipoNotificacionEnum.Advertencia);
                    }
                }
            } catch (ExcepcionConexionServidorMySQL ex) {
                CentroNotificaciones.MostrarNotificacion(ex.Message, TipoNotificacionEnum.Error);
            }
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir<EventoMostrarVistaAutenticacionCuentaUsuario>(OnMostrarVistaAutenticacionUsuario);
            AgregadorEventos.Desuscribir<EventoAutenticarCuentaUsuario>(OnAutenticarCuentaUsuario);
        }
    }
}