using aDVanceERP.Core.Infraestructura.Globales;

using System.Diagnostics;
using System.Text;

namespace aDVanceERP.Core.Controladores {
    /// <summary>
    /// Gestiona la transferencia de archivos entre el ERP desktop y
    /// el dispositivo Android con aDVancePOS instalado.
    ///
    /// ESTRATEGIA DE RUTAS (compatible con APK Release firmada):
    ///   La app usa GetExternalFilesDir() → accesible sin root ni run-as:
    ///     /sdcard/Android/data/cu.davisoft.advancepos/files/
    ///
    ///   Push (catálogo → dispositivo) : adb push directo a ruta externa
    ///   Pull (ventas   → PC)          : adb pull directo desde ruta externa
    ///
    ///   No requiere run-as. Funciona con build Debug y Release.
    /// </summary>
    public class ControladorArchivosAndroid {
        private readonly string _adbPath;
        private readonly string _appPackageName = "cu.davisoft.advancepos";

        // Directorio externo privado de la app en el dispositivo.
        // Visible en Windows Explorer y accesible por ADB sin permisos especiales.
        private string DeviceFilesDir =>
            $"/sdcard/Android/data/{_appPackageName}/files";

        public ControladorArchivosAndroid(string applicationPath) {
            _adbPath = Path.Combine(applicationPath, "tools", "adb.exe");

            if (!File.Exists(_adbPath)) {
                CentroNotificaciones.MostrarNotificacion(
                    "No se encontró adb.exe en el directorio tools. " +
                    "Descárgalo desde developer.android.com/tools/releases/platform-tools",
                    Modelos.Comun.TipoNotificacion.Error);
            }
        }

        // ══════════════════════════════════════════════════════
        //  CONEXIÓN
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Verifica que haya exactamente un dispositivo autorizado conectado.
        /// </summary>
        public bool CheckDeviceConnection(bool mostrarAdvertencia = false) {
            try {
                string output = EjecutarAdb("devices");
                bool conectado = output.Contains("\tdevice") && !output.Contains("unauthorized");

                if (!conectado) {
                    if (output.Contains("unauthorized"))
                        CentroNotificaciones.MostrarNotificacion(
                            "Dispositivo conectado pero no autorizado. " +
                            "Acepta la solicitud de depuración USB en el teléfono.",
                            Modelos.Comun.TipoNotificacion.Advertencia);
                    else if (mostrarAdvertencia)
                        CentroNotificaciones.MostrarNotificacion(
                            "No se detectó ningún dispositivo. " +
                            "Conecta el teléfono y activa Depuración USB.",
                            Modelos.Comun.TipoNotificacion.Advertencia);
                }

                return conectado;
            } catch { return false; }
        }

        /// <summary>
        /// Verifica que la app esté instalada comprobando que su directorio
        /// externo exista. Funciona con Debug y Release.
        /// </summary>
        public bool CheckAppInstalada() {
            try {
                string output = EjecutarAdb($"shell ls \"{DeviceFilesDir}\"");
                return !output.Contains("No such file") && !output.Contains("not found");
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  PUSH — Catálogo hacia el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Envía catalogo.json al directorio externo de la app.
        /// Push directo — no necesita run-as ni paso intermedio por /tmp.
        /// </summary>
        public bool PushCatalogo(string localFilePath) {
            if (!File.Exists(localFilePath)) {
                CentroNotificaciones.MostrarNotificacion(
                    $"No se encontró el archivo de catálogo: {localFilePath}",
                    Modelos.Comun.TipoNotificacion.Error);
                return false;
            }

            try {
                AsegurarDirectorio();

                string devicePath = $"{DeviceFilesDir}/catalogo.json";
                string result = EjecutarAdb($"push \"{localFilePath}\" \"{devicePath}\"");

                if (!result.Contains("pushed") && !result.Contains("1 file")) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"Error al copiar catálogo al dispositivo:\n{result}",
                        Modelos.Comun.TipoNotificacion.Error);
                    return false;
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Catálogo enviado correctamente al dispositivo.",
                    Modelos.Comun.TipoNotificacion.Ok);

                return true;
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error inesperado al enviar catálogo: {ex.Message}",
                    Modelos.Comun.TipoNotificacion.Error);
                return false;
            }
        }

        // ══════════════════════════════════════════════════════
        //  PULL — Ventas desde el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Lista los archivos de ventas disponibles en el dispositivo.
        /// Formato esperado: ventas_YYYYMMDD.json
        /// </summary>
        public List<(string fileName, DateTime fecha)> ListarArchivosVentas() {
            var resultado = new List<(string fileName, DateTime fecha)>();

            try {
                string output = EjecutarAdb($"shell ls \"{DeviceFilesDir}\"");

                foreach (var linea in output.Split('\n', StringSplitOptions.RemoveEmptyEntries)) {
                    var archivo = linea.Trim();
                    if (!archivo.StartsWith("ventas_") || !archivo.EndsWith(".json"))
                        continue;

                    string fechaParte = archivo.Replace("ventas_", "").Replace(".json", "");
                    if (DateTime.TryParseExact(fechaParte, "yyyyMMdd",
                        null, System.Globalization.DateTimeStyles.None, out DateTime fecha)) {
                        resultado.Add((archivo, fecha));
                    }
                }
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al listar ventas en dispositivo: {ex.Message}",
                    Modelos.Comun.TipoNotificacion.Error);
            }

