using System.Diagnostics;

namespace aDVanceERP.Core.Controladores
{
    public class ControladorArchivosAndroid
    {
        private readonly string _toolsPath;
        private readonly string _appPackageName = "cu.davisoft.advancepos";

        public ControladorArchivosAndroid(string applicationPath)
        {
            _toolsPath = Path.Combine(applicationPath, "tools");

            // Verificar que existan las herramientas
            if (!File.Exists(Path.Combine(_toolsPath, "adb.exe")))
            {
                throw new FileNotFoundException("No se encontraron las herramientas ADB en el directorio tools");
            }
        }

        public bool PushFileToDevice(string localFilePath, string deviceFileName)
        {
            string devicePath = $"/sdcard/Android/media/{_appPackageName}/{deviceFileName}";

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(_toolsPath, "adb.exe"),
                    Arguments = $"push \"{localFilePath}\" \"{devicePath}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processInfo))
                {
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        string error = process.StandardError.ReadToEnd();
                        MessageBox.Show($"Error al copiar archivo: {error}");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        public bool PullFileFromDevice(string deviceFileName, string localDestinationPath)
        {
            string devicePath = $"/sdcard/Android/media/{_appPackageName}/{deviceFileName}";

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(_toolsPath, "adb.exe"),
                    Arguments = $"pull \"{devicePath}\" \"{localDestinationPath}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processInfo))
                {
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        string error = process.StandardError.ReadToEnd();
                        MessageBox.Show($"Error al obtener archivo: {error}");
                        return false;
                    }
                    return File.Exists(localDestinationPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        public bool DeleteFileFromDevice(string deviceFileName)
        {
            string devicePath = $"/sdcard/Android/media/{_appPackageName}/{deviceFileName}";

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(_toolsPath, "adb.exe"),
                    Arguments = $"shell rm \"{devicePath}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool CheckDeviceConnection()
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(_toolsPath, "adb.exe"),
                    Arguments = "devices",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using (var process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();

                    // Verificar que haya al menos un dispositivo autorizado
                    return output.Contains("\tdevice") && !output.Contains("unauthorized");
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EnsureDirectoryExists()
        {
            string deviceDirPath = $"/sdcard/Android/media/{_appPackageName}";

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(_toolsPath, "adb.exe"),
                    Arguments = $"shell mkdir -p \"{deviceDirPath}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<(string fileName, DateTime exportTime)> ListVentasFilesOnDevice()
        {
            var result = new List<(string, DateTime)>();
            string deviceDirPath = $"/sdcard/Android/media/{_appPackageName}";

            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(_toolsPath, "adb.exe"),
                    Arguments = $"shell ls \"{deviceDirPath}/ventas_export_*.json\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();

                    if (process.ExitCode != 0)
                    {
                        return result;
                    }

                    // Procesar cada archivo encontrado
                    using (var reader = new StringReader(output))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            string fileName = Path.GetFileName(line.Trim());

                            if (fileName.StartsWith("ventas_export_") && fileName.EndsWith(".json"))
                            {
                                // Extraer la marca de tiempo del nombre del archivo
                                string timePart = fileName
                                    .Replace("ventas_export_", "")
                                    .Replace(".json", "");

                                if (DateTime.TryParseExact(timePart, "yyyyMMdd_HHmmss",
                                    null, System.Globalization.DateTimeStyles.None, out DateTime exportTime))
                                {
                                    result.Add((fileName, exportTime));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar archivos en dispositivo: {ex.Message}");
            }

            return result;
        }

    }
}