namespace aDVancePOS.Mobile.Servicios {
    public static class RutasApp {
        /// <summary>
        /// Directorio privado de la app:
        ///   /data/data/aDVancePOS.Mobile/files/
        /// Accesible vía ADB sin root:
        ///   adb exec-out run-as aDVancePOS.Mobile cat files/ventas_20250222.json
        /// </summary>
        public static string DirectorioBase =>
    Android.App.Application.Context
        .GetExternalFilesDir(null)!.AbsolutePath;

        public static string RutaCatalogo =>
            Path.Combine(DirectorioBase, "catalogo.json");

        public static string RutaConfiguracion =>
            Path.Combine(DirectorioBase, "config.json");

        /// <summary>Un archivo de ventas por día: ventas_20250222.json</summary>
        public static string RutaVentasHoy =>
            Path.Combine(DirectorioBase, $"ventas_{DateTime.Now:yyyyMMdd}.json");
    }
}
