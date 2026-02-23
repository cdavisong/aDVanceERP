// ============================================================
//  aDVancePOS.Mobile — ConfiguracionActivity
//  Archivo: ConfiguracionActivity.cs
//
//  Permite editar IdAlmacen, PrefijoTicket, IdClienteAnonimo.
//  Guarda con ConfiguracionService al pulsar "Guardar".
// ============================================================

using aDVancePOS.Mobile.Servicios;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Configuración",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ConfiguracionActivity : Activity {

        private EditText _txtIdAlmacen        = null!;
        private EditText _txtPrefijoTicket    = null!;
        private EditText _txtIdClienteAnonimo = null!;
        private ConfiguracionApp _config      = null!;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_configuracion);

            _config = ConfiguracionService.Cargar();

            _txtIdAlmacen        = FindViewById<EditText>(Resource.Id.txtIdAlmacen)!;
            _txtPrefijoTicket    = FindViewById<EditText>(Resource.Id.txtPrefijoTicket)!;
            _txtIdClienteAnonimo = FindViewById<EditText>(Resource.Id.txtIdClienteAnonimo)!;

            // Prellenar con valores actuales
            _txtIdAlmacen.Text        = _config.IdAlmacen.ToString();
            _txtPrefijoTicket.Text    = _config.PrefijoTicket;
            _txtIdClienteAnonimo.Text = _config.IdClienteAnonimo.ToString();

            FindViewById<ImageButton>(Resource.Id.btnVolverConfig)!.Click +=
                (s, e) => Finish();

            FindViewById<Button>(Resource.Id.btnGuardarConfig)!.Click +=
                (s, e) => Guardar();

            FindViewById<Button>(Resource.Id.btnVerLicencia)!.Click += (s, e) =>
                StartActivity(new Intent(this, typeof(LicenciaVistaActivity)));
        }

        private void Guardar() {
            if (!int.TryParse(_txtIdAlmacen.Text?.Trim(), out int idAlmacen) || idAlmacen <= 0) {
                Error("El ID de almacén debe ser un número mayor que cero."); return;
            }
            if (!int.TryParse(_txtIdClienteAnonimo.Text?.Trim(), out int idCliente) || idCliente <= 0) {
                Error("El ID de cliente debe ser un número mayor que cero."); return;
            }
            var prefijo = _txtPrefijoTicket.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(prefijo)) {
                Error("El prefijo del ticket no puede estar vacío."); return;
            }

            _config.IdAlmacen        = idAlmacen;
            _config.PrefijoTicket    = prefijo;
            _config.IdClienteAnonimo = idCliente;
            ConfiguracionService.Guardar(_config);

            // Actualizar el singleton de la app también
            ((PosApplication)Application!).RefrescarConfig(_config);

            Toast.MakeText(this, "✓ Configuración guardada", ToastLength.Short)?.Show();
            Finish();
        }

        private void Error(string msg) =>
            new AlertDialog.Builder(this)!
                .SetMessage(msg)!
                .SetPositiveButton("OK", (s, e) => { })!
                .Show();
    }

    // ── Vista de licencia desde configuración ─────────────────
    [Activity(
        Label = "Licencia",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class LicenciaVistaActivity : Activity {
        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            // Reusa el mismo layout pero sin el checkbox/botón de aceptar
            SetContentView(Resource.Layout.activity_licencia);

            // Ocultar sección de aceptación — solo lectura
            FindViewById<CheckBox>(Resource.Id.chkAceptoLicencia)!.Visibility =
                Android.Views.ViewStates.Gone;
            var btnAceptar = FindViewById<Android.Widget.Button>(Resource.Id.btnAceptarLicencia)!;
            btnAceptar.Text    = "← Volver";
            btnAceptar.Enabled = true;
            btnAceptar.Alpha   = 1.0f;
            btnAceptar.Click  += (s, e) => Finish();
        }
    }
}
