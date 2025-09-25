using aDVancePOS.Mobile.Modelos;
using Android.Content;
using Android.Views;

namespace aDVancePOS.Mobile.Adaptadores {
    public class ProductoVendidoAdapter : ArrayAdapter<ProductoVendido> {
        public ProductoVendidoAdapter(Context context, List<ProductoVendido> productosVendidos)
            : base(context, Resource.Layout.producto_vendido_item, productosVendidos) {
        }

        public override View GetView(int position, View convertView, ViewGroup parent) {
            var item = GetItem(position);

            if (convertView == null) {
                convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.producto_vendido_item, parent, false);
            }

            var txtNombre = convertView.FindViewById<TextView>(Resource.Id.txtNombre);
            var txtCantidad = convertView.FindViewById<TextView>(Resource.Id.txtCantidad);
            var txtSubtotal = convertView.FindViewById<TextView>(Resource.Id.txtSubtotal);

            txtNombre.Text = item.Producto.nombre;
            txtCantidad.Text = $"{item.Cantidad:N2}";
            txtSubtotal.Text = $"{(item.Producto.precio_venta_base * item.Cantidad):N2}";

            return convertView;
        }
    }
}