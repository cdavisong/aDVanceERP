using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

using System.Text.Json;

namespace aDVancePOS.Mobile.Servicios {
    public class CatalogoService {
        private CatalogoJson? _catalogoCargado;

        public async Task<CatalogoJson> CargarCatalogoAsync() {
            var json = await File.ReadAllTextAsync(RutasApp.RutaCatalogo);
            _catalogoCargado = JsonSerializer.Deserialize(json, JsonContexto.Default.CatalogoJson)
                               ?? throw new InvalidDataException("Catálogo inválido.");

            // Inicializar StockEnSesion
            foreach (var p in _catalogoCargado.Productos)
                p.StockEnSesion = p.StockDisponible;

            return _catalogoCargado;
        }

        public List<ProductoCatalogo> Buscar(string termino) {
            if (_catalogoCargado == null) return new();
            if (string.IsNullOrWhiteSpace(termino))
                return _catalogoCargado.Productos.ToList();

            var t = termino.ToLowerInvariant();
            return _catalogoCargado.Productos
                .Where(p => p.Nombre.ToLowerInvariant().Contains(t)
                         || p.Codigo.ToLowerInvariant().Contains(t))
                .ToList();
        }

        public ProductoCatalogo? BuscarPorCodigo(string codigo) =>
            _catalogoCargado?.Productos.FirstOrDefault(
                p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));

        public ProductoCatalogo? BuscarPorId(long id) =>
            _catalogoCargado?.Productos.FirstOrDefault(p => p.Id == id);

        /// <summary>
        /// Monedas activas del catálogo. Lista vacía si el catálogo
        /// no fue cargado aún o no incluye monedas.
        /// </summary>
        public List<MonedaCatalogo> ObtenerMonedas() =>
            _catalogoCargado?.Monedas ?? new();

        /// <summary>
        /// Moneda base del sistema (es_base = true).
        /// Fallback: primera moneda o CUP ficticio si el catálogo no tiene monedas.
        /// </summary>
        public MonedaCatalogo ObtenerMonedaBase() =>
            _catalogoCargado?.Monedas.FirstOrDefault(m => m.EsBase)
            ?? new MonedaCatalogo { Id = 1, Codigo = "CUP", Nombre = "Peso Cubano",
                                    Simbolo = "$", EsBase = true, TasaHoy = 1m };
    }
}
