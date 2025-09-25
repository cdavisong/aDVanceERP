using aDVanceERP.Actualizador.Interfaces;
using aDVanceERP.Actualizador.Modelos;

using Newtonsoft.Json;

using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;

namespace aDVanceERP.Actualizador.Servicios;

public class ServicioActualizacionGitHub : IServicioActualizacion {
    private readonly HttpClient _httpClient;
    private readonly string _repositoryOwner;
    private readonly string _repositoryName;

    public ServicioActualizacionGitHub(string propietarioRepositorio, string nombreRepositorio) {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "AutoUpdater");
        _repositoryOwner = propietarioRepositorio;
        _repositoryName = nombreRepositorio;
    }

    public async Task<InfoActualizacion> ComprobarActualizaciones(string versionActual, bool incluirPreActualizaciones = false) {
        try {
            var url = $"https://api.github.com/repos/{_repositoryOwner}/{_repositoryName}/releases";
            var response = await _httpClient.GetStringAsync(url);
            var releases = JsonConvert.DeserializeObject<GitHubRelease[]>(response);

            foreach (var release in releases) {
                if (release.PreRelease && !incluirPreActualizaciones)
                    continue;

                if (Version.TryParse(release.TagName.TrimStart('v', 'V').Replace("-alpha", string.Empty).Replace("-beta", string.Empty), out var releaseVersion)) {
                    var current = Version.Parse(versionActual);

                    if (releaseVersion > current) {
                        var asset = release.Assets?.Count > 0 ? release.Assets[0] : null;

                        return new InfoActualizacion {
                            UltimaVersion = releaseVersion,
                            UrlDescarga = asset?.DownloadUrl,
                            NotasVersion = release.Body,
                            FechaLanzamiento = release.PublishedAt,
                            TamanoArchivo = asset?.Size ?? 0,
                            EsPreActualizacion = release.PreRelease,
                            ActualizacionDisponible = true
                        };
                    }
                }
            }

            return new InfoActualizacion { ActualizacionDisponible = false };
        }
        catch (Exception ex) {
            throw new Exception("Error al verificar actualizaciones", ex);
        }
    }

    public async Task<bool> DescargarActualizacion(InfoActualizacion info, IProgress<double> progreso = null) {
        if (string.IsNullOrEmpty(info.UrlDescarga))
            return false;

        try {
            var tempPath = Path.GetTempPath();
            var fileName = Path.GetFileName(new Uri(info.UrlDescarga).LocalPath);
            var rutaArchivo = Path.Combine(tempPath, fileName);

            using (var response = await _httpClient.GetAsync(info.UrlDescarga, HttpCompletionOption.ResponseHeadersRead)) {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var receivedBytes = 0L;
                var buffer = new byte[8192];

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write)) {
                    int bytesRead;
                    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0) {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        receivedBytes += bytesRead;

                        if (totalBytes > 0)
                            progreso?.Report((double)receivedBytes / totalBytes * 100);
                    }
                }
            }

            info.UrlDescarga = rutaArchivo; // Guardamos la ruta local
            return true;
        }
        catch (Exception ex) {
            throw new Exception("Error al descargar la actualización", ex);
        }
    }

    public void AplicarActualizacion(string rutaDescargaActualizacion, IProgress<double> progreso = null) {
        if (!File.Exists(rutaDescargaActualizacion))
            throw new FileNotFoundException("Archivo de actualización no encontrado");

        try {
            // Cerrar la aplicación si aún está activa
            var procesoApp = Process.GetProcessesByName("aDVanceERP.Desktop").FirstOrDefault();
            if (procesoApp != null) {
                procesoApp.Kill();
                procesoApp.WaitForExit(5000); // Esperar hasta 5 segundos a que cierre
            }

            // Obtener el directorio de destino (directorio actual de la aplicación)
            string directorioDestino = AppDomain.CurrentDomain.BaseDirectory;
            string directorioTemporal = Path.Combine(Path.GetTempPath(), "aDVanceERP_Update");

            // Limpiar directorio temporal si existe
            if (Directory.Exists(directorioTemporal)) {
                Directory.Delete(directorioTemporal, true);
            }

            Directory.CreateDirectory(directorioTemporal);

            // Descomprimir el archivo ZIP
            progreso?.Report(10);
            DescomprimirArchivo(rutaDescargaActualizacion, directorioTemporal, progreso);

            // Buscar y ejecutar el instalador
            progreso?.Report(90);
            string instaladorPath = Path.Combine(directorioTemporal, "aDVanceINSTALL.Desktop.exe");

            if (!File.Exists(instaladorPath)) {
                // Buscar recursivamente en todos los directorios
                instaladorPath = BuscarArchivoRecursivamente(directorioTemporal, "aDVanceINSTALL.Desktop.exe");
            }

            if (File.Exists(instaladorPath)) {
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    FileName = instaladorPath,
                    WorkingDirectory = Path.GetDirectoryName(instaladorPath)
                };

                // Ejecutar el instalador
                var instaladorProcess = Process.Start(startInfo);

                // Esperar a que el instalador termine
                instaladorProcess?.WaitForExit();

                // Verificar si la instalación fue exitosa
                if (instaladorProcess?.ExitCode != 0) {
                    throw new Exception($"El instalador falló con código de error: {instaladorProcess.ExitCode}");
                }
            } else {
                throw new FileNotFoundException("No se encontró el instalador aDVanceINSTALL.Desktop.exe en el paquete de actualización");
            }

            // Limpiar archivos temporales
            progreso?.Report(95);
            LimpiarArchivosTemporales(rutaDescargaActualizacion, directorioTemporal);

            // Reiniciar la aplicación
            progreso?.Report(100);

            Application.Exit();
        } catch (Exception ex) {
            throw new Exception("Error al aplicar la actualización", ex);
        }
    }

    private void DescomprimirArchivo(string archivoZip, string directorioDestino, IProgress<double> progreso) {
        using (ZipArchive archive = ZipFile.OpenRead(archivoZip)) {
            int totalArchivos = archive.Entries.Count;
            int archivosProcesados = 0;

            foreach (ZipArchiveEntry entry in archive.Entries) {
                try {
                    // Ignorar directorios
                    if (string.IsNullOrEmpty(entry.Name))
                        continue;

                    string destinoCompleto = Path.Combine(directorioDestino, entry.FullName);
                    string directorio = Path.GetDirectoryName(destinoCompleto);

                    // Crear directorio si no existe
                    if (!Directory.Exists(directorio)) {
                        Directory.CreateDirectory(directorio);
                    }

                    // Extraer archivo
                    entry.ExtractToFile(destinoCompleto, true);

                    archivosProcesados++;
                    double porcentaje = 10 + (80.0 * archivosProcesados / totalArchivos);
                    progreso?.Report(porcentaje);
                } catch (Exception ex) {
                    // Loggear error pero continuar con otros archivos
                    Console.WriteLine($"Error al extraer {entry.FullName}: {ex.Message}");
                }
            }
        }
    }

    private string BuscarArchivoRecursivamente(string directorio, string nombreArchivo) {
        try {
            // Buscar en el directorio actual
            string[] archivos = Directory.GetFiles(directorio, nombreArchivo, SearchOption.TopDirectoryOnly);
            if (archivos.Length > 0)
                return archivos[0];

            // Buscar recursivamente en subdirectorios
            archivos = Directory.GetFiles(directorio, nombreArchivo, SearchOption.AllDirectories);
            if (archivos.Length > 0)
                return archivos[0];
        } catch (Exception ex) {
            Console.WriteLine($"Error buscando archivo: {ex.Message}");
        }

        return null;
    }

    private void LimpiarArchivosTemporales(string archivoDescarga, string directorioTemporal) {
        try {
            // Eliminar archivo descargado
            if (File.Exists(archivoDescarga)) {
                File.Delete(archivoDescarga);
            }

            // Eliminar directorio temporal después de un breve delay
            Task.Delay(1000).ContinueWith(t =>
            {
                try {
                    if (Directory.Exists(directorioTemporal)) {
                        Directory.Delete(directorioTemporal, true);
                    }
                } catch {
                    // Ignorar errores de limpieza
                }
            });
        } catch {
            // Ignorar errores de limpieza
        }
    }
}