// ============================================================
//  aDVanceSTOCK.Mobile — ConfiguracionActivity
//  Archivo: ConfiguracionActivity.cs
//
//  Permite seleccionar el almacén activo desde el catálogo
//  cargado. El almacén se guarda en config.json y se usa
//  como encabezado de todos los JSON de exportación.
// ============================================================

using aDVanceSTOCK.Mobile.Modelos;
using aDVanceSTOCK.Mobile.Servicios;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "Configuración",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ConfiguracionActivity : Activity {

        private StockApplication App => (StockApplication) Application!;
        private CatalogoService Catalogo => App.CatalogoService;

        private Spinner _spinnerAlmacen = null!;
        private Button _btnGuardar = null!;
        private ImageButton _btnVolver = null!;
        private Button _btnVerLicencia = null!;

        private ConfiguracionApp _config = null!;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_configuracion);

            _config = ConfiguracionService.Cargar();

            _spinnerAlmacen = FindViewById<Spinner>(Resource.Id.spinnerAlmacen)!;
            _btnGuardar = FindViewById<Button>(Resource.Id.btnGuardarConfig)!;
            _btnVolver = FindViewById<ImageButton>(Resource.Id.btnVolverConfig)!;
            _btnVerLicencia = FindViewById<Button>(Resource.Id.btnVerLicencia)!;

            PopularSpinnerAlmacenes();

            _btnVolver.Click += (s, e) => Finish();
            _btnGuardar.Click += (s, e) => Guardar();
            _btnVerLicencia.Click += (s, e) =>
                StartActivity(new Intent(this, typeof(LicenciaVistaActivity)));
        }

        private void PopularSpinnerAlmacenes() {
            if (Catalogo.Almacenes.Count == 0) {
                _spinnerAlmacen.Adapter = new ArrayAdapter<string>(
                    this, Android.Resource.Layout.SimpleSpinnerItem,
                    new[] { "(Carga catalogo_almacenes.json primero)" });
                return;
            }

            var nombres = Catalogo.Almacenes.Select(a => a.ToString()).ToList();
            _spinnerAlmacen.Adapter = new ArrayAdapter<string>(
                this, Android.Resource.Layout.SimpleSpinnerItem, nombres);
            ((ArrayAdapter) _spinnerAlmacen.Adapter)
                .SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            // Pre-seleccionar el almacén guardado
            var idxActual = Catalogo.Almacenes
                .ToList()
                .FindIndex(a => a.Id == _config.IdAlmacen);
            if (idxActual >= 0)
                _spinnerAlmacen.SetSelection(idxActual);
        }

        private void Guardar() {
            if (Catalogo.Almacenes.Count == 0) {
                new AlertDialog.Builder(this)!
                    .SetMessage("No hay almacenes cargados. Importa el catálogo primero.")!
                    .SetPositiveButton("OK", (s, e) => { })!
                    .Show();
                return;
            }

            var almacenSeleccionado = Catalogo.Almacenes[_spinnerAlmacen.SelectedItemPosition];
            _config.IdAlmacen = almacenSeleccionado.Id;
            _config.NombreAlmacen = almacenSeleccionado.Nombre;

            ConfiguracionService.Guardar(_config);
            App.RefrescarConfig(_config);

            Toast.MakeText(this,
                $"Almacén activo: {almacenSeleccionado.Nombre}",
                ToastLength.Short)?.Show();
            Finish();
        }
    }

    // ── Vista de licencia desde configuración ─────────────────────────

    [Activity(
        Label = "Licencia",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class LicenciaVistaActivity : Activity {
        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_licencia);

            FindViewById<Android.Widget.CheckBox>(Resource.Id.chkAceptoLicencia)!
                .Visibility = Android.Views.ViewStates.Gone;

            var btn = FindViewById<Button>(Resource.Id.btnAceptarLicencia)!;
            btn.Text = "← Volver";
            btn.Enabled = true;
            btn.Alpha = 1.0f;
            btn.Click += (s, e) => Finish();
        }
    }
}
