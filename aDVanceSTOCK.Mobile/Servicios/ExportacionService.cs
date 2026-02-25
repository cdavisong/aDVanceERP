// ============================================================
//  aDVanceSTOCK.Mobile — ExportacionService
//  Archivo: Servicios/ExportacionService.cs
//
//  Convierte la sesión en memoria en:
//    1. Un JSON: stock_YYYYMMDD_HHmmss.json
//    2. Las imágenes: /imagenes/{codigo}.jpg
//
//  El ERP desktop recupera todo vía:
//    adb pull /sdcard/Android/data/cu.davisoft.advancestock/files/ ./
// ============================================================

using System.Text.Json;
using aDVanceSTOCK.Mobile.Modelos;

namespace aDVanceSTOCK.Mobile.Servicios {

    public class ExportacionService {

        /// <summary>
        /// Exporta la sesión completa a disco y devuelve la ruta del JSON generado.
        /// </summary>
        public async Task<string> ExportarSesionAsync(
            SesionService sesion,
            ConfiguracionApp config) {

            // 1. Asegurar que el directorio de imágenes existe
            Directory.CreateDirectory(RutasApp.DirectorioImagenes);

            var ahora = DateTime.Now;

            // 2. Copiar imágenes al directorio de exportación
            CopiarImagenes(sesion.Items);

            // 3. Construir el objeto de exportación
            var exportacion = new ExportacionSesion {
                FechaSesion  = ahora,
                IdAlmacen    = config.IdAlmacen,
                NombreAlmacen = config.NombreAlmacen,
                TotalItems   = sesion.TotalItems,
                Productos    = sesion.Items.Select(MapearProducto).ToList()
            };

            // 4. Serializar y guardar
            var rutaJson = RutasApp.RutaExportacionSesion(ahora);
            var json = JsonSerializer.Serialize(exportacion, JsonContexto.Default.ExportacionSesion);
            await File.WriteAllTextAsync(rutaJson, json);

            return rutaJson;
        }

        // ── Privados ──────────────────────────────────────────────────

        private static ProductoExportacion MapearProducto(ProductoSesion p) {
            var exp = new ProductoExportacion {
                Tipo         = p.Tipo == TipoRegistro.Nuevo ? "nuevo" : "entrada_stock",
                Codigo       = p.Codigo,
                Cantidad     = p.Cantidad,
                TieneImagen  = p.TieneImagen,
                FechaRegistro = p.FechaRegistro
            };

            if (p.Tipo == TipoRegistro.Nuevo) {
                exp.Nombre       = p.Nombre;
                exp.Descripcion  = p.Descripcion;
                exp.Categoria    = p.Categoria;
                exp.EsVendible   = p.EsVendible;
                exp.IdProveedor  = p.IdProveedor > 0 ? p.IdProveedor : null;
                exp.IdUnidadMedida   = p.IdUnidadMedida   > 0 ? p.IdUnidadMedida   : null;
                exp.IdClasificacion  = p.IdClasificacion  > 0 ? p.IdClasificacion  : null;
                exp.CostoAdquisicionUnitario = p.CostoAdquisicionUnitario;
                exp.PrecioVentaBase          = p.PrecioVentaBase;
            }

            return exp;
        }

        private static void CopiarImagenes(IReadOnlyList<ProductoSesion> items) {
            foreach (var item in items.Where(i => i.TieneImagen)) {
                var destino = RutasApp.RutaImagen(item.Codigo);
                try {
                    File.Copy(item.RutaImagenLocal!, destino, overwrite: true);
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(
                        $"Error copiando imagen de {item.Codigo}: {ex.Message}");
                }
            }
        }
    }
}
