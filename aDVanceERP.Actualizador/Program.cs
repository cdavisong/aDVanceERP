using aDVanceERP.Actualizador.Interfaces;
using aDVanceERP.Actualizador.Modelos;
using aDVanceERP.Actualizador.Servicios;
using aDVanceERP.Actualizador.Vistas;

using Microsoft.Win32;

namespace aDVanceERP.Actualizador {
    internal static class Program {
        public static string CurrentVersion = "0.0.0.1";
        static IServicioActualizacion? _updateService;        

        const string PropietarioRepositorio = "cdavisong";
        const string NombreRepositorio = "aDVance-ERP";

        static void ShowUpdateDialog(InfoActualizacion updateInfo) {
            ApplicationConfiguration.Initialize();
            Application.Run(new VistaNotificadorActualizacion(_updateService, updateInfo));
        }

        static void ShowUpdateDialog(string message) {
            ApplicationConfiguration.Initialize();
            Application.Run(new VistaNotificadorActualizacion(message));
        }

        static string CargarDirectorioPrograma() {
            try {
                // Primero intentar leer de la configuración específica de la aplicación
                using (RegistryKey appKey = Registry.CurrentUser.OpenSubKey(@"Software\aDVanceERP")) {
                    if (appKey?.GetValue("InstallPath") is string savedPath &&
                        !string.IsNullOrWhiteSpace(savedPath) &&
                        Directory.Exists(savedPath)) {
                        return savedPath;
                    }
                }

                // Si no existe, buscar en PATH
                string envPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User) ?? "";

                foreach (var p in envPath.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
                    if (p.Contains("aDVanceERP", StringComparison.OrdinalIgnoreCase) &&
                        Directory.Exists(p)) {
                        return p;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error al leer el directorio previo: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }

            return null;
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Cargar directorio previo si existe
            var directorioInstalacion = CargarDirectorioPrograma() ?? "C:\\advanceerp\\programa\\";

            // Carga de la versión
            if (File.Exists($@"{directorioInstalacion}\app.ver"))
                using (var fs = new FileStream($@"{directorioInstalacion}\app.ver", FileMode.Open)) {
                    using (var sr = new StreamReader(fs)) {
                        CurrentVersion = sr.ReadToEnd().Trim();
                    }
                }

            // Inicializacion
            _updateService = new ServicioActualizacionGitHub(PropietarioRepositorio, NombreRepositorio);

            try {
                var updateInfo = _updateService.ComprobarActualizaciones(CurrentVersion, true).Result;

                if (updateInfo.ActualizacionDisponible)
                    ShowUpdateDialog(updateInfo);
                else ShowUpdateDialog("no-actualizaciones");
            } catch (Exception ex) {
                ShowUpdateDialog($"no-conexion|{ex.Message}");
            }
        }
    }
}