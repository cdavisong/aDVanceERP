using aDVancePOS.Mobile.Modelos;

using Android.Views;

namespace aDVancePOS.Mobile.Adaptadores {
    public class ProductoAdapter : BaseAdapter<ProductoCatalogo> {
        private readonly Activity _context;
        private readonly List<ProductoCatalogo> _productos = new();
        private readonly Action<ProductoCatalogo> _onAgregar;

        public ProductoAdapter(
            Activity context,
            List<ProductoCatalogo> productos,
            Action<ProductoCatalogo> onAgregar) {
            _context = context;
            _productos.AddRange(productos);
            _onAgregar = onAgregar;
        }

        public override int Count => _productos.Count;
        public override ProductoCatalogo this[int position] => _productos[position];
        public override long GetItemId(int position) => _productos[position].Id;

        /// <summary>
        /// Reemplaza la lista interna y fuerza el redibujado de todas las
        /// filas visibles. Esto actualiza el stock mostrado en cada fila
        /// sin recrear el adapter (lo que evitaría el redibujado en reciclaje).
        /// </summary>
        public void ActualizarLista(List<ProductoCatalogo> nuevaLista) {
            _productos.Clear();
            _productos.AddRange(nuevaLista);
            NotifyDataSetChanged();
        }

        public override View GetView(int position, View? convertView, ViewGroup parent) {
            var producto = _productos[position];

            // Inflar o reutilizar la vista
            var view = convertView
                       ?? _context.LayoutInflater.Inflate(
                           Resource.Layout.item_producto, parent, false)!;

            var txtNombre = view.FindViewById<TextView>(Resource.Id.txtNombreProducto)!;
            var txtCodigo = view.FindViewById<TextView>(Resource.Id.txtCodigoProducto)!;
            var txtPrecio = view.FindViewById<TextView>(Resource.Id.txtPrecioProducto)!;
            var btnAgregar = view.FindViewById<Button>(Resource.Id.btnAgregar)!;

            txtNombre.Text = producto.Nombre;
            txtCodigo.Text = $"Cód: {producto.Codigo}  ·  Stock: {producto.StockEnSesion} {producto.UnidadMedida}";
            txtPrecio.Text = producto.PrecioConImpuesto.ToString("C2");

            // Deshabilitar botón si no hay stock
            var sinStock = producto.StockEnSesion <= 0;
            btnAgregar.Enabled = !sinStock;
            btnAgregar.Alpha = sinStock ? 0.3f : 1.0f;

            // Evitar listener duplicado al reciclar la vista
            btnAgregar.Tag = position;
            btnAgregar.Click -= OnAgregarClick; // limpiar listener anterior
            btnAgregar.Click += OnAgregarClick;

            return view;

            void OnAgregarClick(object? s, EventArgs e) {
                if (s is Button btn && btn.Tag is Java.Lang.Integer pos)
                    _onAgregar(_productos[(int) pos]);
            }
        }
    }
}
