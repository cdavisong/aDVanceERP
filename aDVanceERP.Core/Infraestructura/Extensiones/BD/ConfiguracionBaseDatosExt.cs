using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.BD;

namespace aDVanceERP.Core.Infraestructura.Extensiones.BD {
    public static class ConfiguracionBaseDatosExt {
        public static string ToStringConexion(this ConfiguracionBaseDatos conf) {
            if (conf == null) {
                CentroNotificaciones.Mostrar("La configuración del servidor MySQL no puede ser nula.", Modelos.Comun.TipoNotificacion.Advertencia);
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(conf.Servidor)) {
                CentroNotificaciones.Mostrar("El nombre o dirección del servidor no puede estar vacío.", Modelos.Comun.TipoNotificacion.Advertencia);
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(conf.BaseDatos)) {
                CentroNotificaciones.Mostrar("El nombre de la base de datos no puede estar vacío.", Modelos.Comun.TipoNotificacion.Advertencia);
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(conf.Usuario)) {
                CentroNotificaciones.Mostrar("El nombre de usuario no puede estar vacío.", Modelos.Comun.TipoNotificacion.Advertencia);
                return string.Empty;
            }

            // Password can be empty, depending on the server configuration
            return $"""
                Server = {conf.Servidor};
                Database = {conf.BaseDatos};
                User ID = {conf.Usuario};
                Password = {conf.Password};
                Pooling = true;
                MinPoolSize = 10;
                MaxPoolSize = 100;
                Connection Timeout = 30;
                """;
        }
    }
}