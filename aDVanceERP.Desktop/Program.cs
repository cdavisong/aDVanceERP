using aDVanceERP.Desktop.MVP.Presentadores.Principal;

namespace aDVanceERP.Desktop;

internal static class Program {
    public static string Version = "0.0.0.1";
    public static string NombreVersion = $"aDVance ERP versi�n {Version}";

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
        // Carga de la versi�n
        if (File.Exists(@".\app.ver"))
            using (var fs = new FileStream(@".\app.ver", FileMode.Open)) {
                using (var sr = new StreamReader(fs)) {
                    Version = sr.ReadToEnd().Trim();
                }
            }

        // Configuraci�n de la aplicaci�n
        ApplicationConfiguration.Initialize();
        Application.Run((Form) new PresentadorPrincipal().Vista);
    }
}