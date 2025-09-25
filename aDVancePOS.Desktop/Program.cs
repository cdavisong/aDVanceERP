using aDVanceERP.Core.Utiles;
using aDVancePOS.Desktop.MVP.Presentadores.Principal;

namespace aDVancePOS.Desktop {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Iniciar el servidor TCP
            UtilesServidorScanner.Servidor.IniciarAsync(9509);

            // Configuraci�n de la aplicaci�n
            ApplicationConfiguration.Initialize();
            Application.Run((Form) new PresentadorPrincipal().Vista);

            // Detener el servidor TCP
            UtilesServidorScanner.Servidor.Detener();
        }
    }
}