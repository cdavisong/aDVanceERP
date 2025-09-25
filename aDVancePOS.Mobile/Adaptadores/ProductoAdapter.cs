using aDVancePOS.Mobile.Modelos;

using Android.Content;
using Android.Views;

namespace aDVancePOS.Mobile.Adaptadores {
    public class ProductoAdapter : ArrayAdapter<Producto> {
        public ProductoAdapter(Context context, List<Producto> productos)
            : base(context, Resource.Layout.producto_item, productos) {
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            var producto = GetItem(position);

            if (convertView == null) {
                convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.producto_item, parent, false);
            }

            var txtNombre = convertView.FindViewById<TextView>(Resource.Id.txtNombre);
            var txtPrecio = convertView.FindViewById<TextView>(Resource.Id.txtPrecio);
            var txtStock = convertView.FindViewById<TextView>(Resource.Id.txtStock);

            txtNombre.Text = producto.nombre;
            txtPrecio.Text = $"${producto.precio_venta_base:N2}";
            txtStock.Text = $"Cantidad: {producto.cantidad:N2}";

            return convertView;
        }
    }
}