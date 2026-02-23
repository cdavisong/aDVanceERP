// ============================================================
//  aDVancePOS.Mobile — MainActivity
//  Archivo: MainActivity.cs
//
//  Conecta la UI (activity_main.xml) con los servicios.
// ============================================================

using aDVancePOS.Mobile.Adaptadores;
using aDVancePOS.Mobile.Modelos;
using aDVancePOS.Mobile.Servicios;

using Android.App;
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
        private TextView _lblCarritoBadge = null!;
        private Button _btnVaciarCarrito = null!;
        private Button _btnPagarEfectivo = null!;
        private Button _btnPagarTransferencia = null!;

        // ─────────────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_main);

            InicializarServicios();
            EnlazarControles();
            ConfigurarEventos();
            CargarDatosInicialesAsync();
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
            _lblCarritoBadge = FindViewById<TextView>(Resource.Id.lblCarritoBadge)!;
            _btnVaciarCarrito = FindViewById<Button>(Resource.Id.btnVaciarCarrito)!;
            _btnPagarEfectivo = FindViewById<Button>(Resource.Id.btnPagarEfectivo)!;
            _btnPagarTransferencia = FindViewById<Button>(Resource.Id.btnPagarTransferencia)!;
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
            _btnPagarEfectivo.Click += async (s, e) => await PagarEfectivoAsync();
            _btnPagarTransferencia.Click += async (s, e) => await PagarTransferenciaAsync();
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
                _btnImportar.Text = "Cargando...";

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
                    _carritoService.Vaciar();
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
                _carritoService.Vaciar();
                ActualizarUI();
                MostrarMensaje(
                    $"✓ Transferencia registrada (pendiente de confirmación)\n" +
                    $"Ticket: {venta.NumeroTicket}\n" +
                    $"Confirmación: {confirmacion}");
            });
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
            var adapter = new ProductoAdapter(this, _productosMostrados, producto => {
                if (!_carritoService.AgregarProducto(producto))
                    Toast.MakeText(this, $"Sin stock: {producto.Nombre}",
                        ToastLength.Short)?.Show();
                ActualizarUI();
            });

            _lstProductos.Adapter = adapter;
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
            _lblCarritoBadge.Text = _carritoService.ConteoItems.ToString();

            var carritoVacio = items.Count == 0;
            _lblCarritoVacio.Visibility = carritoVacio
                ? Android.Views.ViewStates.Visible
                : Android.Views.ViewStates.Gone;
            _lstCarrito.Visibility = carritoVacio
                ? Android.Views.ViewStates.Gone
                : Android.Views.ViewStates.Visible;

            if (_catalogoCargado) ActualizarListaProductos();
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

        private void MostrarDialogoTransferencia(Func<string, string, Task> onConfirmar) {
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            layout.SetPadding(48, 24, 48, 0);

            var txtConfirmacion = new EditText(this) {
                Hint = "Número de confirmación *",
                InputType = Android.Text.InputTypes.ClassText
            };
            var txtTransaccion = new EditText(this) {
                Hint = "Número de transacción (opcional)",
                InputType = Android.Text.InputTypes.ClassText
            };
            layout.AddView(txtConfirmacion);
            layout.AddView(txtTransaccion);

            new AlertDialog.Builder(this)!
                .SetTitle($"Transferencia — {_carritoService.ImporteTotal:C2}")!
                .SetView(layout)!
                .SetPositiveButton("Registrar", async (s, e) => {
                    var confirmacion = txtConfirmacion.Text?.Trim() ?? "";
                    if (string.IsNullOrEmpty(confirmacion)) {
                        MostrarMensaje("El número de confirmación es requerido.");
                        return;
                    }
                    await onConfirmar(confirmacion, txtTransaccion.Text?.Trim() ?? "");
                })!
                .SetNegativeButton("Cancelar", (s, e) => { })!
                .Show();
        }
    }
}
