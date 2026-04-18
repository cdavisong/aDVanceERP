using aDVanceERP.Core.Infraestructura.Extensiones.Comun;
using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Infraestructura.Helpers.Comun;
using aDVanceERP.Core.Modelos.Modulos.Seguridad;
using aDVanceERP.Core.Repositorios.Modulos.Seguridad;

using System.Security;

namespace aDVanceERP.Core.Servicios.Modulos.Seguridad {
    /// <summary>
    /// Servicio para manejar autenticación y autorización
    /// </summary>
    public class ServicioAutenticacion {
        private readonly RepoCuentaUsuario _repoUsuario;
        private readonly RepoRol _repoRol;
        private readonly RepoPermisoRol _repoPermisoRol;

        public ServicioAutenticacion() {
            _repoUsuario = RepoCuentaUsuario.Instancia;
            _repoRol = RepoRol.Instancia;
            _repoPermisoRol = RepoPermisoRol.Instancia;
        }

        #region AUTENTICACION

        public ResultadoLogin Login(string nombreUsuario, SecureString passwordSeguro) {
            try {
                // Buscar usuario por nombre usando el método Buscar del repositorio
                var (cantidad, resultados) = _repoUsuario.Buscar(FiltroBusquedaCuentaUsuario.Nombre, nombreUsuario);
                var usuario = resultados.Select(r => r.entidadBase).FirstOrDefault();

                if (usuario == null) {
                    return ResultadoLogin.Fallo("Usuario no encontrado");
                }

                if (!usuario.Estado) {
                    return ResultadoLogin.Fallo("Usuario inactivo");
                }

                if (!usuario.Aprobado) {
                    return ResultadoLogin.Fallo("Usuario pendiente de aprobación");
                }

                // Verificar password usando tu extensión SecureStringExt
                if (!passwordSeguro.VerificarPassword(usuario.PasswordHash, usuario.PasswordSalt)) {
                    return ResultadoLogin.Fallo("Contraseña incorrecta");
                }

                // Obtener rol del usuario
                var rol = _repoRol.ObtenerPorId(usuario.IdRol);

                // Actualizar último acceso
                ActualizarUltimoAcceso(usuario.Id);

                // Cargar permisos del usuario
                var gestorPermisos = ObtenerGestorPermisos(usuario.Id);

                // Iniciar sesión
                SesionUsuario.IniciarSesion(usuario, rol);
                SesionUsuario.EstablecerGestorPermisos(gestorPermisos);

                EventosSesion.OnUsuarioInicioSesion(usuario);

                return ResultadoLogin.Exito(usuario, rol);
            } catch (Exception ex) {
                return ResultadoLogin.Fallo($"Error en autenticación: {ex.Message}");
            }
        }

        public void Logout() {
            var usuarioActual = ObtenerUsuarioActual();
            SesionUsuario.CerrarSesion();

            if (usuarioActual != null) {
                EventosSesion.OnUsuarioCerroSesion(usuarioActual);
            }
        }

