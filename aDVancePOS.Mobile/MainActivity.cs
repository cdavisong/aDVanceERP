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
        private TextView _lblVentasBadge   = null!;
        private TextView _lblEsperasBadge   = null!;
        private LinearLayout _seccionPagos = null!;
        private Button _btnVaciarCarrito = null!;
        private LinearLayout _btnCobrar = null!;
        private ImageButton _btnConfiguracion = null!;

        // ─────────────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_main);

            EnlazarControles();
            ConfigurarEventos();
            CargarDatosInicialesAsync();
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
            _lblVentasBadge  = FindViewById<TextView>(Resource.Id.lblVentasBadge)!;
            _lblEsperasBadge = FindViewById<TextView>(Resource.Id.lblEsperasBadge);
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
            _btnCobrar.Click += (s, e) => {
                if (_carritoService.ConteoItems == 0) {
                    MostrarMensaje("El carrito está vacío.");
                    return;
                }
                var intent = new Intent(this, typeof(CobroActivity));
                StartActivityForResult(intent, CobroActivity.RequestCode);
            };
            _lblVentasBadge.Click  += (s, e) => StartActivity(new Intent(this, typeof(ResumenVentasActivity)));
            if (_lblEsperasBadge != null)
                _lblEsperasBadge.Click += (s, e) => StartActivityForResult(new Intent(this, typeof(VentasEsperaActivity)),  VentasEsperaActivity.RequestCode);
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

            if ((requestCode == CobroActivity.RequestCode ||
                 requestCode == VentasEsperaActivity.RequestCode) && resultCode == Result.Ok) {
                bool esEspera = data?.GetBooleanExtra(CobroActivity.ExtraEsEspera, false) ?? false;
                if (!esEspera)
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
                // Producto encontrado — si tiene una sola presentación activa la usa,
                // si tiene varias muestra el diálogo, si no tiene usa precio base
                var activas = producto.Presentaciones.Where(p => p.Activo).ToList();
                long idPres   = activas.Count == 1 ? activas[0].Id : 0;
                decimal precio = activas.Count == 1 ? activas[0].PrecioVenta : producto.PrecioConImpuesto;
                if (activas.Count > 1) {
                    // El escáner no puede mostrar diálogo fácilmente — usar la primera presentación
                    idPres  = activas[0].Id;
                    precio  = activas[0].PrecioVenta;
                }
                if (_carritoService.AgregarProducto(producto, idPres, precio)) {
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

        // ── Actualización de UI ───────────────────────────────

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
                _productoAdapter = new ProductoAdapter(this, _productosMostrados,
                    (producto, idPresentacion, precio) => {
                        if (!_carritoService.AgregarProducto(producto, idPresentacion, precio))
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
                onRestar: item => { _carritoService.RestarProducto(item.Producto, item.IdPresentacion); ActualizarUI(); },
                onSumar: item => {
                    if (!_carritoService.AgregarProducto(item.Producto, item.IdPresentacion, item.PrecioUnitario))
                        Toast.MakeText(this, "Sin stock disponible", ToastLength.Short)?.Show();
                    ActualizarUI();
                },
                onEliminar: item => { _carritoService.EliminarItem(item); ActualizarUI(); },
                onChangePresentacion: item => MostrarDialogoCambiarPresentacion(item)
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
            if (_lblEsperasBadge != null) {
                int esperas = _ventaService.TotalEnEspera;
                _lblEsperasBadge.Text       = esperas.ToString();
                _lblEsperasBadge.Visibility = esperas > 0
                    ? Android.Views.ViewStates.Visible
                    : Android.Views.ViewStates.Invisible;
            }

            // ── FIX #2: Refrescar lista productos para reflejar stock actualizado ──
            // Cada vez que el carrito cambia, el stock en sesión cambia.
            // Redibujar la lista muestra la cantidad disponible actualizada
            // y deshabilita el botón "+" de productos agotados.
            if (_catalogoCargado)
                ActualizarListaProductos();
        }

        // ── Helpers UI ────────────────────────────────────────

        private void MostrarDialogoCambiarPresentacion(ItemCarrito item) {
            var producto = item.Producto;
            var presentaciones = producto.Presentaciones?.Where(p => p.Activo).ToList() ?? new List<PresentacionVenta>();

            // Crear lista de opciones: primero "Unidad base", luego las presentaciones
            var opciones = new List<(string Etiqueta, long IdPresentacion, decimal Precio)>();

            // Agregar opción de unidad base (idPresentacion = 0)
            opciones.Add(($"Unidad base — {producto.PrecioConImpuesto:C2}", 0, producto.PrecioConImpuesto));

            // Agregar presentaciones activas
            foreach (var p in presentaciones) {
                opciones.Add(($" {p.Cantidad} {p.UnidadMedida} — {p.PrecioVenta:C2}", p.Id, p.PrecioVenta));
            }

            var etiquetas = opciones.Select(o => o.Etiqueta).ToArray();

            new AlertDialog.Builder(this)!
                .SetTitle($"Cambiar presentación de {producto.Nombre}")!
                .SetItems(etiquetas, (s, e) => {
                    var opcionElegida = opciones[e.Which];
                    // Actualizar el ítem del carrito con la nueva presentación
                    _carritoService.CambiarPresentacionItem(item, opcionElegida.IdPresentacion, opcionElegida.Precio);
                    ActualizarUI();
                })!
                .SetNegativeButton("Cancelar", (s, e) => { })!
                .Show();
        }

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

        protected override void OnDestroy() {
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