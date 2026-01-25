using aDVanceERP.Core.Excepciones;
using aDVanceERP.Core.Infraestructura.Extensiones.BD;
using aDVanceERP.Core.Infraestructura.Globales;

using MySql.Data.MySqlClient;

namespace aDVanceERP.Core.Infraestructura.Helpers.BD {
    public static class TablaHelper {
        public static long ObtenerUltimoId(string nombreEntidad) {
            var comando = $"""
                SELECT MAX(id_{nombreEntidad}) 
            FROM adv__{nombreEntidad}
            ORDER BY id_{nombreEntidad} DESC
            LIMIT 1;
            """;

            return ContextoBaseDatos.EjecutarConsultaEscalar<long>(comando);
        }
    }
}
