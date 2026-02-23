using aDVanceERP.Core.Infraestructura.Globales;

using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace aDVanceERP.Core.Controladores {
    /// <summary>
    /// Gestiona la transferencia de archivos entre el ERP desktop y
    /// el dispositivo Android con aDVancePOS instalado.
    ///
    /// ESTRATEGIA DE RUTAS:
    ///   Push (catálogo → dispositivo):
    ///     1. adb push → /data/local/tmp/catalogo.json   (tmp público)
    ///     2. adb shell run-as → cp tmp → files/          (privado app)
    ///
    ///   Pull (ventas → PC):
    ///     adb exec-out run-as → cat files/ventas_*.json
    ///
    ///   Esto evita dependencia de /sdcard/ y no requiere permisos
    ///   de almacenamiento en el dispositivo.
    /// </summary>
    public class ControladorArchivosAndroid {
        private readonly string _adbPath;
        private readonly string _appPackageName = "cu.davisoft.advancepos";

        // Directorio privado de la app en el dispositivo
        private const string DeviceFilesDir = "files";
        private const string DeviceTmpPath = "/data/local/tmp";

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
        public bool CheckDeviceConnection() {
            try {
                string output = EjecutarAdb("devices");
                bool conectado = output.Contains("device") && !output.Contains("unauthorized");

                if (!conectado) {
                    if (output.Contains("unauthorized"))
                        CentroNotificaciones.MostrarNotificacion(
                            "Dispositivo conectado pero no autorizado. " +
                            "Acepta la solicitud de depuración USB en el teléfono.",
                            Modelos.Comun.TipoNotificacion.Advertencia);
                    else
                        CentroNotificaciones.MostrarNotificacion(
                            "No se detectó ningún dispositivo. " +
                            "Conecta el teléfono y activa Depuración USB.",
                            Modelos.Comun.TipoNotificacion.Advertencia);
                }

                return conectado;
            } catch { return false; }
        }

        /// <summary>
        /// Verifica que el paquete de la app esté instalado en modo Debug
        /// (run-as solo funciona con builds debuggeables).
        /// </summary>
        public bool CheckAppInstalada() {
            try {
                string output = EjecutarAdb($"shell run-as {_appPackageName} ls {DeviceFilesDir}/");
                // Si run-as falla devuelve "unknown package" o "exec failed"
                return !output.Contains("unknown package") && !output.Contains("exec failed");
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  PUSH — Catálogo hacia el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Envía catalogo.json al directorio privado de la app.
        /// Estrategia de dos pasos: tmp público → directorio privado.
        /// </summary>
        public bool PushCatalogo(string localFilePath) {
            if (!File.Exists(localFilePath)) {
                CentroNotificaciones.MostrarNotificacion(
                    $"No se encontró el archivo de catálogo: {localFilePath}",
                    Modelos.Comun.TipoNotificacion.Error);
                return false;
            }

            const string deviceFileName = "catalogo.json";
            string tmpPath = $"{DeviceTmpPath}/{deviceFileName}";

            try {
                // Paso 1: push al directorio tmp (no requiere permisos especiales)
                string pushResult = EjecutarAdb($"push \"{localFilePath}\" \"{tmpPath}\"");
                if (!pushResult.Contains("pushed") && !pushResult.Contains("1 file")) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"Error al copiar catálogo al dispositivo:\n{pushResult}",
                        Modelos.Comun.TipoNotificacion.Error);
                    return false;
                }

                // Paso 2: mover desde tmp al directorio privado de la app
                string cpResult = EjecutarAdb(
                    $"shell run-as {_appPackageName} cp {tmpPath} {DeviceFilesDir}/{deviceFileName}");

                // cp no produce salida en éxito; cualquier salida es error
                if (!string.IsNullOrWhiteSpace(cpResult) && cpResult.Contains("Permission")) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Error de permisos al mover el catálogo. " +
                        "Asegúrate de que la app esté instalada en modo Debug.",
                        Modelos.Comun.TipoNotificacion.Error);
                    return false;
                }

                // Limpiar tmp
                EjecutarAdb($"shell rm {tmpPath}");

                CentroNotificaciones.MostrarNotificacion(
                    $"✓ Catálogo enviado correctamente al dispositivo.",
                    Modelos.Comun.TipoNotificacion.Info);

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
            var resultado = new List<(string, DateTime fecha)>();

            try {
                string output = EjecutarAdb(
                    $"shell run-as {_appPackageName} ls {DeviceFilesDir}/");

                foreach (var linea in output.Split('\n', StringSplitOptions.RemoveEmptyEntries)) {
                    var archivo = linea.Trim();
                    if (!archivo.StartsWith("ventas_") || !archivo.EndsWith(".json"))
                        continue;

                    // Extraer fecha del nombre: ventas_YYYYMMDD.json
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
            var deviceFile = $"ventas_{fechaObj:yyyyMMdd}.json";
            return PullArchivoPrivado(deviceFile, localDestinationPath);
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
                    Modelos.Comun.TipoNotificacion.Info);
                return descargados;
            }

            Directory.CreateDirectory(carpetaDestino);

            foreach (var (fileName, _) in archivos) {
                string destino = Path.Combine(carpetaDestino, fileName);
                if (PullArchivoPrivado(fileName, destino))
                    descargados.Add(destino);
            }

            CentroNotificaciones.MostrarNotificacion(
                $"✓ {descargados.Count} de {archivos.Count} archivos descargados.",
                Modelos.Comun.TipoNotificacion.Info);

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
                string result = EjecutarAdb(
                    $"shell run-as {_appPackageName} rm {DeviceFilesDir}/{deviceFileName}");
                return string.IsNullOrWhiteSpace(result); // rm no produce salida en éxito
            } catch { return false; }
        }

        /// <summary>
        /// Elimina el catálogo del dispositivo para forzar recarga.
        /// </summary>
        public bool EliminarCatalogo()
            => EliminarArchivoVentas("catalogo.json");

        /// <summary>
        /// Verifica si el catálogo ya existe en el dispositivo.
        /// </summary>
        public bool ExisteCatalogo() {
            try {
                string output = EjecutarAdb(
                    $"shell run-as {_appPackageName} ls {DeviceFilesDir}/catalogo.json");
                return !output.Contains("No such file");
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  FLUJO COMPLETO — helpers de alto nivel
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Flujo completo de inicio de día:
        ///   1. Verifica conexión y app instalada
        ///   2. Crea el directorio si no existe
        ///   3. Envía el catálogo
        /// </summary>
        public bool FlujoComienzoDia(string rutaCatalogo) {
            if (!CheckDeviceConnection()) return false;
            if (!CheckAppInstalada()) {
                CentroNotificaciones.MostrarNotificacion(
                    "La app aDVancePOS no está instalada o no es una build Debug. " +
                    "Despliega la app desde Visual Studio en modo Debug.",
                    Modelos.Comun.TipoNotificacion.Error);
                return false;
            }

            AsegurarDirectorio();
            return PushCatalogo(rutaCatalogo);
        }

        /// <summary>
        /// Flujo completo de fin de día:
        ///   1. Verifica conexión
        ///   2. Lista archivos disponibles
        ///   3. Descarga todos
        ///   4. (Opcional) elimina del dispositivo tras descarga exitosa
        /// </summary>
        public List<string> FlujoFinDia(string carpetaDestino, bool eliminarDelDispositivo = false) {
            var descargados = new List<string>();

            if (!CheckDeviceConnection()) return descargados;

            descargados = PullTodasLasVentas(carpetaDestino);

            if (eliminarDelDispositivo && descargados.Count > 0) {
                var archivosEnDispositivo = ListarArchivosVentas();
                foreach (var (fileName, _) in archivosEnDispositivo)
                    EliminarArchivoVentas(fileName);
            }

            return descargados;
        }

        // ══════════════════════════════════════════════════════
        //  PRIVADOS
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Descarga un archivo del directorio privado de la app usando exec-out.
        /// exec-out + run-as cat es la forma más confiable para archivos de texto.
        /// </summary>
        private bool PullArchivoPrivado(string deviceFileName, string localDestinationPath) {
            try {
                // Verificar que existe antes de intentar descargarlo
                string check = EjecutarAdb(
                    $"shell run-as {_appPackageName} ls {DeviceFilesDir}/{deviceFileName}");
                if (check.Contains("No such file")) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"El archivo {deviceFileName} no existe en el dispositivo.",
                        Modelos.Comun.TipoNotificacion.Advertencia);
                    return false;
                }

                // exec-out para capturar bytes exactos (correcto para UTF-8 con BOM)
                byte[] contenido = EjecutarAdbBinario(
                    $"exec-out run-as {_appPackageName} cat {DeviceFilesDir}/{deviceFileName}");

                if (contenido.Length < 10) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"El archivo {deviceFileName} parece vacío o corrupto.",
                        Modelos.Comun.TipoNotificacion.Error);
                    return false;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(localDestinationPath)!);
                File.WriteAllBytes(localDestinationPath, contenido);
                return true;
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al descargar {deviceFileName}: {ex.Message}",
                    Modelos.Comun.TipoNotificacion.Error);
                return false;
            }
        }

        private void AsegurarDirectorio() {
            EjecutarAdb($"shell run-as {_appPackageName} mkdir -p {DeviceFilesDir}");
        }

        /// <summary>Ejecuta un comando ADB y devuelve stdout como string.</summary>
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

            // Combinar stdout y stderr para facilitar diagnóstico
            return string.IsNullOrWhiteSpace(stderr) ? stdout : stdout + stderr;
        }

        /// <summary>
        /// Ejecuta ADB y devuelve la salida como bytes crudos.
        /// Necesario para exec-out (evita corrupción de saltos de línea en Windows).
        /// </summary>
        private byte[] EjecutarAdbBinario(string arguments) {
            var psi = new ProcessStartInfo {
                FileName = _adbPath,
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = Process.Start(psi)!;
            using var ms = new MemoryStream();
            process.StandardOutput.BaseStream.CopyTo(ms);
            process.WaitForExit();
            return ms.ToArray();
        }
    }
}