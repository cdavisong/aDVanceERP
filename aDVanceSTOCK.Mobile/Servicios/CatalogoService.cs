// ============================================================
//  aDVanceSTOCK.Mobile — CatalogoService
//  Archivo: Servicios/CatalogoService.cs
//
//  Carga y mantiene en memoria los 5 catálogos de apoyo
//  que el ERP exporta al teléfono vía adb push.
//
//  La búsqueda de productos existentes por código es el
//  mecanismo para distinguir "nuevo" de "entrada_stock".
// ============================================================

using System.Text.Json;

using aDVanceSTOCK.Mobile.Modelos;

namespace aDVanceSTOCK.Mobile.Servicios {

    public class CatalogoService {

        // ── Caché en memoria ──────────────────────────────────────────
        private List<ProductoExistente> _productos = new();
        private List<ProveedorCatalogo> _proveedores = new();
        private List<UnidadMedidaCatalogo> _unidades = new();
        private List<ClasificacionCatalogo> _clasificaciones = new();
        private List<AlmacenCatalogo> _almacenes = new();

        // ── Propiedades de acceso ─────────────────────────────────────
        public IReadOnlyList<ProveedorCatalogo> Proveedores => _proveedores;
        public IReadOnlyList<UnidadMedidaCatalogo> Unidades => _unidades;
        public IReadOnlyList<ClasificacionCatalogo> Clasificaciones => _clasificaciones;
        public IReadOnlyList<AlmacenCatalogo> Almacenes => _almacenes;

        public bool CatalogosListos { get; private set; } = false;

        // ── Carga de todos los catálogos ──────────────────────────────

        /// <summary>
        /// Carga los 5 catálogos desde disco.
        /// Lanza excepciones si algún archivo obligatorio no existe.
        /// Los catálogos de proveedores, unidades y clasificaciones son
        /// opcionales (devuelven lista vacía si no existen).
        /// </summary>
        public async Task CargarTodosAsync() {
            _productos = await CargarListaAsync(
                                   RutasApp.RutaCatalogoProductos,
                                   JsonContexto.Default.ListProductoExistente,
                                   obligatorio: true);

            _proveedores = await CargarListaAsync(
                                   RutasApp.RutaCatalogoProveedores,
                                   JsonContexto.Default.ListProveedorCatalogo,
                                   obligatorio: false);

            _unidades = await CargarListaAsync(
                                   RutasApp.RutaCatalogoUnidades,
                                   JsonContexto.Default.ListUnidadMedidaCatalogo,
                                   obligatorio: false);

            _clasificaciones = await CargarListaAsync(
                                   RutasApp.RutaCatalogoClasificaciones,
                                   JsonContexto.Default.ListClasificacionCatalogo,
                                   obligatorio: false);

            _almacenes = await CargarListaAsync(
                                   RutasApp.RutaCatalogoAlmacenes,
                                   JsonContexto.Default.ListAlmacenCatalogo,
                                   obligatorio: false);

            CatalogosListos = true;
        }

        // ── Búsqueda de producto existente ────────────────────────────

        /// <summary>
        /// Busca un producto por código exacto (case-insensitive).
        /// Si devuelve null → es un producto nuevo.
        /// Si devuelve un objeto → es una entrada de stock.
        /// </summary>
        public ProductoExistente? BuscarProductoPorCodigo(string codigo) {
            if (string.IsNullOrWhiteSpace(codigo)) return null;
            return _productos.FirstOrDefault(p =>
                string.Equals(p.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        // ── Privados ──────────────────────────────────────────────────

        private static async Task<List<T>> CargarListaAsync<T>(
            string ruta,
            System.Text.Json.Serialization.Metadata.JsonTypeInfo<List<T>> typeInfo,
            bool obligatorio) {
            try {
                if (!File.Exists(ruta)) {
                    if (obligatorio)
                        throw new FileNotFoundException(
                            $"Archivo requerido no encontrado: {Path.GetFileName(ruta)}");
                    return new List<T>();
                }
                var json = await File.ReadAllTextAsync(ruta);
                return JsonSerializer.Deserialize(json, typeInfo) ?? new List<T>();
            } catch (FileNotFoundException) {
                throw;
            } catch (Exception ex) {
                if (obligatorio)
                    throw new InvalidDataException(
                        $"Error al leer {Path.GetFileName(ruta)}: {ex.Message}", ex);
                return new List<T>();
            }
        }
    }
}
