using aDVanceERP.Core.Modelos.Modulos.Seguridad;

namespace aDVanceERP.Core.Infraestructura.Globales {
    /// <summary>
    /// Contexto de seguridad global de la aplicación
    /// </summary>
    public static class ContextoSeguridad {
        private static CuentaUsuario? _usuarioAutenticado;
        private static Rol? _rolUsuario;
        private static GestorPermisos? _gestorPermisos;

        /// <summary>
        /// Usuario actualmente autenticado en el sistema
        /// </summary>
        public static CuentaUsuario? UsuarioAutenticado {
            get => _usuarioAutenticado;
            set {
                _usuarioAutenticado = value;
                if (value == null) {
                    _rolUsuario = null;
                    _gestorPermisos = null;
                }
            }
        }

        /// <summary>
        /// Rol del usuario autenticado
        /// </summary>
        public static Rol? RolUsuario {
            get => _rolUsuario;
            set => _rolUsuario = value;
        }

        /// <summary>
        /// Gestor de permisos del usuario autenticado
        /// </summary>
        public static GestorPermisos? GestorPermisos {
            get => _gestorPermisos;
            set => _gestorPermisos = value;
        }

        /// <summary>
        /// Indica si hay un usuario autenticado
        /// </summary>
        public static bool EstaAutenticado => _usuarioAutenticado != null;

        /// <summary>
        /// Indica si el usuario autenticado es administrador
        /// </summary>
        public static bool EsAdministrador => _usuarioAutenticado?.Administrador ?? false;

        /// <summary>
        /// Verifica si el usuario actual tiene un permiso específico
        /// </summary>
        public static bool TienePermiso(string modulo, string accion) {
            if (!EstaAutenticado) return false;
            if (EsAdministrador) return true;
            if (_gestorPermisos == null) return false;

            return accion.ToLower() switch {
                "ver" => _gestorPermisos.TienePermiso(modulo, AccionModuloEnum.Ver),
                "crear" => _gestorPermisos.TienePermiso(modulo, AccionModuloEnum.Crear),
                "editar" => _gestorPermisos.TienePermiso(modulo, AccionModuloEnum.Editar),
                "eliminar" => _gestorPermisos.TienePermiso(modulo, AccionModuloEnum.Eliminar),
                _ => false
            };
        }

        /// <summary>
        /// Verifica si el usuario actual tiene permiso para un módulo y acción
        /// </summary>
        public static bool TienePermiso(ModuloSistemaEnum modulo, AccionModuloEnum accion) {
            return TienePermiso(modulo.ToString().ToLower(), accion.ToString().ToLower());
        }

        /// <summary>
        /// Verifica si el usuario actual tiene acceso a un módulo
        /// </summary>
        public static bool TieneAccesoModulo(string modulo) {
            return TienePermiso(modulo, "ver");
        }

        /// <summary>
        /// Verifica si el usuario actual tiene acceso a un módulo
        /// </summary>
        public static bool TieneAccesoModulo(ModuloSistemaEnum modulo) {
            return TieneAccesoModulo(modulo.ToString().ToLower());
        }

        /// <summary>
        /// Cierra la sesión actual
        /// </summary>
        public static void CerrarSesion() {
            _usuarioAutenticado = null;
            _rolUsuario = null;
            _gestorPermisos = null;

            SesionUsuario.CerrarSesion();
        }
    }
}