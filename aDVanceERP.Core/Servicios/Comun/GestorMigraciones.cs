using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun.Interfaces;

using MySql.Data.MySqlClient;
namespace aDVanceERP.Core.Servicios.Comun {
    public class GestorMigraciones {
        // Diccionario ordenado: versión → SQL del script
        private static readonly SortedDictionary<Version, string> _migraciones = new() {
            { new Version(1,1,0), "ALTER TABLE adv__proveedor ADD COLUMN ..." },
            { new Version(1,2,0), "CREATE TABLE adv__orden_compra ..." },
            // cada nueva versión agrega una entrada aquí
        };

        public void AplicarPendientes() {
            var versionActual = ObtenerVersionActual();

            foreach (var (version, sql) in _migraciones) {
                if (version <= versionActual)
                    continue;

                ContextoBaseDatos.EjecutarComandoNoQuery(sql, null);

                RegistrarVersion(version, $"Migración automática a {version}");
            }
        }

        private Version ObtenerVersionActual() {
            const string consulta = """
                SELECT MAX(version) 
                FROM adv__version_esquema;
                """;

            var resultado = ContextoBaseDatos.EjecutarConsulta(consulta, null, MapearVersion).FirstOrDefault();

            return resultado.entidadBase;
        }

        private (Version, List<IEntidadBaseDatos>) MapearVersion(MySqlDataReader reader) {
            var versionStr = reader.GetString(0);

            return (new Version(versionStr), []);
        }

        private void RegistrarVersion(Version v, string desc) { /* INSERT INTO adv__version_esquema */
            const string consulta = """
                INSERT INTO adv__version_esquema (
                    version, 
                    descripcion, 
                    fecha_aplicacion
                ) VALUES (
                    @version, 
                    @descripcion, 
                    NOW()
                );
                """;

            var parametros = new Dictionary<string, object> {
                { "@version", v.ToString() },
                { "@descripcion", desc }
            };

            ContextoBaseDatos.EjecutarComandoNoQuery(consulta, parametros);
        }
    }
}
