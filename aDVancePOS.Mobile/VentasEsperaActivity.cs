// ============================================================
//  aDVancePOS.Mobile — VentasEsperaActivity
//  Archivo: VentasEsperaActivity.cs
//
//  Muestra la lista de ventas archivadas en espera de confirmación
//  de transferencia. Desde aquí el cajero puede:
//    - Completar: abre CobroActivity para agregar el pago faltante
//    - Cancelar: devuelve el stock y elimina la venta
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
        Label = "Ventas en espera",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class VentasEsperaActivity : Activity {

        public const int RequestCode = 3003;

        private VentaService    _ventaService    = null!;
        private CatalogoService _catalogoService = null!;
        private CarritoService  _carritoService  = null!;

        private ListView _lstEspera   = null!;
        private TextView _lblSinEspera = null!;
        private TextView _lblConteo   = null!;

        // Venta seleccionada para completar (se restaura al carrito)
        private VentaExportacion? _ventaACompletar;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_ventas_espera);

            var app = (PosApplication)Application!;
            _ventaService    = app.VentaService;
            _catalogoService = app.CatalogoService;
            _carritoService  = app.CarritoService;

            _lstEspera    = FindViewById<ListView>(Resource.Id.lstVentasEspera)!;
            _lblSinEspera = FindViewById<TextView>(Resource.Id.lblSinEspera)!;
            _lblConteo    = FindViewById<TextView>(Resource.Id.lblConteoEspera)!;

            FindViewById<ImageButton>(Resource.Id.btnVolverEspera)!.Click +=
                (s, e) => Finish();

            CargarLista();

            _lstEspera.ItemClick += (s, e) =>
                MostrarOpcionesVenta(
                    _ventaService.ObtenerVentasEnEspera()[e.Position]);
        }

        protected override void OnResume() {
            base.OnResume();
            CargarLista();
        }

        private void CargarLista() {
            var ventas = _ventaService.ObtenerVentasEnEspera();
            _lblConteo.Text = $"{ventas.Count} pendiente(s)";

            if (ventas.Count == 0) {
                _lstEspera.Visibility    = ViewStates.Gone;
                _lblSinEspera.Visibility = ViewStates.Visible;
                return;
            }

            _lstEspera.Visibility    = ViewStates.Visible;
            _lblSinEspera.Visibility = ViewStates.Gone;
            _lstEspera.Adapter = new VentaEsperaAdapter(this, ventas);
        }

        private void MostrarOpcionesVenta(VentaExportacion venta) {
            decimal pagado    = venta.Pagos.Sum(p => p.MontoPagado);
            decimal pendiente = venta.ImporteTotal - pagado;

            new AlertDialog.Builder(this)!
                .SetTitle($"Ticket: {venta.NumeroTicket}")!
                .SetMessage(
                    $"Total: {venta.ImporteTotal:N2}\n" +
                    $"Pagado: {pagado:N2}\n" +
                    $"Pendiente: {pendiente:N2}\n\n" +
                    $"¿Qué desea hacer?")!
                .SetPositiveButton("Completar cobro", (s, e) =>
                    CompletarVenta(venta))!
                .SetNeutralButton("Cancelar venta", (s, e) =>
                    ConfirmarCancelar(venta))!
                .SetNegativeButton("Cerrar", (s, e) => { })!
                .Show();
        }

        private void CompletarVenta(VentaExportacion venta) {
            // Restaurar los ítems de la venta al carrito para que
            // CobroActivity vea el total correcto y pueda registrar
            // los pagos restantes.
            // Nota: el stock ya estaba descontado cuando se archivó,
            // así que NO modificamos StockEnSesion aquí.
            _carritoService.RestaurarDesdeVentaEnEspera(venta, _catalogoService);
            _ventaACompletar = venta;

            // Abrir CobroActivity en modo "completar espera"
            var intent = new Intent(this, typeof(CobroCompletarEsperaActivity));
            intent.PutExtra(CobroCompletarEsperaActivity.ExtraIdVentaLocal,
                venta.IdLocal);
            StartActivityForResult(intent, CobroActivity.RequestCode);
        }

        private void ConfirmarCancelar(VentaExportacion venta) {
            new AlertDialog.Builder(this)!
                .SetTitle("Cancelar venta")!
                .SetMessage(
                    $"Se cancelará el ticket {venta.NumeroTicket} y " +
                    $"el stock de {venta.Detalles.Count} producto(s) " +
                    $"será devuelto al catálogo.\n\n¿Confirma?")!
                .SetPositiveButton("Sí, cancelar", async (s, e) => {
                    await _ventaService.CancelarVentaEnEsperaAsync(
                        venta, _catalogoService);
                    CargarLista();
                    Toast.MakeText(this, "Venta cancelada y stock devuelto.",
                        ToastLength.Short)?.Show();
                })!
                .SetNegativeButton("No", (s, e) => { })!
                .Show();
        }

        protected override void OnActivityResult(
            int requestCode, Result resultCode, Intent? data) {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == CobroActivity.RequestCode && resultCode == Result.Ok) {
                CargarLista();
                // Propagar el resultado hacia MainActivity
                SetResult(Result.Ok, data);
            }
        }
    }

    // ── Adapter ───────────────────────────────────────────────
    internal class VentaEsperaAdapter : BaseAdapter<VentaExportacion> {
        private readonly Activity               _ctx;
        private readonly List<VentaExportacion> _ventas;

        public VentaEsperaAdapter(Activity ctx, List<VentaExportacion> ventas) {
            _ctx    = ctx;
            _ventas = ventas;
        }

        public override int            Count             => _ventas.Count;
        public override VentaExportacion this[int i]    => _ventas[i];
        public override long           GetItemId(int pos) => pos;

        public override View GetView(int pos, View? cv, ViewGroup parent) {
            var view = cv
                ?? _ctx.LayoutInflater.Inflate(
                    Resource.Layout.item_venta_espera, parent, false)!;

            var venta     = _ventas[pos];
            decimal pagado    = venta.Pagos.Sum(p => p.MontoPagado);
            decimal pendiente = venta.ImporteTotal - pagado;

            view.FindViewById<TextView>(Resource.Id.lblTicketEspera)!.Text =
                venta.NumeroTicket;
            view.FindViewById<TextView>(Resource.Id.lblTotalEspera)!.Text =
                $"{venta.ImporteTotal:N2}";
            view.FindViewById<TextView>(Resource.Id.lblFechaEspera)!.Text =
                venta.FechaVenta.ToLocalTime().ToString("HH:mm  dd/MM");
            view.FindViewById<TextView>(Resource.Id.lblPendienteEspera)!.Text =
                pendiente > 0 ? $"Pendiente: {pendiente:N2}" : "Pagado ✓";

            return view;
        }
    }
}
