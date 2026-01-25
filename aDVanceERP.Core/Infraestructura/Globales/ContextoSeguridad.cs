using aDVanceERP.Core.Modelos.Modulos.Seguridad;

namespace aDVanceERP.Core.Infraestructura.Globales {
    public static class ContextoSeguridad {
        public static CuentaUsuario? UsuarioAutenticado { get; set; }
        public static string[]? PermisosUsuario { get; set; }
    }
}
