using aDVanceERP.Core.Infraestructura.Globales;
using aDVanceERP.Core.Modelos.Comun;

namespace aDVanceERP.Core.Controladores {

    /// <summary>
    /// Gestiona la transferencia de archivos entre el ERP desktop y
    /// el dispositivo Android con aDVancePOS instalado.
    ///
    /// Hereda la infraestructura ADB de <see cref="ControladorAdbBase"/>.
    /// Esta clase contiene únicamente la lógica específica del POS:
    ///   Push (catálogo → dispositivo) : un único archivo catalogo.json
    ///   Pull (ventas   → PC)          : archivos ventas_YYYYMMDD.json
    /// </summary>
    public class ControladorArchivosAndroidPos : ControladorAdbBase {

        protected override string AppPackageName => "cu.davisoft.advancepos";

        public ControladorArchivosAndroidPos(string applicationPath)
            : base(applicationPath) { }

        // ══════════════════════════════════════════════════════
        //  INFORMACIÓN DEL CATÁLOGO
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Obtiene el estado del catálogo en el dispositivo: si existe y cuándo
        /// fue modificado por última vez.
        /// Devuelve (false, null) si el catálogo no existe o no se puede leer la fecha.
        /// </summary>
        public (bool existe, DateTime? fechaModificacion) ObtenerInfoCatalogo()
            => ObtenerInfoArchivoDispositivo("catalogo.json");

        /// <summary>Verifica si el catálogo ya existe en el dispositivo.</summary>
        public bool ExisteCatalogo() {
            try {
                string output = EjecutarAdb($"shell ls \"{DeviceFilesDir}/catalogo.json\"");
                return !output.Contains("No such file");
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  PUSH — Catálogo hacia el dispositivo
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Envía catalogo.json al directorio externo de la app.
        /// </summary>
        public bool PushCatalogo(string localFilePath) {
            if (!File.Exists(localFilePath)) {
                CentroNotificaciones.MostrarNotificacion(
                    $"No se encontró el archivo de catálogo: {localFilePath}",
                    TipoNotificacionEnum.Error);
                return false;
            }

            try {
                AsegurarDirectorio();

                bool ok = PushArchivoUnico(localFilePath, "catalogo.json");

                if (!ok) {
                    CentroNotificaciones.MostrarNotificacion(
                        "Error al copiar el catálogo al dispositivo.",
                        TipoNotificacionEnum.Error);
                    return false;
                }

                CentroNotificaciones.MostrarNotificacion(
                    "Catálogo enviado correctamente al dispositivo.",
                    TipoNotificacionEnum.Ok);

                return true;
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error inesperado al enviar catálogo: {ex.Message}",
                    TipoNotificacionEnum.Error);
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
        /// <returns>
        /// Tuplas con: nombre del archivo, fecha extraída del nombre
        /// y tamaño estimado en KB (redondeado a 1 decimal).
        /// </returns>
        public List<(string fileName, DateTime fecha, double tamanoKb)> ListarArchivosVentas() {
            var resultado = new List<(string fileName, DateTime fecha, double tamanoKb)>();

            try {
                // "ls -la" devuelve una línea por archivo con el tamaño en bytes
                string output = EjecutarAdb($"shell ls -la \"{DeviceFilesDir}\"");

                foreach (var linea in output.Split('\n', StringSplitOptions.RemoveEmptyEntries)) {
                    var lineaTrim = linea.Trim();
                    if (!lineaTrim.Contains("ventas_") || !lineaTrim.EndsWith(".json"))
                        continue;

                    // Columnas: permisos links owner group SIZE fecha hora nombre
                    var partes = lineaTrim.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length < 8)
                        continue;

                    var archivo = partes[^1];
                    if (!archivo.StartsWith("ventas_") || !archivo.EndsWith(".json"))
                        continue;

                    string fechaParte = archivo.Replace("ventas_", "").Replace(".json", "");
                    if (!DateTime.TryParseExact(fechaParte, "yyyyMMdd",
                        null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
                        continue;

                    double tamanoKb = 0;
                    if (long.TryParse(partes[4], out long bytes))
                        tamanoKb = Math.Round(bytes / 1024.0, 1);

                    resultado.Add((archivo, fecha, tamanoKb));
                }
            } catch (Exception ex) {
                CentroNotificaciones.MostrarNotificacion(
                    $"Error al listar ventas en dispositivo: {ex.Message}",
                    TipoNotificacionEnum.Error);
            }

            return resultado.OrderByDescending(r => r.fecha).ToList();
        }

        /// <summary>
        /// Descarga el archivo de ventas de la fecha indicada.
        /// Por defecto descarga el de hoy.
        /// </summary>
        public bool PullVentas(string localDestinationPath, DateTime? fecha = null) {
            var fechaObj = fecha ?? DateTime.Today;
            return PullArchivo($"ventas_{fechaObj:yyyyMMdd}.json", localDestinationPath);
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
                    TipoNotificacionEnum.Advertencia);
                return descargados;
            }

            Directory.CreateDirectory(carpetaDestino);

            foreach (var (fileName, _, _) in archivos) {
                string destino = Path.Combine(carpetaDestino, fileName);
                if (PullArchivo(fileName, destino))
                    descargados.Add(destino);
            }

            CentroNotificaciones.MostrarNotificacion(
                $"{descargados.Count} de {archivos.Count} archivos descargados.",
                TipoNotificacionEnum.Ok);

            return descargados;
        }

        // ══════════════════════════════════════════════════════
        //  GESTIÓN DE ARCHIVOS EN DISPOSITIVO
        // ══════════════════════════════════════════════════════

        /// <summary>Elimina un archivo de ventas del dispositivo tras importarlo.</summary>
        public bool EliminarArchivoVentas(string deviceFileName) {
            try {
                EliminarArchivoDispositivo(deviceFileName);
                return true;
            } catch { return false; }
        }

        /// <summary>Elimina el catálogo del dispositivo para forzar recarga.</summary>
        public bool EliminarCatalogo() {
            try {
                EliminarArchivoDispositivo("catalogo.json");
                return true;
            } catch { return false; }
        }

        // ══════════════════════════════════════════════════════
        //  FLUJOS DE ALTO NIVEL
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
        public List<string> FlujoFinDia(string carpetaDestino, bool eliminarDelDispositivo = true) {
            var descargados = new List<string>();

            if (!CheckDeviceConnection(true)) return descargados;

            descargados = PullTodasLasVentas(carpetaDestino);

            if (eliminarDelDispositivo && descargados.Count > 0) {
                foreach (var (fileName, _, _) in ListarArchivosVentas())
                    EliminarArchivoVentas(fileName);
            }

            EliminarCatalogo(); // fuerza recarga de catálogo al día siguiente

            return descargados;
        }
    }
}
