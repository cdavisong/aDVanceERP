using aDVancePOS.Mobile.Modelos;

using Android.Views;

namespace aDVancePOS.Mobile.Adaptadores {
    public class ProductoAdapter : BaseAdapter<ProductoCatalogo> {
        private readonly Activity _context;
        private readonly List<ProductoCatalogo> _productos = new();

        // Callback: producto + presentación elegida (id=0 → precio base)
        private readonly Action<ProductoCatalogo, long, decimal> _onAgregar;

        public ProductoAdapter(
            Activity context,
            List<ProductoCatalogo> productos,
            Action<ProductoCatalogo, long, decimal> onAgregar) {
            _context = context;
            _productos.AddRange(productos);
            _onAgregar = onAgregar;
        }

        public override int Count => _productos.Count;
        public override ProductoCatalogo this[int position] => _productos[position];
        public override long GetItemId(int position) => _productos[position].Id;

        public void ActualizarLista(List<ProductoCatalogo> nuevaLista) {
            _productos.Clear();
            _productos.AddRange(nuevaLista);
            NotifyDataSetChanged();
        }

        public override View GetView(int position, View? convertView, ViewGroup parent) {
            var producto = _productos[position];

            var view = convertView
                       ?? _context.LayoutInflater.Inflate(
                           Resource.Layout.item_producto, parent, false)!;

            var txtNombre      = view.FindViewById<TextView>(Resource.Id.txtNombreProducto)!;
            var txtCodigo      = view.FindViewById<TextView>(Resource.Id.txtCodigoProducto)!;
            var txtPrecio      = view.FindViewById<TextView>(Resource.Id.txtPrecioProducto)!;
            var txtChip        = view.FindViewById<TextView>(Resource.Id.txtChipPresentaciones)!;
            var btnAgregar     = view.FindViewById<Button>(Resource.Id.btnAgregar)!;

            txtNombre.Text = producto.Nombre;
            txtCodigo.Text = $"Cód: {producto.Codigo}  ·  Stock: {producto.StockEnSesion} {producto.UnidadMedida}";

            var presentacionesActivas = producto.Presentaciones
                .Where(p => p.Activo).ToList();

            bool tieneVarias = presentacionesActivas.Count > 1;

            // Precio mostrado: mínimo de presentaciones o precio base
            if (presentacionesActivas.Count > 0) {
                var precioMin = presentacionesActivas.Min(p => p.PrecioVenta);
                txtPrecio.Text = tieneVarias
                    ? $"desde {precioMin:C2}"
                    : presentacionesActivas[0].PrecioVenta.ToString("C2");
            } else {
                txtPrecio.Text = producto.PrecioConImpuesto.ToString("C2");
            }

            // Chip "Varias presentaciones"
            txtChip.Visibility = tieneVarias
                ? ViewStates.Visible
                : ViewStates.Gone;

            // Deshabilitar botón si no hay stock
            var sinStock = producto.StockEnSesion <= 0;
            btnAgregar.Enabled = !sinStock;
            btnAgregar.Alpha   = sinStock ? 0.3f : 1.0f;

            btnAgregar.Tag      = position;
            btnAgregar.Click   -= OnAgregarClick;
            btnAgregar.Click   += OnAgregarClick;

            return view;

            void OnAgregarClick(object? s, EventArgs e) {
                if (s is not Button btn || btn.Tag is not Java.Lang.Integer pos) return;
                var prod = _productos[(int)pos];
                var activas = prod.Presentaciones.Where(p => p.Activo).ToList();

                // Siempre mostrar diálogo si hay presentaciones activas (1 o más)
                // Si no hay presentaciones, usar precio base directamente
                if (activas.Count > 0)
                    MostrarDialogoPresentaciones(prod, activas);
                else
                    _onAgregar(prod, 0, prod.PrecioConImpuesto); // precio base
            }
        }

        // ── Diálogo de selección de presentación ──────────────

        private void MostrarDialogoPresentaciones(
            ProductoCatalogo producto,
            List<PresentacionVenta> presentaciones) {

            // Crear lista de opciones: primero "Unidad base", luego las presentaciones
            var opciones = new List<(string Etiqueta, long IdPresentacion, decimal Precio)>();
            
            // Agregar opción de unidad base (idPresentacion = 0)
            opciones.Add(($"Unidad base — {producto.PrecioConImpuesto:C2}", 0, producto.PrecioConImpuesto));
            
            // Agregar presentaciones activas
            foreach (var p in presentaciones) {
                opciones.Add(($" {p.Cantidad} {p.UnidadMedida} — {p.PrecioVenta:C2}", p.Id, p.PrecioVenta));
            }

            var etiquetas = opciones.Select(o => o.Etiqueta).ToArray();

            new AlertDialog.Builder(_context)!
                .SetTitle($"¿Cómo desea vender {producto.Nombre}?")!
                .SetItems(etiquetas, (s, e) => {
                    var opcionElegida = opciones[e.Which];
                    _onAgregar(producto, opcionElegida.IdPresentacion, opcionElegida.Precio);
                })!
                .SetNegativeButton("Cancelar", (s, e) => { })!
                .Show();
        }
    }
}
