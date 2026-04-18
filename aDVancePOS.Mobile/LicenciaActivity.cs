// ============================================================
//  aDVancePOS.Mobile — LicenciaActivity
//  Archivo: LicenciaActivity.cs
//
//  Pantalla de Acuerdo de Licencia de Usuario Final (EULA).
//  El botón "Acepto y comenzar" solo se habilita cuando el
//  usuario marca el checkbox. Al aceptar guarda la preferencia
//  y navega a MainActivity.
// ============================================================

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace aDVancePOS.Mobile {

    [Activity(
        Label = "Licencia",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class LicenciaActivity : Activity {

        private CheckBox _chkAcepto     = null!;
        private Button   _btnAceptar    = null!;

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_licencia);

            _chkAcepto  = FindViewById<CheckBox>(Resource.Id.chkAceptoLicencia)!;
            _btnAceptar = FindViewById<Button>(Resource.Id.btnAceptarLicencia)!;

            _chkAcepto.CheckedChange += (s, e) => {
                _btnAceptar.Enabled = e.IsChecked;
                _btnAceptar.Alpha   = e.IsChecked ? 1.0f : 0.5f;
            };

            _btnAceptar.Alpha = 0.5f;

            _btnAceptar.Click += (s, e) => {
                // Guardar que el onboarding + licencia fue completado
                OnboardingActivity.MarcarCompletado(this);

                // Ir a MainActivity limpiando el back stack
                var intent = new Intent(this, typeof(MainActivity));
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                Finish();
            };
        }

        // No permitir volver atrás sin aceptar
        public override void OnBackPressed() {
            // No hacer nada — el usuario debe aceptar o cerrar la app
        }
    }
}
