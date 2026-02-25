// ============================================================
//  aDVanceSTOCK.Mobile — SplashActivity
//  Archivo: SplashActivity.cs
// ============================================================

using Android.App;
using Android.Content;
using Android.OS;

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        NoHistory = true,
        Theme = "@style/Theme.Splash")]
    public class SplashActivity : Activity {

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_splash);
        }

        protected override void OnResume() {
            base.OnResume();

            Task.Run(async () => {
                await Task.Delay(2000);
                RunOnUiThread(() => {
                    var destino = OnboardingActivity.FueCompletado(this)
                        ? typeof(MainActivity)
                        : typeof(OnboardingActivity);

                    var intent = new Intent(this, destino);
                    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                    StartActivity(intent);
                    Finish();
                });
            });
        }
    }
}
