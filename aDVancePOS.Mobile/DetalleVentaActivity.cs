// ============================================================
//  aDVancePOS.Mobile — DetalleVentaActivity
//  Archivo: DetalleVentaActivity.cs
//
//  Muestra productos, pagos y total de una venta individual.
//  Recibe el IdLocal de la venta como Extra.
// ============================================================

using aDVancePOS.Mobile.Modelos;

using Android.Views;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Detalle de venta",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DetalleVentaActivity : Activity {
        public const string ExtraIdVenta = "id_venta";

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);

            ActionBar?.Hide();

            SetContentView(Resource.Layout.activity_detalle_venta);

            FindViewById<ImageButton>(Resource.Id.btnVolverDetalle)!.Click += (s, e) => Finish();

            var idVenta = Intent?.GetStringExtra(ExtraIdVenta) ?? "";
            var ventaService = ((PosApplication) Application!).VentaService;
            var venta = ventaService.ObtenerVentasDia().FirstOrDefault(v => v.IdLocal == idVenta);

            if (venta == null) {
                Finish();
                return;
            }

            CargarVista(venta);
        }

        private void CargarVista(VentaExportacion venta) {
            FindViewById<TextView>(Resource.Id.lblTituloDetalle)!.Text = venta.NumeroTicket;
            FindViewById<TextView>(Resource.Id.lblDetalleTicket)!.Text = venta.NumeroTicket;
            FindViewById<TextView>(Resource.Id.lblDetalleFecha)!.Text = venta.FechaVenta.ToLocalTime().ToString("dddd dd/MM/yyyy  HH:mm");
            FindViewById<TextView>(Resource.Id.lblDetalleEstado)!.Text = venta.EstadoVenta;
            FindViewById<TextView>(Resource.Id.lblDetalleTotal)!.Text = $"{venta.ImporteTotal:N2} CUP";

            var contenedorItems = FindViewById<LinearLayout>(Resource.Id.contenedorItemsDetalle)!;

            contenedorItems.RemoveAllViews();

            var catalogoService = ((PosApplication) Application!).CatalogoService;

            foreach (var item in venta.Detalles) {
                var fila = new LinearLayout(this) {
                    Orientation = Orientation.Horizontal
                };

                fila.SetPadding(Dp(16), Dp(12), Dp(16), Dp(12));

                var nombre = catalogoService
                    .BuscarPorId(item.IdProducto)?
                    .Nombre ?? $"Producto #{item.IdProducto}";

                var lblNombre = new TextView(this) {
                    Text = $"{nombre}  ×{item.Cantidad}",
                    TextSize = 14f
                };

                lblNombre.SetTextColor(Android.Graphics.Color.ParseColor("#1A1A1A"));
                lblNombre.LayoutParameters = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent, 1f);

                var lblSubtotal = new TextView(this) {
                    Text = $"{item.Subtotal:N2}",
                    TextSize = 14f
                };

                lblSubtotal.SetTextColor(Android.Graphics.Color.ParseColor("#333333"));

                fila.AddView(lblNombre);
                fila.AddView(lblSubtotal);

                // Divisor
                var divisor = new View(this);

                divisor.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, Dp(1));
                divisor.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F0F0F0"));

                contenedorItems.AddView(fila);
                contenedorItems.AddView(divisor);
            }

            var contenedorPagos = FindViewById<LinearLayout>(Resource.Id.contenedorPagosDetalle)!;

            contenedorPagos.RemoveAllViews();

            foreach (var pago in venta.Pagos) {
                var fila = new LinearLayout(this) { Orientation = Orientation.Horizontal };

                fila.SetPadding(Dp(16), Dp(12), Dp(16), Dp(12));
                fila.SetGravity(GravityFlags.CenterVertical);

                // Ícono
                var img = new ImageView(this);

                img.LayoutParameters = new LinearLayout.LayoutParams(Dp(24), Dp(24));
                ((LinearLayout.LayoutParams) img.LayoutParameters).SetMargins(0, 0, Dp(12), 0);

                string icono = pago.MetodoPago == "TransferenciaBancaria" ? "ic_transfer" : "ic_cash";
                var idRes = Resources!.GetIdentifier(icono, "drawable", PackageName);

                if (idRes != 0)
                    img.SetImageResource(idRes);

                var lblMetodo = new TextView(this) {
                    Text = pago.MetodoPago == "TransferenciaBancaria"
                        ? "Transferencia"
                        : "Efectivo",
                    TextSize = 14f
                };

                lblMetodo.SetTextColor(Android.Graphics.Color.ParseColor("#1A1A1A"));
                lblMetodo.LayoutParameters = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent, 1f);

                var lblMonto = new TextView(this) {
                    Text = $"{pago.MontoPagado:N2} CUP",
                    TextSize = 14f
                };

                lblMonto.SetTextColor(Android.Graphics.Color.ParseColor("#B22222"));

                fila.AddView(img);
                fila.AddView(lblMetodo);
                fila.AddView(lblMonto);

                // Nro transacción si existe
                if (pago.DetalleTransferencia != null && !string.IsNullOrEmpty(pago.DetalleTransferencia.NumeroTransaccion)) {
                    var filaExtra = new LinearLayout(this) {
                        Orientation = Orientation.Horizontal
                    };

                    filaExtra.SetPadding(Dp(52), 0, Dp(16), Dp(8));

                    var lblNroTrans = new TextView(this) {
                        Text = $"Nro: {pago.DetalleTransferencia.NumeroTransaccion}",
                        TextSize = 12f
                    };

                    lblNroTrans.SetTextColor(Android.Graphics.Color.ParseColor("#888888"));

                    filaExtra.AddView(lblNroTrans);

                    contenedorPagos.AddView(fila);
                    contenedorPagos.AddView(filaExtra);
                } else {
                    contenedorPagos.AddView(fila);
                }
            }
        }

        private int Dp(int dp) => (int) (dp * Resources!.DisplayMetrics!.Density);
    }
}
