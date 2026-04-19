using aDVanceERP.Core.Eventos;
using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorRegistroUsuario : PresentadorVistaRegistro<IVistaRegistroUsuario, CuentaUsuario, RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
        public PresentadorRegistroUsuario(IVistaRegistroUsuario vista) : base(vista) {
            AgregadorEventos.Suscribir("MostrarVistaRegistroUsuario", OnMostrarVistaRegistroUsuario);
        }

        private void OnMostrarVistaRegistroUsuario(string obj) {
            Vista.ModoEdicion = false;

            Vista.Restaurar();
            Vista.Mostrar();
        }

        public override void PopularVistaDesdeEntidad(CuentaUsuario entidad) {
            // En modo registro, no se deben mostrar datos de la entidad,
            // ya que se está creando una nueva cuenta.
        }

        protected override CuentaUsuario? ObtenerEntidadDesdeVista() {
            // Generar hash de password
            var passwordSeguro = SecureStringHelper.HashPassword(Vista.Password);

            var usuario = new CuentaUsuario {
                Id = Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                IdPersona = Entidad?.IdPersona ?? 0,
                Nombre = Vista.NombreUsuario,
                PasswordHash = passwordSeguro.hash,
                PasswordSalt = passwordSeguro.salt,
                Email = Entidad?.Email,
                IdRol = Entidad?.IdRol ?? 0,
                Administrador = Entidad?.Administrador ?? false,
                Aprobado = Entidad?.Aprobado ?? false,
                Estado = Entidad?.Estado ?? true,
                UltimoAcceso = Entidad?.UltimoAcceso ?? DateTime.Now
            };

            return usuario;
        }

        protected override void RegistroEdicionAuxiliar(RepoCuentaUsuario repositorio, long id) {
            base.RegistroEdicionAuxiliar(repositorio, id);

            // Si es el primer usuario, hacerlo administrador automáticamente
            if (repositorio.Cantidad() == 1 && id > 0) {
                try {
                    repositorio.ConvertirEnAdministrador(id);

                    CentroNotificaciones.MostrarNotificacion(
                        "¡Bienvenido! Su cuenta ha sido creada como Administrador del sistema.",
                        TipoNotificacionEnum.Info);

                    AgregadorEventos.Publicar("MostrarVistaAutenticacionUsuario", string.Empty);
                } catch (Exception ex) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"Error al configurar administrador: {ex.Message}",
                        TipoNotificacionEnum.Error);
                }
            } else if (id > 0) {
                // Para usuarios normales, mostrar mensaje de espera de aprobación
                var usuario = Repositorio.ObtenerPorId(id);

                if (usuario != null && !usuario.Aprobado) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Su solicitud de registro ha sido enviada. Espere la aprobación del administrador.",
                        TipoNotificacionEnum.Info);

                    AgregadorEventos.Publicar("MostrarVistaAprobacionUsuario", AgregadorEventos.SerializarPayload(usuario));
                }
            }
        }

        protected override bool EntidadCorrecta() {
            if (string.IsNullOrEmpty(Vista.NombreUsuario)) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe especificar un nombre de usuario para registrarse en el sistema.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            if (Vista.Password == null || Vista.Password.Length == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "Debe especificar una contraseña para registrarse en el sistema.",
                    TipoNotificacionEnum.Advertencia);
                return false;
            }

            // Validar que el usuario no exista (solo en modo registro)
            if (!Vista.ModoEdicion) {
                var (cantidad, resultados) = Repositorio.Buscar(FiltroBusquedaCuentaUsuario.Nombre, Vista.NombreUsuario);

                if (resultados.Any()) {
                    CentroNotificaciones.MostrarNotificacion(
                        "El nombre de usuario ya está registrado en el sistema.",
                        TipoNotificacionEnum.Advertencia);
                    return false;
                }
            }

            // Verificar si es el primer usuario del sistema
            if (Repositorio.Cantidad() == 0) {
                return true;
            }

            return base.EntidadCorrecta();
        }



        protected override void OnRegistrarEntidad(object? sender, EventArgs e) {
            try {
                base.OnRegistrarEntidad(sender, e);
            } catch (ExcepcionConexionServidorMySQL ex) {
                CentroNotificaciones.MostrarNotificacion(ex.Message, TipoNotificacionEnum.Error);
            }
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir("MostrarVistaRegistroUsuario", OnMostrarVistaRegistroUsuario);

            base.Dispose();
        }
    }
}