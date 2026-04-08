using aDVancePOS.Mobile.Modelos;

using Android.Views;

namespace aDVancePOS.Mobile.Adaptadores {
    public class CarritoAdapter : BaseAdapter<ItemCarrito> {
        private readonly Activity _context;
        private readonly List<ItemCarrito> _items;
        private readonly Action<ItemCarrito> _onRestar;
        private readonly Action<ItemCarrito> _onSumar;
        private readonly Action<ItemCarrito> _onEliminar;

        public CarritoAdapter(
            Activity context,
            List<ItemCarrito> items,
            Action<ItemCarrito> onRestar,
            Action<ItemCarrito> onSumar,
            Action<ItemCarrito> onEliminar) {
            _context = context;
            _items = items;
            _onRestar = onRestar;
            _onSumar = onSumar;
            _onEliminar = onEliminar;
        }

        public override int Count => _items.Count;
        public override ItemCarrito this[int position] => _items[position];
        public override long GetItemId(int position) => position;

        public override View GetView(int position, View? convertView, ViewGroup parent) {
            var item = _items[position];

            var view = convertView
                       ?? _context.LayoutInflater.Inflate(
                           Resource.Layout.item_carrito, parent, false)!;

            var txtNombre = view.FindViewById<TextView>(Resource.Id.txtNombreCarrito)!;
            var txtPresentacion = view.FindViewById<TextView>(Resource.Id.txtPresentacion);
            var txtCantidad = view.FindViewById<TextView>(Resource.Id.txtCantidad)!;
            var txtSubtotal = view.FindViewById<TextView>(Resource.Id.txtSubtotal)!;
            var btnRestar = view.FindViewById<Button>(Resource.Id.btnRestar)!;
            var btnSumar = view.FindViewById<Button>(Resource.Id.btnSumar)!;
            var btnEliminar = view.FindViewById<Button>(Resource.Id.btnEliminar)!;

            txtNombre.Text = item.Producto.Nombre;
            
            if (txtPresentacion != null) {
                if (item.IdPresentacion > 0 && item.Producto.Presentaciones?.Count > 0) {
                    var presentacion = item.Producto.Presentaciones.FirstOrDefault(p => p.Id == item.IdPresentacion);
                    txtPresentacion.Text = presentacion != null 
                        ? $"{presentacion.Cantidad} {presentacion.UnidadMedida}" 
                        : "Unidad base";
                } else {
                    txtPresentacion.Text = "Unidad base";
                }
            }
            
            txtCantidad.Text = item.Cantidad.ToString("G");
            txtSubtotal.Text = item.Subtotal.ToString("C2");

            // Limpiar y volver a asignar listeners
            btnRestar.Click -= OnRestar;
            btnSumar.Click -= OnSumar;
            btnEliminar.Click -= OnEliminar;

            btnRestar.Tag = position;
            btnSumar.Tag = position;
            btnEliminar.Tag = position;

            btnRestar.Click += OnRestar;
            btnSumar.Click += OnSumar;
            btnEliminar.Click += OnEliminar;

            return view;

            void OnRestar(object? s, EventArgs e) {
                if (s is Button b && b.Tag is Java.Lang.Integer p)
                    _onRestar(_items[(int) p]);
            }

            void OnSumar(object? s, EventArgs e) {
                if (s is Button b && b.Tag is Java.Lang.Integer p)
                    _onSumar(_items[(int) p]);
            }

            void OnEliminar(object? s, EventArgs e) {
                if (s is Button b && b.Tag is Java.Lang.Integer p)
                    _onEliminar(_items[(int) p]);
            }
        }
    }
}
