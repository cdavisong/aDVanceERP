// ============================================================
//  aDVancePOS.Mobile — CobroCompletarEsperaActivity
//  Archivo: CobroCompletarEsperaActivity.cs
//
//  Variante de CobroActivity para completar una venta que fue
//  archivada en espera. Muestra los pagos ya registrados y
//  permite agregar los pagos faltantes hasta saldar el total.
// ============================================================

using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

using Android.Content;
using Android.Views;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Completar venta",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class CobroCompletarEsperaActivity : Activity {
        public const string ExtraIdVentaLocal = "id_venta_local";

        private VentaService _ventaService = null!;
        private CatalogoService _catalogoService = null!;
        private CarritoService _carritoService = null!;

        private VentaExportacion _ventaEnEspera = null!;
        private List<MonedaCatalogo> _monedas = new();
        private MonedaCatalogo _monedaBase = null!;
        private MonedaCatalogo _monedaSeleccionada = null!;
        private readonly List<PagoExportacion> _pagosNuevos = new();

        private decimal _total;
        private decimal _yaPageado;

        private TextView _lblTotal = null!;
        private TextView _lblPendiente = null!;
        private LinearLayout _contenedorNuevos = null!;
        private TextView _lblSinNuevos = null!;
        private RadioButton _rbEfectivo = null!;
        private RadioButton _rbTransferencia = null!;
        private Spinner _spinnerMoneda = null!;
        private EditText _txtMonto = null!;
        private TextView _lblEquivalente = null!;
        private LinearLayout _panelTransferencia = null!;
        private EditText _txtNroTransferencia = null!;
        private EditText _txtTelefono = null!;
        private Button _btnAgregarPago = null!;
        private Button _btnConfirmar = null!;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);

            ActionBar?.Hide();

            // Reusa el mismo layout de cobro
            SetContentView(Resource.Layout.activity_cobro);

            var app = (PosApplication) Application!;

            _ventaService = app.VentaService;
            _catalogoService = app.CatalogoService;
            _carritoService = app.CarritoService;

            var idLocal = Intent?.GetStringExtra(ExtraIdVentaLocal) ?? "";

            _ventaEnEspera = _ventaService.ObtenerVentasEnEspera()
                .FirstOrDefault(v => v.IdLocal == idLocal)!;

            if (_ventaEnEspera == null) {
                Finish();
                return;
            }

            _total = _ventaEnEspera.ImporteTotal;
            _yaPageado = _ventaEnEspera.Pagos.Sum(p => p.MontoPagado);
            _monedas = _catalogoService.ObtenerMonedas();
            _monedaBase = _catalogoService.ObtenerMonedaBase();

            if (_monedas.Count == 0)
                _monedas.Add(_monedaBase);

            _monedaSeleccionada = _monedaBase;

            EnlazarVistas();
            ConfigurarMetodosPago();
            ConfigurarSpinnerMoneda();

            _txtMonto.TextChanged += (s, e) => ActualizarEquivalente();

            ConfigurarBotones();

            // Ocultar botón archivar — no tiene sentido en este contexto
            FindViewById<Button>(Resource.Id.btnArchivarEspera)!.Visibility = ViewStates.Gone;

            MostrarPagosExistentes();
            ActualizarResumen();
        }

        private void EnlazarVistas() {
            _lblTotal = FindViewById<TextView>(Resource.Id.lblTotalCobro)!;
            _lblPendiente = FindViewById<TextView>(Resource.Id.lblPendienteCobro)!;
            _contenedorNuevos = FindViewById<LinearLayout>(Resource.Id.contenedorPagosRegistrados)!;
            _lblSinNuevos = FindViewById<TextView>(Resource.Id.lblSinPagos)!;
            _rbEfectivo = FindViewById<RadioButton>(Resource.Id.rbEfectivo)!;
            _rbTransferencia = FindViewById<RadioButton>(Resource.Id.rbTransferencia)!;
            _spinnerMoneda = FindViewById<Spinner>(Resource.Id.spinnerMoneda)!;
            _txtMonto = FindViewById<EditText>(Resource.Id.txtMontoPago)!;
            _lblEquivalente = FindViewById<TextView>(Resource.Id.lblEquivalenteBase)!;
            _panelTransferencia = FindViewById<LinearLayout>(Resource.Id.panelTransferencia)!;
            _txtNroTransferencia = FindViewById<EditText>(Resource.Id.txtNroTransferencia)!;
            _txtTelefono = FindViewById<EditText>(Resource.Id.txtTelefonoRemitente)!;
            _btnAgregarPago = FindViewById<Button>(Resource.Id.btnAgregarPago)!;
            _btnConfirmar = FindViewById<Button>(Resource.Id.btnConfirmarCobro)!;

            _lblTotal.Text = $"Total: {_total:N2} {_monedaBase.Codigo}  ·  {_ventaEnEspera.NumeroTicket}";
            
            FindViewById<ImageButton>(Resource.Id.btnVolverCobro)!.Click +=
                (s, e) => Finish();
        }

        private void MostrarPagosExistentes() {
            _contenedorNuevos.RemoveAllViews();

            if (_ventaEnEspera.Pagos.Count == 0 && _pagosNuevos.Count == 0) {
                _contenedorNuevos.AddView(_lblSinNuevos);
                return;
            }

            foreach (var p in _ventaEnEspera.Pagos.Concat(_pagosNuevos))
                _contenedorNuevos.AddView(CrearFilaInfoPago(p));
        }

        private View CrearFilaInfoPago(PagoExportacion pago) {
            var fila = new LinearLayout(this) { 
                Orientation = Orientation.Horizontal 
            };

            fila.SetPadding(Dp(16), Dp(10), Dp(16), Dp(10));
            fila.SetGravity(GravityFlags.CenterVertical);

            var det = pago.DetallesMoneda.FirstOrDefault();
            string metodo = pago.MetodoPago == "TransferenciaBancaria"
                ? "Transferencia" : "Efectivo";

            var lbl = new TextView(this) {
                Text = $"{metodo} · {det?.CodigoMoneda ?? _monedaBase.Codigo}",
                TextSize = 13f
            };
            lbl.LayoutParameters = new LinearLayout.LayoutParams(
                0, LinearLayout.LayoutParams.WrapContent, 1f);
            lbl.SetTextColor(Android.Graphics.Color.ParseColor("#555555"));

            var mnt = new TextView(this) {
                Text = $"{pago.MontoPagado:N2} {_monedaBase.Codigo}",
                TextSize = 13f,
                Gravity = Android.Views.GravityFlags.End
            };
            mnt.SetTextColor(Android.Graphics.Color.ParseColor(
                _pagosNuevos.Contains(pago) ? "#B22222" : "#2E7D32"));

            fila.AddView(lbl);
            fila.AddView(mnt);

            var wrap = new LinearLayout(this) { Orientation = Orientation.Vertical };
            wrap.AddView(fila);
            var div = new View(this);
            div.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent, 1);
            div.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F0F0F0"));
            wrap.AddView(div);
            return wrap;
        }

        private void ConfigurarMetodosPago() {
            _rbEfectivo.Clickable = false;
            _rbTransferencia.Clickable = false;
            _rbEfectivo.Focusable = false;
            _rbTransferencia.Focusable = false;

            FindViewById<LinearLayout>(Resource.Id.tarjetaEfectivo)!.Click +=
                (s, e) => SeleccionarMetodo(false);
            FindViewById<LinearLayout>(Resource.Id.tarjetaTransferencia)!.Click +=
                (s, e) => SeleccionarMetodo(true);
            SeleccionarMetodo(false);
        }

        private void SeleccionarMetodo(bool esTransf) {
            _rbEfectivo.Checked = !esTransf;
            _rbTransferencia.Checked = esTransf;
            FindViewById<LinearLayout>(Resource.Id.tarjetaEfectivo)!
                .SetBackgroundResource(!esTransf
                    ? Resource.Drawable.btn_peachpuff
                    : Resource.Drawable.btn_outline_firebrick);
            FindViewById<LinearLayout>(Resource.Id.tarjetaTransferencia)!
                .SetBackgroundResource(esTransf
                    ? Resource.Drawable.btn_peachpuff
                    : Resource.Drawable.btn_outline_firebrick);
            _panelTransferencia.Visibility = esTransf
                ? ViewStates.Visible : ViewStates.Gone;
        }

        private void ConfigurarSpinnerMoneda() {
            var etiquetas = _monedas.Select(m => m.Etiqueta).ToArray();
            var adapter = new ArrayAdapter<string>(this,
                Android.Resource.Layout.SimpleSpinnerItem, etiquetas);
            adapter.SetDropDownViewResource(
                Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _spinnerMoneda.Adapter = adapter;
            _spinnerMoneda.ItemSelected += (s, e) => {
                _monedaSeleccionada = _monedas[e.Position];
                ActualizarEquivalente();
            };
        }

        private void ActualizarEquivalente() {
            if (_monedaSeleccionada.EsBase) {
                _lblEquivalente.Visibility = ViewStates.Gone;
                return;
            }
            var str = _txtMonto.Text?.Replace(',', '.') ?? "0";
            decimal.TryParse(str,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal monto);
            decimal enBase = Math.Round(monto * _monedaSeleccionada.TasaHoy, 2);
            _lblEquivalente.Text =
                $"≈ {enBase:N2} {_monedaBase.Codigo}  (×{_monedaSeleccionada.TasaHoy:N2})";
            _lblEquivalente.Visibility = ViewStates.Visible;
        }

        private void ConfigurarBotones() {
            _btnAgregarPago.Click += (s, e) => IntentarAgregarPago();
            _btnConfirmar.Click += async (s, e) => {
                _btnConfirmar.Enabled = false;
                try {
                    var venta = await _ventaService.CompletarVentaEnEsperaAsync(
                        _ventaEnEspera,
                        new List<PagoExportacion>(_pagosNuevos));
                    _carritoService.VaciarTrasVenta();

                    var resultado = new Intent();
                    resultado.PutExtra(CobroActivity.ExtraTicket, venta.NumeroTicket);
                    resultado.PutExtra(CobroActivity.ExtraResumen,
                        $"Venta completada\nTicket: {venta.NumeroTicket}\nTotal: {venta.ImporteTotal:N2}");
                    resultado.PutExtra(CobroActivity.ExtraEsEspera, false);
                    SetResult(Result.Ok, resultado);
                    Finish();
                } finally {
                    _btnConfirmar.Enabled = true;
                }
            };
        }

        private void IntentarAgregarPago() {
            var str = _txtMonto.Text?.Replace(',', '.') ?? "";
            if (!decimal.TryParse(str,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal monto) || monto <= 0) {
                Error("Ingrese un monto válido."); return;
            }
            bool esTransf = _rbTransferencia.Checked;
            var nro = _txtNroTransferencia.Text?.Trim() ?? "";
            if (esTransf && string.IsNullOrEmpty(nro)) {
                Error("El número de transacción es requerido."); return;
            }

            decimal montoBase = _monedaSeleccionada.EsBase
                ? monto
                : Math.Round(monto * _monedaSeleccionada.TasaHoy, 2);

            decimal pendiente = CalcularPendiente();
            if (montoBase > pendiente + 0.005m) {
                Error($"El monto excede el pendiente ({pendiente:N2} {_monedaBase.Codigo})."); return;
            }

            var detalle = new PagoDetalleMoneda {
                IdMoneda = _monedaSeleccionada.Id,
                CodigoMoneda = _monedaSeleccionada.Codigo,
                SimboloMoneda = _monedaSeleccionada.Simbolo,
                MontoMoneda = monto,
                MontoMonedaBase = montoBase,
                TasaCambioAplicada = _monedaSeleccionada.TasaHoy,
                NumeroTransaccion = esTransf && !string.IsNullOrEmpty(nro) ? nro : null,
                TelefonoRemitente = esTransf
                    ? _txtTelefono.Text?.Trim().NullIfEmpty() : null
            };

            _pagosNuevos.Add(new PagoExportacion {
                MetodoPago = esTransf ? "TransferenciaBancaria" : "Efectivo",
                MontoPagado = montoBase,
                FechaPagoCliente = DateTime.UtcNow,
                EstadoPago = esTransf ? "Pendiente" : "Confirmado",
                MontoMonedaBase = montoBase,
                TasaCambioAplicada = _monedaSeleccionada.TasaHoy,
                IdMoneda = _monedaSeleccionada.Id,
                DetallesMoneda = new List<PagoDetalleMoneda> { detalle }
            });

            _txtMonto.Text = "";
            _txtNroTransferencia.Text = "";
            _txtTelefono.Text = "";

            MostrarPagosExistentes();
            ActualizarResumen();
        }

        private decimal CalcularPendiente() {
            decimal totalPagado = _yaPageado +
                _pagosNuevos.Sum(p => p.MontoPagado);
            return Math.Max(0, _total - totalPagado);
        }

        private void ActualizarResumen() {
            decimal pendiente = CalcularPendiente();
            decimal totalPagado = _yaPageado + _pagosNuevos.Sum(p => p.MontoPagado);

            _lblPendiente.Text = pendiente > 0.005m
                ? $"Pendiente: {pendiente:N2} {_monedaBase.Codigo}"
                : $"Pagado: {totalPagado:N2} {_monedaBase.Codigo} ✓";
            _lblPendiente.SetTextColor(Android.Graphics.Color.ParseColor(
                pendiente > 0.005m ? "#FFCCCC" : "#CCFFCC"));

            bool cobrado = pendiente <= 0.005m;
            _btnConfirmar.Enabled = cobrado;
            _btnConfirmar.Alpha = cobrado ? 1.0f : 0.5f;
        }

        private void Error(string msg) =>
            new AlertDialog.Builder(this)!.SetMessage(msg)!
                .SetPositiveButton("OK", (s, e) => { })!.Show();

        private int Dp(int dp) =>
            (int) (dp * Resources!.DisplayMetrics!.Density);
    }
}
