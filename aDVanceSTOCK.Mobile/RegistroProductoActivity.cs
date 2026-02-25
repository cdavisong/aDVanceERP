// ============================================================
//  aDVanceSTOCK.Mobile — RegistroProductoActivity
//  Archivo: RegistroProductoActivity.cs
//
//  Flujo:
//  1. Usuario escanea o escribe el código
//  2. CatalogoService determina si es nuevo o entrada_stock
//  3. Si es NUEVO → formulario completo
//     Si es ENTRADA_STOCK → solo cantidad (resto readonly)
//  4. Foto opcional con la cámara
//  5. Guardar → SesionService.Agregar() → volver a MainActivity
// ============================================================

using aDVanceSTOCK.Mobile.Modelos;
using aDVanceSTOCK.Mobile.Servicios;

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "Registrar producto",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class RegistroProductoActivity : Activity {

        // ── Servicios ─────────────────────────────────────────────────
        private StockApplication  App      => (StockApplication)Application!;
        private CatalogoService   Catalogo => App.CatalogoService;
        private SesionService     Sesion   => App.SesionService;

        // ── Estado ────────────────────────────────────────────────────
        private TipoRegistro _tipoActual = TipoRegistro.Nuevo;
        private string?      _rutaFotoTemporal;

        // ── Spinners — ítems seleccionados ────────────────────────────
        private ProveedorCatalogo?     _proveedorSeleccionado;
        private UnidadMedidaCatalogo?  _unidadSeleccionada;
        private ClasificacionCatalogo? _clasificacionSeleccionada;

        // ── UI — sección código ───────────────────────────────────────
        private EditText    _txtCodigo       = null!;
        private ImageButton _btnEscanear     = null!;
        private TextView    _lblTipoRegistro = null!;

        // ── UI — sección info básica ──────────────────────────────────
        private LinearLayout _seccionNuevo   = null!;
        private EditText     _txtNombre      = null!;
        private EditText     _txtDescripcion = null!;

        // ── UI — spinners ─────────────────────────────────────────────
        private Spinner _spinnerCategoria      = null!;
        private Spinner _spinnerProveedor      = null!;
        private Spinner _spinnerUnidad         = null!;
        private Spinner _spinnerClasificacion  = null!;

        // ── UI — checkbox vendible ────────────────────────────────────
        private CheckBox _chkEsVendible = null!;

        // ── UI — precios ──────────────────────────────────────────────
        private EditText _txtCosto      = null!;
        private EditText _txtPrecio     = null!;

        // ── UI — cantidad ─────────────────────────────────────────────
        private EditText _txtCantidad   = null!;
        private TextView _lblCantidadHint = null!;

        // ── UI — imagen ───────────────────────────────────────────────
        private ImageView    _imgFoto       = null!;
        private LinearLayout _btnTomarFoto  = null!;
        private LinearLayout _btnQuitarFoto = null!;

        // ── UI — guardar ──────────────────────────────────────────────
        private Button    _btnGuardar = null!;
        private ImageButton _btnVolver = null!;

        // ── Ciclo de vida ─────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_registro_producto);

            EnlazarVistas();
            PopularSpinners();
            ConfigurarEventos();
        }

        // ── Inicialización ────────────────────────────────────────────

        private void EnlazarVistas() {
            _btnVolver        = FindViewById<ImageButton>(Resource.Id.btnVolver)!;
            _txtCodigo        = FindViewById<EditText>(Resource.Id.txtCodigo)!;
            _btnEscanear      = FindViewById<ImageButton>(Resource.Id.btnEscanear)!;
            _lblTipoRegistro  = FindViewById<TextView>(Resource.Id.lblTipoRegistro)!;

            _seccionNuevo     = FindViewById<LinearLayout>(Resource.Id.seccionNuevo)!;
            _txtNombre        = FindViewById<EditText>(Resource.Id.txtNombre)!;
            _txtDescripcion   = FindViewById<EditText>(Resource.Id.txtDescripcion)!;

            _spinnerCategoria     = FindViewById<Spinner>(Resource.Id.spinnerCategoria)!;
            _spinnerProveedor     = FindViewById<Spinner>(Resource.Id.spinnerProveedor)!;
            _spinnerUnidad        = FindViewById<Spinner>(Resource.Id.spinnerUnidad)!;
            _spinnerClasificacion = FindViewById<Spinner>(Resource.Id.spinnerClasificacion)!;
            _chkEsVendible        = FindViewById<CheckBox>(Resource.Id.chkEsVendible)!;

            _txtCosto       = FindViewById<EditText>(Resource.Id.txtCosto)!;
            _txtPrecio      = FindViewById<EditText>(Resource.Id.txtPrecio)!;
            _txtCantidad    = FindViewById<EditText>(Resource.Id.txtCantidad)!;
            _lblCantidadHint = FindViewById<TextView>(Resource.Id.lblCantidadHint)!;

            _imgFoto       = FindViewById<ImageView>(Resource.Id.imgFoto)!;
            _btnTomarFoto  = FindViewById<LinearLayout>(Resource.Id.btnTomarFoto)!;
            _btnQuitarFoto = FindViewById<LinearLayout>(Resource.Id.btnQuitarFoto)!;

            _btnGuardar = FindViewById<Button>(Resource.Id.btnGuardar)!;
        }

        private void PopularSpinners() {
            // Categoría — valores fijos del enum del ERP
            var categorias = new[] { "Mercancia", "ProductoTerminado", "MateriaPrima" };
            _spinnerCategoria.Adapter = new ArrayAdapter<string>(
                this, Android.Resource.Layout.SimpleSpinnerItem, categorias) {
                // drop-down style
            };
            ((ArrayAdapter)_spinnerCategoria.Adapter)
                .SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            // Proveedores
            var nombresProveedores = Catalogo.Proveedores.Count > 0
                ? Catalogo.Proveedores.Select(p => p.ToString()).ToList()
                : new List<string> { "(Sin proveedores cargados)" };
            _spinnerProveedor.Adapter = new ArrayAdapter<string>(
                this, Android.Resource.Layout.SimpleSpinnerItem, nombresProveedores);
            ((ArrayAdapter)_spinnerProveedor.Adapter)
                .SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            // Unidades de medida
            var nombresUnidades = Catalogo.Unidades.Count > 0
                ? Catalogo.Unidades.Select(u => u.ToString()).ToList()
                : new List<string> { "(Sin unidades cargadas)" };
            _spinnerUnidad.Adapter = new ArrayAdapter<string>(
                this, Android.Resource.Layout.SimpleSpinnerItem, nombresUnidades);
            ((ArrayAdapter)_spinnerUnidad.Adapter)
                .SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            // Clasificaciones
            var nombresClasif = Catalogo.Clasificaciones.Count > 0
                ? Catalogo.Clasificaciones.Select(c => c.ToString()).ToList()
                : new List<string> { "(Sin clasificaciones cargadas)" };
            _spinnerClasificacion.Adapter = new ArrayAdapter<string>(
                this, Android.Resource.Layout.SimpleSpinnerItem, nombresClasif);
            ((ArrayAdapter)_spinnerClasificacion.Adapter)
                .SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            // Selección inicial
            _spinnerProveedor.ItemSelected     += (s, e) => _proveedorSeleccionado =
                Catalogo.Proveedores.Count > e.Position ? Catalogo.Proveedores[e.Position] : null;
            _spinnerUnidad.ItemSelected        += (s, e) => _unidadSeleccionada =
                Catalogo.Unidades.Count > e.Position ? Catalogo.Unidades[e.Position] : null;
            _spinnerClasificacion.ItemSelected += (s, e) => _clasificacionSeleccionada =
                Catalogo.Clasificaciones.Count > e.Position ? Catalogo.Clasificaciones[e.Position] : null;
        }

        private void ConfigurarEventos() {
            _btnVolver.Click   += (s, e) => Finish();
            _btnEscanear.Click += (s, e) => EscanearCodigo();
            _btnGuardar.Click  += (s, e) => IntentarGuardar();

            _btnTomarFoto.Click  += (s, e) => TomarFoto();
            _btnQuitarFoto.Click += (s, e) => QuitarFoto();

            // Al salir del campo código, resolver tipo automáticamente
            _txtCodigo.FocusChange += (s, e) => {
                if (!e.HasFocus && !string.IsNullOrWhiteSpace(_txtCodigo.Text))
                    ResolverTipoPorCodigo(_txtCodigo.Text.Trim());
            };
        }

        // ── Escáner ───────────────────────────────────────────────────

        private void EscanearCodigo() {
            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted) {
                RequestPermissions(new[] { Manifest.Permission.Camera }, 1001);
                return;
            }
            StartActivityForResult(
                new Intent(this, typeof(EscanerActivity)),
                EscanerActivity.RequestCode);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data) {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == EscanerActivity.RequestCode && resultCode == Result.Ok) {
                var codigo = data?.GetStringExtra(EscanerActivity.ExtraCodigoBarras)?.Trim() ?? "";
                if (!string.IsNullOrEmpty(codigo)) {
                    _txtCodigo.Text = codigo;
                    ResolverTipoPorCodigo(codigo);
                }
                return;
            }

            if (requestCode == FotoActivity.RequestCode && resultCode == Result.Ok) {
                var ruta = data?.GetStringExtra(FotoActivity.ExtraRutaDestino) ?? "";
                if (!string.IsNullOrEmpty(ruta) && File.Exists(ruta))
                    OnFotoCapturada(ruta);
            }
        }

        // ── Resolución nuevo / entrada_stock ──────────────────────────

        private void ResolverTipoPorCodigo(string codigo) {
            // ¿Ya lo registramos en esta sesión?
            if (Sesion.CodigoYaRegistrado(codigo)) {
                MostrarError($"El código «{codigo}» ya está registrado en esta sesión.");
                _txtCodigo.Text = "";
                return;
            }

            var existente = Catalogo.BuscarProductoPorCodigo(codigo);

            if (existente != null) {
                // ENTRADA DE STOCK — producto ya existe en el ERP
                _tipoActual = TipoRegistro.EntradaStock;
                _lblTipoRegistro.Text = $"ENTRADA DE STOCK — {existente.Nombre}";
                _lblTipoRegistro.SetBackgroundColor(Android.Graphics.Color.ParseColor("#1565C0"));
                _seccionNuevo.Visibility = ViewStates.Gone;
                _lblCantidadHint.Text = "Cantidad adicional a sumar al inventario:";
            } else {
                // NUEVO PRODUCTO
                _tipoActual = TipoRegistro.Nuevo;
                _lblTipoRegistro.Text = "PRODUCTO NUEVO";
                _lblTipoRegistro.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2E7D32"));
                _seccionNuevo.Visibility = ViewStates.Visible;
                _lblCantidadHint.Text = "Stock inicial:";
            }
        }

        // ── Foto ──────────────────────────────────────────────────────

        private void TomarFoto() {
            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted) {
                RequestPermissions(new[] { Manifest.Permission.Camera }, 1002);
                return;
            }

            // Usamos Camera2 a través de una Activity dedicada de captura
            // simple (Intent ACTION_IMAGE_CAPTURE con FileProvider no está
            // disponible sin AndroidX, así que la captura se delega a
            // FotoActivity que usa Camera2 con un surface de preview + captura).
            var intent = new Intent(this, typeof(FotoActivity));
            intent.PutExtra(FotoActivity.ExtraRutaDestino, RutasApp.RutaFotoTemporal);
            StartActivityForResult(intent, FotoActivity.RequestCode);
        }

        private void OnFotoCapturada(string rutaTemp) {
            _rutaFotoTemporal = rutaTemp;

            // Mostrar miniatura
            var bitmap = Android.Graphics.BitmapFactory.DecodeFile(rutaTemp);
            if (bitmap != null) {
                _imgFoto.SetImageBitmap(bitmap);
                _imgFoto.Visibility      = ViewStates.Visible;
                _btnQuitarFoto.Visibility = ViewStates.Visible;
                _btnTomarFoto.Visibility  = ViewStates.Gone;
            }
        }

        private void QuitarFoto() {
            _rutaFotoTemporal = null;
            _imgFoto.SetImageBitmap(null);
            _imgFoto.Visibility       = ViewStates.Gone;
            _btnQuitarFoto.Visibility = ViewStates.Gone;
            _btnTomarFoto.Visibility  = ViewStates.Visible;
        }

        // ── Guardar ───────────────────────────────────────────────────

        private void IntentarGuardar() {
            var codigo = _txtCodigo.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(codigo)) {
                MostrarError("El código es obligatorio."); return;
            }

            if (_tipoActual == TipoRegistro.Nuevo) {
                var nombre = _txtNombre.Text?.Trim() ?? "";
                if (string.IsNullOrEmpty(nombre)) {
                    MostrarError("El nombre es obligatorio."); return;
                }
                if (!decimal.TryParse(_txtCosto.Text?.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal costo) || costo < 0) {
                    MostrarError("Ingrese un costo de adquisición válido."); return;
                }
                if (!decimal.TryParse(_txtPrecio.Text?.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal precio) || precio <= 0) {
                    MostrarError("Ingrese un precio de venta válido."); return;
                }
            }

            if (!decimal.TryParse(_txtCantidad.Text?.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal cantidad) || cantidad <= 0) {
                MostrarError("Ingrese una cantidad válida mayor que cero."); return;
            }

            var producto = ConstruirProducto(codigo, cantidad);
            Sesion.Agregar(producto);

            Toast.MakeText(this,
                $"✓ {codigo} registrado como {producto.EtiquetaTipo}",
                ToastLength.Short)?.Show();

            Finish();
        }

        private ProductoSesion ConstruirProducto(string codigo, decimal cantidad) {
            var p = new ProductoSesion {
                Codigo    = codigo,
                Tipo      = _tipoActual,
                Cantidad  = cantidad,
                FechaRegistro = DateTime.Now
            };

            // Imagen: mover de temporal a ruta definitiva
            if (!string.IsNullOrEmpty(_rutaFotoTemporal)
                && File.Exists(_rutaFotoTemporal)) {
                var rutaDef = RutasApp.RutaImagen(codigo);
                Directory.CreateDirectory(RutasApp.DirectorioImagenes);
                File.Copy(_rutaFotoTemporal, rutaDef, overwrite: true);
                p.RutaImagenLocal = rutaDef;
            }

            if (_tipoActual == TipoRegistro.Nuevo) {
                var categorias = new[] { "Mercancia", "ProductoTerminado", "MateriaPrima" };
                decimal.TryParse(_txtCosto.Text?.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out var costo);
                decimal.TryParse(_txtPrecio.Text?.Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out var precio);

                p.Nombre       = _txtNombre.Text?.Trim() ?? "";
                p.Descripcion  = _txtDescripcion.Text?.Trim() ?? "";
                p.Categoria    = categorias[_spinnerCategoria.SelectedItemPosition];
                p.EsVendible   = _chkEsVendible.Checked;

                p.IdProveedor         = _proveedorSeleccionado?.Id ?? 0;
                p.NombreProveedor     = _proveedorSeleccionado?.RazonSocial ?? "";
                p.IdUnidadMedida      = _unidadSeleccionada?.Id ?? 0;
                p.NombreUnidadMedida  = _unidadSeleccionada?.Nombre ?? "";
                p.IdClasificacion     = _clasificacionSeleccionada?.Id ?? 0;
                p.NombreClasificacion = _clasificacionSeleccionada?.Nombre ?? "";

                p.CostoAdquisicionUnitario = costo;
                p.PrecioVentaBase          = precio;
            }

            return p;
        }

        // ── Helpers ───────────────────────────────────────────────────

        private void MostrarError(string msg) =>
            new AlertDialog.Builder(this)!
                .SetMessage(msg)!
                .SetPositiveButton("OK", (s, e) => { })!
                .Show();
    }
}
