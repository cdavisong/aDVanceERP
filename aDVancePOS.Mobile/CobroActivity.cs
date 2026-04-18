// ============================================================
//  aDVancePOS.Mobile — CobroActivity
//  Archivo: CobroActivity.cs
//
//  Pantalla de cobro con soporte para:
//  - Pagos parciales acumulables (efectivo + transferencia)
//  - Múltiples monedas con conversión a moneda base
//  - Archivado de venta en espera si la transferencia no llega
//
//  Flujo:
//    1. Cajero agrega fragmentos de pago (método + moneda + monto)
//    2. Cada fragmento aparece en la lista de "Pagos registrados"
//    3. Cuando el pendiente llega a 0, se habilita "Confirmar cobro"
//    4. Si la transferencia demora → "Archivar" guarda la venta
//       en espera y libera el carrito para el siguiente cliente
// ============================================================

using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

using Android.Content;
using Android.Views;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Cobrar",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class CobroActivity : Activity {
        public const string ExtraTicket = "ticket";
        public const string ExtraResumen = "resumen";
        public const string ExtraEsEspera = "es_espera";
        public const int RequestCode = 3001;

        private CarritoService _carrito = null!;
        private VentaService _ventaService = null!;
        private CatalogoService _catalogoService = null!;
        private decimal _total;

        private List<MonedaCatalogo> _monedas = new();
        private MonedaCatalogo _monedaBase = null!;
        private MonedaCatalogo _monedaSeleccionada = null!;

        private readonly List<PagoExportacion> _pagosRegistrados = new();

        private TextView _lblTotal = null!;
        private TextView _lblPendiente = null!;
        private LinearLayout _contenedorPagos = null!;
        private TextView _lblSinPagos = null!;
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
        private Button _btnArchivar = null!;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();

            SetContentView(Resource.Layout.activity_cobro);

            var app = (PosApplication) Application!;

            _carrito = app.CarritoService;
            _ventaService = app.VentaService;
            _catalogoService = app.CatalogoService;
            _total = _carrito.ImporteTotal;

            CargarMonedas();
            EnlazarVistas();
            ConfigurarMetodosPago();
            ConfigurarSpinnerMoneda();
            ConfigurarMonto();
            ConfigurarBotones();
            ActualizarResumenPendiente();
        }

        private void CargarMonedas() {
            _monedas = _catalogoService.ObtenerMonedas();
            _monedaBase = _catalogoService.ObtenerMonedaBase();

            if (_monedas.Count == 0)
                _monedas.Add(_monedaBase);

            _monedaSeleccionada = _monedaBase;
        }

        private void EnlazarVistas() {
            _lblTotal = FindViewById<TextView>(Resource.Id.lblTotalCobro)!;
            _lblPendiente = FindViewById<TextView>(Resource.Id.lblPendienteCobro)!;
            _contenedorPagos = FindViewById<LinearLayout>(Resource.Id.contenedorPagosRegistrados)!;
            _lblSinPagos = FindViewById<TextView>(Resource.Id.lblSinPagos)!;
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
            _btnArchivar = FindViewById<Button>(Resource.Id.btnArchivarEspera)!;

            _lblTotal.Text = $"Total: {_monedaBase.Simbolo} {_total:N2} {_monedaBase.Codigo}";

            FindViewById<ImageButton>(Resource.Id.btnVolverCobro)!.Click += (s, e) => Finish();
        }

        private void ConfigurarMetodosPago() {
            _rbEfectivo.Clickable = false;
            _rbTransferencia.Clickable = false;
            _rbEfectivo.Focusable = false;
            _rbTransferencia.Focusable = false;

            FindViewById<LinearLayout>(Resource.Id.tarjetaEfectivo)!.Click += (s, e) => SeleccionarMetodo(false);
            FindViewById<LinearLayout>(Resource.Id.tarjetaTransferencia)!.Click += (s, e) => SeleccionarMetodo(true);

            SeleccionarMetodo(false);
        }

        private void SeleccionarMetodo(bool esTransferencia) {
            _rbEfectivo.Checked = !esTransferencia;
            _rbTransferencia.Checked = esTransferencia;

            FindViewById<LinearLayout>(Resource.Id.tarjetaEfectivo)!
                .SetBackgroundResource(!esTransferencia
                    ? Resource.Drawable.btn_peachpuff
                    : Resource.Drawable.btn_outline_firebrick);
            FindViewById<LinearLayout>(Resource.Id.tarjetaTransferencia)!
                .SetBackgroundResource(esTransferencia
                    ? Resource.Drawable.btn_peachpuff
                    : Resource.Drawable.btn_outline_firebrick);

            _panelTransferencia.Visibility = esTransferencia
                ? ViewStates.Visible : ViewStates.Gone;
        }

        private void ConfigurarSpinnerMoneda() {
            var etiquetas = _monedas.Select(m => m.Etiqueta).ToArray();
            var adapter = new ArrayAdapter<string>(
                this,
                Android.Resource.Layout.SimpleSpinnerItem,
                etiquetas);

            adapter.SetDropDownViewResource(
                Android.Resource.Layout.SimpleSpinnerDropDownItem);

            _spinnerMoneda.Adapter = adapter;
            _spinnerMoneda.ItemSelected += (s, e) => {
                _monedaSeleccionada = _monedas[e.Position];

                ActualizarEquivalente();
            };
        }

        private void ConfigurarMonto() {
            _txtMonto.TextChanged += (s, e) => ActualizarEquivalente();
        }

        private void ActualizarEquivalente() {
            if (_monedaSeleccionada == null || _monedaSeleccionada.EsBase) {
                _lblEquivalente.Visibility = ViewStates.Gone;

                return;
            }

            var str = _txtMonto.Text?.Replace(',', '.') ?? "0";
            decimal.TryParse(str,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal monto);
            decimal enBase = Math.Round(monto * _monedaSeleccionada.TasaHoy, 2);

            _lblEquivalente.Text = $"≈ {_monedaBase.Simbolo} {enBase:N2} {_monedaBase.Codigo}  (tasa: ×{_monedaSeleccionada.TasaHoy:N2})";
            _lblEquivalente.Visibility = ViewStates.Visible;
        }

        private void ConfigurarBotones() {
            _btnAgregarPago.Click += (s, e) => IntentarAgregarPago();
            _btnConfirmar.Click += async (s, e) => {
                _btnConfirmar.Enabled = false;

                try {
                    var venta = await _ventaService
                        .RegistrarVentaAsync(_carrito, [.. _pagosRegistrados]);

                    _carrito.VaciarTrasVenta();

                    EnviarResultado(venta, esEspera: false);
                } finally {
                    _btnConfirmar.Enabled = true;
                }
            };

            _btnArchivar.Click += (s, e) => ConfirmarArchivarEnEspera();
        }

        private void IntentarAgregarPago() {
            var str = _txtMonto.Text?.Replace(',', '.') ?? "";

            if (!decimal.TryParse(str,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal monto) || monto <= 0) {

                MostrarError("Ingrese un monto válido mayor que cero.");
                return;
            }

            bool esTransferencia = _rbTransferencia.Checked;
            var nroTrans = _txtNroTransferencia.Text?.Trim() ?? "";

            if (esTransferencia && string.IsNullOrEmpty(nroTrans)) {
                MostrarError("El número de transacción es requerido para transferencias.");
                return;
            }

            decimal montoBase = _monedaSeleccionada.EsBase
                ? monto
                : Math.Round(monto * _monedaSeleccionada.TasaHoy, 2);

            decimal pendiente = CalcularPendiente();

            if (montoBase > pendiente + 0.005m) {
                MostrarError(
                    $"El monto en moneda base ({montoBase:N2} {_monedaBase.Codigo}) " +
                    $"excede el pendiente ({pendiente:N2} {_monedaBase.Codigo}).");
                return;
            }

            var detalle = new PagoDetalleMoneda {
                IdMoneda = _monedaSeleccionada.Id,
                CodigoMoneda = _monedaSeleccionada.Codigo,
                SimboloMoneda = _monedaSeleccionada.Simbolo,
                MontoMoneda = monto,
                MontoMonedaBase = montoBase,
                TasaCambioAplicada = _monedaSeleccionada.TasaHoy,
                NumeroTransaccion = esTransferencia && !string.IsNullOrEmpty(nroTrans)
                    ? nroTrans : null,
                TelefonoRemitente = esTransferencia
                    ? _txtTelefono.Text?.Trim().NullIfEmpty()
                    : null
            };

            _pagosRegistrados.Add(new PagoExportacion {
                MetodoPago = esTransferencia ? "TransferenciaBancaria" : "Efectivo",
                MontoPagado = montoBase,
                FechaPagoCliente = DateTime.UtcNow,
                EstadoPago = esTransferencia ? "Pendiente" : "Confirmado",
                MontoMonedaBase = montoBase,
                TasaCambioAplicada = _monedaSeleccionada.TasaHoy,
                IdMoneda = _monedaSeleccionada.Id,
                DetallesMoneda = new List<PagoDetalleMoneda> { detalle }
            });

            // Limpiar campos
            _txtMonto.Text = "";
            _txtNroTransferencia.Text = "";
            _txtTelefono.Text = "";

            ActualizarListaPagos();
            ActualizarResumenPendiente();
        }

        private decimal CalcularPendiente() =>
            Math.Max(0, _total - _pagosRegistrados.Sum(p => p.MontoPagado));

        private void ActualizarResumenPendiente() {
            decimal pendiente = CalcularPendiente();
            decimal pagado = _pagosRegistrados.Sum(p => p.MontoPagado);

            _lblPendiente.Text = pendiente > 0.005m
                ? $"Pendiente: {_monedaBase.Simbolo} {pendiente:N2} {_monedaBase.Codigo}"
                : $"Pagado: {_monedaBase.Simbolo} {pagado:N2} {_monedaBase.Codigo} ✓";
            _lblPendiente.SetTextColor(Android.Graphics.Color.ParseColor(
                pendiente > 0.005m ? "#FFCCCC" : "#CCFFCC"));

            bool cobrado = pendiente <= 0.005m;

            _btnConfirmar.Enabled = cobrado;
            _btnConfirmar.Alpha = cobrado ? 1.0f : 0.5f;
        }

        private void ActualizarListaPagos() {
            _contenedorPagos.RemoveAllViews();

            if (_pagosRegistrados.Count == 0) {
                _contenedorPagos.AddView(_lblSinPagos);
                return;
            }

            foreach (var pago in _pagosRegistrados)
                _contenedorPagos.AddView(CrearFilaPago(pago));
        }

        private View CrearFilaPago(PagoExportacion pago) {
            var wrap = new LinearLayout(this) { Orientation = Orientation.Vertical };
            var fila = new LinearLayout(this) { Orientation = Orientation.Horizontal };

            fila.SetPadding(Dp(16), Dp(11), Dp(16), Dp(11));
            fila.SetGravity(GravityFlags.CenterVertical);

            // Ícono
            var img = new ImageView(this);

            img.LayoutParameters = new LinearLayout.LayoutParams(Dp(22), Dp(22));
            ((LinearLayout.LayoutParams) img.LayoutParameters).SetMargins(0, 0, Dp(12), 0);

            int idRes = Resources!.GetIdentifier(
                pago.MetodoPago == "TransferenciaBancaria" ? "ic_transfer" : "ic_cash",
                "drawable", PackageName);

            if (idRes != 0)
                img.SetImageResource(idRes);

            // Texto descripción
            var det = pago.DetallesMoneda.FirstOrDefault();
            var lblDesc = new TextView(this) { TextSize = 13f };

            lblDesc.LayoutParameters = new LinearLayout.LayoutParams(
                0, LinearLayout.LayoutParams.WrapContent, 1f);

            string metodo = pago.MetodoPago == "TransferenciaBancaria"
                ? "Transferencia"
                : "Efectivo";
            string nro = det?.NumeroTransaccion is { Length: > 0 } n
                ? $"  #{n}"
                : "";

            lblDesc.Text = $"{metodo} · {det?.CodigoMoneda ?? _monedaBase.Codigo}{nro}";
            lblDesc.SetTextColor(Android.Graphics.Color.ParseColor("#333333"));

            // Monto
            var lblMonto = new TextView(this) { TextSize = 13f };

            lblMonto.Gravity = GravityFlags.End;

            bool monedaDistinta = det != null &&
                !det.CodigoMoneda.Equals(_monedaBase.Codigo,
                    StringComparison.OrdinalIgnoreCase);

            lblMonto.Text = monedaDistinta
                ? $"{det!.SimboloMoneda}{det.MontoMoneda:N2}\n≈{pago.MontoPagado:N2} {_monedaBase.Codigo}"
                : $"{pago.MontoPagado:N2} {_monedaBase.Codigo}";
            lblMonto.SetTextColor(Android.Graphics.Color.ParseColor("#B22222"));

            // Botón eliminar
            var btnX = new Button(this) { Text = "✕", TextSize = 10f };

            btnX.LayoutParameters = new LinearLayout.LayoutParams(Dp(28), Dp(28));

            ((LinearLayout.LayoutParams) btnX.LayoutParameters).SetMargins(Dp(8), 0, 0, 0);
            btnX.SetTextColor(Android.Graphics.Color.ParseColor("#CCCCCC"));
            btnX.SetBackgroundColor(Android.Graphics.Color.Transparent);

            var pagoCapture = pago;

            btnX.Click += (s, e) => {
                _pagosRegistrados.Remove(pagoCapture);

                ActualizarListaPagos();
                ActualizarResumenPendiente();
            };

            fila.AddView(img);
            fila.AddView(lblDesc);
            fila.AddView(lblMonto);
            fila.AddView(btnX);
            wrap.AddView(fila);

            // Divisor
            var div = new View(this);

            div.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 1);
            div.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F0F0F0"));
            wrap.AddView(div);

            return wrap;
        }

        private void ConfirmarArchivarEnEspera() {
            string msg = _pagosRegistrados.Count > 0
                ? $"La venta quedará archivada con {_pagosRegistrados.Count} pago(s) parcial(es) ya registrado(s).\n\nPodrá recuperarla desde Ventas en espera cuando llegue la confirmación."
                : "Se archivará la venta sin pagos. Podrá completarla más tarde cuando llegue la confirmación de la transferencia.";

            DialogHelper.MostrarConfirmar(
                this,
                titulo: "Archivar en espera",
                mensaje: msg,
                textoConfirmar: "Archivar",
                onConfirmar: async () => {
                    try {
                        var venta = await _ventaService.ArchivarEnEsperaAsync(
                            _carrito,
                            new List<PagoExportacion>(_pagosRegistrados));
                        _carrito.VaciarTrasVenta();
                        EnviarResultado(venta, esEspera: true);
                    } catch (Exception ex) {
                        MostrarError($"Error al archivar: {ex.Message}");
                    }
                });
        }

        private void EnviarResultado(VentaExportacion venta, bool esEspera) {
            var intent = new Intent();
            intent.PutExtra(ExtraTicket, venta.NumeroTicket);
            intent.PutExtra(ExtraResumen, ConstruirResumen(venta, esEspera));
            intent.PutExtra(ExtraEsEspera, esEspera);
            SetResult(Result.Ok, intent);
            Finish();
        }

        private string ConstruirResumen(VentaExportacion venta, bool esEspera) {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(esEspera
                ? $"Venta archivada en espera"
                : $"Venta registrada");
            sb.AppendLine($"Ticket: {venta.NumeroTicket}");
            sb.AppendLine($"Total: {venta.ImporteTotal:N2} {_monedaBase.Codigo}");
            if (venta.Pagos.Count > 0) {
                sb.AppendLine("Pagos:");
                foreach (var p in venta.Pagos) {
                    var det = p.DetallesMoneda.FirstOrDefault();
                    string met = p.MetodoPago == "TransferenciaBancaria"
                        ? "Transferencia" : "Efectivo";
                    bool dist = det != null && !det.CodigoMoneda.Equals(
                        _monedaBase.Codigo, StringComparison.OrdinalIgnoreCase);
                    sb.AppendLine(dist
                        ? $"  {met}: {det!.SimboloMoneda}{det.MontoMoneda:N2} {det.CodigoMoneda} ≈ {p.MontoPagado:N2} {_monedaBase.Codigo}"
                        : $"  {met}: {p.MontoPagado:N2} {_monedaBase.Codigo}");
                }
            }
            return sb.ToString().TrimEnd();
        }

        private void MostrarError(string msg) =>
            DialogHelper.MostrarInfo(this, msg, titulo: "Atención");

        private int Dp(int dp) =>
            (int) (dp * Resources!.DisplayMetrics!.Density);
    }

    internal static class StringExtensions {
        public static string? NullIfEmpty(this string? s) =>
            string.IsNullOrWhiteSpace(s) ? null : s;
    }
}
