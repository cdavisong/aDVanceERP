// ============================================================
//  aDVanceSTOCK.Mobile — MainActivity
//  Archivo: MainActivity.cs
//
//  Pantalla principal. Muestra:
//    - Resumen de la sesión actual (contadores)
//    - Lista de productos registrados (más reciente arriba)
//    - Botón para ir a RegistroProductoActivity
//    - Botón para exportar la sesión
//    - Acceso a configuración
// ============================================================

using aDVanceSTOCK.Mobile.Adaptadores;
using aDVanceSTOCK.Mobile.Servicios;

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "@string/app_name",
        MainLauncher = false,
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity {

        // ── Servicios ─────────────────────────────────────────────────
        private StockApplication App  => (StockApplication)Application!;
        private CatalogoService   Catalogo    => App.CatalogoService;
        private SesionService     Sesion      => App.SesionService;
        private ExportacionService Exportacion => App.ExportacionService;
        private ConfiguracionApp  Config      => App.Config;

        // ── UI ────────────────────────────────────────────────────────
        private TextView     _lblAlmacen       = null!;
        private TextView     _lblTotalItems    = null!;
        private TextView     _lblNuevos        = null!;
        private TextView     _lblEntradas      = null!;
        private ListView     _lstSesion        = null!;
        private TextView     _lblSesionVacia   = null!;
        private LinearLayout _btnRegistrar     = null!;
        private LinearLayout _btnExportar      = null!;
        private ImageButton  _btnConfiguracion = null!;
        private LinearLayout _btnImportar      = null!;

        private SesionAdapter? _adapter;

        // ── Ciclo de vida ─────────────────────────────────────────────

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_main);

            EnlazarControles();
            ConfigurarEventos();
            SolicitarPermisos();
            CargarCatalogosAsync();
        }

        protected override void OnResume() {
            base.OnResume();
            ActualizarUI(); // Refresca al volver de RegistroProductoActivity
        }

        // ── Inicialización ────────────────────────────────────────────

        private void EnlazarControles() {
            _lblAlmacen       = FindViewById<TextView>(Resource.Id.lblAlmacen)!;
            _lblTotalItems    = FindViewById<TextView>(Resource.Id.lblTotalItems)!;
            _lblNuevos        = FindViewById<TextView>(Resource.Id.lblNuevos)!;
            _lblEntradas      = FindViewById<TextView>(Resource.Id.lblEntradas)!;
            _lstSesion        = FindViewById<ListView>(Resource.Id.lstSesion)!;
            _lblSesionVacia   = FindViewById<TextView>(Resource.Id.lblSesionVacia)!;
            _btnRegistrar     = FindViewById<LinearLayout>(Resource.Id.btnRegistrar)!;
            _btnExportar      = FindViewById<LinearLayout>(Resource.Id.btnExportar)!;
            _btnConfiguracion = FindViewById<ImageButton>(Resource.Id.btnConfiguracion)!;
            _btnImportar      = FindViewById<LinearLayout>(Resource.Id.btnImportar)!;
        }

        private void ConfigurarEventos() {
            _btnRegistrar.Click     += (s, e) => IrARegistro();
            _btnExportar.Click      += async (s, e) => await ExportarSesionAsync();
            _btnConfiguracion.Click += (s, e) => StartActivity(
                                           new Intent(this, typeof(ConfiguracionActivity)));
            _btnImportar.Click      += async (s, e) => await CargarCatalogosAsync(forzar: true);
        }

        private void SolicitarPermisos() {
            var faltantes = new List<string>();
            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted)
                faltantes.Add(Manifest.Permission.Camera);
            if (faltantes.Count > 0)
                RequestPermissions(faltantes.ToArray(), 1000);
        }

        // ── Carga de catálogos ────────────────────────────────────────

        private async void CargarCatalogosAsync() => await CargarCatalogosAsync(forzar: false);

        private async Task CargarCatalogosAsync(bool forzar = false) {
            if (Catalogo.CatalogosListos && !forzar) return;

            _btnImportar.Enabled = false;
            _btnImportar.Alpha   = 0.5f;

            try {
                await Catalogo.CargarTodosAsync();
                ActualizarUI();
                if (forzar)
                    Toast.MakeText(this, "Catálogos cargados correctamente", ToastLength.Short)?.Show();
            } catch (FileNotFoundException ex) {
                MostrarMensaje(
                    "Archivo de catálogo no encontrado:\n" + ex.Message +
                    "\n\nEjecuta el export desde el ERP desktop y haz adb push de los archivos.");
            } catch (Exception ex) {
                MostrarMensaje("Error al cargar catálogos:\n" + ex.Message);
            } finally {
                _btnImportar.Enabled = true;
                _btnImportar.Alpha   = 1.0f;
            }
        }

        // ── Navegación ────────────────────────────────────────────────

        private void IrARegistro() {
            if (!Catalogo.CatalogosListos) {
                MostrarMensaje(
                    "Los catálogos aún no están cargados.\n" +
                    "Pulsa «Importar catálogos» primero.");
                return;
            }
            StartActivity(new Intent(this, typeof(RegistroProductoActivity)));
        }

        // ── Exportación ───────────────────────────────────────────────

        private async Task ExportarSesionAsync() {
            if (Sesion.TotalItems == 0) {
                MostrarMensaje("No hay productos registrados en la sesión actual.");
                return;
            }

            ConfirmarAccion(
                "¿Exportar sesión?",
                $"Se exportarán {Sesion.TotalItems} producto(s).\n" +
                $"Almacén: {Config.NombreAlmacen}\n\n" +
                "La sesión en memoria se mantendrá hasta que inicies una nueva.",
                async () => {
                    try {
                        _btnExportar.Enabled = false;
                        _btnExportar.Alpha   = 0.5f;

                        var ruta = await Exportacion.ExportarSesionAsync(Sesion, Config);
                        var nombre = Path.GetFileName(ruta);

                        MostrarMensaje(
                            $"✓ Sesión exportada correctamente\n\n" +
                            $"Archivo: {nombre}\n\n" +
                            $"Recupera los datos desde el ERP con:\n" +
                            $"adb pull /sdcard/Android/data/cu.davisoft.advancestock/files/ ./");

                    } catch (Exception ex) {
                        MostrarMensaje("Error al exportar:\n" + ex.Message);
                    } finally {
                        _btnExportar.Enabled = true;
                        _btnExportar.Alpha   = 1.0f;
                    }
                });
        }

        // ── UI ────────────────────────────────────────────────────────

        private void ActualizarUI() {
            // Encabezado
            _lblAlmacen.Text    = Config.IdAlmacen > 0
                ? $"{Config.NombreAlmacen} (ID {Config.IdAlmacen})"
                : "Sin almacén configurado";
            _lblTotalItems.Text = Sesion.TotalItems.ToString();
            _lblNuevos.Text     = Sesion.TotalNuevos.ToString();
            _lblEntradas.Text   = Sesion.TotalEntradas.ToString();

            // Lista
            var items = Sesion.Items.ToList();
            bool hayItems = items.Count > 0;

            _lblSesionVacia.Visibility = hayItems
                ? Android.Views.ViewStates.Gone
                : Android.Views.ViewStates.Visible;
            _lstSesion.Visibility = hayItems
                ? Android.Views.ViewStates.Visible
                : Android.Views.ViewStates.Gone;

            if (_adapter == null) {
                _adapter = new SesionAdapter(this, items, onEliminar: producto => {
                    ConfirmarAccion(
                        "¿Eliminar producto?",
                        $"Se eliminará: {producto.Nombre} ({producto.Codigo})",
                        () => { Sesion.Eliminar(producto); ActualizarUI(); });
                });
                _lstSesion.Adapter = _adapter;
            } else {
                _adapter.ActualizarLista(items);
            }

            // Botón exportar: solo activo si hay items
            _btnExportar.Enabled = hayItems;
            _btnExportar.Alpha   = hayItems ? 1.0f : 0.5f;
        }

        // ── Helpers ───────────────────────────────────────────────────

        private void MostrarMensaje(string msg) =>
            RunOnUiThread(() =>
                new AlertDialog.Builder(this)!
                    .SetMessage(msg)!
                    .SetPositiveButton("Aceptar", (s, e) => { })!
                    .Show());

        private void ConfirmarAccion(string titulo, string msg, Action onConfirmar) =>
            new AlertDialog.Builder(this)!
                .SetTitle(titulo)!
                .SetMessage(msg)!
                .SetPositiveButton("Confirmar", (s, e) => onConfirmar())!
                .SetNegativeButton("Cancelar", (s, e) => { })!
                .Show();
    }
}
