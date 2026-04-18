// ============================================================
//  aDVancePOS.Mobile — ResumenVentasActivity
//  Archivo: ResumenVentasActivity.cs
//
//  Muestra tarjetas de totales + lista de ventas del día.
//  Al pulsar una venta abre DetalleVentaActivity.
// ============================================================

using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Ventas del día",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ResumenVentasActivity : Activity {

        public const int RequestCode = 3002;

        private VentaService _ventaService = null!;

        private TextView _lblTotalVentas      = null!;
        private TextView _lblTotalRecaudado   = null!;
        private TextView _lblEfectivo         = null!;
        private TextView _lblTransferencia    = null!;
        private ListView _lstVentas           = null!;
        private TextView _lblVacio            = null!;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_resumen_ventas);

            _ventaService = ((PosApplication)Application!).VentaService;

            _lblTotalVentas    = FindViewById<TextView>(Resource.Id.lblResumenTotalVentas)!;
            _lblTotalRecaudado = FindViewById<TextView>(Resource.Id.lblResumenTotalRecaudado)!;
            _lblEfectivo       = FindViewById<TextView>(Resource.Id.lblResumenEfectivo)!;
            _lblTransferencia  = FindViewById<TextView>(Resource.Id.lblResumenTransferencia)!;
            _lstVentas         = FindViewById<ListView>(Resource.Id.lstVentasDia)!;
            _lblVacio          = FindViewById<TextView>(Resource.Id.lblResumenVacio)!;

            FindViewById<ImageButton>(Resource.Id.btnVolverResumen)!.Click +=
                (s, e) => Finish();

            CargarDatos();

            _lstVentas.ItemClick += (s, e) => {
                var venta = _ventaService.ObtenerVentasDia()[e.Position];
                var intent = new Intent(this, typeof(DetalleVentaActivity));
                intent.PutExtra(DetalleVentaActivity.ExtraIdVenta, venta.IdLocal);
                StartActivity(intent);
            };
        }

        protected override void OnResume() {
            base.OnResume();
            CargarDatos(); // refrescar si se volvió de DetalleVenta
        }

        private void CargarDatos() {
            var ventas  = _ventaService.ObtenerVentasDia();
            var resumen = _ventaService.ObtenerResumenPorMetodo();

            _lblTotalVentas.Text    = ventas.Count.ToString();
            _lblTotalRecaudado.Text = $"{_ventaService.TotalRecaudadoHoy:N2} CUP";
            _lblEfectivo.Text       = $"{resumen.Efectivo:N2} CUP";
            _lblTransferencia.Text  = $"{resumen.Transferencia:N2} CUP";

            if (ventas.Count == 0) {
                _lstVentas.Visibility = ViewStates.Gone;
                _lblVacio.Visibility  = ViewStates.Visible;
                return;
            }

            _lstVentas.Visibility = ViewStates.Visible;
            _lblVacio.Visibility  = ViewStates.Gone;
            _lstVentas.Adapter    = new VentaResumenAdapter(this, ventas);
        }
    }

    // ── Adapter inline ────────────────────────────────────────
    internal class VentaResumenAdapter : BaseAdapter<VentaExportacion> {
        private readonly Activity               _ctx;
        private readonly List<VentaExportacion> _ventas;

        public VentaResumenAdapter(Activity ctx, List<VentaExportacion> ventas) {
            _ctx    = ctx;
            _ventas = ventas;
        }

        public override int     Count               => _ventas.Count;
        public override VentaExportacion this[int i] => _ventas[i];
        public override long    GetItemId(int pos)   => pos;

        public override View GetView(int pos, View? convertView, ViewGroup parent) {
            var view = convertView
                ?? _ctx.LayoutInflater.Inflate(Resource.Layout.item_venta_resumen, parent, false)!;

            var venta = _ventas[pos];

            view.FindViewById<TextView>(Resource.Id.lblTicketVenta)!.Text =
                venta.NumeroTicket;
            view.FindViewById<TextView>(Resource.Id.lblHoraVenta)!.Text =
                venta.FechaVenta.ToLocalTime().ToString("HH:mm");
            view.FindViewById<TextView>(Resource.Id.lblImporteVenta)!.Text =
                $"{venta.ImporteTotal:N2} CUP";

            // Ícono según método de pago principal
            var img    = view.FindViewById<ImageView>(Resource.Id.imgMetodoPagoVenta)!;
            bool tieneEfectivo     = venta.Pagos.Any(p => p.MetodoPago == "Efectivo");
            bool tieneTransferencia = venta.Pagos.Any(p => p.MetodoPago == "TransferenciaBancaria");

            string icono = tieneEfectivo && tieneTransferencia ? "ic_hybrid"
                         : tieneTransferencia                   ? "ic_transfer"
                         : "ic_cash";

            int idRes = _ctx.Resources!.GetIdentifier(icono, "drawable", _ctx.PackageName);
            if (idRes != 0)
                img.SetImageResource(idRes);
            else
                img.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F5E6E6"));

            return view;
        }
    }
}