            return resultado.OrderByDescending(r => r.fecha).ToList();
        }

        /// <summary>
        /// Descarga el archivo de ventas de la fecha indicada.
        /// Por defecto descarga el de hoy.
        /// </summary>
        public bool PullVentas(string localDestinationPath, DateTime? fecha = null) {
            var fechaObj = fecha ?? DateTime.Today;
            var deviceFileName = $"ventas_{fechaObj:yyyyMMdd}.json";
            return PullArchivo(deviceFileName, localDestinationPath);
        }

        /// <summary>
        /// Descarga TODOS los archivos de ventas disponibles en el dispositivo.
        /// Útil si la app estuvo varios días sin sincronizar.
        /// </summary>
        public List<string> PullTodasLasVentas(string carpetaDestino) {
            var descargados = new List<string>();
            var archivos = ListarArchivosVentas();

            if (archivos.Count == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "No hay archivos de ventas en el dispositivo.",
                    Modelos.Comun.TipoNotificacion.Advertencia);
                return descargados;
            }

            Directory.CreateDirectory(carpetaDestino);

            foreach (var (fileName, _) in archivos) {
                string destino = Path.Combine(carpetaDestino, fileName);
                if (PullArchivo(fileName, destino))
                    descargados.Add(destino);
            }

            CentroNotificaciones.MostrarNotificacion(
                $"{descargados.Count} de {archivos.Count} archivos descargados.",
                Modelos.Comun.TipoNotificacion.Ok);

            return descargados;
        }

        // ══════════════════════════════════════════════════════
        //  GESTIÓN DE ARCHIVOS EN DISPOSITIVO
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Elimina un archivo de ventas del dispositivo tras importarlo.
        /// Útil para limpiar después de la sincronización.
        /// </summary>
        public bool EliminarArchivoVentas(string deviceFileName) {
            try {
                string result = EjecutarAdb($"shell rm \"{DeviceFilesDir}/{deviceFileName}\"");
                return string.IsNullOrWhiteSpace(result);
            } catch { return false; }
        }

        /// <summary>Elimina el catálogo del dispositivo para forzar recarga.</summary>
        public bool EliminarCatalogo()
            => EliminarArchivoVentas("catalogo.json");

        /// <summary>Verifica si el catálogo ya existe en el dispositivo.</summary>
        public bool ExisteCatalogo() {
            try {
                string output = EjecutarAdb($"shell ls \"{DeviceFilesDir}/catalogo.json\"");
                return !output.Contains("No such file");
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  FLUJO COMPLETO — helpers de alto nivel
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Flujo completo de inicio de día:
        ///   1. Verifica conexión
        ///   2. Asegura que el directorio existe en el dispositivo
        ///   3. Envía el catálogo
        /// </summary>
        public bool FlujoComienzoDia(string rutaCatalogo) {
            if (!CheckDeviceConnection()) return false;

            AsegurarDirectorio();
            return PushCatalogo(rutaCatalogo);
        }

        /// <summary>
        /// Flujo completo de fin de día:
        ///   1. Verifica conexión
        ///   2. Descarga todos los archivos de ventas disponibles
        ///   3. (Opcional) elimina los archivos del dispositivo tras descarga exitosa
        /// </summary>
        public List<string> FlujoFinDia(string carpetaDestino, bool eliminarDelDispositivo = false) {
            var descargados = new List<string>();

            if (!CheckDeviceConnection(true)) return descargados;

            descargados = PullTodasLasVentas(carpetaDestino);

            if (eliminarDelDispositivo && descargados.Count > 0) {
                foreach (var (fileName, _) in ListarArchivosVentas())
                    EliminarArchivoVentas(fileName); // Elimina todos los archivos de ventas tras descargar, para limpiar el dispositivo
            }

            EliminarCatalogo(); // Opcional: fuerza recarga de catálogo al día siguiente

            return descargados;
        }

        // ══════════════════════════════════════════════════════
        //  PRIVADOS
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Descarga un archivo del directorio externo de la app con adb pull.
        /// No necesita run-as — la ruta externa es accesible directamente.
        /// </summary>
        private bool PullArchivo(string deviceFileName, string localDestinationPath) {
            try {
                string devicePath = $"{DeviceFilesDir}/{deviceFileName}";

                // Verificar que el archivo existe antes de intentar descargarlo
                string check = EjecutarAdb($"shell ls \"{devicePath}\"");
                if (check.Contains("No such file")) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"El archivo {deviceFileName} no existe en el dispositivo.",
                        Modelos.Comun.TipoNotificacion.Advertencia);
                    return false;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(localDestinationPath)!);

                string result = EjecutarAdb($"pull \"{devicePath}\" \"{localDestinationPath}\"");

                if (!File.Exists(localDestinationPath)) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"Error al descargar {deviceFileName}:\n{result}",
                        Modelos.Comun.TipoNotificacion.Error);
                    return false;
                }

                return true;
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al descargar {deviceFileName}: {ex.Message}",
                    Modelos.Comun.TipoNotificacion.Error);
                return false;
            }
        }

        /// <summary>
        /// Crea el directorio externo de la app si no existe.
        /// En la mayoría de los casos Android lo crea al instalar la app,
        /// pero es buena práctica asegurarlo antes del primer push.
        /// </summary>
        private void AsegurarDirectorio() {
            EjecutarAdb($"shell mkdir -p \"{DeviceFilesDir}\"");
        }

        /// <summary>Ejecuta un comando ADB y devuelve stdout + stderr como string.</summary>
        private string EjecutarAdb(string arguments) {
            var psi = new ProcessStartInfo {
                FileName = _adbPath,
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8
            };

            using var process = Process.Start(psi)!;
            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return string.IsNullOrWhiteSpace(stderr) ? stdout : stdout + stderr;
        }
    }
}