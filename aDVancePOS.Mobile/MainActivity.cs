// ============================================================
//  aDVancePOS.Mobile — MainActivity
//  Archivo: MainActivity.cs
//
//  Conecta la UI (activity_main.xml) con los servicios.
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
        // ── Servicios — obtenidos del singleton PosApplication ──
        private ConfiguracionApp _config       => ((PosApplication)Application!).Config;
        private CatalogoService  _catalogoService => ((PosApplication)Application!).CatalogoService;
        private CarritoService   _carritoService  => ((PosApplication)Application!).CarritoService;
        private VentaService     _ventaService    => ((PosApplication)Application!).VentaService;

        // ── Estado ───────────────────────────────────────────
        private List<ProductoCatalogo> _productosMostrados = new();
        private bool _catalogoCargado = false;
        private ProductoAdapter? _productoAdapter;

        // ── Controles UI ─────────────────────────────────────
        private EditText _txtBuscar = null!;
        private ImageButton _btnEscanear = null!;
        private Button _btnLimpiarBusqueda = null!;
        private LinearLayout _btnImportar = null!;
        private ListView _lstProductos = null!;
        private TextView _lblConteoProductos = null!;
        private TextView _lblProductosVacio = null!;
        private ListView _lstCarrito = null!;
        private TextView _lblCarritoVacio = null!;
        private TextView _lblTotal = null!;
        private TextView _lblVentasBadge = null!;
        private LinearLayout _seccionPagos = null!;
        private Button _btnVaciarCarrito = null!;
        private LinearLayout _btnCobrar = null!;
        private ImageButton _btnConfiguracion = null!;

        // ── SMS PAGOxMOVIL ────────────────────────────────────
        // Se registra al abrir el diálogo de cobro y se desregistra al cerrarlo
        private SmsTransferenciaBroadcastReceiver? _smsReceiver;

        // ─────────────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_main);

            EnlazarControles();
            ConfigurarEventos();
            CargarDatosInicialesAsync();
            SolicitarPermisosApp();
            ActualizarUI(); // arrancar con btnCobrar deshabilitado
        }

        // ── Inicialización ───────────────────────────────────

        // Los servicios son provistos por PosApplication (singleton)

        private void EnlazarControles() {
            _txtBuscar = FindViewById<EditText>(Resource.Id.txtBuscar)!;
            _btnLimpiarBusqueda = FindViewById<Button>(Resource.Id.btnLimpiarBusqueda)!;
            _btnImportar = FindViewById<LinearLayout>(Resource.Id.btnImportar)!;
            _lstProductos = FindViewById<ListView>(Resource.Id.lstProductos)!;
            _lblConteoProductos = FindViewById<TextView>(Resource.Id.lblConteoProductos)!;
            _lblProductosVacio = FindViewById<TextView>(Resource.Id.lblProductosVacio)!;
            _lstCarrito = FindViewById<ListView>(Resource.Id.lstCarrito)!;
            _lblCarritoVacio = FindViewById<TextView>(Resource.Id.lblCarritoVacio)!;
            _lblTotal = FindViewById<TextView>(Resource.Id.lblTotal)!;
            _lblVentasBadge = FindViewById<TextView>(Resource.Id.lblVentasBadge)!;
            _seccionPagos = FindViewById<LinearLayout>(Resource.Id.seccionPagos)!;
            _btnVaciarCarrito = FindViewById<Button>(Resource.Id.btnVaciarCarrito)!;
            _btnCobrar = FindViewById<LinearLayout>(Resource.Id.btnCobrar)!;
            _btnEscanear       = FindViewById<ImageButton>(Resource.Id.btnEscanear)!;
            _btnConfiguracion  = FindViewById<ImageButton>(Resource.Id.btnConfiguracion)!;
        }

        private void ConfigurarEventos() {
            // Búsqueda en tiempo real
            _txtBuscar.TextChanged += (s, e) => {
                var termino = _txtBuscar.Text ?? "";
                _btnLimpiarBusqueda.Visibility =
                    string.IsNullOrEmpty(termino)
                    ? Android.Views.ViewStates.Gone
                    : Android.Views.ViewStates.Visible;
                FiltrarProductos(termino);
            };

            _btnLimpiarBusqueda.Click += (s, e) => {
                _txtBuscar.Text = "";
                FiltrarProductos("");
            };

            _btnImportar.Click += async (s, e) => await ImportarCatalogoAsync();
            _btnVaciarCarrito.Click += (s, e) => SolicitarVaciarCarrito();
            _btnCobrar.Click += (s, e) => StartActivityForResult(new Intent(this, typeof(CobroActivity)), CobroActivity.RequestCode);
            _lblVentasBadge.Click += (s, e) => StartActivity(new Intent(this, typeof(ResumenVentasActivity)));
            _btnEscanear.Click      += (s, e) => EscanearCodigo();
            _btnConfiguracion.Click += (s, e) => StartActivity(new Intent(this, typeof(ConfiguracionActivity)));
        }

        private async void CargarDatosInicialesAsync() {
            await _ventaService.CargarVentasDelDiaAsync();

            // Si ya existe catalogo.json en disco, cargarlo silenciosamente
            if (System.IO.File.Exists(RutasApp.RutaCatalogo))
                await ImportarCatalogoAsync(silencioso: true);
        }

        // ── Catálogo ─────────────────────────────────────────

        private async Task ImportarCatalogoAsync(bool silencioso = false) {
            try {
                _btnImportar.Enabled = false;
                _btnImportar.Alpha = 0.5f;

                var catalogo = await _catalogoService.CargarCatalogoAsync();
                _catalogoCargado = true;

                // Sincronizar idAlmacen desde el catálogo
                if (_config.IdAlmacen == 1) {
                    _config.IdAlmacen = catalogo.Meta.IdAlmacen;
                    ConfiguracionService.Guardar(_config);
                }

                FiltrarProductos(_txtBuscar.Text ?? "");

                if (!silencioso)
                    MostrarMensaje(
                        $"Catálogo cargado\n" +
                        $"{catalogo.Productos.Count} productos · {catalogo.Meta.NombreAlmacen}");
            } catch (System.IO.FileNotFoundException) {
                if (!silencioso)
                    MostrarMensaje(
                        "No se encontró catalogo.json.\n\n" +
                        "Exporta el catálogo desde la aplicación de escritorio de aDVance ERP, desde el módulo de inventario, pestaña de maestros, sección de almacenes");
            } catch (Exception ex) {
                MostrarMensaje($"Error al cargar catálogo:\n{ex.Message}");
            } finally {
                _btnImportar.Enabled = true;
                _btnImportar.Alpha = 1.0f;
            }
        }

        // ── Permisos ──────────────────────────────────────────

        private void SolicitarPermisosApp() {
            var permisosFaltantes = new List<string>();

            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted)
                permisosFaltantes.Add(Manifest.Permission.Camera);
            if (CheckSelfPermission(Manifest.Permission.ReceiveSms) != Permission.Granted)
                permisosFaltantes.Add(Manifest.Permission.ReceiveSms);
            if (CheckSelfPermission(Manifest.Permission.ReadSms) != Permission.Granted)
                permisosFaltantes.Add(Manifest.Permission.ReadSms);

            if (permisosFaltantes.Count > 0)
                RequestPermissions(permisosFaltantes.ToArray(), requestCode: 1000);
        }

        // ── Escáner de código de barras ───────────────────────

        private void EscanearCodigo() {
            // Verificar permiso de cámara en tiempo de ejecución (Android 6+)
            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted) {
                RequestPermissions(new[] { Manifest.Permission.Camera }, requestCode: 1002);
                return;
            }

            // Abrir EscanerActivity y esperar el resultado
            var intent = new Intent(this, typeof(EscanerActivity));
            StartActivityForResult(intent, EscanerActivity.RequestCode);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data) {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == EscanerActivity.RequestCode && resultCode == Result.Ok) {
                var codigo = data?.GetStringExtra(EscanerActivity.ExtraCodigoBarras)?.Trim() ?? "";
                if (!string.IsNullOrEmpty(codigo))
                    ProcesarCodigoEscaneado(codigo);
                return;
            }

            if (requestCode == CobroActivity.RequestCode && resultCode == Result.Ok) {
                // Venta registrada en CobroActivity — limpiar carrito y refrescar
                _carritoService.VaciarTrasVenta();
                ActualizarUI();
                var resumen = data?.GetStringExtra(CobroActivity.ExtraResumen) ?? "";
                if (!string.IsNullOrEmpty(resumen))
                    MostrarMensaje(resumen);
            }
        }

        private void ProcesarCodigoEscaneado(string codigo) {
            var producto = _catalogoService.BuscarPorCodigo(codigo);

            if (producto != null) {
                // Producto encontrado — agregar directo al carrito
                if (_carritoService.AgregarProducto(producto)) {
                    ActualizarUI();
                    Toast.MakeText(this,
                        $"{producto.Nombre} agregado",
                        ToastLength.Short)?.Show();
                } else {
                    MostrarMensaje($"Sin stock disponible para: {producto.Nombre}");
                }
            } else {
                // No existe — abrir búsqueda con el código escaneado
                _txtBuscar.Text = codigo;
                _txtBuscar.RequestFocus();
                Toast.MakeText(this,
                    $"Código '{codigo}' no encontrado en catálogo",
                    ToastLength.Long)?.Show();
            }
        }

        private void FiltrarProductos(string termino) {
            _productosMostrados = _catalogoCargado
                ? _catalogoService.Buscar(termino)
                : new List<ProductoCatalogo>();
            ActualizarListaProductos();
        }

        // ── Pagos ─────────────────────────────────────────────

        // ── Cobro unificado ───────────────────────────────────

        private void MostrarDialogoCobro() {
            if (_carritoService.ConteoItems == 0) {
                MostrarMensaje("El carrito está vacío.");
                return;
            }
            var total = _carritoService.ImporteTotal;
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            layout.SetPadding(48, 16, 48, 8);

            // ── Selector de método ────────────────────────────
            var lblMetodo = new TextView(this) { Text = "Método de pago:" };
            lblMetodo.SetTextColor(Android.Graphics.Color.ParseColor("#555555"));

            var rbEfectivo = new RadioButton(this) { Text = "💵  Efectivo" };
            var rbTransferencia = new RadioButton(this) { Text = "📲  Transferencia" };
            var rbHibrido = new RadioButton(this) { Text = "💵 + 📲  Pago mixto" };

            var rgMetodo = new RadioGroup(this) { Orientation = Orientation.Vertical };
            rgMetodo.AddView(rbEfectivo);
            rgMetodo.AddView(rbTransferencia);
            rgMetodo.AddView(rbHibrido);
            rbEfectivo.Checked = true;

            // ── Panel transferencia (Nro. transacción) ─────────
            var panelTransferencia = new LinearLayout(this) { Orientation = Orientation.Vertical };
            panelTransferencia.SetPadding(0, 8, 0, 0);

            var lblEstadoSms = new TextView(this) {
                Text = "📱 Esperando SMS de PAGOxMOVIL...",
                TextSize = 12f
            };
            lblEstadoSms.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));

            var txtTransaccion = new EditText(this) {
                Hint = "Nro. Transacción *",
                InputType = Android.Text.InputTypes.ClassText
            };
            var txtConfirmacion = new EditText(this) {
                Hint = "Confirmación adicional (opcional)",
                InputType = Android.Text.InputTypes.ClassText
            };
            panelTransferencia.AddView(lblEstadoSms);
            panelTransferencia.AddView(txtTransaccion);
            panelTransferencia.AddView(txtConfirmacion);
            panelTransferencia.Visibility = Android.Views.ViewStates.Gone;

            // ── Panel híbrido ─────────────────────────────────
            var panelHibrido = new LinearLayout(this) { Orientation = Orientation.Vertical };
            panelHibrido.SetPadding(0, 8, 0, 0);

            var txtEfectivo = new EditText(this) {
                Hint = "Monto en efectivo *",
                InputType = Android.Text.InputTypes.NumberFlagDecimal |
                            Android.Text.InputTypes.ClassNumber
            };
            var txtTransaccionHib = new EditText(this) {
                Hint = "Nro. Transacción (transferencia) *",
                InputType = Android.Text.InputTypes.ClassText
            };
            var txtConfirmacionHib = new EditText(this) {
                Hint = "Confirmación adicional (opcional)",
                InputType = Android.Text.InputTypes.ClassText
            };

            // Indicador de balance en tiempo real
            var lblBalance = new TextView(this) {
                TextSize = 13f,
                Text = $"Pendiente: {total:N2} CUP"
            };
            lblBalance.SetTextColor(Android.Graphics.Color.ParseColor("#E65100"));

            var lblEstadoSmsHib = new TextView(this) {
                Text = "📱 Esperando SMS de PAGOxMOVIL...",
                TextSize = 12f
            };
            lblEstadoSmsHib.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));

            panelHibrido.AddView(txtEfectivo);
            panelHibrido.AddView(lblBalance);
            panelHibrido.AddView(lblEstadoSmsHib);
            panelHibrido.AddView(txtTransaccionHib);
            panelHibrido.AddView(txtConfirmacionHib);
            panelHibrido.Visibility = Android.Views.ViewStates.Gone;

            layout.AddView(lblMetodo);
            layout.AddView(rgMetodo);
            layout.AddView(panelTransferencia);
            layout.AddView(panelHibrido);

            // ── SMS receiver — detecta PAGOxMOVIL en ambos modos ──
            _smsReceiver = new SmsTransferenciaBroadcastReceiver();
            _smsReceiver.OnPagoDetectado = resultado => {
                RunOnUiThread(() => {
                    if (rbTransferencia.Checked) {
                        // ── Modo transferencia pura ───────────────────────────
                        txtTransaccion.Text = resultado.NumeroTransaccion;
                        bool coincide = resultado.Monto == total;
                        lblEstadoSms.Text = coincide
                            ? $"SMS · {resultado.Monto:N2} CUP"
                            : $"⚠ SMS: {resultado.Monto:N2} CUP (total: {total:N2})";
                        lblEstadoSms.SetTextColor(Android.Graphics.Color.ParseColor(
                            coincide ? "#2E7D32" : "#E65100"));

                    } else if (rbHibrido.Checked) {
                        // ── Modo híbrido ──────────────────────────────────────
                        // Autocompleta el Nro. Transacción del panel mixto
                        txtTransaccionHib.Text = resultado.NumeroTransaccion;

                        // Calcular monto de transferencia esperado según lo que
                        // el cajero ya haya escrito en el campo de efectivo
                        var efectivoStr = txtEfectivo.Text?.Replace(',', '.') ?? "0";
                        decimal.TryParse(efectivoStr,
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out decimal montoEfectivoActual);
                        decimal montoTransfEsperado = total - montoEfectivoActual;

                        bool coincideHib = resultado.Monto == montoTransfEsperado;
                        lblEstadoSmsHib.Text = coincideHib
                            ? $"SMS · {resultado.Monto:N2} CUP"
                            : $"⚠ SMS: {resultado.Monto:N2} CUP  (esperado: {montoTransfEsperado:N2})";
                        lblEstadoSmsHib.SetTextColor(Android.Graphics.Color.ParseColor(
                            coincideHib ? "#2E7D32" : "#E65100"));
                    }
                });
            };
            RegisterReceiver(_smsReceiver,
                new IntentFilter(Android.Provider.Telephony.Sms.Intents.SmsReceivedAction));

            // ── Mostrar/ocultar paneles según método ──────────
            rgMetodo.CheckedChange += (s, e) => {
                panelTransferencia.Visibility =
                    rbTransferencia.Checked || rbHibrido.Checked
                    ? Android.Views.ViewStates.Visible
                    : Android.Views.ViewStates.Gone;
                panelHibrido.Visibility =
                    rbHibrido.Checked
                    ? Android.Views.ViewStates.Visible
                    : Android.Views.ViewStates.Gone;

                // Al cambiar a híbrido, reusar el txtTransaccion del panel superior
                // pero mostrar también el campo de efectivo
            };

            // ── Validar balance en tiempo real (modo híbrido) ─
            txtEfectivo.TextChanged += (s, e) => {
                if (!rbHibrido.Checked) return;
                var efectivoStr = txtEfectivo.Text?.Replace(',', '.') ?? "0";
                if (!decimal.TryParse(efectivoStr,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal montoEfectivo)) montoEfectivo = 0;

                decimal montoTransf = total - montoEfectivo;
                if (montoEfectivo <= 0 || montoEfectivo >= total) {
                    lblBalance.Text = montoEfectivo >= total
                        ? "El efectivo cubre el total completo — use solo Efectivo"
                        : $"Pendiente transferencia: {total:N2} CUP";
                    lblBalance.SetTextColor(Android.Graphics.Color.ParseColor("#E65100"));
                } else {
                    lblBalance.Text = $"Transferencia: {montoTransf:N2} CUP  ✓";
                    lblBalance.SetTextColor(Android.Graphics.Color.ParseColor("#2E7D32"));
                }
            };

            // ── Construir diálogo ─────────────────────────────
            var dialogo = new AlertDialog.Builder(this)!
                .SetTitle($"💰 Cobrar — {total:N2} CUP")!
                .SetView(layout)!
                .SetPositiveButton("Confirmar", (IDialogInterfaceOnClickListener?) null)!
                .SetNegativeButton("Cancelar", (s, e) => DesregistrarSmsReceiver())!
                .Create()!;

            dialogo.SetOnShowListener(new DialogShowListener(() => {
                var btnConfirmar = dialogo.GetButton((int) DialogButtonType.Positive);
                btnConfirmar!.Click += async (s, e) => {
                    if (rbEfectivo.Checked) {
                        // ── EFECTIVO ──────────────────────────
                        DesregistrarSmsReceiver();
                        dialogo.Dismiss();
                        var venta = await _ventaService.RegistrarVentaEfectivoAsync(_carritoService);
                        _carritoService.VaciarTrasVenta();
                        ActualizarUI();
                        MostrarMensaje(
                            $"Venta registrada\n" +
                            $"Ticket: {venta.NumeroTicket}\n" +
                            $"Total: {venta.ImporteTotal:N2} CUP");

                    } else if (rbTransferencia.Checked) {
                        // ── TRANSFERENCIA ─────────────────────
                        var nroTransaccion = txtTransaccion.Text?.Trim() ?? "";
                        if (string.IsNullOrEmpty(nroTransaccion)) {
                            MostrarMensaje("El número de transacción es requerido.");
                            return;
                        }
                        DesregistrarSmsReceiver();
                        dialogo.Dismiss();
                        var venta = await _ventaService.RegistrarVentaTransferenciaAsync(
                            _carritoService,
                            txtConfirmacion.Text?.Trim() ?? "",
                            nroTransaccion);
                        _carritoService.VaciarTrasVenta();
                        ActualizarUI();
                        MostrarMensaje(
                            $"Transferencia registrada\n" +
                            $"Ticket: {venta.NumeroTicket}\n" +
                            $"Nro. Transacción: {nroTransaccion}");

                    } else {
                        // ── HÍBRIDO ───────────────────────────
                        var efectivoStr = txtEfectivo.Text?.Replace(',', '.') ?? "";
                        if (!decimal.TryParse(efectivoStr,
                                System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture,
                                out decimal montoEfectivo) || montoEfectivo <= 0) {
                            MostrarMensaje("Ingrese un monto válido en efectivo.");
                            return;
                        }
                        decimal montoTransf = total - montoEfectivo;
                        if (montoTransf <= 0) {
                            MostrarMensaje("El efectivo cubre el total — use solo Efectivo.");
                            return;
                        }
                        var nroTransHib = txtTransaccionHib.Text?.Trim() ?? "";
                        if (string.IsNullOrEmpty(nroTransHib)) {
                            MostrarMensaje("El número de transacción es requerido.");
                            return;
                        }
                        DesregistrarSmsReceiver();
                        dialogo.Dismiss();
                        var venta = await _ventaService.RegistrarVentaHibridaAsync(
                            _carritoService,
                            montoEfectivo,
                            montoTransf,
                            nroTransHib,
                            txtConfirmacionHib.Text?.Trim() ?? "");
                        _carritoService.VaciarTrasVenta();
                        ActualizarUI();
                        MostrarMensaje(
                            $"Pago mixto registrado\n" +
                            $"Ticket: {venta.NumeroTicket}\n" +
                            $"💵 Efectivo: {montoEfectivo:N2} CUP\n" +
                            $"📲 Transferencia: {montoTransf:N2} CUP");
                    }
                };
            }));

            dialogo.SetOnCancelListener(new DialogCancelListener(DesregistrarSmsReceiver));
            dialogo.Show();
        }

        private void MostrarResumenVentasDia() {
            int totalVentas = _ventaService.TotalVentasHoy;
            decimal totalRecaudado = _ventaService.TotalRecaudadoHoy;

            if (totalVentas == 0) {
                MostrarMensaje("Sin ventas registradas hoy.");
                return;
            }

            // Desglose por método de pago
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
            // FIX STOCK: reusar el adapter con NotifyDataSetChanged en lugar de
            // recrearlo. Recrear el adapter cada vez hace que ListView no redibuje
            // las filas visibles recicladas, por lo que el stock no se actualiza
            // en pantalla aunque StockEnSesion sí cambió en el modelo.
            if (_productoAdapter == null || _lstProductos.Adapter == null) {
                // Primera carga: crear el adapter y asignarlo
                _productoAdapter = new ProductoAdapter(this, _productosMostrados, producto => {
                    if (!_carritoService.AgregarProducto(producto))
                        Toast.MakeText(this, $"Sin stock: {producto.Nombre}",
                            ToastLength.Short)?.Show();
                    ActualizarUI();
                });
                _lstProductos.Adapter = _productoAdapter;
            } else {
                // Actualizaciones posteriores: reemplazar la lista interna y
                // notificar al adapter para que redibuje TODAS las filas visibles
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

            // ── Carrito ───────────────────────────────────────────
            _lstCarrito.Adapter = new CarritoAdapter(this, items,
                onRestar: item => { _carritoService.RestarProducto(item.Producto); ActualizarUI(); },
                onSumar: item => {
                    if (!_carritoService.AgregarProducto(item.Producto))
                        Toast.MakeText(this, "Sin stock disponible", ToastLength.Short)?.Show();
                    ActualizarUI();
                },
                onEliminar: item => { _carritoService.EliminarItem(item); ActualizarUI(); }
            );

            // ── Total ─────────────────────────────────────────────
            _lblTotal.Text = _carritoService.ImporteTotal.ToString("C2");

            // ── FIX #4: Sección de pagos — solo habilitada con carrito lleno ──
            _seccionPagos.Alpha = carritoConItems ? 1.0f : 0.35f;
            _btnCobrar.Enabled = carritoConItems;

            // ── Visibilidad carrito vacío ──────────────────────────
            _lblCarritoVacio.Visibility = carritoConItems
                ? Android.Views.ViewStates.Gone
                : Android.Views.ViewStates.Visible;
            _lstCarrito.Visibility = carritoConItems
                ? Android.Views.ViewStates.Visible
                : Android.Views.ViewStates.Gone;

            // ── FIX #3: Badge ventas del día ──────────────────────
            _lblVentasBadge.Text = _ventaService.TotalVentasHoy.ToString();

            // ── FIX #2: Refrescar lista productos para reflejar stock actualizado ──
            // Cada vez que el carrito cambia, el stock en sesión cambia.
            // Redibujar la lista muestra la cantidad disponible actualizada
            // y deshabilita el botón "+" de productos agotados.
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

        // ── SMS: limpieza ─────────────────────────────────────

        private void DesregistrarSmsReceiver() {
            if (_smsReceiver == null) return;
            try { UnregisterReceiver(_smsReceiver); } catch { /* ya desregistrado */ }
            _smsReceiver = null;
        }

        protected override void OnDestroy() {
            DesregistrarSmsReceiver();
            base.OnDestroy();
        }
    }

    // ══════════════════════════════════════════════════════════
    //  HELPERS — listeners de AlertDialog
    // ══════════════════════════════════════════════════════════

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