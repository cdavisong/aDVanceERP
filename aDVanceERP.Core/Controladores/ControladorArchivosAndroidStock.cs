using aDVanceERP.Core.Infraestructura.Globales;

using System.Diagnostics;
using System.Text;

namespace aDVanceERP.Core.Controladores {
    /// <summary>
    /// Gestiona la transferencia de archivos entre el ERP desktop y
    /// el dispositivo Android con aDVanceSTOCK instalado.
    ///
    /// ESTRATEGIA DE RUTAS (compatible con APK Release firmada):
    ///   La app usa GetExternalFilesDir() → accesible sin root ni run-as:
    ///     /sdcard/Android/data/cu.davisoft.advancestock/files/
    ///
    ///   Push (catálogos → dispositivo) : 5 archivos de apoyo antes de iniciar registro
    ///   Pull (stock     → PC)          : archivos stock_YYYYMMDD_HHmmss.json + carpeta imagenes/
    ///
    ///   No requiere run-as. Funciona con build Debug y Release.
    /// </summary>
    public class ControladorArchivosAndroidStock {

        private readonly string _adbPath;
        private readonly string _appPackageName = "cu.davisoft.advancestock";

        private string DeviceFilesDir =>
            $"/sdcard/Android/data/{_appPackageName}/files";

        private string DeviceImagenesDir => $"{DeviceFilesDir}/imagenes";

        public ControladorArchivosAndroidStock(string applicationPath) {
            _adbPath = Path.Combine(applicationPath, "tools", "adb.exe");

            if (!File.Exists(_adbPath)) {
                CentroNotificaciones.MostrarNotificacion(
                    "No se encontró adb.exe en el directorio tools. " +
                    "Descárgalo desde developer.android.com/tools/releases/platform-tools",
                    Modelos.Comun.TipoNotificacionEnum.Error);
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
                            Modelos.Comun.TipoNotificacionEnum.Advertencia);
                    else if (mostrarAdvertencia)
                        CentroNotificaciones.MostrarNotificacion(
                            "No se detectó ningún dispositivo. " +
                            "Conecta el teléfono y activa Depuración USB.",
                            Modelos.Comun.TipoNotificacionEnum.Advertencia);
                }

