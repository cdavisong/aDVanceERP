using System.Security;

namespace aDVanceERP.Core.Modelos.Modulos.Seguridad {
    /// <summary>
    /// Maneja la información del usuario que ha iniciado sesión
    /// </summary>
    public static class SesionUsuario {
        private static long _idUsuario;
        private static string _nombreUsuario = string.Empty;
        private static string? _email;
        private static long _idRol;
        private static string _nombreRol = string.Empty;
        private static bool _esAdministrador;
        private static bool _estaAutenticado;
        private static DateTime _fechaInicioSesion;
        private static GestorPermisos? _gestorPermisos;

        public static long IdUsuario => _idUsuario;
        public static string NombreUsuario => _nombreUsuario;
        public static string? Email => _email;
        public static long IdRol => _idRol;
        public static string NombreRol => _nombreRol;
        public static bool EsAdministrador => _esAdministrador;
        public static bool EstaAutenticado => _estaAutenticado;
        public static DateTime FechaInicioSesion => _fechaInicioSesion;

        /// <summary>
        /// Inicia la sesión del usuario
        /// </summary>
        public static void IniciarSesion(CuentaUsuario usuario, Rol? rol) {
            _idUsuario = usuario.Id;
            _nombreUsuario = usuario.Nombre;
            _email = usuario.Email;
            _idRol = usuario.IdRol;
            _nombreRol = rol?.Nombre ?? "Sin rol";
            _esAdministrador = usuario.Administrador;
            _estaAutenticado = true;
            _fechaInicioSesion = DateTime.Now;
            _gestorPermisos = null; // Se cargará bajo demanda
        }

        /// <summary>
        /// Cierra la sesión actual
        /// </summary>
        public static void CerrarSesion() {
            _idUsuario = 0;
            _nombreUsuario = string.Empty;
            _email = null;
            _idRol = 0;
            _nombreRol = string.Empty;
            _esAdministrador = false;
            _estaAutenticado = false;
            _fechaInicioSesion = DateTime.MinValue;
            _gestorPermisos = null;
        }

        /// <summary>
        /// Establece el gestor de permisos para el usuario actual
        /// </summary>
        public static void EstablecerGestorPermisos(GestorPermisos gestor) {
            _gestorPermisos = gestor;
        }

        /// <summary>
        /// Verifica si el usuario actual tiene permiso para una acción específica
        /// </summary>
        public static bool TienePermiso(string modulo, string accion) {
            if (!_estaAutenticado) return false;
            if (_esAdministrador) return true;
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
    }
}