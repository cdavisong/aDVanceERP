// ============================================================
//  aDVanceSTOCK.Mobile — RutasApp
//  Archivo: Servicios/RutasApp.cs
//
//  Centraliza todas las rutas de archivos de la app.
//  GetExternalFilesDir es accesible por ADB sin root.
//
//  Acceso ADB:
//    Push (ERP → Teléfono):
//      adb push catalogo_productos.json \
//        /sdcard/Android/data/cu.davisoft.advancestock/files/catalogo_productos.json
//
//    Pull (Teléfono → ERP):
//      adb pull /sdcard/Android/data/cu.davisoft.advancestock/files/ ./exportacion/
// ============================================================

namespace aDVanceSTOCK.Mobile.Servicios {

    public static class RutasApp {

        public static string DirectorioBase =>
            Android.App.Application.Context
                .GetExternalFilesDir(null)!.AbsolutePath;

        // ── Configuración ─────────────────────────────────────────────
        public static string RutaConfiguracion =>
            Path.Combine(DirectorioBase, "config.json");

        // ── Catálogos de apoyo (importados desde el ERP) ──────────────
        public static string RutaCatalogoProductos =>
            Path.Combine(DirectorioBase, "catalogo_productos.json");

        public static string RutaCatalogoProveedores =>
            Path.Combine(DirectorioBase, "catalogo_proveedores.json");

        public static string RutaCatalogoUnidades =>
            Path.Combine(DirectorioBase, "catalogo_unidades.json");

        public static string RutaCatalogoClasificaciones =>
            Path.Combine(DirectorioBase, "catalogo_clasificaciones.json");

        public static string RutaCatalogoAlmacenes =>
            Path.Combine(DirectorioBase, "catalogo_almacenes.json");

        // ── Exportación (generada por la app) ────────────────────────
        /// <summary>Archivo de sesión con timestamp: stock_20250225_103045.json</summary>
        public static string RutaExportacionSesion(DateTime cuando) =>
            Path.Combine(DirectorioBase, $"stock_{cuando:yyyyMMdd_HHmmss}.json");

        // ── Imágenes ──────────────────────────────────────────────────
        public static string DirectorioImagenes =>
            Path.Combine(DirectorioBase, "imagenes");

        /// <summary>Ruta de imagen por código: /imagenes/12837691283764.jpg</summary>
        public static string RutaImagen(string codigo) =>
            Path.Combine(DirectorioImagenes, $"{codigo}.jpg");

        /// <summary>
        /// Ruta temporal donde la cámara guarda la foto antes de moverla
        /// al directorio de imágenes con el nombre definitivo del código.
        /// </summary>
        public static string RutaFotoTemporal =>
            Path.Combine(DirectorioBase, "foto_temp.jpg");
    }
}