                return conectado;
            } catch { return false; }
        }

        /// <summary>
        /// Verifica que aDVanceSTOCK esté instalado comprobando que su directorio
        /// externo exista en el dispositivo.
        /// </summary>
        public bool CheckAppInstalada() {
            try {
                string output = EjecutarAdb($"shell ls \"{DeviceFilesDir}\"");
                return !output.Contains("No such file") && !output.Contains("not found");
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  PUSH — Catálogos de apoyo hacia el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Comprueba si el catálogo principal de productos existe en el dispositivo
        /// y cuándo fue modificado por última vez. Una sola llamada ADB.
        /// Devuelve (false, null) si no existe o no se puede leer la fecha.
        /// </summary>
        public (bool existen, DateTime? fechaModificacion) ObtenerInfoCatalogos() {
            try {
                // El catálogo de productos es el obligatorio; usarlo como referencia.
                string output = EjecutarAdb(
                    $"shell ls -la \"{DeviceFilesDir}/catalogo_productos.json\"");

                if (output.Contains("No such file") || output.Contains("not found"))
                    return (false, null);

                // Formato: -rw-rw---- 1 root sdcard_rw 34102 2026-03-09 08:15 catalogo_productos.json
                var partes = output.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length < 7)
                    return (true, null);

                // partes[5] = "2026-03-09", partes[6] = "08:15"
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

        /// <summary>
        /// Envía los 5 catálogos de apoyo al dispositivo de una sola vez.
        /// Llama esto antes de que el almacenero inicie el registro de productos.
        ///
        /// Parámetros: rutas locales de cada archivo. Si una ruta es null o
        /// el archivo no existe, ese catálogo se omite sin abortar los demás.
        /// </summary>
        public ResultadoPushCatalogos PushCatalogos(
            string rutaProductos,
            string rutaProveedores,
            string rutaUnidades,
            string rutaClasificaciones,
            string rutaAlmacenes) {

            if (!CheckDeviceConnection(mostrarAdvertencia: true))
                return new ResultadoPushCatalogos { Exitoso = false };

            AsegurarDirectorio();

            var resultado = new ResultadoPushCatalogos();

            resultado.Productos = PushArchivoUnico(rutaProductos, "catalogo_productos.json");
            resultado.Proveedores = PushArchivoUnico(rutaProveedores, "catalogo_proveedores.json");
            resultado.Unidades = PushArchivoUnico(rutaUnidades, "catalogo_unidades.json");
            resultado.Clasificaciones = PushArchivoUnico(rutaClasificaciones, "catalogo_clasificaciones.json");
            resultado.Almacenes = PushArchivoUnico(rutaAlmacenes, "catalogo_almacenes.json");

            resultado.Exitoso = resultado.Productos; // productos es el único obligatorio

            if (resultado.Exitoso)
                CentroNotificaciones.MostrarNotificacion(
                    $"Catálogos enviados al dispositivo correctamente.\n" +
                    $"Productos: {Si(resultado.Productos)}  " +
                    $"Proveedores: {Si(resultado.Proveedores)}  " +
                    $"Unidades: {Si(resultado.Unidades)}\n" +
                    $"Clasificaciones: {Si(resultado.Clasificaciones)}  " +
                    $"Almacenes: {Si(resultado.Almacenes)}",
                    Modelos.Comun.TipoNotificacionEnum.Ok);
            else
                CentroNotificaciones.MostrarNotificacion(
                    "No se pudo enviar el catálogo de productos (requerido). " +
                    "Verifica que el archivo exista y que el dispositivo esté conectado.",
                    Modelos.Comun.TipoNotificacionEnum.Error);

            return resultado;
        }

        // ══════════════════════════════════════════════════════
        //  PULL — Sesiones de stock desde el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Lista los archivos de sesión de stock en el dispositivo incluyendo
        /// el tamaño aproximado en KB. Reemplaza al ListarArchivosStock() anterior.
        /// Formato de nombre esperado: stock_YYYYMMDD_HHmmss.json
        /// </summary>
        public List<(string fileName, DateTime fechaHora, double tamanoKb)> ListarArchivosStock() {
            var resultado = new List<(string fileName, DateTime fechaHora, double tamanoKb)>();

            try {
                // ls -la incluye el tamaño en la misma pasada — sin llamadas ADB extra.
                string output = EjecutarAdb($"shell ls -la \"{DeviceFilesDir}\"");

                foreach (var linea in output.Split('\n', StringSplitOptions.RemoveEmptyEntries)) {
                    var lineaTrim = linea.Trim();
                    if (!lineaTrim.Contains("stock_") || !lineaTrim.EndsWith(".json"))
                        continue;

                    // Columnas: permisos links owner group SIZE fecha hora nombre
                    var partes = lineaTrim.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length < 8)
                        continue;

                    var archivo = partes[^1];
                    if (!archivo.StartsWith("stock_") || !archivo.EndsWith(".json"))
                        continue;

                    string fechaParte = archivo.Replace("stock_", "").Replace(".json", "");
                    if (!DateTime.TryParseExact(fechaParte, "yyyyMMdd_HHmmss",
                        null, System.Globalization.DateTimeStyles.None, out DateTime fechaHora))
                        continue;

                    double tamanoKb = 0;
                    if (long.TryParse(partes[4], out long bytes))
                        tamanoKb = Math.Round(bytes / 1024.0, 1);

                    resultado.Add((archivo, fechaHora, tamanoKb));
                }
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al listar archivos de stock: {ex.Message}",
                    Modelos.Comun.TipoNotificacionEnum.Error);
            }

            return resultado.OrderByDescending(r => r.fechaHora).ToList();
        }

        /// <summary>
        /// Descarga un único archivo de sesión del dispositivo.
        /// Útil cuando el usuario importa una sesión individual desde la tabla.
        /// </summary>
        public ResultadoPullStock PullSesionIndividual(string deviceFileName, string localDestPath) {
            var resultado = new ResultadoPullStock();

            if (!CheckDeviceConnection(mostrarAdvertencia: true))
                return resultado;

            Directory.CreateDirectory(Path.GetDirectoryName(localDestPath)!);

            if (PullArchivo(deviceFileName, localDestPath)) {
                resultado.JsonDescargados.Add(localDestPath);
                resultado.Exitoso = true;

                // Eliminar del dispositivo tras descargar
                LimpiarSesionesImportadas(new[] { deviceFileName });
            }

            return resultado;
        }

        /// <summary>
        /// Descarga todos los archivos de stock pendientes en el dispositivo
        /// y la carpeta de imágenes completa.
        ///
        /// Devuelve la lista de rutas locales de los JSON descargados.
        /// </summary>
        public ResultadoPullStock PullSesiones(string carpetaDestino) {
            var resultado = new ResultadoPullStock();

            if (!CheckDeviceConnection(mostrarAdvertencia: true))
                return resultado;

            Directory.CreateDirectory(carpetaDestino);

            // 1. Descargar archivos JSON de sesiones
            var archivos = ListarArchivosStock();

            if (archivos.Count == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "No hay sesiones de stock pendientes en el dispositivo.",
                    Modelos.Comun.TipoNotificacionEnum.Advertencia);
                return resultado;
            }

            foreach (var (fileName, _, _) in archivos) {
                string destino = Path.Combine(carpetaDestino, fileName);
                if (PullArchivo(fileName, destino))
                    resultado.JsonDescargados.Add(destino);
            }

            // 2. Descargar carpeta de imágenes
            resultado.ImagenesDescargadas = PullImagenes(carpetaDestino);

            resultado.Exitoso = resultado.JsonDescargados.Count > 0;

            CentroNotificaciones.MostrarNotificacion(
                $"Importación completada:\n" +
                $"  {resultado.JsonDescargados.Count}/{archivos.Count} sesiones descargadas\n" +
                $"  {resultado.ImagenesDescargadas} imagen(es) descargada(s)",
                resultado.Exitoso
                    ? Modelos.Comun.TipoNotificacionEnum.Ok
                    : Modelos.Comun.TipoNotificacionEnum.Advertencia);

            return resultado;
        }

        /// <summary>
        /// Elimina los archivos de stock del dispositivo tras importarlos exitosamente.
        /// Llama esto después de procesar los JSON en el ERP.
        /// </summary>
        public void LimpiarSesionesImportadas(IEnumerable<string> nombresArchivos) {
            foreach (var nombre in nombresArchivos)
                EjecutarAdb($"shell rm \"{DeviceFilesDir}/{nombre}\"");
        }

        /// <summary>
        /// Elimina todos los catálogos del dispositivo.
        /// Útil para forzar recarga en la próxima sesión.
        /// </summary>
        public void LimpiarCatalogos() {
            var catalogos = new[] {
                "catalogo_productos.json",
                "catalogo_proveedores.json",
                "catalogo_unidades.json",
                "catalogo_clasificaciones.json",
                "catalogo_almacenes.json"
            };
            foreach (var c in catalogos)
                EjecutarAdb($"shell rm \"{DeviceFilesDir}/{c}\"");
        }

        // ══════════════════════════════════════════════════════
        //  FLUJOS DE ALTO NIVEL
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Flujo completo de preparación:
        ///   1. Verifica conexión y app instalada
        ///   2. Asegura directorio en el dispositivo
        ///   3. Envía los 5 catálogos
        /// Llama esto antes de entregarle el teléfono al almacenero.
        /// </summary>
        public ResultadoPushCatalogos FlujoPreparacion(
            string rutaProductos,
            string rutaProveedores,
            string rutaUnidades,
            string rutaClasificaciones,
            string rutaAlmacenes) {

            if (!CheckDeviceConnection(true))
                return new ResultadoPushCatalogos { Exitoso = false };

            if (!CheckAppInstalada()) {
                CentroNotificaciones.MostrarNotificacion(
                    "aDVance Stock no está instalado en el dispositivo. " +
                    "Instala el APK primero.",
                    Modelos.Comun.TipoNotificacionEnum.Error);
                return new ResultadoPushCatalogos { Exitoso = false };
            }

            return PushCatalogos(
                rutaProductos, rutaProveedores,
                rutaUnidades, rutaClasificaciones, rutaAlmacenes);
        }

        /// <summary>
        /// Flujo completo de importación:
        ///   1. Verifica conexión
        ///   2. Descarga sesiones JSON e imágenes
        ///   3. (Opcional) limpia el dispositivo tras descarga exitosa
        /// Llama esto cuando el almacenero devuelve el teléfono.
        /// </summary>
        public ResultadoPullStock FlujoImportacion(
            string carpetaDestino,
            bool limpiarDispositivoTrasImportar = false) {

            if (!CheckDeviceConnection(true))
                return new ResultadoPullStock();

            var resultado = PullSesiones(carpetaDestino);

            if (limpiarDispositivoTrasImportar && resultado.Exitoso) {
                var nombres = resultado.JsonDescargados
                    .Select(Path.GetFileName)
                    .Where(n => n != null)
                    .Select(n => n!);
                LimpiarSesionesImportadas(nombres);
            }

            return resultado;
        }

        // ══════════════════════════════════════════════════════
        //  PRIVADOS
        // ══════════════════════════════════════════════════════

        private bool PushArchivoUnico(string rutaLocal, string nombreDestino) {
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

        private bool PullArchivo(string deviceFileName, string localDestinationPath) {
            try {
                string devicePath = $"{DeviceFilesDir}/{deviceFileName}";

                string check = EjecutarAdb($"shell ls \"{devicePath}\"");
                if (check.Contains("No such file")) return false;

                Directory.CreateDirectory(Path.GetDirectoryName(localDestinationPath)!);

                string result = EjecutarAdb($"pull \"{devicePath}\" \"{localDestinationPath}\"");
                return File.Exists(localDestinationPath);
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Descarga toda la carpeta /imagenes/ del dispositivo.
        /// Devuelve el número de imágenes descargadas.
        /// </summary>
        private int PullImagenes(string carpetaDestino) {
            try {
                // Verificar que la carpeta de imágenes existe
                string check = EjecutarAdb($"shell ls \"{DeviceImagenesDir}\"");
                if (check.Contains("No such file") || check.Contains("not found"))
                    return 0;

                // Listar archivos de imagen
                var archivos = check
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim())
                    .Where(l => l.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (archivos.Count == 0) return 0;

                string carpetaImagenesLocal = Path.Combine(carpetaDestino, "imagenes");
                Directory.CreateDirectory(carpetaImagenesLocal);

                int descargadas = 0;
                foreach (var archivo in archivos) {
                    string devicePath = $"{DeviceImagenesDir}/{archivo}";
                    string localPath = Path.Combine(carpetaImagenesLocal, archivo);
                    string result = EjecutarAdb($"pull \"{devicePath}\" \"{localPath}\"");
                    if (File.Exists(localPath)) descargadas++;
                }

                return descargadas;
            } catch {
                return 0;
            }
        }

        private void AsegurarDirectorio() {
            EjecutarAdb($"shell mkdir -p \"{DeviceFilesDir}\"");
        }

        private static string Si(bool valor) => valor ? "✓" : "✗";

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

    // ══════════════════════════════════════════════════════════════
    //  DTOs de resultado
    // ══════════════════════════════════════════════════════════════

    /// <summary>Resultado del push de catálogos al dispositivo.</summary>
    public class ResultadoPushCatalogos {
        public bool Exitoso { get; set; }
        public bool Productos { get; set; }
        public bool Proveedores { get; set; }
        public bool Unidades { get; set; }
        public bool Clasificaciones { get; set; }
        public bool Almacenes { get; set; }
    }

    /// <summary>Resultado del pull de sesiones de stock desde el dispositivo.</summary>
    public class ResultadoPullStock {
        public bool Exitoso { get; set; }
        public List<string> JsonDescargados { get; set; } = new();
        public int ImagenesDescargadas { get; set; }
    }
}