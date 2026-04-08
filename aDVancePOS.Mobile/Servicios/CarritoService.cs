using aDVancePOS.Mobile.Modelos;

namespace aDVancePOS.Mobile.Servicios {
    public class CarritoService {
        private readonly List<ItemCarrito> _items = new();

        public IReadOnlyList<ItemCarrito> Items => _items.AsReadOnly();

        public decimal TotalBruto =>
            _items.Sum(i => Math.Round(i.PrecioUnitario * i.Cantidad, 2));

        public decimal TotalImpuesto => 0; // Por ahora no se calcula el impuesto, pero si se implementa, sería algo como:
                                           //_items.Sum(i => Math.Round(
                                           //    i.PrecioUnitario * (i.Producto.ImpuestoVentaPorcentaje / 100) * i.Cantidad, 2));

        public decimal ImporteTotal => TotalBruto + TotalImpuesto;

        public int ConteoItems => _items.Sum(i => (int) i.Cantidad);

        /// <summary>
        /// Agrega una unidad del producto al carrito usando la presentación especificada.
        /// Devuelve false si no hay stock suficiente.
        /// </summary>
        public bool AgregarProducto(ProductoCatalogo producto, long idPresentacion, decimal precioUnitario) {
            if (producto.StockEnSesion <= 0)
                return false; // Sin stock

            var existente = _items.FirstOrDefault(i => i.Producto.Id == producto.Id && i.IdPresentacion == idPresentacion);
            if (existente != null) {
                existente.Cantidad++;
            } else {
                _items.Add(new ItemCarrito { 
                    Producto = producto, 
                    Cantidad = 1,
                    IdPresentacion = idPresentacion,
                    PrecioUnitario = precioUnitario
                });
            }

            producto.StockEnSesion--;
            return true;
        }

        /// <summary>Resta una unidad. Elimina el ítem si queda en 0.</summary>
        public void RestarProducto(ProductoCatalogo producto, long idPresentacion) {
            var item = _items.FirstOrDefault(i => i.Producto.Id == producto.Id && i.IdPresentacion == idPresentacion);
            if (item == null) return;

            item.Cantidad--;
            producto.StockEnSesion++;

            if (item.Cantidad <= 0)
                _items.Remove(item);
        }

        /// <summary>Elimina completamente un ítem del carrito.</summary>
        public void EliminarItem(ItemCarrito item) {
            item.Producto.StockEnSesion += item.Cantidad;
            _items.Remove(item);
        }

        /// <summary>
        /// Vacía el carrito devolviendo el stock a cada producto.
        /// Usar cuando el cajero CANCELA la operación (botón "Vaciar").
        /// </summary>
        public void Vaciar() {
            foreach (var item in _items)
                item.Producto.StockEnSesion += item.Cantidad;
            _items.Clear();
        }

        /// <summary>
        /// Vacía el carrito SIN devolver el stock.
        /// Usar tras confirmar una VENTA: el stock ya fue descontado
        /// al agregar cada producto y debe mantenerse reducido.
        /// </summary>
        public void VaciarTrasVenta() {
            _items.Clear();
        }

        /// <summary>
        /// Cambia la presentación de un ítem del carrito, ajustando precio y devolviendo/recuperando stock según corresponda.
        /// </summary>
        public void CambiarPresentacionItem(ItemCarrito item, long nuevaIdPresentacion, decimal nuevoPrecioUnitario) {
            // Si la presentación es la misma, no hacer nada
            if (item.IdPresentacion == nuevaIdPresentacion && item.PrecioUnitario == nuevoPrecioUnitario)
                return;

            // Buscar si ya existe otro ítem con esta presentación para el mismo producto
            var itemExistente = _items.FirstOrDefault(i => 
                i.Producto.Id == item.Producto.Id && i.IdPresentacion == nuevaIdPresentacion);

            if (itemExistente != null && itemExistente != item) {
                // Fusionar: sumar cantidad al existente y eliminar el actual
                itemExistente.Cantidad += item.Cantidad;
                item.Producto.StockEnSesion += item.Cantidad; // Devolver stock del item original
                itemExistente.Producto.StockEnSesion -= item.Cantidad; // Descontar stock en la nueva presentación
                _items.Remove(item);
            } else {
                // Solo cambiar presentación y precio del ítem actual
                item.IdPresentacion = nuevaIdPresentacion;
                item.PrecioUnitario = nuevoPrecioUnitario;
            }
        }
    }
}
