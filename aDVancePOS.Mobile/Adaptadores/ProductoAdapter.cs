using aDVancePOS.Mobile;
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
            var view = convertView ?? _context.LayoutInflater
                .Inflate(Resource.Layout.item_producto, parent, false)!;

            var txtNombre = view.FindViewById<TextView>(Resource.Id.txtNombreProducto)!;
            var txtCodigo = view.FindViewById<TextView>(Resource.Id.txtCodigoProducto)!;
            var txtPrecio = view.FindViewById<TextView>(Resource.Id.txtPrecioProducto)!;
            var txtChip = view.FindViewById<TextView>(Resource.Id.txtChipPresentaciones)!;
            var btnAgregar = view.FindViewById<Button>(Resource.Id.btnAgregar)!;

            txtNombre.Text = producto.Nombre;
            txtCodigo.Text = $"CÓD: {producto.Codigo}  ·  Cantidad: {producto.StockEnSesion:N1} {producto.UnidadMedida}";
            txtPrecio.Text = $"$ {producto.PrecioConImpuesto:N2}";

            var presentacionesActivas = producto.Presentaciones
                .Where(p => p.Activo)
                .ToList();
            
            var tieneVarias = presentacionesActivas.Count > 0;

            // Chip "Varias presentaciones"
            txtChip.Visibility = tieneVarias
                ? ViewStates.Visible
                : ViewStates.Gone;

            // Deshabilitar botón si no hay stock
            var sinStock = producto.StockEnSesion <= 0;

            btnAgregar.Enabled = !sinStock;
            btnAgregar.Alpha = sinStock ? 0.3f : 1.0f;
            btnAgregar.Tag = position;
            btnAgregar.Click -= OnAgregarClick;
            btnAgregar.Click += OnAgregarClick;

            return view;

            void OnAgregarClick(object? s, EventArgs e) {
                if (s is not Button btn || btn.Tag is not Java.Lang.Integer pos) 
                    return;

                var prod = _productos[(int) pos];
                var activas = prod.Presentaciones.Where(p => p.Activo).ToList();

                // Siempre mostrar diálogo si hay presentaciones activas (1 o más)
                // Si no hay presentaciones, usar precio base directamente
                if (activas.Count > 0)
                    MostrarDialogoPresentaciones(prod, activas);
                else
                    _onAgregar(prod, 0, prod.PrecioConImpuesto); // precio base
            }
        }

        private void MostrarDialogoPresentaciones(
            ProductoCatalogo producto,
            List<PresentacionVenta> presentaciones) {

            var opciones = new List<(string Etiqueta, long IdPresentacion, decimal Precio)>();

            // Unidad base siempre primera
            opciones.Add((
                $"1 {producto.UnidadMedida}  —  {producto.PrecioConImpuesto:N2} CUP",
                0,
                producto.PrecioConImpuesto));

            foreach (var p in presentaciones)
                opciones.Add((
                    $"{p.Cantidad:N0} {p.UnidadMedida}  —  {p.PrecioVenta:N2} CUP",
                    p.Id,
                    p.PrecioVenta));

            var etiquetas = opciones.Select(o => o.Etiqueta).ToArray();

            DialogHelper.MostrarLista(
                _context,
                titulo:       producto.Nombre,
                items:        etiquetas,
                onElegir:     idx => {
                    var elegida = opciones[idx];
                    _onAgregar(producto, elegida.IdPresentacion, elegida.Precio);
                });
        }
    }
}
