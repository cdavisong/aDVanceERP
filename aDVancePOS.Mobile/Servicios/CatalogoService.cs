using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

using System.Text.Json;

namespace aDVancePOS.Mobile.Servicios {
    public class CatalogoService {
        // Caché en memoria durante la sesión
        private CatalogoJson? _catalogoCargado;

        /// <summary>
        /// Lee catalogo.json del almacenamiento privado de la app.
        /// Lanza FileNotFoundException si el archivo no existe todavía.
        /// </summary>
        public async Task<CatalogoJson> CargarCatalogoAsync() {
            if (!File.Exists(RutasApp.RutaCatalogo))
                throw new FileNotFoundException(
                    "No se encontró catalogo.json. " +
                    "Exporta el catálogo desde el ERP desktop y cópialo al dispositivo.");

            var json = await File.ReadAllTextAsync(RutasApp.RutaCatalogo);
            _catalogoCargado = JsonSerializer.Deserialize(json, JsonContexto.Default.CatalogoJson)
                               ?? throw new InvalidDataException("El archivo catalogo.json está vacío o corrupto.");

            // Inicializar stock en sesión para control de disponibilidad
            foreach (var p in _catalogoCargado.Productos)
                p.StockEnSesion = p.StockDisponible;

            return _catalogoCargado;
        }

        /// <summary>
        /// Filtra productos por nombre o código. Búsqueda case-insensitive.
        /// </summary>
        public List<ProductoCatalogo> Buscar(string termino) {
            if (_catalogoCargado is null) return new();
            if (string.IsNullOrWhiteSpace(termino))
                return _catalogoCargado.Productos;

            termino = termino.Trim().ToLowerInvariant();
            return _catalogoCatalogo.Productos
                .Where(p =>
                    p.Nombre.ToLowerInvariant().Contains(termino) ||
                    p.Codigo.ToLowerInvariant().Contains(termino))
                .ToList();
        }

        // Alias interno
        private CatalogoJson _catalogoCatalogo =>
            _catalogoCargado ?? throw new InvalidOperationException("Catálogo no cargado.");

        public bool EstaDisponible => _catalogoCargado != null;
        public long IdAlmacenCatalogo => _catalogoCatalogo.Meta.IdAlmacen;

        /// <summary>
        /// Busca un producto por código exacto (case-insensitive).
        /// Usado por el escáner de código de barras.
        /// Devuelve null si no existe en el catálogo.
        /// </summary>
        public ProductoCatalogo? BuscarPorId(long id) =>
            _catalogoCargado?.Productos.FirstOrDefault(p => p.Id == id);

        public ProductoCatalogo? BuscarPorCodigo(string codigo) {
            if (_catalogoCargado is null || string.IsNullOrWhiteSpace(codigo))
                return null;

            return _catalogoCatalogo.Productos.FirstOrDefault(p =>
                string.Equals(p.Codigo, codigo, StringComparison.OrdinalIgnoreCase));
        }
    }
}