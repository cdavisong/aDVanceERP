// ============================================================
//  aDVanceSTOCK.Mobile — SesionService
//  Archivo: Servicios/SesionService.cs
//
//  Mantiene en memoria la lista de productos registrados
//  durante la sesión actual antes de exportar.
//
//  Es el único punto de verdad sobre qué se registró.
// ============================================================

using aDVanceSTOCK.Mobile.Modelos;

namespace aDVanceSTOCK.Mobile.Servicios {

    public class SesionService {

        private readonly List<ProductoSesion> _items = new();

        public IReadOnlyList<ProductoSesion> Items => _items;

        public int TotalItems    => _items.Count;
        public int TotalNuevos   => _items.Count(i => i.Tipo == TipoRegistro.Nuevo);
        public int TotalEntradas => _items.Count(i => i.Tipo == TipoRegistro.EntradaStock);

        // ── Operaciones CRUD ──────────────────────────────────────────

        public void Agregar(ProductoSesion producto) {
            _items.Insert(0, producto); // más reciente primero
        }

        public void Eliminar(ProductoSesion producto) {
            _items.Remove(producto);
            // Limpiar imagen local si existe
            if (producto.TieneImagen) {
                try { File.Delete(producto.RutaImagenLocal!); } catch { }
            }
        }

        public void Reemplazar(ProductoSesion original, ProductoSesion editado) {
            var idx = _items.IndexOf(original);
            if (idx >= 0) _items[idx] = editado;
        }

        public bool CodigoYaRegistrado(string codigo) =>
            _items.Any(i => string.Equals(i.Codigo, codigo, StringComparison.OrdinalIgnoreCase));

        public void Limpiar() {
            // Eliminar imágenes temporales
            foreach (var item in _items) {
                if (item.TieneImagen) {
                    try { File.Delete(item.RutaImagenLocal!); } catch { }
                }
            }
            _items.Clear();
        }
    }
}
