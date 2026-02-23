// ============================================================
//  aDVancePOS.Mobile — CobroActivity
//  Archivo: CobroActivity.cs
//
//  Reemplaza el diálogo MostrarDialogoCobro().
//  Recibe el total y el carrito serializado vía Intent extras,
//  realiza el cobro y devuelve Result.Ok a MainActivity.
//
//  Extras de entrada:
//    EXTRA_TOTAL  (string)  — total formateado para mostrar
//
//  La venta se registra directamente en VentaService desde aquí.
//  Al terminar: SetResult(Result.Ok) → MainActivity limpia carrito.
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
        Label = "Cobrar",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class CobroActivity : Activity {

        public const string ExtraTotalFormateado = "total_formateado";
        public const string ExtraTicket          = "ticket";
        public const string ExtraResumen         = "resumen";
        public const int    RequestCode          = 3001;

        // ── Servicios ────────────────────────────────────────
        private CarritoService _carritoService = null!;
        private VentaService   _ventaService   = null!;
        private decimal        _total;

        // ── UI ───────────────────────────────────────────────
        private TextView     _lblTotalCobro      = null!;
        private RadioButton  _rbEfectivo         = null!;
        private RadioButton  _rbTransferencia    = null!;
        private RadioButton  _rbMixto            = null!;
        private LinearLayout _panelTransferencia = null!;
        private LinearLayout _panelMixto         = null!;
        private TextView     _lblEstadoSms       = null!;
        private EditText     _txtTransaccion      = null!;
        private EditText     _txtConfirmacion     = null!;
        private TextView     _lblEstadoSmsMixto  = null!;
        private TextView     _lblBalanceMixto     = null!;
        private EditText     _txtEfectivoMixto   = null!;
        private EditText     _txtTransaccionMixto = null!;
        private EditText     _txtConfirmacionMixto = null!;
        private Button       _btnConfirmar        = null!;

        // ── SMS ──────────────────────────────────────────────
        private SmsTransferenciaBroadcastReceiver? _smsReceiver;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_cobro);

            // Obtener servicios desde el Application (singleton compartido con MainActivity)
            var app = (PosApplication)Application!;
            _carritoService = app.CarritoService;
            _ventaService   = app.VentaService;
            _total          = _carritoService.ImporteTotal;

            EnlazarVistas();
            ConfigurarMetodosPago();
            ConfigurarSmsReceiver();
            ConfigurarBotonConfirmar();

            _lblTotalCobro.Text =
                $"Cobrar — {_total:N2} CUP";
            _lblBalanceMixto.Text = $"Pendiente transferencia: {_total:N2} CUP";

            FindViewById<ImageButton>(Resource.Id.btnVolverCobro)!.Click +=
                (s, e) => { DesregistrarSmsReceiver(); Finish(); };
        }

        private void EnlazarVistas() {
            _lblTotalCobro       = FindViewById<TextView>(Resource.Id.lblTotalCobro)!;
            _rbEfectivo          = FindViewById<RadioButton>(Resource.Id.rbEfectivo)!;
            _rbTransferencia     = FindViewById<RadioButton>(Resource.Id.rbTransferencia)!;
            _rbMixto             = FindViewById<RadioButton>(Resource.Id.rbMixto)!;
            _panelTransferencia  = FindViewById<LinearLayout>(Resource.Id.panelTransferencia)!;
            _panelMixto          = FindViewById<LinearLayout>(Resource.Id.panelMixto)!;
            _lblEstadoSms        = FindViewById<TextView>(Resource.Id.lblEstadoSms)!;
            _txtTransaccion      = FindViewById<EditText>(Resource.Id.txtTransaccion)!;
            _txtConfirmacion     = FindViewById<EditText>(Resource.Id.txtConfirmacion)!;
            _lblEstadoSmsMixto   = FindViewById<TextView>(Resource.Id.lblEstadoSmsMixto)!;
            _lblBalanceMixto     = FindViewById<TextView>(Resource.Id.lblBalanceMixto)!;
            _txtEfectivoMixto    = FindViewById<EditText>(Resource.Id.txtEfectivoMixto)!;
            _txtTransaccionMixto = FindViewById<EditText>(Resource.Id.txtTransaccionMixto)!;
            _txtConfirmacionMixto = FindViewById<EditText>(Resource.Id.txtConfirmacionMixto)!;
            _btnConfirmar        = FindViewById<Button>(Resource.Id.btnConfirmarCobro)!;
        }

        private void ConfigurarMetodosPago() {
            // Tarjetas táctiles — tocar la fila activa el radio
            FindViewById<LinearLayout>(Resource.Id.tarjetaEfectivo)!.Click +=
                (s, e) => { _rbEfectivo.Checked = true; ActualizarPaneles(); };
            FindViewById<LinearLayout>(Resource.Id.tarjetaTransferencia)!.Click +=
                (s, e) => { _rbTransferencia.Checked = true; ActualizarPaneles(); };
            FindViewById<LinearLayout>(Resource.Id.tarjetaMixto)!.Click +=
                (s, e) => { _rbMixto.Checked = true; ActualizarPaneles(); };

            _rbEfectivo.CheckedChange      += (s, e) => { if (e.IsChecked) ActualizarPaneles(); };
            _rbTransferencia.CheckedChange += (s, e) => { if (e.IsChecked) ActualizarPaneles(); };
            _rbMixto.CheckedChange         += (s, e) => { if (e.IsChecked) ActualizarPaneles(); };

            // Validación en tiempo real del monto efectivo en modo mixto
            _txtEfectivoMixto.TextChanged += (s, e) => ActualizarBalanceMixto();
        }

        private void ActualizarPaneles() {
            _panelTransferencia.Visibility = _rbTransferencia.Checked || _rbMixto.Checked
                ? ViewStates.Visible : ViewStates.Gone;
            _panelMixto.Visibility = _rbMixto.Checked
                ? ViewStates.Visible : ViewStates.Gone;
        }

        private void ActualizarBalanceMixto() {
            var str = _txtEfectivoMixto.Text?.Replace(',', '.') ?? "0";
            decimal.TryParse(str,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal ef);

            decimal transf = _total - ef;
            if (ef <= 0 || ef >= _total) {
                _lblBalanceMixto.Text = ef >= _total
                    ? "El efectivo cubre el total — use solo Efectivo"
                    : $"Pendiente transferencia: {_total:N2} CUP";
                _lblBalanceMixto.SetTextColor(
                    Android.Graphics.Color.ParseColor("#E65100"));
            } else {
                _lblBalanceMixto.Text =
                    $"Transferencia: {transf:N2} CUP  ✓";
                _lblBalanceMixto.SetTextColor(
                    Android.Graphics.Color.ParseColor("#2E7D32"));
            }
        }

        // ── SMS receiver ──────────────────────────────────────

        private void ConfigurarSmsReceiver() {
            _smsReceiver = new SmsTransferenciaBroadcastReceiver();
            _smsReceiver.OnPagoDetectado = resultado => RunOnUiThread(() => {
                if (_rbTransferencia.Checked) {
                    _txtTransaccion.Text = resultado.NumeroTransaccion;
                    bool ok = resultado.Monto == _total;
                    _lblEstadoSms.Text = ok
                        ? $"✓ SMS · {resultado.Monto:N2} CUP"
                        : $"⚠ SMS: {resultado.Monto:N2} CUP (total: {_total:N2})";
                    _lblEstadoSms.SetTextColor(Android.Graphics.Color.ParseColor(
                        ok ? "#2E7D32" : "#E65100"));

                } else if (_rbMixto.Checked) {
                    _txtTransaccionMixto.Text = resultado.NumeroTransaccion;
                    var str = _txtEfectivoMixto.Text?.Replace(',', '.') ?? "0";
                    decimal.TryParse(str,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal ef);
                    decimal esperado = _total - ef;
                    bool ok = resultado.Monto == esperado;
                    _lblEstadoSmsMixto.Text = ok
                        ? $"✓ SMS · {resultado.Monto:N2} CUP"
                        : $"⚠ SMS: {resultado.Monto:N2} (esperado: {esperado:N2})";
                    _lblEstadoSmsMixto.SetTextColor(Android.Graphics.Color.ParseColor(
                        ok ? "#2E7D32" : "#E65100"));
                }
            });

            RegisterReceiver(_smsReceiver,
                new IntentFilter(
                    Android.Provider.Telephony.Sms.Intents.SmsReceivedAction));
        }

        private void DesregistrarSmsReceiver() {
            if (_smsReceiver == null) return;
            try { UnregisterReceiver(_smsReceiver); } catch { }
            _smsReceiver = null;
        }

        // ── Confirmar cobro ───────────────────────────────────

        private void ConfigurarBotonConfirmar() {
            _btnConfirmar.Click += async (s, e) => await ProcesarCobroAsync();
        }

        private async Task ProcesarCobroAsync() {
            _btnConfirmar.Enabled = false;

            try {
                VentaExportacion venta;
                string resumen;

                if (_rbEfectivo.Checked) {
                    venta = await _ventaService.RegistrarVentaEfectivoAsync(_carritoService);
                    resumen = $"✓ Venta registrada\nTicket: {venta.NumeroTicket}\nTotal: {venta.ImporteTotal:N2} CUP";

                } else if (_rbTransferencia.Checked) {
                    var nro = _txtTransaccion.Text?.Trim() ?? "";
                    if (string.IsNullOrEmpty(nro)) {
                        MostrarError("El número de transacción es requerido.");
                        return;
                    }
                    venta = await _ventaService.RegistrarVentaTransferenciaAsync(
                        _carritoService, _txtConfirmacion.Text?.Trim() ?? "", nro);
                    resumen = $"✓ Transferencia registrada\nTicket: {venta.NumeroTicket}\nNro. Transacción: {nro}";

                } else {
                    // Mixto
                    var str = _txtEfectivoMixto.Text?.Replace(',', '.') ?? "";
                    if (!decimal.TryParse(str,
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out decimal ef) || ef <= 0) {
                        MostrarError("Ingrese un monto válido en efectivo.");
                        return;
                    }
                    decimal transf = _total - ef;
                    if (transf <= 0) {
                        MostrarError("El efectivo cubre el total — use solo Efectivo.");
                        return;
                    }
                    var nroMixto = _txtTransaccionMixto.Text?.Trim() ?? "";
                    if (string.IsNullOrEmpty(nroMixto)) {
                        MostrarError("El número de transacción es requerido.");
                        return;
                    }
                    venta = await _ventaService.RegistrarVentaHibridaAsync(
                        _carritoService, ef, transf,
                        nroMixto, _txtConfirmacionMixto.Text?.Trim() ?? "");
                    resumen = $"✓ Pago mixto registrado\nTicket: {venta.NumeroTicket}\n💵 Efectivo: {ef:N2} CUP\n📲 Transferencia: {transf:N2} CUP";
                }

                DesregistrarSmsReceiver();

                var resultado = new Intent();
                resultado.PutExtra(ExtraTicket,  venta.NumeroTicket);
                resultado.PutExtra(ExtraResumen, resumen);
                SetResult(Result.Ok, resultado);
                Finish();

            } finally {
                _btnConfirmar.Enabled = true;
            }
        }

        private void MostrarError(string msg) {
            _btnConfirmar.Enabled = true;
            new AlertDialog.Builder(this)!
                .SetMessage(msg)!
                .SetPositiveButton("OK", (s, e) => { })!
                .Show();
        }

        protected override void OnDestroy() {
            DesregistrarSmsReceiver();
            base.OnDestroy();
        }
    }
}
