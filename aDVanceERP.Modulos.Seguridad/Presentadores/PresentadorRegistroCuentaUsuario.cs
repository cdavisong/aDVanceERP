using aDVanceERP.Core.Eventos.Comun;
using aDVanceERP.Core.Eventos.Modulos.Seguridad;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Comun;
using aDVanceERP.Core.Modelos.Modulos.Maestros;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Presentadores.Comun;
using aDVanceERP.Core.Repositorios.Modulos.Maestros;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;
using aDVanceERP.Modulos.Seguridad.Interfaces;

using System.Security;

namespace aDVanceERP.Modulos.Seguridad.Presentadores {
    public class PresentadorRegistroCuentaUsuario : PresentadorVistaRegistro<IVistaRegistroCuentaUsuario, CuentaUsuario, RepoCuentaUsuario, FiltroBusquedaCuentaUsuario> {
        public PresentadorRegistroCuentaUsuario(IVistaRegistroCuentaUsuario vista) : base(vista) {
            AgregadorEventos.Suscribir<EventoMostrarVistaRegistroCuentaUsuario>(OnMostrarVistaRegistroCuentaUsuario);
            AgregadorEventos.Suscribir<EventoMostrarVistaEdicionCuentaUsuario>(OnMostrarVistaEdicionCuentaUsuario);
        }

        private void OnMostrarVistaRegistroCuentaUsuario(EventoMostrarVistaRegistroCuentaUsuario e) {
            Vista.ModoEdicion = false;
            Vista.Restaurar();

            CargarDatosComunes();

            Vista.Mostrar();
        }

        private void OnMostrarVistaEdicionCuentaUsuario(EventoMostrarVistaEdicionCuentaUsuario e) {
            Vista.ModoEdicion = true;
            Vista.Restaurar();

            CargarDatosComunes();
            PopularVistaDesdeEntidad(e.CuentaUsuario);

            Vista.Mostrar();
        }

        private void CargarDatosComunes() {
            Vista.CargarRoles([.. RepoRol.Instancia.ObtenerTodos().Select(r => r.entidadBase)]);
        }

        public override void PopularVistaDesdeEntidad(CuentaUsuario entidad) {
            base.PopularVistaDesdeEntidad(entidad);

            var rol = RepoRol.Instancia.ObtenerPorId(entidad.Id);
            var persona = RepoPersona.Instancia.ObtenerPorId(entidad.IdPersona);
            var correoContacto = RepoCorreoContacto.Instancia
                .Buscar(FiltroBusquedaCorreoContacto.DireccionCorreo, entidad.Email)
                .resultadosBusqueda
                .FirstOrDefault()
                .entidadBase;

            Vista.NombreUsuario = entidad.Nombre;
            Vista.RolUsuario = rol;
            
            Vista.NombreCompleto = persona?.NombreCompleto ?? string.Empty;
            Vista.TipoDocumento = persona?.TipoDocumento ?? default;
            Vista.NumeroDocumento = persona?.NumeroDocumento ?? string.Empty;
            Vista.DireccionPrincipal = persona?.DireccionPrincipal ?? string.Empty;
            Vista.CorreoContacto = correoContacto;
        }

        protected override CuentaUsuario? ObtenerEntidadDesdeVista() {
            var passwordSeguro = SecureStringHelper.HashPassword(Vista.Password);

            return new CuentaUsuario() { 
                Id = Vista.ModoEdicion && Entidad != null ? Entidad.Id : 0,
                IdPersona = 0, // IdPersona se asignará posteriormente
                Nombre = Vista.NombreUsuario,
                PasswordHash = passwordSeguro.hash,
                PasswordSalt = passwordSeguro.salt,
                Email = Vista.CorreoContacto?.DireccionCorreo ?? string.Empty,
                IdRol = Vista.RolUsuario?.Id ?? 0,
                Administrador = Entidad?.Administrador ?? false,
                Aprobado = Entidad?.Aprobado ?? false,
                Estado = Entidad?.Estado ?? true,
                UltimoAcceso = Entidad?.UltimoAcceso ?? DateTime.Now
            };
        }

        protected override async void RegistroEdicionAuxiliar(RepoCuentaUsuario repositorio, long id) {
            if (!Vista.ModoEdicion) {
                var persona = new Persona() {
                    Id = 0,
                    NombreCompleto = string.IsNullOrEmpty(Vista.NombreCompleto) 
                        ? Vista.NombreUsuario 
                        : Vista.NombreCompleto,
                    TipoDocumento = Vista.TipoDocumento,
                    NumeroDocumento = string.IsNullOrEmpty(Vista.NumeroDocumento) 
                        ? "N/A" 
                        : Vista.NumeroDocumento,
                    DireccionPrincipal = Vista.DireccionPrincipal,
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                AgregadorEventos.Publicar(new EventoCuentaUsuarioRegistrada() {
                    CuentaUsuario = Entidad!,
                    Persona = persona,
                    CorreoContacto = Vista.CorreoContacto!
                });
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

            return base.EntidadCorrecta();
        }

        public override void Dispose() {
            AgregadorEventos.Desuscribir<EventoMostrarVistaRegistroCuentaUsuario>(OnMostrarVistaRegistroCuentaUsuario);
            AgregadorEventos.Desuscribir<EventoMostrarVistaEdicionCuentaUsuario>(OnMostrarVistaEdicionCuentaUsuario);

            base.Dispose();
        }
    }
}