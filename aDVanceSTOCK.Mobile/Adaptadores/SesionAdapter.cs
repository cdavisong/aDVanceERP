// ============================================================
//  aDVanceSTOCK.Mobile — SesionAdapter
//  Archivo: Adaptadores/SesionAdapter.cs
//
//  Muestra la lista de productos registrados en la sesión.
//  Cada ítem muestra:
//    - Badge NUEVO (verde) o ENTRADA STOCK (azul)
//    - Código y nombre
//    - Cantidad y precio (si aplica)
//    - Ícono de imagen si tiene foto
//    - Botón eliminar
// ============================================================

using aDVanceSTOCK.Mobile.Modelos;
using Android.App;
using Android.Views;
using Android.Widget;

namespace aDVanceSTOCK.Mobile.Adaptadores {

    internal class SesionAdapter : BaseAdapter<ProductoSesion> {

        private readonly Activity               _ctx;
        private List<ProductoSesion>            _items;
        private readonly Action<ProductoSesion> _onEliminar;

        public SesionAdapter(Activity ctx, List<ProductoSesion> items,
            Action<ProductoSesion> onEliminar) {
            _ctx        = ctx;
            _items      = items;
            _onEliminar = onEliminar;
        }

        public override int Count => _items.Count;
        public override ProductoSesion this[int pos] => _items[pos];
        public override long GetItemId(int pos) => pos;

        public void ActualizarLista(List<ProductoSesion> nuevos) {
            _items = nuevos;
            NotifyDataSetChanged();
        }

        public override View GetView(int pos, View? convertView, ViewGroup parent) {
            var view = convertView
                ?? _ctx.LayoutInflater.Inflate(Resource.Layout.item_sesion, parent, false)!;

            var item = _items[pos];

            var lblBadge    = view.FindViewById<TextView>(Resource.Id.lblBadgeTipo)!;
            var lblCodigo   = view.FindViewById<TextView>(Resource.Id.lblItemCodigo)!;
            var lblNombre   = view.FindViewById<TextView>(Resource.Id.lblItemNombre)!;
            var lblCantidad = view.FindViewById<TextView>(Resource.Id.lblItemCantidad)!;
            var lblPrecio   = view.FindViewById<TextView>(Resource.Id.lblItemPrecio)!;
            var icnFoto     = view.FindViewById<ImageView>(Resource.Id.icnFoto)!;
            var btnEliminar = view.FindViewById<ImageButton>(Resource.Id.btnEliminarItem)!;

            // Badge tipo
            lblBadge.Text = item.EtiquetaTipo;
            if (item.Tipo == TipoRegistro.Nuevo) {
                lblBadge.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2E7D32"));
            } else {
                lblBadge.SetBackgroundColor(Android.Graphics.Color.ParseColor("#1565C0"));
            }

            lblCodigo.Text   = item.Codigo;
            lblNombre.Text   = item.Tipo == TipoRegistro.Nuevo
                ? item.Nombre
                : "(Producto existente)";

            lblCantidad.Text = item.Tipo == TipoRegistro.Nuevo
                ? $"Stock inicial: {item.Cantidad:N0} {item.NombreUnidadMedida}"
                : $"Cantidad adicional: {item.Cantidad:N0}";

            lblPrecio.Visibility = item.Tipo == TipoRegistro.Nuevo
                ? ViewStates.Visible
                : ViewStates.Gone;
            if (item.Tipo == TipoRegistro.Nuevo) {
                lblPrecio.Text = $"Costo: {item.CostoAdquisicionUnitario:N2}  ·  " +
                                 $"Precio: {item.PrecioVentaBase:N2}";
            }

            icnFoto.Visibility = item.TieneImagen
                ? ViewStates.Visible
                : ViewStates.Gone;

            btnEliminar.Click += (s, e) => _onEliminar(item);

            return view;
        }
    }
}
