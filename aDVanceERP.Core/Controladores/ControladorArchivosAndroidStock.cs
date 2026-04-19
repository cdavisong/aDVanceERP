using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Gestiona la transferencia de archivos entre el ERP desktop y
    /// el dispositivo Android con aDVanceSTOCK instalado.
    ///
    /// Hereda la infraestructura ADB de <see cref="ControladorAdbBase"/>.
    /// Esta clase contiene únicamente la lógica específica de Stock:
    ///   Push (catálogos → dispositivo) : 5 archivos de apoyo antes de iniciar registro
    ///   Pull (sesiones  → PC)          : archivos stock_YYYYMMDD_HHmmss.json + carpeta imagenes/
    /// </summary>
    public class ControladorArchivosAndroidStock : ControladorAdbBase {

        protected override string AppPackageName => "cu.davisoft.advancestock";

        private string DeviceImagenesDir => $"{DeviceFilesDir}/imagenes";

        public ControladorArchivosAndroidStock(string applicationPath)
            : base(applicationPath) { }

        // ══════════════════════════════════════════════════════
        //  INFORMACIÓN DE CATÁLOGOS
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Comprueba si el catálogo principal de productos existe en el dispositivo
        /// y cuándo fue modificado por última vez.
        /// Devuelve (false, null) si no existe o no se puede leer la fecha.
        /// </summary>
        public (bool existen, DateTime? fechaModificacion) ObtenerInfoCatalogos()
            => ObtenerInfoArchivoDispositivo("catalogo_productos.json");

        // ══════════════════════════════════════════════════════
        //  PUSH — Catálogos hacia el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Envía los 5 catálogos de apoyo al dispositivo de una sola vez.
        /// El catálogo de productos es el único obligatorio; los demás se envían
        /// si existen y no bloquean la operación si fallan.
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

            var resultado = new ResultadoPushCatalogos {
                Productos       = PushArchivoUnico(rutaProductos,       "catalogo_productos.json"),
                Proveedores     = PushArchivoUnico(rutaProveedores,     "catalogo_proveedores.json"),
                Unidades        = PushArchivoUnico(rutaUnidades,        "catalogo_unidades.json"),
                Clasificaciones = PushArchivoUnico(rutaClasificaciones, "catalogo_clasificaciones.json"),
                Almacenes       = PushArchivoUnico(rutaAlmacenes,       "catalogo_almacenes.json")
            };

            resultado.Exitoso = resultado.Productos; // productos es el único obligatorio

            if (resultado.Exitoso)
                CentroNotificaciones.MostrarNotificacion(
                    $"Catálogos enviados al dispositivo correctamente.\n" +
                    $"Productos: {Si(resultado.Productos)}  " +
                    $"Proveedores: {Si(resultado.Proveedores)}  " +
                    $"Unidades: {Si(resultado.Unidades)}\n" +
                    $"Clasificaciones: {Si(resultado.Clasificaciones)}  " +
                    $"Almacenes: {Si(resultado.Almacenes)}",
                    TipoNotificacionEnum.Ok);
            else
                CentroNotificaciones.MostrarNotificacion(
                    "No se pudo enviar el catálogo de productos (requerido). " +
                    "Verifica que el archivo exista y que el dispositivo esté conectado.",
                    TipoNotificacionEnum.Error);

            return resultado;
        }

        // ══════════════════════════════════════════════════════
        //  PULL — Sesiones de stock desde el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Lista los archivos de sesión de stock en el dispositivo.
        /// Formato de nombre esperado: stock_YYYYMMDD_HHmmss.json
        /// </summary>
        public List<(string fileName, DateTime fechaHora, double tamanoKb)> ListarArchivosStock() {
            var resultado = new List<(string fileName, DateTime fechaHora, double tamanoKb)>();

            try {
                string output = EjecutarAdb($"shell ls -la \"{DeviceFilesDir}\"");

                foreach (var linea in output.Split('\n', StringSplitOptions.RemoveEmptyEntries)) {
                    var lineaTrim = linea.Trim();
                    if (!lineaTrim.Contains("stock_") || !lineaTrim.EndsWith(".json"))
                        continue;

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
                    TipoNotificacionEnum.Error);
            }

            return resultado.OrderByDescending(r => r.fechaHora).ToList();
        }

        /// <summary>
        /// Descarga un único archivo de sesión del dispositivo.
        /// Útil cuando el usuario importa una sesión individual desde la tabla.
        /// Elimina el archivo del dispositivo tras la descarga exitosa.
        /// </summary>
        public ResultadoPullStock PullSesionIndividual(string deviceFileName, string localDestPath) {
            var resultado = new ResultadoPullStock();

            if (!CheckDeviceConnection(mostrarAdvertencia: true))
                return resultado;

            Directory.CreateDirectory(Path.GetDirectoryName(localDestPath)!);

            if (PullArchivo(deviceFileName, localDestPath)) {
                resultado.JsonDescargados.Add(localDestPath);
                resultado.Exitoso = true;

                LimpiarSesionesImportadas(new[] { deviceFileName });
            }

            return resultado;
        }

        /// <summary>
        /// Descarga todos los archivos de stock pendientes en el dispositivo
        /// y la carpeta de imágenes completa.
        /// </summary>
        public ResultadoPullStock PullSesiones(string carpetaDestino) {
            var resultado = new ResultadoPullStock();

            if (!CheckDeviceConnection(mostrarAdvertencia: true))
                return resultado;

            Directory.CreateDirectory(carpetaDestino);

            var archivos = ListarArchivosStock();

            if (archivos.Count == 0) {
                CentroNotificaciones.MostrarNotificacion(
                    "No hay sesiones de stock pendientes en el dispositivo.",
                    TipoNotificacionEnum.Advertencia);
                return resultado;
            }

            foreach (var (fileName, _, _) in archivos) {
                string destino = Path.Combine(carpetaDestino, fileName);
                if (PullArchivo(fileName, destino))
                    resultado.JsonDescargados.Add(destino);
            }

            resultado.ImagenesDescargadas = PullImagenes(carpetaDestino);
            resultado.Exitoso = resultado.JsonDescargados.Count > 0;

            CentroNotificaciones.MostrarNotificacion(
                $"Importación completada:\n" +
                $"  {resultado.JsonDescargados.Count}/{archivos.Count} sesiones descargadas\n" +
                $"  {resultado.ImagenesDescargadas} imagen(es) descargada(s)",
                resultado.Exitoso
                    ? TipoNotificacionEnum.Ok
                    : TipoNotificacionEnum.Advertencia);

            return resultado;
        }

        /// <summary>
        /// Elimina los archivos de stock del dispositivo tras importarlos.
        /// Llama esto después de procesar los JSON en el ERP.
        /// </summary>
        public void LimpiarSesionesImportadas(IEnumerable<string> nombresArchivos) {
            foreach (var nombre in nombresArchivos)
                EliminarArchivoDispositivo(nombre);
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
                EliminarArchivoDispositivo(c);
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
                    TipoNotificacionEnum.Error);
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

        /// <summary>Descarga toda la carpeta /imagenes/ del dispositivo.</summary>
        private int PullImagenes(string carpetaDestino) {
            try {
                string check = EjecutarAdb($"shell ls \"{DeviceImagenesDir}\"");
                if (check.Contains("No such file") || check.Contains("not found"))
                    return 0;

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
                    string localPath  = Path.Combine(carpetaImagenesLocal, archivo);
                    EjecutarAdb($"pull \"{devicePath}\" \"{localPath}\"");
                    if (File.Exists(localPath)) descargadas++;
                }

                return descargadas;
            } catch {
                return 0;
            }
        }

        private static string Si(bool valor) => valor ? "✓" : "✗";
    }

    // ══════════════════════════════════════════════════════════════
    //  DTOs de resultado
    // ══════════════════════════════════════════════════════════════

    /// <summary>Resultado del push de catálogos al dispositivo.</summary>
    public class ResultadoPushCatalogos {
        public bool Exitoso         { get; set; }
        public bool Productos       { get; set; }
        public bool Proveedores     { get; set; }
        public bool Unidades        { get; set; }
        public bool Clasificaciones { get; set; }
        public bool Almacenes       { get; set; }
    }

    /// <summary>Resultado del pull de sesiones de stock desde el dispositivo.</summary>
    public class ResultadoPullStock {
        public bool         Exitoso             { get; set; }
        public List<string> JsonDescargados     { get; set; } = new();
        public int          ImagenesDescargadas { get; set; }
    }
}
