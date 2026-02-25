// ============================================================
//  aDVanceSTOCK.Mobile — LicenciaActivity
//  Archivo: LicenciaActivity.cs
// ============================================================

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "Licencia",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class LicenciaActivity : Activity {

        private CheckBox _chkAcepto  = null!;
        private Button   _btnAceptar = null!;

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
                OnboardingActivity.MarcarCompletado(this);
                var intent = new Intent(this, typeof(MainActivity));
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                StartActivity(intent);
                Finish();
            };
        }

        public override void OnBackPressed() { /* bloquear volver */ }
    }
}
