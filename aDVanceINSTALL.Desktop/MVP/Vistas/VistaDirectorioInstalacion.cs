using System.Runtime.InteropServices;

using Microsoft.Win32;

namespace aDVanceINSTALL.Desktop.MVP.Vistas {
    public partial class VistaDirectorioInstalacion : Form {
        // Importaciones necesarias para notificar al sistema
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

        private const int HWND_BROADCAST = 0xffff;
        private const int WM_SETTINGCHANGE = 0x001A;
        private const int SMTO_ABORTIFHUNG = 0x0002;

        public VistaDirectorioInstalacion() {
            InitializeComponent();
            ConfigurarControles();
        }

        private void ConfigurarControles() {
            // Configurar el diálogo de selección de directorio
            selectorDirectorio.Description = "Seleccione el directorio de instalación";
            selectorDirectorio.ShowNewFolderButton = true;

            // Cargar directorio previo si existe
            DirectorioInstalacion = CargarDirectorioPrograma() ?? "C:\\advanceerp\\programa\\";

            // Configurar eventos
            btnExaminar.Click += (s, e) => {
                if (selectorDirectorio.ShowDialog(this) == DialogResult.OK) {
                    DirectorioInstalacion = selectorDirectorio.SelectedPath;
                    SalvarDirectorioPrograma(DirectorioInstalacion);
                }
            };
        }


        public string DirectorioInstalacion {
            get => fieldDirectorio.Text.Trim();
            set => fieldDirectorio.Text = value?.Trim() ?? string.Empty;
        }

        public string CargarDirectorioPrograma() {
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

        public void SalvarDirectorioPrograma(string installPath) {
            if (string.IsNullOrWhiteSpace(installPath)) 
                return;

            try {
                // 1. Guardar en ubicación específica de la aplicación
                using (RegistryKey appKey = Registry.CurrentUser.CreateSubKey(@"Software\aDVanceERP")) {
                    appKey?.SetValue("InstallPath", installPath, RegistryValueKind.String);
                }

                // 2. Actualizar PATH del usuario
                using (RegistryKey envKey = Registry.CurrentUser.OpenSubKey(@"Environment", true)) {
                    string currentPath = envKey?.GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames)?.ToString() ?? "";

                    if (!currentPath.Split(';').Any(p => p.Equals(installPath, StringComparison.OrdinalIgnoreCase))) {
                        string newPath = $"{installPath};{currentPath}";
                        envKey?.SetValue("PATH", newPath, RegistryValueKind.ExpandString);

                        // Notificar al sistema
                        Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
                        SendMessageTimeout(
                            new IntPtr(HWND_BROADCAST), WM_SETTINGCHANGE,
                            IntPtr.Zero, "Environment", SMTO_ABORTIFHUNG, 100, out _);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error al guardar el directorio: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
    }
}
