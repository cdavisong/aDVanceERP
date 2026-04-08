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

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Cobrar",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class CobroActivity : Activity {

        public const string ExtraTotalFormateado = "total_formateado";
        public const string ExtraTicket = "ticket";
        public const string ExtraResumen = "resumen";
        public const int RequestCode = 3001;

        // ── Servicios ────────────────────────────────────────
        private CarritoService _carritoService = null!;
        private VentaService _ventaService = null!;
        private decimal _total;

        // ── UI ───────────────────────────────────────────────
        private TextView _lblTotalCobro = null!;
        private RadioButton _rbEfectivo = null!;
        private RadioButton _rbTransferencia = null!;
        private RadioButton _rbMixto = null!;
        private LinearLayout _panelTransferencia = null!;
        private LinearLayout _panelMixto = null!;
        private TextView _lblEstadoSms = null!;
        private EditText _txtTransaccion = null!;
        private EditText _txtConfirmacion = null!;
        private TextView _lblEstadoSmsMixto = null!;
        private TextView _lblBalanceMixto = null!;
        private EditText _txtEfectivoMixto = null!;
        private EditText _txtTransaccionMixto = null!;
        private EditText _txtConfirmacionMixto = null!;
        private Button _btnConfirmar = null!;

        // ── SMS ──────────────────────────────────────────────
        private SmsTransferenciaBroadcastReceiver? _smsReceiver;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_cobro);

            // Solicitar permisos de SMS si es necesario
            if (CheckSelfPermission(Manifest.Permission.ReceiveSms) != Permission.Granted ||
                CheckSelfPermission(Manifest.Permission.ReadSms) != Permission.Granted) {
                RequestPermissions(new[] {
                    Manifest.Permission.ReceiveSms,
                    Manifest.Permission.ReadSms
                }, requestCode: 1001);
            }

            // Obtener servicios desde el Application (singleton compartido con MainActivity)
            var app = (PosApplication) Application!;
            _carritoService = app.CarritoService;
            _ventaService = app.VentaService;
            _total = _carritoService.ImporteTotal;

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
            _lblTotalCobro = FindViewById<TextView>(Resource.Id.lblTotalCobro)!;
            _rbEfectivo = FindViewById<RadioButton>(Resource.Id.rbEfectivo)!;
            _rbTransferencia = FindViewById<RadioButton>(Resource.Id.rbTransferencia)!;
            _rbMixto = FindViewById<RadioButton>(Resource.Id.rbMixto)!;
            _panelTransferencia = FindViewById<LinearLayout>(Resource.Id.panelTransferencia)!;
            _panelMixto = FindViewById<LinearLayout>(Resource.Id.panelMixto)!;
            _lblEstadoSms = FindViewById<TextView>(Resource.Id.lblEstadoSms)!;
            _txtTransaccion = FindViewById<EditText>(Resource.Id.txtTransaccion)!;
            _txtConfirmacion = FindViewById<EditText>(Resource.Id.txtConfirmacion)!;
            _lblEstadoSmsMixto = FindViewById<TextView>(Resource.Id.lblEstadoSmsMixto)!;
            _lblBalanceMixto = FindViewById<TextView>(Resource.Id.lblBalanceMixto)!;
            _txtEfectivoMixto = FindViewById<EditText>(Resource.Id.txtEfectivoMixto)!;
            _txtTransaccionMixto = FindViewById<EditText>(Resource.Id.txtTransaccionMixto)!;
            _txtConfirmacionMixto = FindViewById<EditText>(Resource.Id.txtConfirmacionMixto)!;
            _btnConfirmar = FindViewById<Button>(Resource.Id.btnConfirmarCobro)!;
        }

        private void ConfigurarMetodosPago() {
            // Desactivar clicks directos en los RadioButton — la TARJETA completa es el control
            _rbEfectivo.Clickable = false;
            _rbTransferencia.Clickable = false;
            _rbMixto.Clickable = false;
            _rbEfectivo.Focusable = false;
            _rbTransferencia.Focusable = false;
            _rbMixto.Focusable = false;

            // Tocar cualquier parte de la tarjeta selecciona ese método
            FindViewById<LinearLayout>(Resource.Id.tarjetaEfectivo)!.Click +=
                (s, e) => SeleccionarMetodo(0);
            FindViewById<LinearLayout>(Resource.Id.tarjetaTransferencia)!.Click +=
                (s, e) => SeleccionarMetodo(1);
            FindViewById<LinearLayout>(Resource.Id.tarjetaMixto)!.Click +=
                (s, e) => SeleccionarMetodo(2);

            // Seleccionar Efectivo por defecto
            SeleccionarMetodo(0);

            // Validación en tiempo real del monto efectivo en modo mixto
            _txtEfectivoMixto.TextChanged += (s, e) => ActualizarBalanceMixto();
        }

        /// <summary>
        /// Gestiona la selección mutuamente excluyente de los tres métodos de pago.
        /// metodo: 0=Efectivo, 1=Transferencia, 2=Mixto
        /// </summary>
        private void SeleccionarMetodo(int metodo) {
            // Actualizar estado de RadioButtons manualmente
            _rbEfectivo.Checked = metodo == 0;
            _rbTransferencia.Checked = metodo == 1;
            _rbMixto.Checked = metodo == 2;

            // Resaltar visualmente la tarjeta activa vs inactivas
            ActualizarAparienciaTarjetas(metodo);

            // Mostrar/ocultar paneles adicionales
            ActualizarPaneles();
        }

        private void ActualizarAparienciaTarjetas(int metodoActivo) {
            var tarjetas = new[] {
                FindViewById<LinearLayout>(Resource.Id.tarjetaEfectivo)!,
                FindViewById<LinearLayout>(Resource.Id.tarjetaTransferencia)!,
                FindViewById<LinearLayout>(Resource.Id.tarjetaMixto)!
            };

            for (int i = 0; i < tarjetas.Length; i++) {
                // Tarjeta activa: fondo rojo sólido suave; inactiva: borde outline
                tarjetas[i].SetBackgroundResource(i == metodoActivo
                    ? Resource.Drawable.btn_peachpuff      // fondo seleccionado
                    : Resource.Drawable.btn_outline_firebrick); // borde inactivo
            }
        }

        private void ActualizarPaneles() {
            // panelTransferencia: SOLO en modo transferencia pura
            // panelMixto:         SOLO en modo mixto (tiene sus propios campos de transacción)
            _panelTransferencia.Visibility = _rbTransferencia.Checked
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
                        ? $"SMS · {resultado.Monto:N2} CUP"
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
                        ? $"SMS · {resultado.Monto:N2} CUP"
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
                    resumen = $"Venta registrada\nTicket: {venta.NumeroTicket}\nTotal: {venta.ImporteTotal:N2} CUP";

                } else if (_rbTransferencia.Checked) {
                    var nro = _txtTransaccion.Text?.Trim() ?? "";
                    if (string.IsNullOrEmpty(nro)) {
                        MostrarError("El número de transacción es requerido.");
                        return;
                    }
                    venta = await _ventaService.RegistrarVentaTransferenciaAsync(
                        _carritoService, _txtConfirmacion.Text?.Trim() ?? "", nro);
                    resumen = $"Transferencia registrada\nTicket: {venta.NumeroTicket}\nNro. Transacción: {nro}";

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
                    resumen = $"Pago mixto registrado\nTicket: {venta.NumeroTicket}\n💵 Efectivo: {ef:N2} CUP\n📲 Transferencia: {transf:N2} CUP";
                }

                DesregistrarSmsReceiver();

                var resultado = new Intent();
                resultado.PutExtra(ExtraTicket, venta.NumeroTicket);
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