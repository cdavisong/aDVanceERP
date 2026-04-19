using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;

using System.Diagnostics;
using System.Text;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Clase base abstracta que encapsula toda la infraestructura ADB
    /// compartida por los controladores de las apps Android de la suite aDVance.
    ///
    /// ESTRATEGIA DE RUTAS (compatible con APK Release firmada):
    ///   La app usa GetExternalFilesDir() → accesible sin root ni run-as:
    ///     /sdcard/Android/data/{AppPackageName}/files/
    ///
    ///   Push (catálogos  → dispositivo) : adb push directo a ruta externa
    ///   Pull (datos      → PC)          : adb pull directo desde ruta externa
    ///
    ///   No requiere run-as. Funciona con build Debug y Release.
    ///
    /// Para añadir soporte a una nueva app Android de la suite:
    ///   1. Crear clase que herede de esta.
    ///   2. Definir AppPackageName.
    ///   3. Implementar las operaciones específicas de la app usando
    ///      PushArchivoUnico, PullArchivo, EjecutarAdb y AsegurarDirectorio.
    /// </summary>
    public abstract class ControladorAdbBase {

        protected readonly string AdbPath;

        /// <summary>Paquete Android de la app destino. Define la ruta de trabajo en el dispositivo.</summary>
        protected abstract string AppPackageName { get; }

        /// <summary>Directorio externo privado de la app en el dispositivo.</summary>
        protected string DeviceFilesDir =>
            $"/sdcard/Android/data/{AppPackageName}/files";

        protected ControladorAdbBase(string applicationPath) {
            AdbPath = Path.Combine(applicationPath, "tools", "adb.exe");

            if (!File.Exists(AdbPath))
                CentroNotificaciones.MostrarNotificacion(
                    "No se encontró adb.exe en el directorio tools. " +
                    "Descárgalo desde developer.android.com/tools/releases/platform-tools",
                    TipoNotificacionEnum.Error);
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
                            TipoNotificacionEnum.Advertencia);
                    else if (mostrarAdvertencia)
                        CentroNotificaciones.MostrarNotificacion(
                            "No se detectó ningún dispositivo. " +
                            "Conecta el teléfono y activa Depuración USB.",
                            TipoNotificacionEnum.Advertencia);
                }

                return conectado;
            } catch { return false; }
        }

        /// <summary>
        /// Verifica que la app esté instalada comprobando que su directorio
        /// externo exista en el dispositivo. Funciona con Debug y Release.
        /// </summary>
        public bool CheckAppInstalada() {
            try {
                string output = EjecutarAdb($"shell ls \"{DeviceFilesDir}\"");
                return !output.Contains("No such file") && !output.Contains("not found");
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  INFORMACIÓN DE ARCHIVOS
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Obtiene el estado de un archivo en el dispositivo: si existe y cuándo
        /// fue modificado por última vez. Una sola llamada ADB con ls -la.
        /// Devuelve (false, null) si el archivo no existe o no se puede leer la fecha.
        /// </summary>
        protected (bool existe, DateTime? fechaModificacion) ObtenerInfoArchivoDispositivo(
            string nombreArchivo) {
            try {
                string output = EjecutarAdb(
                    $"shell ls -la \"{DeviceFilesDir}/{nombreArchivo}\"");

                if (output.Contains("No such file") || output.Contains("not found"))
                    return (false, null);

                // Formato: -rw-rw---- 1 root sdcard_rw SIZE fecha hora nombre
                var partes = output.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length < 7)
                    return (true, null); // existe pero no se pudo leer la fecha

                // partes[5] = "2026-03-08", partes[6] = "14:30"
                if (DateTime.TryParse($"{partes[5]} {partes[6]}",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime fecha)) {
                    return (true, fecha);
                }

                return (true, null);
            } catch {
                return (false, null);
            }
        }

        // ══════════════════════════════════════════════════════
        //  PUSH GENÉRICO
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Envía un archivo local al directorio externo de la app en el dispositivo.
        /// Devuelve true si el push fue exitoso.
        /// Si rutaLocal es null, vacía o el archivo no existe, devuelve false sin error.
        /// </summary>
        protected bool PushArchivoUnico(string rutaLocal, string nombreDestino) {
            if (string.IsNullOrEmpty(rutaLocal) || !File.Exists(rutaLocal))
                return false;

            try {
                string devicePath = $"{DeviceFilesDir}/{nombreDestino}";
                string result = EjecutarAdb($"push \"{rutaLocal}\" \"{devicePath}\"");
                return result.Contains("pushed") || result.Contains("1 file");
            } catch {
                return false;
            }
        }

        // ══════════════════════════════════════════════════════
        //  PULL GENÉRICO
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Descarga un archivo del directorio externo de la app.
        /// Verifica existencia antes de intentar la descarga.
        /// Muestra notificación si el archivo no existe o la descarga falla.
        /// </summary>
        protected bool PullArchivo(string deviceFileName, string localDestinationPath) {
            try {
                string devicePath = $"{DeviceFilesDir}/{deviceFileName}";

                string check = EjecutarAdb($"shell ls \"{devicePath}\"");
                if (check.Contains("No such file")) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"El archivo {deviceFileName} no existe en el dispositivo.",
                        TipoNotificacionEnum.Advertencia);
                    return false;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(localDestinationPath)!);

                string result = EjecutarAdb($"pull \"{devicePath}\" \"{localDestinationPath}\"");

                if (!File.Exists(localDestinationPath)) {
                    CentroNotificaciones.MostrarNotificacion(
                        $"Error al descargar {deviceFileName}:\n{result}",
                        TipoNotificacionEnum.Error);
                    return false;
                }

                return true;
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al descargar {deviceFileName}: {ex.Message}",
                    TipoNotificacionEnum.Error);
                return false;
            }
        }

        // ══════════════════════════════════════════════════════
        //  INFRAESTRUCTURA INTERNA
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Crea el directorio externo de la app si no existe.
        /// En la mayoría de los casos Android lo crea al instalar la app,
        /// pero es buena práctica asegurarlo antes del primer push.
        /// </summary>
        protected void AsegurarDirectorio() {
            EjecutarAdb($"shell mkdir -p \"{DeviceFilesDir}\"");
        }

        /// <summary>
        /// Elimina un archivo del directorio externo de la app en el dispositivo.
        /// Operación silenciosa — no notifica si el archivo no existe.
        /// </summary>
        protected void EliminarArchivoDispositivo(string deviceFileName) {
            EjecutarAdb($"shell rm \"{DeviceFilesDir}/{deviceFileName}\"");
        }

        /// <summary>Ejecuta un comando ADB y devuelve stdout + stderr como string.</summary>
        protected string EjecutarAdb(string arguments) {
            var psi = new ProcessStartInfo {
                FileName = AdbPath,
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