        private void ActualizarUltimoAcceso(long idUsuario) {
            var consulta = $"""
                UPDATE adv__cuenta_usuario 
                SET ultimo_acceso = @ultimo_acceso
                WHERE id_cuenta_usuario = @id;
                """;

            var parametros = new Dictionary<string, object> {
                { "@ultimo_acceso", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@id", idUsuario }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
        }

        #endregion

        #region AUTORIZACION

        public bool TienePermiso(long idUsuario, string modulo, string accion) {
            try {
                // Si es administrador, tiene todos los permisos
                if (EsAdministrador(idUsuario)) {
                    return true;
                }

                // Obtener usuario y su rol
                var usuario = _repoUsuario.ObtenerPorId(idUsuario);
                if (usuario == null) return false;

                // Obtener permiso específico
                var permiso = _repoPermisoRol.ObtenerPermisoRolModulo(usuario.IdRol, modulo);
                if (permiso == null) return false;

                return accion.ToLower() switch {
                    "ver" => permiso.PuedeVer,
                    "crear" => permiso.PuedeCrear,
                    "editar" => permiso.PuedeEditar,
                    "eliminar" => permiso.PuedeEliminar,
                    _ => false
                };
            } catch {
                return false;
            }
        }

        public bool TienePermiso(long idUsuario, ModuloSistemaEnum modulo, AccionModulo accion) {
            return TienePermiso(idUsuario, modulo.ToString().ToLower(), accion.ToString().ToLower());
        }

        public bool TieneAccesoModulo(long idUsuario, string modulo) {
            return TienePermiso(idUsuario, modulo, "ver");
        }

        public bool TieneAccesoModulo(long idUsuario, ModuloSistemaEnum modulo) {
            return TieneAccesoModulo(idUsuario, modulo.ToString().ToLower());
        }

        private bool EsAdministrador(long idUsuario) {
            var usuario = _repoUsuario.ObtenerPorId(idUsuario);
            return usuario?.Administrador ?? false;
        }

        public GestorPermisos ObtenerGestorPermisos(long idUsuario) {
            var gestor = new GestorPermisos();

            if (!EsAdministrador(idUsuario)) {
                var usuario = _repoUsuario.ObtenerPorId(idUsuario);
                if (usuario != null) {
                    var permisos = _repoPermisoRol.ObtenerPermisosPorRol(usuario.IdRol);
                    gestor.CargarPermisos(permisos);
                }
            }

            return gestor;
        }

        #endregion

        #region GESTION USUARIOS

        public void CrearUsuario(CuentaUsuario usuario, SecureString passwordSeguro) {
            // Generar hash y salt usando tu helper SecureStringHelper
            var (hash, salt) = SecureStringHelper.HashPassword(passwordSeguro);

            usuario.PasswordHash = hash;
            usuario.PasswordSalt = salt;
            usuario.UltimoAcceso = DateTime.Now;

            _repoUsuario.Adicionar(usuario);
        }

        public void ActualizarUsuario(CuentaUsuario usuario) {
            _repoUsuario.Editar(usuario);
        }

        public void CambiarPassword(long idUsuario, SecureString nuevoPasswordSeguro) {
            var (hash, salt) = SecureStringHelper.HashPassword(nuevoPasswordSeguro);
            _repoUsuario.CambiarPassword(idUsuario, (hash, salt));
        }

        public void ActivarUsuario(long idUsuario) {
            var usuario = _repoUsuario.ObtenerPorId(idUsuario);
            if (usuario != null) {
                usuario.Estado = true;
                _repoUsuario.Editar(usuario);
            }
        }

        public void DesactivarUsuario(long idUsuario) {
            var usuario = _repoUsuario.ObtenerPorId(idUsuario);
            if (usuario != null) {
                usuario.Estado = false;
                _repoUsuario.Editar(usuario);
            }
        }

        public void AprobarUsuario(long idUsuario) {
            var usuario = _repoUsuario.ObtenerPorId(idUsuario);
            if (usuario != null) {
                usuario.Aprobado = true;
                _repoUsuario.Editar(usuario);
            }
        }

        public List<CuentaUsuarioExtendida> ObtenerUsuariosConRol() {
            var (cantidad, resultados) = _repoUsuario.Buscar(FiltroBusquedaCuentaUsuario.Todos);
            var resultado = new List<CuentaUsuarioExtendida>();

            foreach (var item in resultados) {
                var usuario = item.entidadBase;
                var rol = _repoRol.ObtenerPorId(usuario.IdRol);
                resultado.Add(CuentaUsuarioExtendida.DesdeCuentaUsuario(usuario, rol));
            }

            return resultado;
        }

        private CuentaUsuario? ObtenerUsuarioActual() {
            if (SesionUsuario.EstaAutenticado) {
                return _repoUsuario.ObtenerPorId(SesionUsuario.IdUsuario);
            }
            return null;
        }

        #endregion

        #region GESTION ROLES

        public void CrearRol(Rol rol, List<PermisoRol>? permisos = null) {
            var idRol = _repoRol.Adicionar(rol);
            rol.Id = idRol;

            if (permisos != null && permisos.Any()) {
                _repoPermisoRol.GuardarPermisosRol(idRol, permisos);
            }
        }

        public void ActualizarRol(Rol rol) {
            _repoRol.Editar(rol);
        }

        public void EliminarRol(long idRol) {
            // Verificar que no haya usuarios con este rol
            var (cantidad, resultados) = _repoUsuario.Buscar(FiltroBusquedaCuentaUsuario.Todos);
            var usuariosConRol = resultados.Select(r => r.entidadBase).Count(u => u.IdRol == idRol);

            if (usuariosConRol > 0) {
                throw new InvalidOperationException("No se puede eliminar el rol porque hay usuarios asignados a él");
            }

            _repoRol.Eliminar(idRol);
        }

        public void AsignarRolAUsuario(long idUsuario, long idRol) {
            var usuario = _repoUsuario.ObtenerPorId(idUsuario);
            if (usuario != null) {
                usuario.IdRol = idRol;
                _repoUsuario.Editar(usuario);
            }
        }

        public List<Rol> ObtenerRolesDisponibles() {
            return _repoRol.ObtenerRolesActivos();
        }

        #endregion

        #region SINGLETON

        public static ServicioAutenticacion Instancia { get; } = new ServicioAutenticacion();

        #endregion
    }
}