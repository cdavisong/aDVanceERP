using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace aDVancePOS.Mobile {
    // NoHistory = true : al pulsar "Atrás" desde MainActivity no vuelve al splash
    // MainLauncher = true : esta es ahora la actividad de entrada
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

        // OnResume es el momento ideal para lanzar el hilo de carga,
        // así el layout ya está dibujado cuando empieza la espera.
        protected override void OnResume() {
            base.OnResume();

            // Simulamos carga (2 segundos). Aquí podrías inicializar
            // base de datos, preferencias, tokens de sesión, etc.
            Task.Run(async () => {
                await InicializarAppAsync();

                // Volver al hilo principal para lanzar MainActivity
                RunOnUiThread(() => {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish(); // elimina el splash del back-stack
                });
            });
        }

        /// <summary>
        /// Coloca aquí cualquier inicialización real (DB, config, sesión...).
        /// De momento solo espera 2 segundos.
        /// </summary>
        private async Task InicializarAppAsync() {
            // TODO: cargar preferencias, verificar token de sesión, etc.
            await Task.Delay(2000);
        }
    }
}