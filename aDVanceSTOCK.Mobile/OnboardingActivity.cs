// ============================================================
//  aDVanceSTOCK.Mobile — OnboardingActivity
//  Archivo: OnboardingActivity.cs
// ============================================================

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace aDVanceSTOCK.Mobile {

    [Activity(
        Label = "Bienvenido",
        Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class OnboardingActivity : Activity {

        public const string PrefOnboardingCompletado = "onboarding_completado";
        public const string NombrePrefs              = "advancestock_prefs";

        private FrameLayout  _frameOnboarding  = null!;
        private LinearLayout _layoutIndicadores = null!;
        private Button       _btnSiguiente     = null!;
        private Button       _btnOmitir        = null!;

        private int _paginaActual = 0;

        private readonly (string Icono, string Titulo, string Desc)[] _paginas = {
            (
                "ic_onboarding_1",
                "Bienvenido a aDVance Stock",
                "Registra nuevos productos directamente desde el almacén, sin necesidad de conexión a internet."
            ),
            (
                "ic_onboarding_2",
                "Escanea o escribe el código",
                "Usa la cámara para leer el código de barras del producto o escríbelo manualmente. La app detecta si ya existe en el ERP."
            ),
            (
                "ic_onboarding_3",
                "Sincroniza con el ERP",
                "Al terminar, exporta toda la sesión al sistema central con un solo comando ADB. Las imágenes viajan junto al registro."
            )
        };

        protected override void OnCreate(Bundle? savedInstanceState) {
            base.OnCreate(savedInstanceState);
            ActionBar?.Hide();
            SetContentView(Resource.Layout.activity_onboarding);

            _frameOnboarding   = FindViewById<FrameLayout>(Resource.Id.frameOnboarding)!;
            _layoutIndicadores = FindViewById<LinearLayout>(Resource.Id.layoutIndicadores)!;
            _btnSiguiente      = FindViewById<Button>(Resource.Id.btnSiguiente)!;
            _btnOmitir         = FindViewById<Button>(Resource.Id.btnOmitir)!;

            CrearIndicadores();
            MostrarPagina(0);

            _btnSiguiente.Click += (s, e) => {
                if (_paginaActual < _paginas.Length - 1)
                    MostrarPagina(_paginaActual + 1);
                else
                    IrALicencia();
            };

            _btnOmitir.Click += (s, e) => IrALicencia();
        }

        private void MostrarPagina(int indice) {
            _paginaActual = indice;

            var vista = LayoutInflater.Inflate(Resource.Layout.fragment_onboarding_page, null)!;

            var imgOnboarding = vista.FindViewById<ImageView>(Resource.Id.imgOnboarding)!;
            var lblTitulo     = vista.FindViewById<TextView>(Resource.Id.lblTituloOnboarding)!;
            var lblDesc       = vista.FindViewById<TextView>(Resource.Id.lblDescOnboarding)!;

            var (icono, titulo, desc) = _paginas[indice];
            var idRecurso = Resources!.GetIdentifier(icono, "drawable", PackageName);
            if (idRecurso != 0)
                imgOnboarding.SetImageResource(idRecurso);
            else
                imgOnboarding.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F5E6E6"));

            lblTitulo.Text = titulo;
            lblDesc.Text   = desc;

            _frameOnboarding.RemoveAllViews();
            _frameOnboarding.AddView(vista);

            for (int i = 0; i < _layoutIndicadores.ChildCount; i++) {
                var dot = (View)_layoutIndicadores.GetChildAt(i)!;
                dot.Alpha = i == indice ? 1.0f : 0.3f;
            }

            bool esUltima = indice == _paginas.Length - 1;
            _btnSiguiente.Text = esUltima ? "Ver licencia →" : "Siguiente →";
            _btnOmitir.Visibility = esUltima ? ViewStates.Gone : ViewStates.Visible;
        }

        private void CrearIndicadores() {
            _layoutIndicadores.RemoveAllViews();
            var densidad = Resources!.DisplayMetrics!.Density;
            int tamano = (int)(10 * densidad);
            int margen = (int)(6  * densidad);

            for (int i = 0; i < _paginas.Length; i++) {
                var dot = new View(this);
                var lp  = new LinearLayout.LayoutParams(tamano, tamano);
                lp.SetMargins(margen, 0, margen, 0);
                dot.LayoutParameters = lp;
                dot.SetBackgroundResource(Resource.Drawable.circle_outline);
                dot.Alpha = 0.3f;
                _layoutIndicadores.AddView(dot);
            }
        }

        private void IrALicencia() =>
            StartActivity(new Intent(this, typeof(LicenciaActivity)));

        public static bool FueCompletado(Context ctx) =>
            ctx.GetSharedPreferences(NombrePrefs, FileCreationMode.Private)!
               .GetBoolean(PrefOnboardingCompletado, false);

        public static void MarcarCompletado(Context ctx) {
            var editor = ctx.GetSharedPreferences(NombrePrefs, FileCreationMode.Private)!.Edit()!;
            editor.PutBoolean(PrefOnboardingCompletado, true);
            editor.Apply();
        }
    }
}
