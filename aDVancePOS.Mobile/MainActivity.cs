// ============================================================
//  aDVancePOS.Mobile — MainActivity
//  Archivo: MainActivity.cs
// ============================================================

using aDVancePOS.Mobile.Adaptadores;
using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace aDVancePOS.Mobile {
    [Activity(
        Label = "@string/app_name",
        MainLauncher = false,
        Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : Activity {

        // ── Servicios ────────────────────────────────────────
        private ConfiguracionApp _config = null!;
        private CatalogoService _catalogoService = null!;
        private CarritoService _carritoService = null!;
        private VentaService _ventaService = null!;

        // ── Estado ───────────────────────────────────────────
        private List<ProductoCatalogo> _productosMostrados = new();
        private bool _catalogoCargado = false;
        private ProductoAdapter? _productoAdapter;

        // ── Controles UI ─────────────────────────────────────
        private EditText _txtBuscar = null!;
        private Button _btnLimpiarBusqueda = null!;
        private Button _btnImportar = null!;
        private ListView _lstProductos = null!;
        private TextView _lblConteoProductos = null!;
        private TextView _lblProductosVacio = null!;
        private ListView _lstCarrito = null!;
        private TextView _lblCarritoVacio = null!;
        private TextView _lblTotal = null!;
        private TextView _lblVentasBadge = null!;
        private LinearLayout _seccionPagos = null!;
        private Button _btnVaciarCarrito = null!;
        private Button _btnPagarEfectivo = null!;
        private Button _btnPagarTransferencia = null!;

        // ── SMS PAGOxMOVIL ────────────────────────────────────
        // Se registra al abrir el diálogo y se desregistra al cerrarlo
        private SmsTransferenciaBroadcastReceiver? _smsReceiver;

        private const int RequestCodeSms = 1001;

        // ─────────────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_main);

            InicializarServicios();
            EnlazarControles();
            ConfigurarEventos();
            CargarDatosInicialesAsync();
            SolicitarPermisosSms();
        }

        // ── Inicialización ───────────────────────────────────

        private void InicializarServicios() {
            _config = ConfiguracionService.Cargar();
            _catalogoService = new CatalogoService();
            _carritoService = new CarritoService();
            _ventaService = new VentaService(_config);
        }

        private void EnlazarControles() {
            _txtBuscar = FindViewById<EditText>(Resource.Id.txtBuscar)!;
            _btnLimpiarBusqueda = FindViewById<Button>(Resource.Id.btnLimpiarBusqueda)!;
            _btnImportar = FindViewById<Button>(Resource.Id.btnImportar)!;
            _lstProductos = FindViewById<ListView>(Resource.Id.lstProductos)!;
            _lblConteoProductos = FindViewById<TextView>(Resource.Id.lblConteoProductos)!;
            _lblProductosVacio = FindViewById<TextView>(Resource.Id.lblProductosVacio)!;
            _lstCarrito = FindViewById<ListView>(Resource.Id.lstCarrito)!;
            _lblCarritoVacio = FindViewById<TextView>(Resource.Id.lblCarritoVacio)!;
            _lblTotal = FindViewById<TextView>(Resource.Id.lblTotal)!;
            _lblVentasBadge = FindViewById<TextView>(Resource.Id.lblVentasBadge)!;
            _seccionPagos = FindViewById<LinearLayout>(Resource.Id.seccionPagos)!;
            _btnVaciarCarrito = FindViewById<Button>(Resource.Id.btnVaciarCarrito)!;
            _btnPagarEfectivo = FindViewById<Button>(Resource.Id.btnPagarEfectivo)!;
            _btnPagarTransferencia = FindViewById<Button>(Resource.Id.btnPagarTransferencia)!;
        }

        private void ConfigurarEventos() {
            _txtBuscar.TextChanged += (s, e) => {
                var termino = _txtBuscar.Text ?? "";
                _btnLimpiarBusqueda.Visibility = string.IsNullOrEmpty(termino)
                    ? Android.Views.ViewStates.Gone
                    : Android.Views.ViewStates.Visible;
                FiltrarProductos(termino);
            };

            _btnLimpiarBusqueda.Click += (s, e) => { _txtBuscar.Text = ""; FiltrarProductos(""); };
            _btnImportar.Click += async (s, e) => await ImportarCatalogoAsync();
            _btnVaciarCarrito.Click += (s, e) => SolicitarVaciarCarrito();
            _btnPagarEfectivo.Click += async (s, e) => await PagarEfectivoAsync();
            _btnPagarTransferencia.Click += async (s, e) => await PagarTransferenciaAsync();
            _lblVentasBadge.Click += (s, e) => MostrarResumenVentasDia();
        }

        private async void CargarDatosInicialesAsync() {
            await _ventaService.CargarVentasDelDiaAsync();
            if (System.IO.File.Exists(RutasApp.RutaCatalogo))
                await ImportarCatalogoAsync(silencioso: true);
        }

        // ── Permisos SMS ─────────────────────────────────────

        private void SolicitarPermisosSms() {
            // En Android 6+ los permisos peligrosos se piden en tiempo de ejecución
            if (CheckSelfPermission(Manifest.Permission.ReceiveSms) != Permission.Granted ||
                CheckSelfPermission(Manifest.Permission.ReadSms) != Permission.Granted) {
                RequestPermissions(
                    new[] { Manifest.Permission.ReceiveSms, Manifest.Permission.ReadSms },
                    RequestCodeSms);
            }
        }

        // ── Catálogo ─────────────────────────────────────────

        private async Task ImportarCatalogoAsync(bool silencioso = false) {
            try {
                _btnImportar.Enabled = false;
                _btnImportar.Text = "Cargando...";

                var catalogo = await _catalogoService.CargarCatalogoAsync();
                _catalogoCargado = true;

                if (_config.IdAlmacen == 1) {
                    _config.IdAlmacen = catalogo.Meta.IdAlmacen;
                    ConfiguracionService.Guardar(_config);
                }

                FiltrarProductos(_txtBuscar.Text ?? "");

                if (!silencioso)
                    MostrarMensaje(
                        $"✓ Catálogo cargado\n" +
                        $"{catalogo.Productos.Count} productos · {catalogo.Meta.NombreAlmacen}");
            } catch (System.IO.FileNotFoundException) {
                if (!silencioso)
                    MostrarMensaje(
                        "No se encontró catalogo.json.\n\n" +
                        "Exporta el catálogo desde el ERP desktop y cópialo al dispositivo con:\n\n" +
                        $"adb push catalogo.json \"{RutasApp.RutaCatalogo}\"");
            } catch (Exception ex) {
                MostrarMensaje($"Error al cargar catálogo:\n{ex.Message}");
            } finally {
                _btnImportar.Enabled = true;
                _btnImportar.Text = "＋ Catálogo";
            }
        }

        private void FiltrarProductos(string termino) {
            _productosMostrados = _catalogoCargado
                ? _catalogoService.Buscar(termino)
                : new List<ProductoCatalogo>();
            ActualizarListaProductos();
        }

        // ── Pagos ─────────────────────────────────────────────

        private async Task PagarEfectivoAsync() {
            if (_carritoService.ConteoItems == 0) {
                MostrarMensaje("El carrito está vacío.");
                return;
            }

            ConfirmarAccion(
                "Confirmar pago en efectivo",
                $"Total a cobrar: {_carritoService.ImporteTotal:C2}",
                async () => {
                    var venta = await _ventaService.RegistrarVentaEfectivoAsync(_carritoService);
                    _carritoService.VaciarTrasVenta();
                    ActualizarUI();
                    MostrarMensaje(
                        $"✓ Venta registrada\n" +
                        $"Ticket: {venta.NumeroTicket}\n" +
                        $"Total: {venta.ImporteTotal:C2}\n\n" +
                        $"Ventas hoy: {_ventaService.TotalVentasHoy}  ·  " +
                        $"Recaudado: {_ventaService.TotalRecaudadoHoy:C2}");
                });
        }

        private async Task PagarTransferenciaAsync() {
            if (_carritoService.ConteoItems == 0) {
                MostrarMensaje("El carrito está vacío.");
                return;
            }

            MostrarDialogoTransferencia(async (confirmacion, transaccion) => {
                var venta = await _ventaService.RegistrarVentaTransferenciaAsync(
                    _carritoService, confirmacion, transaccion);
                _carritoService.VaciarTrasVenta();
                ActualizarUI();
                MostrarMensaje(
                    $"✓ Transferencia registrada (pendiente de confirmación)\n" +
                    $"Ticket: {venta.NumeroTicket}\n" +
                    $"Confirmación: {confirmacion}");
            });
        }

        private void MostrarResumenVentasDia() {
            int totalVentas = _ventaService.TotalVentasHoy;
            decimal totalRecaudado = _ventaService.TotalRecaudadoHoy;

            if (totalVentas == 0) {
                MostrarMensaje("Sin ventas registradas hoy.");
                return;
            }

            var resumen = _ventaService.ObtenerResumenPorMetodo();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"📅 {DateTime.Now:dddd dd/MM/yyyy}");
            sb.AppendLine();
            sb.AppendLine($"Ventas completadas: {totalVentas}");
            sb.AppendLine($"Total recaudado:    {totalRecaudado:C2}");
            sb.AppendLine();
            if (resumen.Efectivo > 0)
                sb.AppendLine($"💵 Efectivo:         {resumen.Efectivo:C2}");
            if (resumen.Transferencia > 0)
                sb.AppendLine($"📲 Transferencia:    {resumen.Transferencia:C2}");

            MostrarMensaje(sb.ToString().TrimEnd());
        }

        private void SolicitarVaciarCarrito() {
            if (_carritoService.ConteoItems == 0) return;
            ConfirmarAccion(
                "¿Vaciar el carrito?",
                "Se eliminarán todos los productos agregados.",
                () => { _carritoService.Vaciar(); ActualizarUI(); });
        }

        // ── Actualización de UI ───────────────────────────────

        private void ActualizarListaProductos() {
            if (_productoAdapter == null || _lstProductos.Adapter == null) {
                _productoAdapter = new ProductoAdapter(this, _productosMostrados, producto => {
                    if (!_carritoService.AgregarProducto(producto))
                        Toast.MakeText(this, $"Sin stock: {producto.Nombre}",
                            ToastLength.Short)?.Show();
                    ActualizarUI();
                });
                _lstProductos.Adapter = _productoAdapter;
            } else {
                _productoAdapter.ActualizarLista(_productosMostrados);
            }

            _lblConteoProductos.Text = $"{_productosMostrados.Count} resultado(s)";

            var sinResultados = _productosMostrados.Count == 0 && _catalogoCargado;
            _lblProductosVacio.Visibility = sinResultados
                ? Android.Views.ViewStates.Visible
                : Android.Views.ViewStates.Gone;
            _lstProductos.Visibility = sinResultados
                ? Android.Views.ViewStates.Gone
                : Android.Views.ViewStates.Visible;
        }

        private void ActualizarUI() {
            var items = _carritoService.Items.ToList();
            bool carritoConItems = items.Count > 0;

            _lstCarrito.Adapter = new CarritoAdapter(this, items,
                onRestar: item => { _carritoService.RestarProducto(item.Producto); ActualizarUI(); },
                onSumar: item => {
                    if (!_carritoService.AgregarProducto(item.Producto))
                        Toast.MakeText(this, "Sin stock disponible", ToastLength.Short)?.Show();
                    ActualizarUI();
                },
                onEliminar: item => { _carritoService.EliminarItem(item); ActualizarUI(); }
            );

            _lblTotal.Text = _carritoService.ImporteTotal.ToString("C2");

            _seccionPagos.Alpha = carritoConItems ? 1.0f : 0.35f;
            _btnPagarEfectivo.Enabled = carritoConItems;
            _btnPagarTransferencia.Enabled = carritoConItems;

            _lblCarritoVacio.Visibility = carritoConItems
                ? Android.Views.ViewStates.Gone
                : Android.Views.ViewStates.Visible;
            _lstCarrito.Visibility = carritoConItems
                ? Android.Views.ViewStates.Visible
                : Android.Views.ViewStates.Gone;

            _lblVentasBadge.Text = _ventaService.TotalVentasHoy.ToString();

            if (_catalogoCargado)
                ActualizarListaProductos();
        }

        // ── Helpers UI ────────────────────────────────────────

        private void MostrarMensaje(string mensaje) {
            RunOnUiThread(() =>
                new AlertDialog.Builder(this)!
                    .SetMessage(mensaje)!
                    .SetPositiveButton("Aceptar", (s, e) => { })!
                    .Show());
        }

        private void ConfirmarAccion(string titulo, string mensaje, Action onConfirmar) {
            new AlertDialog.Builder(this)!
                .SetTitle(titulo)!
                .SetMessage(mensaje)!
                .SetPositiveButton("Confirmar", (s, e) => onConfirmar())!
                .SetNegativeButton("Cancelar", (s, e) => { })!
                .Show();
        }

        // ── Diálogo de transferencia con detección SMS ────────

        private void MostrarDialogoTransferencia(Func<string, string, Task> onConfirmar) {
            var totalCarrito = _carritoService.ImporteTotal;

            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            layout.SetPadding(48, 16, 48, 0);

            // Etiqueta de estado: se actualiza cuando llega el SMS
            var lblEstadoSms = new TextView(this) {
                Text = "📱 Esperando SMS de PAGOxMOVIL...",
                TextSize = 12f
            };
            lblEstadoSms.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));
            lblEstadoSms.SetPadding(0, 0, 0, 16);

            // Campo principal — se autocompleta con el Nro. Transaccion del SMS
            var txtTransaccion = new EditText(this) {
                Hint = "Nro. Transacción *",
                InputType = Android.Text.InputTypes.ClassText
            };

            // Campo opcional para confirmación adicional manual
            var txtConfirmacion = new EditText(this) {
                Hint = "Confirmación adicional (opcional)",
                InputType = Android.Text.InputTypes.ClassText
            };

            layout.AddView(lblEstadoSms);
            layout.AddView(txtTransaccion);
            layout.AddView(txtConfirmacion);

            // ── Registrar BroadcastReceiver ───────────────────
            _smsReceiver = new SmsTransferenciaBroadcastReceiver();
            _smsReceiver.OnPagoDetectado = resultado => {
                // Siempre autocompletar, sin importar si el monto coincide
                RunOnUiThread(() => {
                    txtTransaccion.Text = resultado.NumeroTransaccion;

                    bool montoCoincide = resultado.Monto == totalCarrito;

                    if (montoCoincide) {
                        // Verde: todo OK
                        lblEstadoSms.Text =
                            $"✓ SMS detectado · {resultado.Monto:N2} CUP" +
                            (resultado.TelefonoRemitente != null
                                ? $" · Tel: {resultado.TelefonoRemitente}"
                                : "");
                        lblEstadoSms.SetTextColor(
                            Android.Graphics.Color.ParseColor("#2E7D32"));
                    } else {
                        // Naranja: monto distinto, el cajero debe verificar
                        lblEstadoSms.Text =
                            $"⚠ Monto SMS: {resultado.Monto:N2} CUP  " +
                            $"(carrito: {totalCarrito:N2} CUP) · Verifique";
                        lblEstadoSms.SetTextColor(
                            Android.Graphics.Color.ParseColor("#E65100"));
                    }
                });
            };

            var filtro = new IntentFilter(
                Android.Provider.Telephony.Sms.Intents.SmsReceivedAction);
            RegisterReceiver(_smsReceiver, filtro);

            // ── Mostrar diálogo ───────────────────────────────
            var dialogo = new AlertDialog.Builder(this)!
                .SetTitle($"📲 Transferencia — {totalCarrito:N2} CUP")!
                .SetView(layout)!
                .SetPositiveButton("Registrar", (IDialogInterfaceOnClickListener?) null)!
                .SetNegativeButton("Cancelar", (s, e) => DesregistrarSmsReceiver())!
                .Create()!;

            // SetPositiveButton con null listener para poder validar antes de cerrar
            dialogo.SetOnShowListener(new DialogShowListener(() => {
                var btnRegistrar = dialogo.GetButton((int) DialogButtonType.Positive);
                btnRegistrar!.Click += async (s, e) => {
                    var transaccion = txtTransaccion.Text?.Trim() ?? "";
                    if (string.IsNullOrEmpty(transaccion)) {
                        MostrarMensaje("El número de transacción es requerido.");
                        return; // No cerrar el diálogo
                    }

                    var confirmacion = txtConfirmacion.Text?.Trim() ?? "";
                    DesregistrarSmsReceiver();
                    dialogo.Dismiss();
                    await onConfirmar(confirmacion, transaccion);
                };
            }));

            dialogo.SetOnCancelListener(new DialogCancelListener(DesregistrarSmsReceiver));
            dialogo.Show();
        }

        private void DesregistrarSmsReceiver() {
            if (_smsReceiver == null) return;
            try { UnregisterReceiver(_smsReceiver); } catch { /* ya desregistrado */ }
            _smsReceiver = null;
        }

        // ── Ciclo de vida ─────────────────────────────────────

        protected override void OnDestroy() {
            DesregistrarSmsReceiver();
            base.OnDestroy();
        }
    }

    // ── Helpers de listeners para AlertDialog ─────────────────

    internal class DialogShowListener : Java.Lang.Object, IDialogInterfaceOnShowListener {
        private readonly Action _onShow;
        public DialogShowListener(Action onShow) => _onShow = onShow;
        public void OnShow(IDialogInterface? dialog) => _onShow();
    }

    internal class DialogCancelListener : Java.Lang.Object, IDialogInterfaceOnCancelListener {
        private readonly Action _onCancel;
        public DialogCancelListener(Action onCancel) => _onCancel = onCancel;
        public void OnCancel(IDialogInterface? dialog) => _onCancel();
    }
}