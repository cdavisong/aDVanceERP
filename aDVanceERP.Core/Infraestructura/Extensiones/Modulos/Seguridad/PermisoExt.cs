namespace aDVanceERP.Core.Infraestructura.Extensiones.Modulos.Seguridad {
    public static class PermisoExt {
        public static bool ContienePermisoParcial(this string[] permisos, string value) {
            return permisos?.Any(p => p.Contains(value, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        public static bool ContienePermisoExacto(this string[] permisos, string value) {
            return permisos?.Any(p => p.Equals(value, StringComparison.OrdinalIgnoreCase)) ?? false;
        }
    }
}