using aDVanceERP.Core.Modelos.BD;
using aDVanceERP.Core.Repositorios.Comun.Interfaces;

namespace aDVanceERP.Core.Repositorios.BD
{
    public class RepoConfiguracionBaseDatos : IRepoConfiguracionBaseDatos<ConfiguracionBaseDatos> {
        private const string NombreArchivo = "confServidorMySQL.json";

        private List<ConfiguracionBaseDatos> _configuraciones;
        private readonly string _directorioRaiz;

        public RepoConfiguracionBaseDatos() {
            _configuraciones = new List<ConfiguracionBaseDatos>();
            _directorioRaiz = ".\\settings";
        }

        public RepoConfiguracionBaseDatos(string directorioTrabajo) {
            _configuraciones = new List<ConfiguracionBaseDatos>();
            _directorioRaiz = $"{directorioTrabajo}settings";
        }

        public ConfiguracionBaseDatos? ObtenerPorId(object id) {
            var rutaArchivo = Path.Combine(_directorioRaiz, (long.TryParse(id.ToString(), out long result) ? result : 0m) <= 0 ? NombreArchivo : id.ToString());

            if (File.Exists(rutaArchivo)) {
                var contenido = File.ReadAllText(rutaArchivo);

                _configuraciones = System.Text.Json.JsonSerializer.Deserialize<List<ConfiguracionBaseDatos>>(contenido) ?? new List<ConfiguracionBaseDatos>();
            } else {
                // Si el archivo no existe, retornar una configuración por defecto
                _configuraciones = new List<ConfiguracionBaseDatos> {
                    new ConfiguracionBaseDatos {
                        Servidor = "localhost",
                        BaseDatos = "advanceerp",
                        Usuario = "admin",
                        Password = "admin",
                        RecordarConfiguracion = false
                    }
                };

                Salvar(Path.GetDirectoryName(rutaArchivo), _configuraciones[0]);
            }

            return _configuraciones.FirstOrDefault() ?? new ConfiguracionBaseDatos();
        }

        public List<ConfiguracionBaseDatos> ObtenerTodos() {
            var rutaArchivo = Path.Combine(_directorioRaiz, NombreArchivo);

            if (File.Exists(rutaArchivo)) {
                var contenido = File.ReadAllText(rutaArchivo);
                _configuraciones = System.Text.Json.JsonSerializer.Deserialize<List<ConfiguracionBaseDatos>>(contenido) ?? new List<ConfiguracionBaseDatos>();
            } else {
                // Si el archivo no existe, retornar una lista vacía
                _configuraciones = new List<ConfiguracionBaseDatos>();
            }

            return _configuraciones;
        }

        public void Salvar(string directorio, ConfiguracionBaseDatos entidad) {
            if (string.IsNullOrWhiteSpace(directorio)) {
                directorio = _directorioRaiz;
            }
            if (entidad == null) {
                throw new ArgumentNullException(nameof(entidad), "La entidad no puede ser nula.");
            }
            if (!Directory.Exists(directorio)) {
                Directory.CreateDirectory(directorio);
            }

            var rutaArchivo = Path.Combine(_directorioRaiz, NombreArchivo);

            _configuraciones.Clear();
            _configuraciones.Add(entidad);

            var contenido = System.Text.Json.JsonSerializer.Serialize(_configuraciones, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

            using (var stream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None)) {
                using (var writer = new StreamWriter(stream)) {
                    writer.Write(contenido);
                }
            }
        }

        public void Dispose() {
            //...
        }
    }
}